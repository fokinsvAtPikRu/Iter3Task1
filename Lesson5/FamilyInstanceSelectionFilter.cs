using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;

namespace Lesson5
{
    public class FamilyInstanceSelectionFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem) => elem is FamilyInstance;

        public bool AllowReference(Reference reference, XYZ position) => false;
        
    }
}
