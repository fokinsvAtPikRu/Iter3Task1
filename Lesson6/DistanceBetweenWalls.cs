using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson6
{
    [Transaction(TransactionMode.Manual)]
    public class DistanceBetweenWalls : IExternalCommand
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
                refs = uiDoc.Selection.PickObjects(ObjectType.Element, new WallsSelectionFilter(), "Select two walls");
            }
            catch
            {
                return Result.Cancelled;
            }
            if (refs.Count != 2)
            {
                TaskDialog.Show("Error", "Need to select two elements");
                return Result.Failed;
            }
            Wall firstWall = doc.GetElement(refs.First()) as Wall;
            Wall secondWall = doc.GetElement(refs.Last()) as Wall;
            // проверка избыточна, но пусть будет
            if (firstWall == null || secondWall == null)
            {
                TaskDialog.Show("Error", "Need to select only walls");
                return Result.Failed;
            }
            // получаем Curve из LocationCurve
            Curve curve1 = GetCurve(firstWall);
            Curve curve2 = GetCurve(secondWall);
            if(curve1 == null || curve2 == null)
            {
                TaskDialog.Show("Error", "Zero-length wall");
                return Result.Failed;
            }

            // получаем вектор направления стены
            XYZ direction1 = GetWallDirection(curve1);
            XYZ direction2 = GetWallDirection(curve2);
            // получаем вектор нормали
            XYZ normal1 = GetNormal(direction1);
            XYZ normal2 = GetNormal(direction2);
            // проверяем на параллельность стены
            if (!IsParalel(normal1, normal2))
            {
                TaskDialog.Show("Error", "The walls are not parallel");
                return Result.Failed;
            }
            // получаем координаты середин стен
            XYZ middle1 = GetCoordinateMiddleWall(curve1);
            XYZ middle2 = GetCoordinateMiddleWall(curve2);
            // переносим вектор в начало координат
            XYZ middle = new XYZ
                (middle2.X - middle1.X,
                middle2.Y - middle1.Y,
                0);
            // вычисляем расстояние
            double distance = Math.Abs(middle.DotProduct(normal1));
            distance = UnitUtils.ConvertFromInternalUnits(distance, DisplayUnitType.DUT_MILLIMETERS);


            TaskDialog.Show("result", $"Distance between walls is {distance:N0} mm");
            return Result.Succeeded;
        }
        /// <summary>
        /// возвращает линию по которой построена стена
        /// </summary>
        /// <param name="wall"></param>
        /// <returns></returns>
        private Curve GetCurve(Wall wall)
        {
            var loc = wall.Location as LocationCurve;
            if (loc == null)
                return null;
            return loc.Curve;
        }
        /// <summary>
        /// Возвращает вектор напрвления для стены
        /// </summary>
        /// <param name="wall"></param>
        /// <returns></returns>
        private XYZ GetWallDirection(Curve curve)
        {            
            XYZ start = curve.GetEndPoint(0);
            XYZ end = curve.GetEndPoint(1);

            return (end - start).Normalize();
        }
        /// <summary>
        /// возвращает единичный вектор нормали к плоскости образованной ветором из параметров метода и вектором (0,0,1) 
        /// </summary>
        /// <param name="vector1"></param>
        /// <returns></returns>
        private XYZ GetNormal(XYZ vector1)
        {
            XYZ vector2 = new XYZ(0, 0, 1);
            return vector2.CrossProduct(vector1).Normalize();
        }
        /// <summary>
        /// проверяет параллельны ли вектора
        /// </summary>
        /// <param name="vector1"></param>
        /// <param name="vector2"></param>
        /// <returns></returns>
        private bool IsParalel(XYZ vector1, XYZ vector2)
        {
            double tolerance = 1e-10;
            double result = vector1.Normalize().DotProduct(vector2.Normalize());
            return 1 - Math.Abs(result) < tolerance;
        }
        /// <summary>
        /// возвращает координаты середины стены
        /// </summary>
        /// <param name="wall"></param>
        /// <returns></returns>
        private XYZ GetCoordinateMiddleWall(Curve curve)
        {            
            XYZ start = curve.GetEndPoint(0);
            XYZ end = curve.GetEndPoint(1);

            return new XYZ(
                (end.X + start.X) / 2,
                (end.Y + start.Y) / 2,
                (end.Z + start.Z) / 2);
        }

    }
}
