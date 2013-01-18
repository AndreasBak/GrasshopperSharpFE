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

    public class NodeSupportComponent : GH_Component
    {

        public NodeSupportComponent()
            : base("Node Support", "S", "A new fixed node support", "SharpFE", "Supports")
        {
        }


        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {

      
            pManager.AddPointParameter("Points", "P", "Positions of node support", GH_ParamAccess.list);
            pManager.AddBooleanParameter("UX", "UX", "X direction", GH_ParamAccess.item);
            pManager.AddBooleanParameter("UY", "UY", "Y direction", GH_ParamAccess.item);
            pManager.AddBooleanParameter("UZ", "UZ", "Z direction", GH_ParamAccess.item);
            pManager.AddBooleanParameter("RX", "RX", "X rotation", GH_ParamAccess.item);
            pManager.AddBooleanParameter("RY", "RY", "Y rotation", GH_ParamAccess.item);
            pManager.AddBooleanParameter("RZ", "RZ", "Z rotation", GH_ParamAccess.item);
        }

        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.Register_GenericParam("Node support", "S", "Robot fixed node support output");
        }

        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // Declare a variable for the input

            List<Point3d> positions = new List<Point3d>();
            bool UX = false;
            bool UY = false;
            bool UZ = false;
            bool RX = false;
            bool RY = false;
            bool RZ = false;

            // Use the DA object to retrieve the data inside the first input parameter.
            // If the retieval fails (for example if there is no data) we need to abort.

            if (!DA.GetDataList<Point3d>(0, positions)) { return; }
            if (!DA.GetData(1, ref UX )) { return; }
            if (!DA.GetData(2, ref UY)) { return; }
            if (!DA.GetData(3, ref UZ)) { return; }
            if (!DA.GetData(4, ref RX)) { return; }
            if (!DA.GetData(5, ref RY)) { return; }
            if (!DA.GetData(6, ref RZ)) { return; }
            
            
            //Create node constrain
            GH_NodeSupport nodeSupport = new GH_NodeSupport(positions, UX, UY, UZ, RX, RY, RZ);
            

            //Return
            DA.SetData(0, nodeSupport);
            
        }

        public override Guid ComponentGuid
        {
            get { return new Guid("23a16691-3a3b-42b3-80ea-a11ea4d6a808"); }
        }

        protected override Bitmap Icon { get { return Resources.NodalSupportIcon; } }

    }
}
