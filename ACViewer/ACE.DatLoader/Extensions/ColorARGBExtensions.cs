using DatReaderWriter.Types;

namespace ACE.DatLoader.Extensions
{
    public static class ColorARGBExtensions
    {
        public static uint ToUint32(this ColorARGB c)
        {
            return (uint)((c.Alpha << 24) | (c.Red << 16) | (c.Green << 8) | (c.Blue));
        }
    }
}
