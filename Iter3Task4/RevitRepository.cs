using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Iter3Task4
{
    internal class RevitRepository : IRevitRepository
    {
        private Document _doc;
        public RevitRepository(Document doc) 
        {
            _doc = doc;        
        }
        public List<T> GetElementsCurrentDocument<T>()
        {
            return new FilteredElementCollector(_doc)
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
