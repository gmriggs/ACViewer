using System;

using ACE.DatLoader;
using ACE.DatLoader.FileTypes;
using ACE.Entity.Enum;

using DatReaderWriter.DBObjs;

namespace ACE.Server.Managers
{
    public partial class RecipeManager
    {
        public static uint MaterialDualDID = 0x27000000;

        public static string GetMaterialName(MaterialType materialType)
        {
            DatManager.PortalDat.TryReadFileCache(MaterialDualDID, out DualDataIdMapper dualDIDs);

            if (!dualDIDs.ClientEnumToName.TryGetValue((uint)materialType, out var materialName))
            {
                Console.WriteLine($"RecipeManager.GetMaterialName({materialType}): couldn't find material name");
                return materialType.ToString();
            }
            return materialName.Replace("_", " ");
        }
    }
}
