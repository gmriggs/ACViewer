using DatReaderWriter.Enums;
using DatReaderWriter.Types;

namespace ACE.DatLoader.Extensions
{
    public static class BuildingPortalExtensions
    {
        public static bool PortalSide(this BuildingPortal buildingPortal)
        {
            // careful, this is reversed from what one might expect.
            // making this a centralized method call to avoid errors...
            return (buildingPortal.Flags & PortalFlags.PortalSide) == 0;
        }
    }
}
