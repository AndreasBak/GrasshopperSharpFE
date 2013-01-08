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
			pManager.Register_VectorParam("Displacement", "dV", "Nodal Displacement Vector");


		}

		protected override void SolveInstance(IGH_DataAccess DA)
		{
			// Declare a variable for the input
			GH_Model model = null;


			// Use the DA object to retrieve the data inside the first input parameter.
			// If the retieval fails (for example if there is no data) we need to abort.
			if (!DA.GetData(0, ref model)) { return; }

			List<Vector3d> displacementVectors = new List<Vector3d>();

			if (model.Results == null) {

				this.AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "Results not availiable, solve model first.");
				return;

			} else {



				FiniteElementResults results = model.Results;


				switch (model.ModelType) {


					case ModelType.Truss2D:
						foreach (FiniteElementNode node in model.Nodes) {

							Vector3d vector = new Vector3d();

							if (model.Model.IsConstrained(node, DegreeOfFreedom.X)) {

								vector.X = 0;


							} else {
								vector.X = results.GetDisplacement(node).X;
							}
							if (model.Model.IsConstrained(node, DegreeOfFreedom.Z)) {

								vector.Z = 0;


							} else {
								vector.Z = results.GetDisplacement(node).Z;
							}


							displacementVectors.Add(vector);
						}
						break;


					case ModelType.Full3D:
						foreach (FiniteElementNode node in model.Nodes) {

							Vector3d vector = new Vector3d();

							if (model.Model.IsConstrained(node, DegreeOfFreedom.X)) {

								vector.X = 0;

							} else {
								vector.X = results.GetDisplacement(node).X;
							}
							if (model.Model.IsConstrained(node, DegreeOfFreedom.Y)) {

								vector.Y = 0;


							} else {
				
								vector.Y = results.GetDisplacement(node).Y;
							}

							if (model.Model.IsConstrained(node, DegreeOfFreedom.Z)) {

								vector.Z = 0;
							} else {
								vector.Z = results.GetDisplacement(node).Z;
							}
							displacementVectors.Add(vector);
						}
						break;


					default:
						throw new Exception("No such model type: " + model.ModelType);

				}



			}



			DA.SetDataList(0, displacementVectors);




		}

		public override Guid ComponentGuid
		{
			get { return new Guid("7caf8f28-672b-47f7-84e5-3db47e0e2638"); }
		}

		//       protected override Bitmap Icon { get { return Resources.RobotLinearAnalysisComponentIcon; } }

	}
}
