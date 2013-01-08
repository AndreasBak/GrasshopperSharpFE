
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
        private List<Point3d> Positions { get; set; }
        
        
        public GH_NodalLoad(Point3d position, Vector3d force, Vector3d moment) {
        
        	this.Positions = new List<Point3d>();
        	this.Force = force;
        	this.Moment = moment;
        	this.Positions.Add(position);
        	
        }

        public GH_NodalLoad(List<Point3d> positions, Vector3d force, Vector3d moment)
        {
        	this.Positions = positions;
        	this.Force = force;
        	this.Moment = moment;
          
        }




        public override void ToSharpLoad(GH_Model model)
        {
        	foreach (Point3d position in Positions) {
        	int index = model.Points.IndexOf(position);
        	
        	if (index != -1) {
        		
        		ForceVector forceVector = null;
        		FiniteElementNode node = model.Nodes[index];  
        	
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
        		
        		model.Model.ApplyForceToNode(forceVector, node);
        			
        		
        	} else {
        	
        		throw new Exception("Can not add force, no node at " + position);
        		
        	
        		
        		
        	}
        			
        			
        	
        	

        	}

           
            
        }
    }

}