using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using SharpFEGrasshopper.Core.TypeClass;


namespace SharpFEGrasshopper.Core.ClassComponent {

    public class ConstantStrainTriangleComponent : GH_Component
    {

        public ConstantStrainTriangleComponent()
            : base("Constant Strain Triangle", "T", "A new constant strain triangle", "SharpFE", "Elements")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
     
        	pManager.AddPointParameter("Point 0", "P0", "First point of triangle", GH_ParamAccess.item);
        	pManager.AddPointParameter("Point 1", "P1", "Second point of triangle", GH_ParamAccess.item);
           
        	pManager.AddPointParameter("Point 2", "P2", "Third point of triangle", GH_ParamAccess.item);
           
           
             pManager.AddGenericParameter("Material", "M", "Defines material of triangle", GH_ParamAccess.item);
             pManager.AddNumberParameter("Thickness", "T", "Thickness of element", GH_ParamAccess.item);

        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("Constant Strain Triangle", "T", "Triangle output");
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare a variable for the input
            
            Point3d p0 = new Point3d();
            Point3d p1 = new Point3d();
            Point3d p2 = new Point3d();

          
            GH_Material material = null;
            
            double thickness = 0;


            // Use the DA object to retrieve the data inside the first input parameter.
            // If the retieval fails (for example if there is no data) we need to abort.
            if (!DA.GetData(0, ref p0 )) { return; }
            if (!DA.GetData(1, ref p1 )) { return; }
            if (!DA.GetData(2, ref p2 )) { return; }
            if (!DA.GetData(3, ref material)) { return; }
            if (!DA.GetData(4, ref thickness)) { return; }
            
            

            GH_ConstantStrainTriangle triangle = new GH_ConstantStrainTriangle(p0,p1,p2,material,thickness);
      
          
               

            DA.SetData(0, triangle);
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("4b81c7a4-ae6f-433b-b89c-c069a0d74c8c"); }
        }
    //    protected override Bitmap Icon { get { return Resources.BarComponentIcon; } }
    }
}
