using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using Grasshopper.Kernel;
using Rhino;
using Rhino.Geometry;
using SharpFE;
using SharpFEGrasshopper.Core;
using SharpFEGrasshopper.Core.TypeClass;
using SharpFEGrasshopper.Properties;

namespace SharpFEGrasshopper.Core.ClassComponent {

    public class AssembleModelComponent : GH_Component
    {

        public AssembleModelComponent()
            : base("AssembleModel", "A", "Assemble SharpFE model", "SharpFE", "Application")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)


        {
            pManager.AddGenericParameter("Elements", "E", "Elements", GH_ParamAccess.list);     
            pManager.AddGenericParameter("Supports", "S", "Supports", GH_ParamAccess.list);
            pManager.AddGenericParameter("Loads", "L", "Loads", GH_ParamAccess.list);
            pManager.AddIntegerParameter("ModelType","T", "Model type: 0 = 2D truss, 1 = 3D full", GH_ParamAccess.item, 0);

            pManager[0].Optional = true;
            pManager[1].Optional = true;
            pManager[2].Optional = true;
        //    pManager[3].Optional = true;
           
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("SharpFE Finite Element Model", "#FE", "Finite element model");
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            

            // Declare a variable for the input
        
            List<GH_Element> elements = new List<GH_Element>();
            List<GH_Support> supports = new List<GH_Support>();
           
            List<GH_Load> loads = new List<GH_Load>();
          
            int modelType = 0;

            // Use the DA object to retrieve the data inside the first input parameter.
            // If the retieval fails (for example if there is no data) we need to abort.
       //     if (!DA.GetDataList<Point3d>(0, points)) { points = new List<Point3d>(); }
       //     if (!DA.GetDataList<GHBar>(1, bars)) { bars = new List<GHBar>(); }
            if (!DA.GetDataList<GH_Element>(0, elements)) {  }
        
            if (!DA.GetDataList<GH_Support>(1, supports)) {  }
            if (!DA.GetDataList<GH_Load>(2, loads)) { }
            if (!DA.GetData(3, ref modelType)) { }



            //Clear current structure... Perhaps change this for a more parametric approach, or opening existing files
           
            
            GH_Model model = null;
            
            switch (modelType) {

            	case 0:

            model = new GH_Model(SharpFE.ModelType.Truss2D);
            break;
            
           case 1:
            model = new GH_Model(SharpFE.ModelType.Full3D);
            break;
            
            
           default:
            
            throw new Exception("Model type does not exist or not yet implemented");
            }
           
            

            //Loop trough and create elements
 
            foreach (GH_Element element in elements) {
                element.ToSharpElement(model);
            }

            //Loop trough and create supports
            foreach (GH_Support support in supports)
            {
                support.ToSharpSupport(model);
            }


            //Set loads
            foreach (GH_Load load in loads)
            {
              load.ToSharpLoad(model);       
            }


            DA.SetData(0, model);           
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("dbf71b83-513f-4cc8-958d-0d4d4dc36538"); }
        }

        protected override Bitmap Icon { get { return Resources.RobotApplicationComponentIcon; } }

    }
}
