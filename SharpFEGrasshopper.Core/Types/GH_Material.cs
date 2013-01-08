/*
 * Created by SharpDevelop.
 * User: Andreas Bak
 * Date: 07/01/2013
 * Time: 15:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using SharpFE;

namespace SharpFEGrasshopper.Core.TypeClass
{
	/// <summary>
	/// Description of GH_CrossSection.
	/// </summary>
	public abstract class GH_Material : SimpleGooImplementation
	{
		
		public GH_Material()
		{
		}
		
	
	public abstract IMaterial ToSharpMaterial();
		
		
		
		

	}
}
