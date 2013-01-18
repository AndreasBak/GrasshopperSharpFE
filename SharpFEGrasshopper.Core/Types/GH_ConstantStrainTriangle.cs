
using System;
using System.Collections.Generic;
using Rhino.Geometry;
using SharpFE;

namespace SharpFEGrasshopper.Core.TypeClass
{

    public class GH_ConstantStrainTriangle : GH_Element
    {


        private List<Point3d> Points { get; set; }
        
       
        private GH_Material Material {get; set;}
        
        private double Thickness {get; set;}


        public GH_ConstantStrainTriangle(Point3d p0, Point3d p1, Point3d p2, GH_Material material, double thickness)
        {
        	this.Points = new List<Point3d>();
        	this.Points.Add(p0);
        	this.Points.Add(p1);
        	this.Points.Add(p2);
        	
        	this.Material = material;
        	this.Thickness = thickness;
        }

        public override string ToString()
        {
        	string s = "Triangle element: p0 = " + this.Points[0] + " p1 = " + this.Points[1] + " p2 = " + this.Points[2];
            return s;
        }

        public override void ToSharpElement(GH_Model model)
        {
        	
        	FiniteElementNode n0 = null;
        	FiniteElementNode  n1 = null;
        	FiniteElementNode  n2 = null;
        	
        	int n0Index = model.Points.IndexOf(this.Points[0]);
        	int n1Index = model.Points.IndexOf(this.Points[1]);
        	int n2Index = model.Points.IndexOf(this.Points[2]);
        	
        	switch (model.ModelType) {
        			
        			
        		case ModelType.Full3D:
        	
        	if (n0Index == -1) {      //Node does not exist		
        				n0 = model.Model.NodeFactory.Create(this.Points[0].X, this.Points[0].Y, this.Points[0].Z);
        		model.Nodes.Add(n0);
        		model.Points.Add(this.Points[0]);
        	} else {
        		n0 = model.Nodes[n0Index];
        	}   
        			        	if (n1Index == -1) {      //Node does not exist		
        				n1 = model.Model.NodeFactory.Create(this.Points[1].X, this.Points[1].Y, this.Points[1].Z);
        		model.Nodes.Add(n1);
        		model.Points.Add(this.Points[1]);
        	} else {
        		n1 = model.Nodes[n1Index];
        	}   
        			        	if (n2Index == -1) {      //Node does not exist		
        				n2 = model.Model.NodeFactory.Create(this.Points[2].X, this.Points[2].Y, this.Points[2].Z);
        		model.Nodes.Add(n2);
        		model.Points.Add(this.Points[2]);
        	} else {
        		n2 = model.Nodes[n2Index];
        	}   

			
			
 
        	    
			
			if (n0 != null && n1 != null && n2 != null) {
        				model.Model.ElementFactory.CreateLinearConstantStrainTriangle(n0, n1, n2, this.Material.ToSharpMaterial(), this.Thickness);
        				                                                          
			}
			
			break;
			
		default:
			throw new Exception("Model type not valid: " + model.ModelType);
			
        	}
        }
    	
	
    	

    	
		public override GeometryBase GetGeometry(GH_Model model)
		{
			throw new NotImplementedException("Triangle element. Geometry");
		}
    	
		public override GeometryBase GetDeformedGeometry(GH_Model model)
		{
			throw new NotImplementedException("Triangle element. Deformed geometry");
		}
    }
}