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

    public class RectangularCrossSectionComponent : GH_Component
    {

        public RectangularCrossSectionComponent()
            : base("Rectangular Cross Section", "CS", "A new rectangular cross section", "SharpFE", "Properties")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
     
        	pManager.AddNumberParameter("Heigth", "H", "Height of cross section", GH_ParamAccess.item, 100);
        	pManager.AddNumberParameter("Width", "W", "Width of cross section", GH_ParamAccess.item, 100);
        	
        	 pManager[0].Optional = true;
            pManager[1].Optional = true;
          

        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("CrossSection", "CS", "Cross section output");
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare a variable for the input
            double heigth = 100;
            double width  = 100;


            // Use the DA object to retrieve the data inside the first input parameter.
            // If the retieval fails (for example if there is no data) we need to abort.
            if (!DA.GetData(0, ref heigth )) { return; }
            if (!DA.GetData(1, ref width)) { return; }



      
            GH_RectangularCrossSection crossSection = new GH_RectangularCrossSection(heigth, width);
               

            DA.SetData(0, crossSection);
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("52d5a4e6-eda3-47ff-bb93-a3432f3fd351"); }
        }
   //     protected override Bitmap Icon { get { return Resources.CrossSectionIcon; } }
    }
}
