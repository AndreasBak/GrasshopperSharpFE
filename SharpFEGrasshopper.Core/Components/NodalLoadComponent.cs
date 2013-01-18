using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using SharpFEGrasshopper.Core.TypeClass;
using SharpGrasshopper;

namespace SharpFEGrasshopper.Core.ClassComponent {

    public class NodalLoadComponent : GH_Component
    {

        public NodalLoadComponent()
            : base("NodalLoad", "L", "Force acting on a node", "SharpFE", "Loads")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
           
            pManager.AddPointParameter("Points", "P", "Apply load to these points", GH_ParamAccess.list);
            pManager.AddVectorParameter("Force", "F", "Vector defining the magnitude and direction of the force", GH_ParamAccess.item);
            pManager.AddVectorParameter("Moment", "M", "Vector defining bending moment magnitude and axis applied to point", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("Nodal Force", "L", "Robot load output");
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare a variable for the input
            List<Point3d> positions = new List<Point3d>();     
            Vector3d force = new Vector3d();
            Vector3d moment = new Vector3d();
    
            // Use the DA object to retrieve the data inside the first input parameter.
            // If the retieval fails (for example if there is no data) we need to abort.

            if (!DA.GetDataList<Point3d>(0, positions)) { return; }         
            if (!DA.GetData(1, ref force)) { return; }
            if (!DA.GetData(2, ref moment)) { return; }

            //Create nodal force
            GH_NodalLoad nodalForce = new GH_NodalLoad(positions, force, moment);
                     
            //Return         
            DA.SetData(0, nodalForce);
            

        }

        public override Guid ComponentGuid
        {
            get { return new Guid("9b05fe6d-5bc0-4201-a3a2-cb8ce983e660"); }
        }

        protected override Bitmap Icon { get { return Resources.NodalForceIcon; } }

    }
}
