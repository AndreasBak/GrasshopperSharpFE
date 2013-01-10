using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using SharpFE;
using SharpFEGrasshopper.Core.TypeClass;

namespace SharpFEGrasshopper.Core.ClassComponent {

	public class NodeDisplacementsComponent : GH_Component
	{

		public NodeDisplacementsComponent()
			: base("NodeDisplaments", "dV", "Get displacements for nodes", "SharpFE", "Results")
		{
		}


		protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
		{
			pManager.AddGenericParameter("Finite Element Model", "#FE", "SharpFE Finite Element Model", GH_ParamAccess.item);


		}

		protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
		{
			pManager.Register_VectorParam("Displacement vectors", "dV", "Nodal Displacement Vector");
			pManager.Register_PointParam("Displaced points", "P", "Position of displaced nodes");
			pManager.Register_GeometryParam("Displaced elements", "G", "Geometry of displaced elements");

		}

		protected override void SolveInstance(IGH_DataAccess DA)
		{
			// Declare a variable for the input
			GH_Model model = null;


			// Use the DA object to retrieve the data inside the first input parameter.
			// If the retieval fails (for example if there is no data) we need to abort.
			if (!DA.GetData(0, ref model)) { return; }

			List<Vector3d> displacementVectors = new List<Vector3d>();
			List<Point3d> displacedPoints = new List<Point3d>();
			List<GeometryBase> displacedElements = new List<GeometryBase>();

			for (int i = 0; i < model.Nodes.Count; i++) {			
				Vector3d vector = model.GetNodeDisplacement(i);
				Point3d point = model.GetDisplacedPoint(i);
				displacementVectors.Add(vector);
				displacedPoints.Add(point);		
			}
			
			foreach (GH_Element element in model.Elements) {
				
				displacedElements.Add(element.GetDeformedGeometry(model));
				
				
			}

			DA.SetDataList(0, displacementVectors);
			DA.SetDataList(1, displacedPoints);
			DA.SetDataList(2, displacedElements);

		}

		public override Guid ComponentGuid
		{
			get { return new Guid("7caf8f28-672b-47f7-84e5-3db47e0e2638"); }
		}

		//       protected override Bitmap Icon { get { return Resources.RobotLinearAnalysisComponentIcon; } }

	}
}
