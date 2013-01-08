
using System;
using System.Collections.Generic;
using Rhino.Geometry;
using SharpFE;

namespace SharpFEGrasshopper.Core.TypeClass
{

    public class GH_Beam : GH_Element
    {


        private Point3d Start { get; set; }
        private Point3d End { get; set; }
        private GH_CrossSection CrossSection { get; set; }
        private GH_Material Material {get; set;}


        public GH_Beam(Point3d start, Point3d end, GH_CrossSection crossSection, GH_Material material)
        {
        	this.Start = start;
        	this.End = end;
        	this.CrossSection = crossSection;
        	this.Material = material;
        }

        public override string ToString()
        {
            string s = "Beam from " + this.Start + " to " + this.End;
            return s;
        }

        public override void ToSharpElement(GH_Model model)
        {
        	
        	FiniteElementNode start = null;
        	FiniteElementNode end = null;
        	
        	int startIndex = model.Points.IndexOf(this.Start);
        		int endIndex = model.Points.IndexOf(this.End);  
        	
        	switch (model.ModelType) {
        			
        			
        		case ModelType.Full3D:
        	
        	if (startIndex == -1) {      //Start node does not exist		
        		start = model.Model.NodeFactory.Create(this.Start.X, this.Start.Y, this.Start.Z);
        		model.Nodes.Add(start);
        		model.Points.Add(this.Start);
        	} else {
        		start = model.Nodes[startIndex];
        	}   

		
			
        	if (endIndex == -1) {      	//End node does not exist	
        		end = model.Model.NodeFactory.Create(this.End.X, this.End.Y, this.End.Z); 
        		model.Nodes.Add(end);
        		model.Points.Add(this.End);
        	} else {
        		end = model.Nodes[endIndex];
        	}
        	    
			
			if (start != null && end != null) {
				model.Model.ElementFactory.CreateLinear3DBeam(start, end, Material.ToSharpMaterial(), CrossSection.ToSharpCrossSection());
			}
			
			
			break;
			
		default:
			throw new Exception("Model type not valid: " + model.ModelType);
			
        	}
        }
    }
}