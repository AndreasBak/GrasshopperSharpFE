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

    public class ElasticMaterialComponent : GH_Component
    {

        public ElasticMaterialComponent()
            : base("Elastic Material", "M", "A new elastic material", "SharpFE", "Properties")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
     
        	pManager.AddNumberParameter("Youngs Modulus", "E", "Modulus of elasticity", GH_ParamAccess.item, 210000);
        	pManager.AddNumberParameter("Density", "Rho", "Density of material", GH_ParamAccess.item, 100);
        	pManager.AddNumberParameter("Poissons Ratio", "Nu", "Poissons Ratio", GH_ParamAccess.item, 0.3);
        	pManager.AddNumberParameter("Shear Modulus", "G", "Shear modulus", GH_ParamAccess.item, 1000);
        	
        	 pManager[0].Optional = true;
            pManager[1].Optional = true;
            pManager[2].Optional = true;
             pManager[3].Optional = true;
            

        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("Material", "M", "Material output");
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare a variable for the input
            double e = 0;
            double rho  = 0;
            double nu = 0;
            double g = 0;


            // Use the DA object to retrieve the data inside the first input parameter.
            // If the retieval fails (for example if there is no data) we need to abort.
            if (!DA.GetData(0, ref e )) { return; }
            if (!DA.GetData(1, ref rho)) { return; }
                 if (!DA.GetData(0, ref nu )) { return; }
            if (!DA.GetData(1, ref g)) { return; }     
            GH_ElasticMaterial material = new GH_ElasticMaterial(rho, e, nu, g);
               

            DA.SetData(0, material);
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("0322a1cc-0aca-4753-a1f6-5cc650bcdecc"); }
        }
        protected override Bitmap Icon { get { return Resources.MaterialIcon; } }
    }
}
