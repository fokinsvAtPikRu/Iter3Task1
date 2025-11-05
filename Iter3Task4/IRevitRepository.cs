using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter3Task4
{
    public interface IRevitRepository
    {
        List<T> GetElementsCurrentDocument<T>(Document doc);
        List<T> GetSelectedElementCurrentDocument<T>();
    }
}
