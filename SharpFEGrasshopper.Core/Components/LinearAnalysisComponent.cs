using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using SharpFE;
using SharpFEGrasshopper.Core.TypeClass;
using SharpFEGrasshopper.Properties;

namespace SharpFEGrasshopper.Core.ClassComponent {

    public class RobotLinearAnalysisComponent : GH_Component
    {

        public RobotLinearAnalysisComponent()
            : base("Linear analysis", "Calc", "Perform FE analysis", "SharpFE", "Calculation")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("SharpFE model", "#FE", "SharpFE finite element model to analyse", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("SharpFE model", "#FE", "Finite Element mdoel with results");
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare a variable for the input
            GH_Model model = null;

            // Use the DA object to retrieve the data inside the first input parameter.
            // If the retieval fails (for example if there is no data) we need to abort.
            if (!DA.GetData(0, ref model)) { return; }
            
            
            model.Solve();


            DA.SetData(0, model);
            

        }

        public override Guid ComponentGuid
        {
            get { return new Guid("314b8fd6-bffa-462e-812b-601b6fb69615"); }
        }

        protected override Bitmap Icon { get { return Resources.CalculationIcon; } }

    }
}
