
using System;
using System.Collections.Generic;
using Rhino.Geometry;
using SharpFE;

namespace SharpFEGrasshopper.Core.TypeClass
{

    public class GH_Spring : GH_Element
    {


        private GH_Node Start { get; set; }
        private GH_Node End { get; set; }
        private double SpringConstant { get; set; }


        public GH_Spring(Point3d start, Point3d end, double springConstant)
        {
        	this.Start = new GH_Node(start);
        	this.End = new GH_Node(end);
        	this.SpringConstant = springConstant;
        }

        public override string ToString()
        {
            string s = "Spring from " + this.Start + " to " + this.End;
            return s;
        }

        public override void ToSharpElement(GH_Model model)
        {
        	
  
        	
        	Start.ToSharpElement(model);
        	End.ToSharpElement(model);
        	
        	
        	FiniteElementNode startNode = model.Nodes[Start.Index];
        	FiniteElementNode endNode = model.Nodes[End.Index];
        	
        	model.Model.ElementFactory.CreateLinearConstantSpring(startNode,endNode, this.SpringConstant);
        	
        }
    	
		public override GeometryBase GetGeometry(GH_Model model)
		{
			throw new NotImplementedException("Spring element. Geometry");
		}
    	
		public override GeometryBase GetDeformedGeometry(GH_Model model)
		{
			throw new NotImplementedException("Spring element. Deformed geometry");
		}
    }
}