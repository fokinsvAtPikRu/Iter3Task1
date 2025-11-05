using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter3Task4
{
    class ElementsListServices<T> where T : Element
    {
        List<T> _elements;
        BuiltInParameter _parametr;
        public ElementsListServices(List<T> elements, BuiltInParameter parametr)
        {
            _elements = elements;
            _parametr = parametr;
        }

        public TOut GetSumma<TOut>(Func<Parameter,TOut> valueExtractor) =>_elements

                .Sum(e=>valueExtractor(e.get_Parameter(_parametr)));
        

    }
}
