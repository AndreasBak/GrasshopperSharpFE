/*
 * Created by SharpDevelop.
 * User: Andreas Bak
 * Date: 07/01/2013
 * Time: 15:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using SharpFE;

namespace SharpFEGrasshopper.Core.TypeClass
{
	/// <summary>
	/// Description of GH_RectangularCrossSection.
	/// </summary>
	public class GH_RectangularCrossSection : GH_CrossSection
	{
		
		
		public double Height { get; set; }
		public double Width { get; set; }
		
		public GH_RectangularCrossSection(double heigth, double width)
		{
			this.Height = heigth;
			this.Width = width;
		}
		
		public override ICrossSection ToSharpCrossSection()
		{
			SolidRectangle crossSection = new SolidRectangle(Height, Width);
			return crossSection;
		}
	}
}
