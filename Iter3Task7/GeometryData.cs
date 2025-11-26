using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iter3Task7
{
    public class GeometryData
    {
        public double Volume { get; set; }
        public FaceArray Faces { get; set; }
        public EdgeArray Edges { get; set; }                    
    }
}
