﻿using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace NEWPROJ_CMD
{
    /// <remarks>
    /// The "NewProj_Cmd" external command. The class must be Public.
    /// </remarks>

    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class NewProj_Cmd : IExternalCommand
    {
        // The main Execute method (inherited from IExternalCommand) must be public
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit,
            ref string message, ElementSet elements)
        {
            TaskDialog.Show("Hazen", "Here goes my FIRST Hazen Form");
            return Autodesk.Revit.UI.Result.Succeeded;
        }
    }
}