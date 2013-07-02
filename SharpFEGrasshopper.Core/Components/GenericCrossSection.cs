

namespace SharpFEGrasshopper.Core.ClassComponent
{
    using System;
    using System.Drawing;
    
    using Grasshopper.Kernel;
    using SharpFEGrasshopper.Core.TypeClass;
    using SharpGrasshopper;

    public class GenericCrossSectionComponent : GH_Component
    {

        public GenericCrossSectionComponent()
            : base("Generic Cross Section", "GCS", "A new generic cross section", "SharpFE", "Properties")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            
            pManager.AddNumberParameter("Area", "A", "Area of cross section", GH_ParamAccess.item, 100);
            pManager.AddNumberParameter("Iyy", "Iyy", "Second moment of area around y-y axis of cross section", GH_ParamAccess.item, 200000);
            
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
            double area = -1;
            double iyy  = -1;

            // Use the DA object to retrieve the data inside the first input parameter.
            // If the retieval fails (for example if there is no data) we need to abort.
            if (!DA.GetData(0, ref area ))
            {
                return;
            }
            
            if (!DA.GetData(1, ref iyy))
            {
                return;
            }
            
            GH_GenericCrossSection crossSection = new GH_GenericCrossSection(area, iyy);
            
            DA.SetData(0, crossSection);
        }

        public override Guid ComponentGuid
        {
            get
            {
                return new Guid("72CD2357-2D5B-44B1-9A47-B6CCB4057F76");
            }
        }
        
        protected override Bitmap Icon
        {
            get
            {
                return Resources.RectangularSectionIcon; ///TODO change the icon
            }
        }
    }
}
