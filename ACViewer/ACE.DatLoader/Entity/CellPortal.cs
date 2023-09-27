using System.IO;
using ACE.Entity.Enum;

namespace ACE.DatLoader.Entity
{
    public class CellPortal : IUnpackable
    {
        public PortalFlags Flags { get; set; }
        public ushort PolygonId { get; set; }
        public ushort OtherCellId { get; set; }
        public ushort OtherPortalId { get; set; }

        public bool ExactMatch => (Flags & PortalFlags.ExactMatch) != 0;
        public bool PortalSide => (Flags & PortalFlags.PortalSide) == 0;

        public void Unpack(BinaryReader reader)
        {
            Flags           = (PortalFlags)reader.ReadUInt16();
            PolygonId       = reader.ReadUInt16();
            OtherCellId     = reader.ReadUInt16();
            OtherPortalId   = reader.ReadUInt16();
        }
    }
}
