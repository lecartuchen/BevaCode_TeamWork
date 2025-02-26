﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

using Beva.FormData;
using Beva.Forms;
using Beva.Managers;

namespace Beva.Commands
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.Attributes.Regeneration(Autodesk.Revit.Attributes.RegenerationOption.Manual)]
    public class cmdNewProj : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // first validate the commandData
                if (commandData == null)
                {
                    message = "ExternalCommandData is missing";
                    return Result.Failed;
                }

                // second validate the application
                if (commandData.Application == null)
                {
                    message = "Application commandData is missing";
                    return Result.Failed;
                }

                NewProjManager newProjManager;

                try
                {
                    newProjManager = new NewProjManager(commandData);
                    if (newProjManager == null)
                    {
                        message = "Failed to create the NewProjManager";
                        return Result.Failed;
                    }
                }
                catch (Exception ex)
                {

                    message = $"Error creating the project manager: {ex.Message}";
                    return Result.Failed;
                }

                DialogResult result = DialogResult.None;

                try
                {
                    using (FrmNewProj form = new FrmNewProj(newProjManager))
                    {
                        if (form == null)
                        {
                            message = "Failed to create the form";
                            return Result.Failed;
                        }

                        result = form.ShowDialog();

                        if (result == DialogResult.OK)
                        {
                            NewProjData data = form.FormData;

                            // Validate data and required properties
                            if (data == null)
                            {
                                TaskDialog.Show("Error", "Data is missing");
                                return Result.Failed;
                            }

                            else if (data.WallType == null)
                            {
                                TaskDialog.Show("Error", "Required data ( Wall Type ) is missing");
                                return Result.Failed;
                            }

                            if (data.RoofType == null)
                            {
                                TaskDialog.Show("Error", "Required data ( Roof Type ) is missing");
                                return Result.Failed;
                            }

                            CreateHouse(commandData, data);

                            GetSetProjectLocation(commandData, data);

                            return Result.Succeeded;
                        }
                    }
                }
                catch (Exception ex)
                {

                    message = $"Error creating or showing the form: {ex.Message}";
                    return Result.Failed;
                }

                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;

                return Result.Failed;
            }
        }

        private void CreateHouse(ExternalCommandData commandData, NewProjData data)
        {
            UIApplication app = commandData.Application;
            Document doc = app.ActiveUIDocument.Document;
            UIDocument uidoc = app.ActiveUIDocument;

            ViewFamilyType viewFamilyType = GetViewFamiliyType(doc);
            if (viewFamilyType == null)
            {
                TaskDialog.Show("Create Basic House", "No 3D view family type found");
                return;
            }

            using (Transaction t = new Transaction(doc))
            {
                if (t.Start("Create Basic House") == TransactionStatus.Started)
                {
                    View3D view3d = View3D.CreateIsometric(doc, viewFamilyType.Id);
                    view3d.get_Parameter(BuiltInParameter.VIEW_DETAIL_LEVEL).Set(3);
                    view3d.get_Parameter(BuiltInParameter.MODEL_GRAPHICS_STYLE).Set(6);

                    CreateLevel(doc, data.Height);

                    List<XYZ> corners = new List<XYZ>(4);
                    // Determine the levels where the walls will be located:
                    Level levelBottom = null;
                    Level levelTop = null;

                    List<Wall> walls = CreateWalls(doc, ref corners, data, ref levelBottom, ref levelTop);

                    double wallThickness = walls[0].WallType.Width;

                    if (data.DrawingSlab)
                    {
                        CreateFloor(doc, data, levelBottom, wallThickness, ref corners);
                    }

                    if (data.DrawingRoof)
                    {
                        AddRoof(doc, data, walls);
                    }

                    if (TransactionStatus.Committed != t.Commit())
                    {
                        TaskDialog.Show("Failure", "Transaction could not be commited");
                    }
                }
                else
                {
                    t.RollBack();
                }
            }

            SetActiveView3D(uidoc, doc);
        }

        private List<Wall> CreateWalls(Document doc, ref List<XYZ> corners, NewProjData formData, ref Level levelBottom, ref Level levelTop)
        {
            if (doc == null || corners == null || formData == null)
            {
                TaskDialog.Show("Create walls", "Invalid parameters provided");
                return null;
            }

            if (!Utils.GetBottomAndTopLevels(doc, ref levelBottom, ref levelTop))
            {
                TaskDialog.Show("Create walls", "Unable to determine wall bottom and top levels");
                return null;
            }

            List<Element> wallsTypes = new List<Element>(Utils.GetElementsOfType(doc, typeof(WallType), BuiltInCategory.OST_Walls));
            Debug.Assert(0 < wallsTypes.Count, "expected at least one wall type" + " to be loaded into project");
            WallType wallType = wallsTypes.Cast<WallType>().First<Element>(ft => ft.Id == formData.WallType.Id) as WallType;
            double wallThickness = wallType.Width / 2;

            if (wallType == null)
            {
                TaskDialog.Show("Create walls", "Unable to determine wall type.");
                return null;
            }

            double widthParam = formData.Width; //* Constants.MeterToFeet;
            double depthParam = formData.Length; //* Constants.MeterToFeet;
            double heightParam = formData.Height; //* Constants.MeterToFeet;
            double xParam = wallThickness;
            double yParam = wallThickness;
            double zParam = 0;

            corners.Add(new XYZ(xParam, yParam, zParam));
            corners.Add(new XYZ(xParam, (widthParam - yParam), zParam));
            corners.Add(new XYZ((depthParam - xParam), (widthParam - yParam), zParam));
            corners.Add(new XYZ((depthParam - xParam), yParam, zParam));

            BuiltInParameter topLevelParam = BuiltInParameter.WALL_HEIGHT_TYPE;
            //levelBottom.Elevation = formData.Z;
            ElementId levelBottomId = levelBottom.Id;
            levelTop.Elevation = heightParam; //UnitUtils.Convert(heightParam, DisplayUnitType.DUT_METERS, DisplayUnitType.DUT_FEET_FRACTIONAL_INCHES);
            ElementId topLevelId = levelTop.Id;
            List<Wall> walls = new List<Wall>(4);


            List<Curve> geomLine = new List<Curve>();

            for (int i = 0; i < 4; ++i)
            {
                Line line = Line.CreateBound(corners[i], corners[3 == i ? 0 : i + 1]);
                Wall wall = Wall.Create(doc, line, levelBottomId, false); // 2013

                Parameter param = wall.get_Parameter(topLevelParam);
                param.Set(topLevelId);
                //wall.get_Parameter(BuiltInParameter.WALL_BASE_OFFSET).Set(formData.Z);
                Parameter paramLocation = wall.get_Parameter(BuiltInParameter.WALL_KEY_REF_PARAM);
                paramLocation.Set(2);
                wall.WallType = wallType;

                walls.Add(wall);
            }

            return walls;
        }

        private void CreateFloor(Document doc, NewProjData formData, Level levelBottom, double wallThickness, ref List<XYZ> corners)
        {
            try
            {
                Autodesk.Revit.Creation.Document createDoc = doc.Create;

                double w = 0.5 * wallThickness;
                corners[0] -= w * (XYZ.BasisX + XYZ.BasisY);
                corners[1] -= w * (XYZ.BasisX - XYZ.BasisY);
                corners[2] += w * (XYZ.BasisX + XYZ.BasisY);
                corners[3] += w * (XYZ.BasisX - XYZ.BasisY);

                CurveArray profile = new CurveArray();
                for (int i = 0; i < 4; ++i)
                {
                    Line line = Line.CreateBound(
                      corners[i], corners[3 == i ? 0 : i + 1]);

                    profile.Append(line);
                }

                List<Curve> curves = new List<Curve>(4);
                foreach (Curve curve in profile)
                {
                    curves.Add(curve);
                }

                CurveLoop loop = CurveLoop.Create(curves);
                List<CurveLoop> curveLoops = new List<CurveLoop>(1)
                {
                    loop
                };


                List<Element> floorTypes = new List<Element>(Utils.GetElementsOfType(doc, typeof(FloorType), BuiltInCategory.OST_Floors));

                Debug.Assert(0 < floorTypes.Count, "expected at least one floor type" + " to be loaded into project");

                FloorType floorType = floorTypes.Cast<FloorType>().FirstOrDefault(); //First<Element>(ft => ft.Id == floorTypeSelect.Id) as FloorType;

                XYZ normal = XYZ.BasisZ;

                bool structural = false;
                Floor floor = Floor.Create(doc, curveLoops, floorType.Id, levelBottom.Id);
                Parameter p1 = floor.get_Parameter(BuiltInParameter.FLOOR_HEIGHTABOVELEVEL_PARAM);
                p1.Set(formData.Z);
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Create Floors", ex.Message);
            }
        }

        private void AddRoof(Document doc, NewProjData formData, List<Wall> walls)
        {
            List<Element> roofTypes = new List<Element>(Utils.GetElementsOfType(doc, typeof(RoofType), BuiltInCategory.OST_Roofs));
            RoofType roofType = roofTypes.Cast<RoofType>().First<Element>(rt => rt.Id == formData.RoofType.Id) as RoofType;

            if (roofType == null)
            {
                TaskDialog.Show("Add roof", "Cannot find (" + formData.RoofType + "). Maybe you use a different template? Try with DefaultMetric.rte.");
            }

            double wallThickness = walls[0].Width;

            double dt = wallThickness / 2.0;
            List<XYZ> dts = new List<XYZ>(5);
            dts.Add(new XYZ(-dt, -dt, 0.0));
            dts.Add(new XYZ(-dt, dt, 0.0));
            dts.Add(new XYZ(dt, dt, 0.0));
            dts.Add(new XYZ(dt, -dt, 0.0));
            dts.Add(dts[0]);

            CurveArray footPrint = new CurveArray();
            for (int i = 0; i <= 3; i++)
            {
                LocationCurve locCurve = (LocationCurve)walls[i].Location;
                XYZ pt1 = locCurve.Curve.GetEndPoint(0) + dts[i];
                XYZ pt2 = locCurve.Curve.GetEndPoint(1) + dts[i + 1];
                Line line = Line.CreateBound(pt1, pt2);
                footPrint.Append(line);
            }

            // Get the level2 from the wall 

            ElementId idLevel2 = walls[0].get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE).AsElementId();
            //Level level2 = (Level)_doc.get_Element(idLevel2); // 2012
            Level level2 = (Level)doc.GetElement(idLevel2); // since 2013

            ModelCurveArray mapping = new ModelCurveArray();

            FootPrintRoof aRoof = doc.Create.NewFootPrintRoof(
              footPrint, level2, roofType, out mapping);

            foreach (ModelCurve modelCurve in mapping)
            {
                aRoof.set_DefinesSlope(modelCurve, true);
                aRoof.set_SlopeAngle(modelCurve, 0.8);
            }
        }

        private void GetSetProjectLocation(ExternalCommandData commandData, NewProjData data)
        {
            UIApplication app = commandData.Application;
            Document doc = app.ActiveUIDocument.Document;

            using (Transaction t = new Transaction(doc))
            {
                t.Start("Create Basic House");

                ProjectLocation currentLocation = doc.ActiveProjectLocation;

                XYZ newOrigin = new XYZ(0, 0, 0);
                //const double angleRatio = Math.PI / 180;

                ProjectPosition projectPosition = currentLocation.GetProjectPosition(newOrigin);

                double angle = 0.0;
                double eastWest = data.X;
                double northSouth = data.Y;
                double elevation = data.Z;

                ProjectPosition newPosition = doc.Application.Create.NewProjectPosition(eastWest, northSouth, elevation, angle);
                if (null != newPosition)
                {
                    currentLocation.SetProjectPosition(newOrigin, newPosition);
                }

                t.Commit();
            }
        }

        private View3D Get3dView(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc).OfClass(typeof(View3D));
            foreach (View3D v in collector)
            {
                if (!v.IsTemplate)
                {
                    return v;
                }
            }

            return null;
        }

        private void SetActiveView3D(UIDocument uidoc, Document doc)
        {
            View3D view = Get3dView(doc);
            if (null == view)
            {
                TaskDialog.Show("View 3D", "Sorry, not suitable 3D view found");
            }
            else
            {
                uidoc.ActiveView = view;
            }
        }

        private ViewFamilyType GetViewFamiliyType(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector = collector.OfClass(typeof(ViewFamilyType));
            ViewFamilyType viewFamilyType = collector.Cast<ViewFamilyType>().FirstOrDefault(vfp => vfp.ViewFamily == ViewFamily.ThreeDimensional);

            return viewFamilyType;
        }

        private void CreateLevel(Document doc, double elevation)
        {
            FilteredElementCollector levels = Utils.GetElementsOfType(doc, typeof(Level), BuiltInCategory.OST_Levels);
            int levelsCount = levels.Cast<Level>().ToList().Count();
            if (levelsCount == 0)
            {
                Level level = Level.Create(doc, 0.0);
                level.Name = "Level 1";

                Level level2 = Level.Create(doc, elevation);
                level2.Name = "Level 2";
            }
            else if (levelsCount == 1)
            {
                Level level = Level.Create(doc, elevation);
                level.Name = "Level 2";
            }
        }
    }
}
