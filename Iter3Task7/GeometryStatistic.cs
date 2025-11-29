using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System.Linq;

namespace Iter3Task7
{
    [Transaction(TransactionMode.Manual)]
    public class GeometryStatistic : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiApp=commandData.Application;
            Application app= uiApp.Application;
            UIDocument uiDoc=uiApp.ActiveUIDocument;
            Document doc=uiDoc.Document;

            Reference reference = null;
            try
            {
                reference = uiDoc.Selection.PickObject
                    (ObjectType.Element, 
                    new FamilyInstanceFilter(), 
                    "Выберете экземпляр системного семейства");
            }
            catch
            {
                return Result.Cancelled;
            }
            Element elem =doc.GetElement (reference);
            if (elem == null)
            {
                TaskDialog.Show("Ошибка", "Не удалось получить элемент");
                return Result.Failed;
            }
            Options options = new Options();
            
            var geometry = elem.get_Geometry(options)
                .Where(g => g is Solid)
                .OfType<Solid>()
                .Select(s=> new GeometryData()
                {
                    Volume = s.Volume,
                    Faces = s.Faces,
                    Edges = s.Edges
                })
                .ToList();
            // сумма объемов
            var volumes = geometry
                .Select(s => s.Volume)
                .Sum();
            volumes= UnitUtils.ConvertFromInternalUnits(volumes, DisplayUnitType.DUT_CUBIC_METERS);
            // площади поверхностей
            var areas = geometry
                .SelectMany(g => g.Faces.Cast<Face>())
                .Select(f=>f.Area)
                .Sum();
            areas= UnitUtils.ConvertFromInternalUnits(areas, DisplayUnitType.DUT_SQUARE_METERS);
            // длина ребер
            var length = geometry
                .SelectMany(g => g.Edges.Cast<Edge>())
                .Select(f => f.AsCurve().Length)
                .Sum();
            length= UnitUtils.ConvertFromInternalUnits(length, DisplayUnitType.DUT_METERS);
            // количество граней
            var facesCount = geometry
                .SelectMany(g => g.Faces.Cast<Face>())
                .Count();
            // количество ребер
            var edgesCount = geometry
                .SelectMany(g => g.Edges.Cast<Edge>())
                .Count();
            TaskDialog.Show("итог",
                $"Сумма объемов - {volumes:N3}\n" +
                $"Сумма площадей - { areas:N3}\n" +
                $"Сумма длин ребер - {length:N3}\n" +
                $"Количество граней - {facesCount}\n" +
                $"Количество ребер - {edgesCount}");
            return Result.Succeeded;
        }
    }
}
