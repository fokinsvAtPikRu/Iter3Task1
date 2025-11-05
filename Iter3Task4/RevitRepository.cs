using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter3Task4
{
    internal class RevitRepository : IRevitRepository
    {
        public List<T> GetElementsCurrentDocument<T>(Document doc)
        {
            return new FilteredElementCollector(doc)
                .OfClass(typeof(T))
                .OfType<T>()
                .ToList();
        }

        public List<T> GetSelectedElementCurrentDocument<T>()
        {
            throw new NotImplementedException();
        }
    }
}
