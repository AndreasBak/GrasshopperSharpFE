
using Rhino.Geometry;
using System.Collections.Generic;

namespace SharpFEGrasshopper.Core.TypeClass
{

    public abstract class GH_Load : SimpleGooImplementation

    {
        public string name;


       

        public override string ToString()
        {
            string s = "Load: " + this.name;
            return s;
        }

       

        public abstract void ToSharpLoad(GH_Model model);

    }

}