﻿using Autodesk.Revit.DB;

namespace Beva.FormData
{
    public class NewProjData
    {
        private NewProjData _formData;

        public NewProjData FormData
        {
            get
            {
                if (_formData == null)
                {
                    _formData = new NewProjData();
                }
                return _formData;
            }
        }
        public WallType WallType { get; set; }

        public RoofType RoofType { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public bool DrawingRoof { get; set; }

        public bool DrawingSlab { get; set; }
    }
}
