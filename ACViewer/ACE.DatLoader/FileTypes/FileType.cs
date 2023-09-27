using System.ComponentModel;
using System.IO;

namespace ACE.DatLoader.FileTypes
{
    public abstract class FileType : IUnpackable
    {
        [Browsable(false)]
        public uint Id { get; protected set; }

        public abstract void Unpack(BinaryReader reader);
    }
}
