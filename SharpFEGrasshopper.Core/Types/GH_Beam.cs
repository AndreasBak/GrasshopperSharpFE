
using System;
using System.Collections.Generic;
using Rhino.Geometry;
using SharpFE;

namespace SharpFEGrasshopper.Core.TypeClass
{

	public class GH_Beam : GH_Element
	{
		private GH_Node Start { get; set; }
		private GH_Node End { get; set; }
		private GH_Material Material {get; set;}
		private GH_CrossSection CrossSection { get; set; }


		public GH_Beam(Point3d start, Point3d end, GH_CrossSection crossSection, GH_Material material)
		{

			this.Start = new GH_Node(start);
			this.End = new GH_Node(end);

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
			Start.ToSharpElement(model);
			End.ToSharpElement(model);
			FiniteElementNode startNode = model.Nodes[Start.Index];
			FiniteElementNode endNode = model.Nodes[End.Index];
			model.Model.ElementFactory.CreateLinear1DBeam(startNode, endNode, Material.ToSharpMaterial(), CrossSection.ToSharpCrossSection());
		}

		public override GeometryBase GetGeometry(GH_Model model)
		{
			LineCurve line = new LineCurve(model.Points[Start.Index], model.Points[End.Index]);
			return line;
		}

		public override GeometryBase GetDeformedGeometry(GH_Model model)
		{
			LineCurve line = new LineCurve(model.GetDisplacedPoint(Start.Index), model.GetDisplacedPoint(End.Index));
			return line;
		}
	}
}