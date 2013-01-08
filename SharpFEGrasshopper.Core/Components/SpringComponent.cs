using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using SharpFEGrasshopper.Core.TypeClass;
using SharpFEGrasshopper.Properties;

namespace SharpFEGrasshopper.Core.ClassComponent {

    public class SpringComponent : GH_Component
    {

        public SpringComponent()
            : base("Spring", "S", "A new spring", "SharpFE", "Elements")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
     
            pManager.AddLineParameter("Lines", "L", "Lines to be converted", GH_ParamAccess.item);
            pManager.AddNumberParameter("Spring Constant", "k", "Defines stiffness of spring", GH_ParamAccess.item);

        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("Spring", "S", "Spring output");
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare a variable for the input
            Line line = new Line();
            double springConstant = 0;


            // Use the DA object to retrieve the data inside the first input parameter.
            // If the retieval fails (for example if there is no data) we need to abort.
            if (!DA.GetData(0, ref line )) { return; }
            if (!DA.GetData(1, ref springConstant)) { return; }
    //        if (!DA.GetDataList<GHBarRelease>(2, releases)) { return; }
    //        if (!DA.GetDataList<GHBarGroup>(2, groups)) { return; }

      
                GH_Spring spring = new GH_Spring(line.From, line.To, springConstant);
               

            DA.SetData(0, spring);
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("6d10b6e0-ed24-4380-aefa-d1f2b3104613"); }
        }
  //      protected override Bitmap Icon { get { return Resources.BarComponentIcon; } }
    }
}
