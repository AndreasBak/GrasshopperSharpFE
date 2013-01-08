
using System;
using System.Collections.Generic;
using Rhino.Geometry;
using SharpFE;

namespace SharpFEGrasshopper.Core.TypeClass
{

    public class GH_Spring : GH_Element
    {


        private Point3d Start { get; set; }
        private Point3d End { get; set; }
        private double SpringConstant { get; set; }


        public GH_Spring(Point3d start, Point3d end, double springConstant)
        {
        	this.Start = start;
        	this.End = end;
        	this.SpringConstant = springConstant;
        }

        public override string ToString()
        {
            string s = "Spring from " + this.Start + " to " + this.End;
            return s;
        }

        public override void ToSharpElement(GH_Model model)
        {
        	
        	FiniteElementNode start = null;
        	FiniteElementNode end = null;
        	
        	int startIndex = model.Points.IndexOf(this.Start);
        	
        	
        	switch (model.ModelType) {
        			
        			
        		case ModelType.Truss2D:
        	
        	if (startIndex == -1) {      //Start node does not exist		
        		start = model.Model.NodeFactory.CreateForTruss(this.Start.X, this.Start.Z); 
        		model.Nodes.Add(start);
        		model.Points.Add(this.Start);
        	} else {
        		start = model.Nodes[startIndex];
        	}   

			int endIndex = model.Points.IndexOf(this.End);  
			
        	if (endIndex == -1) {      	//End node does not exist	
        		end = model.Model.NodeFactory.CreateForTruss(this.End.X, this.End.Z); 
        		model.Nodes.Add(end);
        		model.Points.Add(this.End);
        	} else {
        		end = model.Nodes[endIndex];
        	}
        	    
			
			if (start != null && end != null) {
				model.Model.ElementFactory.CreateLinearConstantSpring (start, end, SpringConstant);
			}
			break;
			
		default:
			throw new Exception("Model type not valid: " + model.ModelType);
			
        	}
        }
    }
}