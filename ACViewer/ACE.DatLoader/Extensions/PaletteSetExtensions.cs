using DatReaderWriter.DBObjs;

namespace ACE.DatLoader.Extensions
{
    public static class PaletteSetExtensions
    {
        /// <summary>
        /// Returns the palette ID (uint, 0x04 file) from the Palette list based on the corresponding hue
        /// Hue is mostly (only?) used in Character Creation data.
        /// "Hue" referred to as "shade" in acclient.c
        /// </summary>
        public static uint GetPaletteID(this PaletteSet paletteSet, double hue)
        {
            // Make sure the PaletteList has valid data and the hue is within valid ranges
            if (paletteSet.Palettes.Count == 0 || hue < 0 || hue > 1)
                return 0;

            // This was the original function - had an issue specifically with Aerfalle's Pallium, WCID 8133
            // int palIndex = Convert.ToInt32(Convert.ToDouble(PaletteList.Count - 0.000001) * hue); // Taken from acclient.c (PalSet::GetPaletteID)

            // Hue is stored in DB as a percent of the total, so do some math to figure out the int position
            int palIndex = (int)((paletteSet.Palettes.Count - 0.000001) * hue); // Taken from acclient.c (PalSet::GetPaletteID)

            // Since the hue numbers are a little odd, make sure we're in the bounds.
            if (palIndex < 0)
                palIndex = 0;

            if (palIndex > paletteSet.Palettes.Count - 1)
                palIndex = paletteSet.Palettes.Count - 1;

            return paletteSet.Palettes[palIndex];
        }
    }
}
