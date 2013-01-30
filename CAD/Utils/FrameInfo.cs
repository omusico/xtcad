using System;
using System.Collections.Generic;
using System.Text;
using Autodesk.AutoCAD.DatabaseServices;

namespace CAD
{
    public class FrameInfo
    {
        public double scale { get; set; }
        public double width { get; set; }
        public double heigth { get; set; }
        public Extents2d extents2d { get; set; }
    }
}
