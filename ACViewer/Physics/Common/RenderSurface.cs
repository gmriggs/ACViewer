using ACE.DatLoader.FileTypes;
using ACE.Entity.Enum;

namespace ACE.Server.Physics.Common
{
    public class RenderSurface
    {
        public DatReaderWriter.DBObjs.RenderSurface _texture;

        public int Width;
        public int Height;
        public DatReaderWriter.Enums.PixelFormat Format;
        public int Length;
        public byte[] Data;
        public uint? DefaultPaletteID;

        public RenderSurface()
        {
        }

        public RenderSurface(DatReaderWriter.DBObjs.RenderSurface texture)
        {
            if (texture == null) return;
            
            _texture = texture;

            Width = texture.Width;
            Height = texture.Height;
            Format = texture.Format;
            Length = texture.SourceData.Length;
            Data = texture.SourceData;
            DefaultPaletteID = texture.DefaultPaletteId;
        }

        // RenderSurfaceD3D.Create()
        public bool Create(uint width, uint height, DatReaderWriter.Enums.PixelFormat format, bool localData)
        {
            Width = (int)width;
            Height = (int)height;
            Format = format;

            // get bits per pixel in PFDesc
            var bitsPerPixel = 32;
            Length = (int)(width * height * (bitsPerPixel / 8));
            Data = new byte[Length];
            return true;
        }
    }
}
