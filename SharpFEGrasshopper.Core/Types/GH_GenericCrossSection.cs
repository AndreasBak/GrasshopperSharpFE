/*
 * Iain Sproat, 2013
 */

namespace SharpFEGrasshopper.Core.TypeClass
{
    using System;
    using SharpFE;

    public class GH_GenericCrossSection : GH_CrossSection
    {
        public double Area
        {
            get;
            private set;
        }
        
        public double Iyy
        {
            get;
            private set;
        }
        
        public GH_GenericCrossSection(double area, double iyy)
        {
            this.Area = area;
            this.Iyy = iyy;
        }
        
        public override ICrossSection ToSharpCrossSection()
        {
            GenericCrossSection crossSection = new GenericCrossSection(this.Area, this.Iyy);
            return crossSection;
        }
    }
}
