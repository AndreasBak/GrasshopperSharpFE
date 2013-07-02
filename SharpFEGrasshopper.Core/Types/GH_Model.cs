
using System;
using System.Collections.Generic;
using Rhino.Geometry;
using SharpFE;

namespace SharpFEGrasshopper.Core.TypeClass
{

    public class GH_Model : SimpleGooImplementation
    {
    	
    	public FiniteElementModel Model { get; set; }
        public List<FiniteElementNode> Nodes { get; set; }
  		public FiniteElementResults Results { get; set; } 
  		public List<Point3d> Points { get; set; }
  		
  		public List<GH_Element> Elements { get; set; }
  		public List<GH_Load> Loads { get; set; }
  		public List<GH_Support> Supports { get; set; }
  		
  		public SharpFE.ModelType ModelType {get; set;}
        

        

        public GH_Model(ModelType modelType)
        {
        	this.ModelType = modelType;
            this.Model = new FiniteElementModel(ModelType);
            this.Nodes = new List<FiniteElementNode>();    
            this.Points = new List<Point3d>();
            this.Results = null;
            this.Elements = new List<GH_Element>();
            this.Loads = new List<GH_Load>();
            this.Supports = new List<GH_Support>();
        }

        public override string ToString()
        {
            return this.Model.ToString();
        }
        
        public void Solve() {
        	
        	IFiniteElementSolver solver = new LinearSolver(this.Model);    	
        	this.Results = solver.Solve();
        
        }
        
        
        public Vector3d GetNodeDisplacement(int nodeIndex) {
        	
        	
        	
        	   	
        	if (this.Results == null) {

        		throw new Exception("Results not availiable, please solve model first");

			} else {
				Vector3d vector = new Vector3d();
				FiniteElementNode node = this.Nodes[nodeIndex];

				switch (this.ModelType) {
					case ModelType.Truss2D:
											
							if (this.Model.IsConstrained(node, DegreeOfFreedom.X)) {

								vector.X = 0;

							} else {
								vector.X = Results.GetDisplacement(node).X;
							}
							if (this.Model.IsConstrained(node, DegreeOfFreedom.Z)) {

								vector.Z = 0;

							} else {
								vector.Z = Results.GetDisplacement(node).Z;
							}
					
					break;
					case ModelType.Full3D:

							if (this.Model.IsConstrained(node, DegreeOfFreedom.X)) {
								vector.X = 0;

							} else {
								vector.X = Results.GetDisplacement(node).X;
							}
							if (this.Model.IsConstrained(node, DegreeOfFreedom.Y)) {

								vector.Y = 0;

							} else {
				
								vector.Y = Results.GetDisplacement(node).Y;
							}

							if (this.Model.IsConstrained(node, DegreeOfFreedom.Z)) {

								vector.Z = 0;
							} else {
								vector.Z = Results.GetDisplacement(node).Z;
							}
					
					
					break;


					default:
					throw new Exception("No such model type: " + this.ModelType);
				}
				return vector;	
        	}
        }
        
        
        public Vector3d GetNodeReaction(int nodeIndex) {
        	
        	Vector3d vector = new Vector3d();
        	
        	if (this.Results == null) {

        		throw new Exception("Results not availiable, please solve model first");

			} else {
				
				FiniteElementNode node = this.Nodes[nodeIndex];
				
				ReactionVector reaction = this.Results.GetReaction(node);
			
        	}
        
        	return vector;
        }
        
        	
        	
        	public Point3d GetDisplacedPoint(int nodeIndex) {	
        		return this.Points[nodeIndex] + GetNodeDisplacement(nodeIndex);
        	}
        




			
        	
        
        
        public void AssembleSharpModel() {
        	          //Loop through and create elements
            foreach (GH_Element element in Elements) {
                element.ToSharpElement(this);
            }    
          

            //Loop through and create supports
            foreach (GH_Support support in Supports)
            {
                support.ToSharpSupport(this);
            }

            //Set loads
            foreach (GH_Load load in Loads)
            {
              load.ToSharpLoad(this);       
            }
        }
        	
 
       


    }

}