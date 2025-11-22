using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Collections.Generic;
using System.Text;

namespace Lesson5
{
    [Transaction(TransactionMode.Manual)]
    public class StatisticSelectionElements : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            Application app = uiApp.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            IList<Reference> refs;
            try 
            {
                    refs = uiDoc.Selection.PickObjects(ObjectType.Element,
                    new FamilyInstanceSelectionFilter(),
                    "Pick Objects");            
            }
            catch 
            {
                TaskDialog.Show("Result", "No Items Selected");
                return Result.Cancelled;
            }
            Dictionary<string, int> categories = new Dictionary<string, int>();
            var resultMessage = new StringBuilder($"Общее количество элементов - {refs.Count}\n");
            foreach (Reference r in refs)
            {
                Element element =doc.GetElement(r);
                string categoryName = element.Category.Name;
                if (categories.ContainsKey(categoryName))
                {
                    categories[categoryName]++;
                }
                else
                {
                    categories.Add(categoryName, 1);
                }
            }
            foreach (var category in categories)
            {
                resultMessage.Append($"{category.Key} - {category.Value}\n");
            }
            TaskDialog.Show("Итого", resultMessage.ToString());
            return Result.Succeeded;
        }
    }
}
