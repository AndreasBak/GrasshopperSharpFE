
using Rhino.Geometry;
using SharpFE;

namespace SharpFEGrasshopper.Core.TypeClass
{

    public abstract class GH_Support : SimpleGooImplementation
    {
       public string name;

       public abstract void ToSharpSupport(GH_Model model);

       public override string ToString()
       {
           return name;
       }
    }

}