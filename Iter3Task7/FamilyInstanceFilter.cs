using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter3Task7
{
    internal class FamilyInstanceFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem) => !(elem is FamilyInstance);        

        public bool AllowReference(Reference reference, XYZ position) => false;
        
    }
}
