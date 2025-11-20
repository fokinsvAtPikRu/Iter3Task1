using System.Collections.Generic;

namespace Iter3Task4
{
    public interface IRevitRepository
    {
        List<T> GetElementsCurrentDocument<T>();
        List<T> GetSelectedElementCurrentDocument<T>();
    }
}
