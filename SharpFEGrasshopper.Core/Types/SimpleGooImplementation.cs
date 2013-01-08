using GH_IO;
using GH_IO.Serialization;
using Grasshopper.Kernel.Types;

namespace SharpFEGrasshopper.Core.TypeClass
{
    public abstract class SimpleGooImplementation : IGH_Goo, GH_ISerializable
    {
        bool IGH_Goo.IsValid
        {
            get
            {
                return true;
            }
        }

        string IGH_Goo.IsValidWhyNot
        {
            get
            {
                return "";
            }
        }

        string IGH_Goo.TypeDescription
        {
            get
            {
                return this.GetType().Name;
            }
        }

        string IGH_Goo.TypeName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        bool IGH_Goo.CastFrom(object source)
        {
            return source.GetType().Equals(source.GetType());
        }

        bool IGH_Goo.CastTo<T>(out T target)
        {
            target = default(T);
            return false;
        }

        IGH_Goo IGH_Goo.Duplicate()
        {
            return (IGH_Goo)this.MemberwiseClone();
        }

        IGH_GooProxy IGH_Goo.EmitProxy()
        {
            return (IGH_GooProxy)null;
        }

        object IGH_Goo.ScriptVariable()
        {
            return (object)this;
        }

        bool GH_ISerializable.Read(GH_IReader reader)
        {
            return true;
        }

        bool GH_ISerializable.Write(GH_IWriter writer)
        {
            return true;
        }



    }
}

