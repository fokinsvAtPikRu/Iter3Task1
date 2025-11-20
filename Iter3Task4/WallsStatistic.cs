using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Linq;

namespace Iter3Task4
{
    [Transaction(TransactionMode.Manual)]
    public class WallsStatistic : IExternalCommand
    {
        private IRevitRepository _repository;
        //public WallsStatistic(IRevitRepository repository)
        //{
        //    _repository = repository;
        //}

        
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uiDoc=commandData.Application.ActiveUIDocument;
            Document doc=uiDoc.Document;

            _repository = new RevitRepository(doc);

            // получаем все стены
            var walls = _repository.GetElementsCurrentDocument<Wall>();

            // количество стен
            var count = walls.Count;

            // если стен нет, завершаем работу
            if (count == 0)
                return Result.Failed;            

            // упорядочиваем список стен по их длине
            var orderWallsByLength = walls
                .Select(w => new WallData
                {
                    Id = w.Id,
                    Length = UnitUtils.ConvertFromInternalUnits(
                        w.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsDouble(),
                        DisplayUnitType.DUT_MILLIMETERS),
                    LengthAsString = w.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH).AsValueString()
                })
                .OrderBy(w => w.Length)
                .ToList();

            // сумма длин всех стен
            var sumLength = orderWallsByLength
                .Sum(w => w.Length);

            // средняя длина стены
            var averageWallLength = sumLength / count;

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
                SetParameter<Wall>(doc,"Комментарии", minLengthWall.Id,"Самая короткая стена");
                SetParameter<Wall>(doc, "Комментарии", maxLengthWall.Id, "Самая длинная стена");
                t.Commit();
            }
            TaskDialog.Show("Инфо", $"Количество стен - {count}\nСредняя длина стен - {averageWallLength}\n" +
                $"Самая длинная стена Id {maxLengthWall.Id} Длина {maxLengthWall.Length}\n" +
                $"Самая короткая стена Id {minLengthWall.Id} Длина {minLengthWall.Length}");
            return Result.Succeeded;
        }

        public static void SetParameter<T>(Document doc,string parameterName, ElementId id, string parameterValue) 
            where T :Element 
        {
            if (doc==null && !doc.IsValidObject)
                throw new ArgumentException("Invalid document");

            T element = doc.GetElement(id) as T;
            if (element == null)
                return;

            Parameter commentParameter = element.LookupParameter(parameterName);
            if (commentParameter != null || !commentParameter.IsReadOnly)
                commentParameter.Set(parameterValue);
            //else TaskDialog.Show("Error", "Parameter Not Found");
        }
    }
}
