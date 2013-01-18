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

    public class BeamComponent : GH_Component
    {

        public BeamComponent()
            : base("Beam", "B", "A new beam", "SharpFE", "Elements")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
     
            pManager.AddLineParameter("Lines", "L", "Lines to be converted", GH_ParamAccess.item);
            pManager.AddGenericParameter("CrossSection", "CS", "Defines cross section of beam", GH_ParamAccess.item);
             pManager.AddGenericParameter("Material", "M", "Defines material of beam", GH_ParamAccess.item);

        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("Beam", "B", "Beam output");
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare a variable for the input
            Line line = new Line();
            GH_CrossSection crossSection = null;
            GH_Material material = null;


            // Use the DA object to retrieve the data inside the first input parameter.
            // If the retieval fails (for example if there is no data) we need to abort.
            if (!DA.GetData(0, ref line )) { return; }
            if (!DA.GetData(1, ref crossSection)) { return; }
            if (!DA.GetData(2, ref material)) { return; }
            


      
            GH_Beam beam = new GH_Beam(line.From, line.To, crossSection, material);
               

            DA.SetData(0, beam);
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("b00ab4d8-8d86-4a5a-b1fa-09696d696bfe"); }
        }
        protected override Bitmap Icon { get { return Resources.BeamIcon ; } }
    }
}
