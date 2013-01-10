/*
 * Created by SharpDevelop.
 * User: Andreas Bak
 * Date: 11/12/2012
 * Time: 14:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using NUnit.Framework;
using SharpFEGrasshopper.Core.TypeClass;
using SharpFE;
using Rhino.Geometry;


namespace SharpFEGrasshopper.Tests.TypesTests
{
	[TestFixture]
	public class Full3DTestClass
	{
		
	    GH_Model model;
	    Point3d point1, point2, point3, point4;  
	    GH_NodalLoad nodalLoad1;
	    GH_NodeSupport nodeSupport1, nodeSupport2;
	    Vector3d force, moment;
	    GH_RectangularCrossSection crossSection;
	    GH_ElasticMaterial material;
	    GH_Beam beam1, beam2, beam3;
		
		[SetUp]
		public void Setup()
		{
			model = new GH_Model(ModelType.Full3D);
			crossSection = new GH_RectangularCrossSection(0.5,0.2);		
			material = new GH_ElasticMaterial(0, 2000,0.1,1000);
			
			force = new Vector3d(10,0,0);
			moment = new Vector3d(0,0,0);
						
			point1 = new Point3d(0,0,0);
			point2 = new Point3d(0,0,1);
			point3 = new Point3d(1,0,1);
			point4 = new Point3d(1,0,0);
				
		    beam1 = new GH_Beam(point1, point2, crossSection, material);
		    beam2 = new GH_Beam(point2, point3, crossSection, material);
		    beam3 = new GH_Beam(point3, point4, crossSection, material);

			nodalLoad1 = new GH_NodalLoad(point2, force, moment);	
			nodeSupport1 = new GH_NodeSupport(point1, true, true, true, true, true, true);
			nodeSupport2 = new GH_NodeSupport(point4, true, true, true, true, true, true);
			
					
		}
		
	
		[Test]
				public void CanCreateFull3DModel()
		{
				
			
			Assert.NotNull(model);
			Assert.NotNull(model.Model);
			Assert.NotNull(model.Nodes);
			Assert.NotNull(model.Points);			
			Assert.AreEqual(model.Model.ModelType, ModelType.Full3D);
					
		}
		
		
		
				[Test]
		public void CanCreateBeam() {
					
		
		
        	Assert.NotNull(beam1);
		}
		
		
	
		
	
		
		
				[Test]
		public void CanCreateNodalLoad()
		{
			
						
			Assert.NotNull(nodalLoad1);
			
			
				    
		}
		
		[Test]
		public void CanAddLoadToModel() {
			
			
			beam1.ToSharpElement(model);			
			nodalLoad1.ToSharpLoad(model);

		}
		
		[Test]
		public void CanCreateNodeSupport() {
			Assert.NotNull(nodeSupport1);
		}
		
		
		[Test]
		
		public void CanAddSupportToModel() {
			
			beam1.ToSharpElement(model);			
			nodeSupport1.ToSharpSupport(model);			
			
		
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.X));
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.Y));	
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.Z));	
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.XX));	
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.YY));
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.ZZ));
		}
		
		[Test]		
		public void Truss() {
			
					model = new GH_Model(ModelType.Full3D);
			
					GH_CrossSection crossSection = new GH_RectangularCrossSection(100,100);
					GH_Material material = new GH_ElasticMaterial(100,100,100,100);
					
					
					Point3d p1 = new Point3d(-10,0,0);
					Point3d p2 = new Point3d(0,0,0);
					Point3d p3 = new Point3d(10,0,0);
					Point3d p4 = new Point3d(-10,0,10);
					Point3d p5 = new Point3d(0,0,10);
					Point3d p6 = new Point3d(10,0,10);
					
					
					
					model.Elements.Add(new GH_Beam(p1,p2, crossSection, material));					
					model.Elements.Add(new GH_Beam(p2,p3, crossSection, material));	
					model.Elements.Add(new GH_Beam(p4,p5, crossSection, material));				
					model.Elements.Add(new GH_Beam(p5,p6, crossSection, material));
					model.Elements.Add(new GH_Beam(p1,p4, crossSection, material));
					model.Elements.Add(new GH_Beam(p2,p5, crossSection, material));
					model.Elements.Add(new GH_Beam(p3,p6, crossSection, material));
					model.Elements.Add(new GH_Beam(p4,p2, crossSection, material));
					model.Elements.Add(new GH_Beam(p2,p6, crossSection, material));			
					model.Supports.Add(new GH_NodeSupport(p1, true, true, true, true, true,true));
					model.Supports.Add(new GH_NodeSupport(p3, true, true, true, true, true,true));
					model.Loads.Add(new GH_NodalLoad(p5, new Vector3d(0,0,-1000), new Vector3d(0,0,0)));
					
					model.AssembleSharpModel();
					
			
					Assert.AreEqual(9, model.Model.ElementCount);
			
					model.Solve();
			
			
					Assert.NotNull(model.Results);
			

			
			
					Vector3d displacement = model.GetNodeDisplacement(1); //Displacement at point2 (index 1)
				
			Assert.NotNull(displacement);
			Assert.AreEqual(0.0, displacement.X, 0.001);
			Assert.AreEqual(0.0, displacement.Y, 0.001);
			Assert.AreNotEqual(0.0, displacement.Z); //TODO Calculate real value
			
			
		}
		
		
		[Test]
		
		public void DoesModelSolve() {
			

			beam1.ToSharpElement(model);
			beam2.ToSharpElement(model);
			beam3.ToSharpElement(model);
			
			
			nodeSupport1.ToSharpSupport(model);
			nodeSupport2.ToSharpSupport(model);
			
			
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.X));
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.Y));	
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.Z));	
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.XX));	
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.YY));
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.ZZ));
			
			
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[3], DegreeOfFreedom.X));
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[3], DegreeOfFreedom.Y));
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[3], DegreeOfFreedom.Z));	
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[3], DegreeOfFreedom.XX));	
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[3], DegreeOfFreedom.YY));
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[3], DegreeOfFreedom.ZZ));
			
			nodalLoad1.ToSharpLoad(model);
			
			Assert.AreEqual(3, model.Model.ElementCount);
			
			model.Solve();
			
			
			Assert.NotNull(model.Results);
			
			Assert.False(model.Model.IsConstrained(model.Nodes[1], DegreeOfFreedom.X));
			Assert.False(model.Model.IsConstrained(model.Nodes[1], DegreeOfFreedom.Y));
			Assert.False(model.Model.IsConstrained(model.Nodes[1], DegreeOfFreedom.Z));
			
			
			DisplacementVector displacement = model.Results.GetDisplacement(model.Nodes[1]);
				
			Assert.NotNull(displacement);
			Assert.AreNotEqual(0.0, displacement.X);
			Assert.AreEqual(0.0, displacement.Y, 0.001);
			
			
		}
	}
}
