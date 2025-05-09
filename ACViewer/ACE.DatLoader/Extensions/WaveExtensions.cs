using System.IO;
using System.Linq;

using DatReaderWriter.DBObjs;

namespace ACE.DatLoader.Extensions
{
    public static class WaveExtensions
    {
        public static void ReadData(this Wave wave, Stream stream)
        {
            var binaryWriter = new BinaryWriter(stream);

            binaryWriter.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));

            uint filesize = (uint)(wave.Data.Length + 36); // 36 is added for all the extra we're adding for the WAV header format
            binaryWriter.Write(filesize);

            binaryWriter.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"));

            binaryWriter.Write(System.Text.Encoding.ASCII.GetBytes("fmt"));
            binaryWriter.Write((byte)0x20); // Null ending to the fmt

            binaryWriter.Write((int)0x10); // 16 ... length of all the above

            // AC audio headers start at Format Type,
            // and are usually 18 bytes, with some exceptions
            // notably objectID A000393 which is 30 bytes

            // WAV headers are always 16 bytes from Format Type to end of header,
            // so this extra data is truncated here.
            binaryWriter.Write(wave.Header.Take(16).ToArray());

            binaryWriter.Write(System.Text.Encoding.ASCII.GetBytes("data"));
            binaryWriter.Write((uint)wave.Data.Length);
            binaryWriter.Write(wave.Data);
        }
    }
}
