using DatReaderWriter.Enums;
using DatReaderWriter.Types;

namespace ACE.DatLoader.Extensions
{
    public static class CellPortalExtensions
    {
        public static bool PortalSide(this CellPortal cellPortal)
        {
            // careful, this is reversed from what one might expect.
            // making this a centralized method call to avoid errors...
            return (cellPortal.Flags & PortalFlags.PortalSide) == 0;
        }
    }
}
