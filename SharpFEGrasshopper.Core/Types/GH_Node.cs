
using System;
using System.Collections.Generic;
using Rhino.Geometry;
using SharpFE;

namespace SharpFEGrasshopper.Core.TypeClass
{

    public class GH_Node : GH_Element
    {


        private Point3d Position { get; set; }
    

        public GH_Node(Point3d position)
        {
        	this.Position = position;
      
        }

        public override string ToString()
        {
            string s = "Node at " + this.Position;
            return s;
        }

        public override void ToSharpElement(GH_Model model)
        {
        	

        	//Check if node already exist at position
        	int index = model.Points.IndexOf(this.Position);
        		
        	FiniteElementNode node = null;
        	
        	switch (model.ModelType) {
        			
        			
        	case ModelType.Full3D:
        	
        	if (index == -1) {      //Node does not exist		
        		node = model.Model.NodeFactory.Create(this.Position.X, this.Position.Y, this.Position.Z);
        		model.Nodes.Add(node);
        		model.Points.Add(this.Position);
        		this.Index = model.Points.Count-1;
        		
        	} else {
        		node = model.Nodes[index];
        		this.Index = index;
        	}   
        			break;
        			
        	case ModelType.Truss2D:
        	
        	if (index == -1) {      //Node does not exist		
        		node = model.Model.NodeFactory.CreateFor2DTruss(this.Position.X, this.Position.Z);
        		model.Nodes.Add(node);
        		model.Points.Add(this.Position);
        		this.Index = model.Points.Count-1;
        		
        	} else {
        		node = model.Nodes[index];
        		this.Index = index;
        	}   


			
			break;
			
		default:
			throw new Exception("Model type not valid: " + model.ModelType);
			
        	}
        }
    	


    	
		public override GeometryBase GetGeometry(GH_Model model)
		{
			throw new NotImplementedException("Node element. Geometry");
		}
    	
		public override GeometryBase GetDeformedGeometry(GH_Model model)
		{
			throw new NotImplementedException("Node element. Deformed geometry");
		}
    }
}