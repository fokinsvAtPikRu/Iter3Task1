using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson6
{
    public class WallsSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem) => elem is Wall;
        
        public bool AllowReference(Reference reference, XYZ position)=> false;
    }
}
