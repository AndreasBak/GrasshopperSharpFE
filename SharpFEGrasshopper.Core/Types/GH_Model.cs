
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
  		
  		public SharpFE.ModelType ModelType {get; set;}
        

        

        public GH_Model(SharpFE.ModelType modelType)
        {
        	this.ModelType = modelType;
            this.Model = new FiniteElementModel(ModelType);
            this.Nodes = new List<FiniteElementNode>();    
            this.Points = new List<Point3d>();
            this.Results = null;
        }

        public override string ToString()
        {
            return this.Model.ToString();
        }
        
        public void Solve() {
        	
        	IFiniteElementSolver solver = new LinearSolver(this.Model);    	
        	this.Results = solver.Solve();
        
        }
       


    }

}