using System.Collections.Generic;
using System.IO;

namespace ACE.DatLoader.Entity
{
    public class SkyTimeOfDay : IUnpackable
    {
        public float Begin { get; set; }

        public float DirBright { get; set; }
        public float DirHeading { get; set; }
        public float DirPitch { get; set; }
        public uint DirColor { get; set; }

        public float AmbBright { get; set; }
        public uint AmbColor { get; set; }

        public float MinWorldFog { get; set; }
        public float MaxWorldFog { get; set; }
        public uint WorldFogColor { get; set; }
        public uint WorldFog { get; set; }

        public List<SkyObjectReplace> SkyObjReplace { get; } = new List<SkyObjectReplace>();

        public void Unpack(BinaryReader reader)
        {
            Begin       = reader.ReadSingle();

            DirBright   = reader.ReadSingle();
            DirHeading  = reader.ReadSingle();
            DirPitch    = reader.ReadSingle();
            DirColor    = reader.ReadUInt32();

            AmbBright   = reader.ReadSingle();
            AmbColor    = reader.ReadUInt32();

            MinWorldFog     = reader.ReadSingle();
            MaxWorldFog     = reader.ReadSingle();
            WorldFogColor   = reader.ReadUInt32();
            WorldFog        = reader.ReadUInt32();

            reader.AlignBoundary();

            SkyObjReplace.Unpack(reader);
        }
    }
}
