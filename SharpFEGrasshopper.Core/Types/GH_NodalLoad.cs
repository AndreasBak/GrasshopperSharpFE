
using System;
using System.Collections.Generic;
using Rhino.Geometry;
using SharpFE;

namespace SharpFEGrasshopper.Core.TypeClass
{

  

    public class GH_NodalLoad : GH_Load
    {

		private Vector3d Force { get; set; }      
		private Vector3d Moment { get; set; }
        private List<GH_Node> Nodes { get; set; }
        
        
        public GH_NodalLoad(Point3d position, Vector3d force, Vector3d moment) {
        
        	this.Nodes = new List<GH_Node>();
        	
        	
        	this.Force = force;
        	this.Moment = moment;
        	this.Nodes.Add(new GH_Node(position));
        	
        }

        public GH_NodalLoad(List<Point3d> positions, Vector3d force, Vector3d moment)
        {
        	this.Nodes = new List<GH_Node>();
        	
        	
        	foreach(Point3d position in positions) {
        		this.Nodes.Add(new GH_Node(position));
        	}
        	this.Force = force;
        	this.Moment = moment;
          
        }
        
        public override string ToString()
		{
			return string.Format("Nodal Load: Force={0}, Moment={1}, at Nodes={2}]", Force, Moment, Nodes);
		}




        public override void ToSharpLoad(GH_Model model)
        {
        	
        	
        	ForceVector forceVector = null;
        	
        	switch (model.ModelType) {
        			
        		case SharpFE.ModelType.Truss2D:        	      				
        		forceVector = model.Model.ForceFactory.CreateForTruss(Force.X, Force.Z);        		
        		      		
        		
        		break;
        		
        	case SharpFE.ModelType.Full3D:
        		
        		forceVector = model.Model.ForceFactory.Create(Force.X, Force.Y, Force.Z,Moment.X,Moment.Y,Moment.Z);
        		break;
        		
        		
        		
        		default:
        		throw new Exception("No such model type implemented: "  + model.ModelType);
        
        		}
        	foreach (GH_Node node in Nodes) {
        	
        		node.ToSharpElement(model);
        		FiniteElementNode FEnode = model.Nodes[node.Index];
        		
        		
        		model.Model.ApplyForceToNode(forceVector, FEnode);
        		

        	}

           
            
        }
    }

}