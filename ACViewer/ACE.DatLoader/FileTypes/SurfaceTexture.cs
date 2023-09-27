using ACE.Common;
using ACE.DatLoader.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ACE.DatLoader.FileTypes
{
    [DatFileType(DatFileType.SurfaceTexture)]
    public class SurfaceTexture : FileType
    {
        // public int Id { get; set; }
        public int Unknown { get; set; }
        public byte UnknownByte { get; set; }
        // public int TextureCount { get; set; }
        public List<uint> Textures { get; set; } = new List<uint>(); // These values correspond to a Surface (0x06) entry

        public override void Unpack(BinaryReader reader)
        {
            Id = reader.ReadUInt32();
            Unknown = reader.ReadInt32();
            UnknownByte = reader.ReadByte();
            Textures.Unpack(reader);
        }
    }
}
