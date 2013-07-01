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
using SharpGrasshopper;

//using SharpFEGrasshopper.Properties;

namespace SharpFEGrasshopper.Core.ClassComponent {

    public class AssembleModelComponent : GH_Component
    {

        public AssembleModelComponent()
            : base("AssembleModel", "A", "Assemble SharpFE model", "SharpFE", "Application")
        {
            //empty
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
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("SharpFE Finite Element Model", "#FE", "Finite element model");
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<GH_Element> elements = new List<GH_Element>();
            List<GH_Support> supports = new List<GH_Support>();
            
            List<GH_Load> loads = new List<GH_Load>();
            
            int modelType = 0;

            if (!DA.GetDataList<GH_Element>(0, elements))
            { 
                return;
            }
            
            if (!DA.GetDataList<GH_Support>(1, supports))
            { 
                return;
            }
            
            if (!DA.GetDataList<GH_Load>(2, loads))
            {
                return;
            }
            
            if (!DA.GetData(3, ref modelType))
            {
                return;
            }

            //Clear current structure... Perhaps change this for a more parametric approach, or opening existing files
            GH_Model model = null;
            
            switch (modelType)
            {
                case 0:
                    model = new GH_Model(ModelType.Truss2D);
                    break;
                case 1:
                    model = new GH_Model(ModelType.Full3D);
                    break;  
                default:
                    throw new Exception("Model type does not exist or not yet implemented");
            }
            
            model.Elements = elements;
            model.Loads = loads;
            model.Supports = supports;
            
            model.AssembleSharpModel();
            
            DA.SetData(0, model);
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("dbf71b83-513f-4cc8-958d-0d4d4dc36538"); }
        }

        protected override Bitmap Icon
        {
            get 
            { 
                return Resources.SharpFEIcon;
            }
        }

    }
}
