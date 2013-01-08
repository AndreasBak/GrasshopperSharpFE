using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using SharpFEGrasshopper.Core.TypeClass;

namespace SharpFEGrasshopper.Core.ClassComponent {

    public class NodeReactionsComponent : GH_Component
    {

        public NodeReactionsComponent()
            : base("RobotNodeReactionResults", "R", "Get reactions for nodes", "Robot", "Results")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Robot Application", "RA", "A robot application", GH_ParamAccess.item);
            pManager.AddPointParameter("Point", "P", "Position of node", GH_ParamAccess.item);
            pManager.AddGenericParameter("Loadcase", "LC", "Loadcase for results", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_DoubleParam("Moment X", "MX", "Bending moment around X-axis");
            pManager.Register_DoubleParam("Moment Y",  "MY", "Bending moment around Y-axis");
            pManager.Register_DoubleParam("Moment Z", "MZ", "Bending moment around Z-axis");
            pManager.Register_DoubleParam("Force X", "FX", "Force in direction of X-axis");
            pManager.Register_DoubleParam("Force Y", "FY", "Force in direction of Y-axis");
            pManager.Register_DoubleParam("Force Z", "FZ", "Force in direction of Z-axis");


        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare a variable for the input
            GH_Model GH_RobotApplication = null;
            Point3d node = new Point3d() ;
           

            // Use the DA object to retrieve the data inside the first input parameter.
            // If the retieval fails (for example if there is no data) we need to abort.
            if (!DA.GetData(0, ref GH_RobotApplication)) { return; }
            if (!DA.GetData(1, ref node)) { return; }




            throw new NotImplementedException();
            

        }

        public override Guid ComponentGuid
        {
            get { return new Guid("81f77682-de88-415c-a857-efb7a1145866"); }
        }

 //       protected override Bitmap Icon { get { return Resources.RobotLinearAnalysisComponentIcon; } }

    }
}
