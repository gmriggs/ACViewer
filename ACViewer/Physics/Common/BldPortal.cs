using System.Collections.Generic;

using ACE.DatLoader.Extensions;

using DatReaderWriter.Enums;

namespace ACE.Server.Physics.Common
{
    public class BldPortal
    {
        public PortalFlags Flags { get; set; }
        public bool ExactMatch { get; set; }
        public bool PortalSide { get; set; }
        public ushort OtherCellId { get; set; }
        public ushort OtherPortalId { get; set; }
        public List<ushort> StabList { get; set; }

        public BldPortal() { }

        public BldPortal(DatReaderWriter.Types.BuildingPortal bldPortal)
        {
            Flags = bldPortal.Flags;
            ExactMatch = bldPortal.Flags.HasFlag(PortalFlags.ExactMatch);
            PortalSide = bldPortal.PortalSide();
            OtherCellId = bldPortal.OtherCellId;
            OtherPortalId = bldPortal.OtherPortalId;
            StabList = bldPortal.StabList;
        }

        public EnvCell GetOtherCell(uint landblockID)
        {
            var blockCellID = landblockID & 0xFFFF0000 | OtherCellId;

            return (EnvCell)LScape.get_landcell(blockCellID);
        }

        public void add_to_stablist(ref List<ushort> stabList, ref uint maxSize, ref uint stabNum)
        {
            // is maxSize needed with list?
            for (var i = 0; i < StabList.Count; i++)
            {
                var j = (int)stabNum;
                while (j > 0)
                {
                    if (StabList[i] == stabList[j - 1])
                        break;
                    j--;
                }
                if (j > 0)
                {
                    if (stabNum >= maxSize)
                    {
                        var old = stabList;
                        stabList = new List<ushort>();
                        maxSize += 10;
                        foreach (var stab in StabList)
                            stabList.Add(stab);
                    }
                    stabList.Add(StabList[i]);
                    stabNum++;
                }
            }
        }
    }
}
