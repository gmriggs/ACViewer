using ACE.DatLoader;
using ACE.DatLoader.Extensions;
using ACE.DatLoader.FileTypes;
using ACE.Entity;
using ACE.Entity.Enum.Properties;
using ACE.Entity.Models;

namespace ACE.Server.WorldObjects
{
    public class Clothing : WorldObject
    {
        /// <summary>
        /// A new biota be created taking all of its values from weenie.
        /// </summary>
        public Clothing(Weenie weenie, ObjectGuid guid) : base(weenie, guid)
        {
            SetEphemeralValues();
        }

        /// <summary>
        /// Restore a WorldObject from the database.
        /// </summary>
        public Clothing(Biota biota) : base(biota)
        {
            SetEphemeralValues();
        }

        private void SetEphemeralValues()
        {
        }

        /// <summary>
        /// This will also set the icon based on the palette
        /// </summary>
        public void SetProperties(int palette, double shade)
        {
            DatManager.PortalDat.TryReadFileCache(GetProperty(PropertyDataId.ClothingBase) ?? 0, out DatReaderWriter.DBObjs.Clothing clothing);
            var icon = clothing.GetIcon((uint)palette);

            SetProperty(PropertyDataId.Icon, icon);
            SetProperty(PropertyInt.PaletteTemplate, palette);
            SetProperty(PropertyFloat.Shade, shade);
        }
    }
}
