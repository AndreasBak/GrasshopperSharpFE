
using Rhino.Geometry;
using SharpFE;


namespace SharpFEGrasshopper.Core.TypeClass
{

    public abstract class GH_Element : SimpleGooImplementation

    {
        public int Index {
			get { return index; }
			set { index = value; }
		}
    	
    	private int index = -1;  //When not represented in Robot the index is -1
       
   
        public override string ToString()
        {
            string s = "Element " + index;
            return s;
        }

        public abstract void ToSharpElement(GH_Model model);
        
        public abstract GeometryBase GetGeometry(GH_Model model);
        
        public abstract GeometryBase GetDeformedGeometry(GH_Model model);
    }

}