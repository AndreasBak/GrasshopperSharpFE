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
	public class Truss2DTestClass
	{
		
		GH_Spring spring1, spring2;
		
		
	    GH_Model model;
	    
	    Point3d point1, point2, point3;
	    
	    double springConstant;
	    GH_NodalLoad nodalLoad1, nodalLoad2;
	    
	    GH_NodeSupport nodeSupport1, nodeSupport2;
	    
	    Vector3d force, moment;
	    

		
		[SetUp]
		public void Setup()
		{
			
			
			model = new GH_Model(ModelType.Truss2D);
	
			
			springConstant = 10;
			force = new Vector3d(0,0,10);	
			moment = new Vector3d(0,0,0);
						
			point1 = new Point3d(0,0,0);
			point2 = new Point3d(0,0,10);
			point3 = new Point3d(0,0,20);		
			spring1 = new GH_Spring(point1, point2, springConstant);
		    spring2 = new GH_Spring(point2, point3, springConstant);
			nodalLoad1 = new GH_NodalLoad(point2, force, moment);
			nodalLoad2 = new GH_NodalLoad(point3, force, moment);
			
			nodeSupport1 = new GH_NodeSupport(point1, true, true, true, true, true, true);
			nodeSupport2 = new GH_NodeSupport(point2, true, true, false, true, true, true);
					
		}
		
		public void CanCreateTruss2DModel()
		{

			
			Assert.NotNull(model);
			Assert.NotNull(model.Model);
			Assert.NotNull(model.Nodes);
			Assert.NotNull(model.Points);			
			Assert.AreEqual(model.Model.ModelType, ModelType.Truss2D);
					
		}

		
		[Test]
		public void CanCreateSpring() {
			
			Assert.NotNull(spring1);
		}
		
		[Test]	
		public void CanAddSpringToModel() 
		{
			spring1.ToSharpElement(model);		
			Assert.AreEqual(1, model.Model.ElementCount);					
			Assert.AreEqual(model.Nodes.Count, model.Points.Count);
			Assert.AreEqual(2, model.Nodes.Count);
		}
		
		[Test]
		public void	CanAddTwoSpringsWithSamePointToModel() {			
			spring1.ToSharpElement(model);
			spring2.ToSharpElement(model);
			Assert.AreEqual(2, model.Model.ElementCount);
		}
		
		
				[Test]
		public void CanCreateNodalLoad()
		{
			
						
			Assert.NotNull(nodalLoad1);
				    
		}
		
		[Test]
		public void CanAddLoadToModel() {
			
			
			spring1.ToSharpElement(model);			
			nodalLoad1.ToSharpLoad(model);

		}
		
		[Test]
		public void CanCreateNodeSupport() {
			Assert.NotNull(nodeSupport1);
		}
		
		
		[Test]
		
		public void CanAddSupportToModel() {
			
			spring1.ToSharpElement(model);			
			nodeSupport1.ToSharpSupport(model);			
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.X));
			Assert.IsTrue(model.Model.IsConstrained(model.Nodes[0], DegreeOfFreedom.Z));	
		}
		
		
		[Test]
		
		public void DoesModelSolve() {
			
			spring1.ToSharpElement(model);
		//	spring2.toSharpElement(model);
			
			nodeSupport1.ToSharpSupport(model);
			nodeSupport2.ToSharpSupport(model);
			nodalLoad1.ToSharpLoad(model);
			
			model.Solve();
			
			
			
			
		}
	}
}
