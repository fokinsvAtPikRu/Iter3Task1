using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;

namespace Iter3Task4
{
    [Transaction(TransactionMode.Manual)]
    public class WallsStatistic : IExternalCommand
    {
        private IRevitRepository _repository;
        public WallsStatistic(IRevitRepository repository)
        {
            _repository = repository;
        }
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc=commandData.Application.ActiveUIDocument;
            Document doc=uiDoc.Document;

            // получаем все стены
            var walls = _repository.GetElementsCurrentDocument<Wall>(doc);

            // количество стен
            var count = walls.Count;

            // сумма длин всех стен
            var sumLength = walls
                .Sum(w => w.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble());

            // средняя длина стены
            var averageWallLength = sumLength / count;

            // упорядочиваем список стен по их длине
            var orderWallsByLength = walls
                .Select(w => new
                {
                    Id = w.Id,
                    Length = w.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(),
                    LengthAsString = w.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsValueString()
                })
                .OrderBy(w => w.Length)
                .ToList();
                
            // самая короткая стена
            var minLengthWall = orderWallsByLength                
                .FirstOrDefault();

            // самая длинная стена
            var maxLengthWall= orderWallsByLength                
                .LastOrDefault();

            // добавляем комментарий в самую длинную и короткую стену
            using (Transaction t= new Transaction(doc, "Commentary for Walls"))
            {
                t.Start();
                SetParameter(doc, minLengthWall);

            }
            return Result.Succeeded;
        }

        private static void SetParameter(Document doc, object minLengthWall)
        {
            var wall = doc.GetElement(minLengthWall.Id) as Wall;
            Parameter commentParameter = wall.LookupParameter("Комментарии");
            if (commentParameter != null || !commentParameter.IsReadOnly)
                commentParameter.Set(minLengthWall.LengthAsString);
        }
    }
}
