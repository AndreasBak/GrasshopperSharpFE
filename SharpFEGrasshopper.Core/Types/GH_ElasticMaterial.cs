/*
 * Created by SharpDevelop.
 * User: Andreas Bak
 * Date: 07/01/2013
 * Time: 15:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using SharpFE;

namespace SharpFEGrasshopper.Core.TypeClass
{
	/// <summary>
	/// Description of GH_ElasticMaterial.
	/// </summary>
	public class GH_ElasticMaterial : GH_Material
	{
		
		public double Rho { get; set; }
		public double E { get; set; }
		public double Nu { get; set; }
		public double G { get; set; }
		
		public GH_ElasticMaterial(double rho, double e, double nu, double g)
		{
			this.Rho = rho;
			this.E = e;
			this.G = g;
			this.Nu = nu;
		}
		
		public override IMaterial ToSharpMaterial()
		{
			GenericElasticMaterial material = new GenericElasticMaterial(Rho,E,Nu,G);
			
			return material;
		}
	}
}
