using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

using DatReaderWriter.DBObjs;
using DatReaderWriter.Enums;

namespace ACE.DatLoader.Extensions
{
    public static class TextureExtensions
    {
        /// <summary>
        /// Reads RenderSurface to bitmap structure
        /// </summary>
        public static Bitmap GetBitmap(this RenderSurface texture)
        {
            switch (texture.Format)
            {
                case PixelFormat.PFID_CUSTOM_RAW_JPEG:
                    {
                        var stream = new MemoryStream(texture.SourceData);
                        var image = Image.FromStream(stream);
                        return new Bitmap(image);
                    }
                case PixelFormat.PFID_DXT1:
                    {
                        var image = DxtUtil.DecompressDxt1(texture.SourceData, texture.Width, texture.Height);
                        return texture.GetBitmap(image);
                    }
                case PixelFormat.PFID_DXT3:
                    {
                        var image = DxtUtil.DecompressDxt3(texture.SourceData, texture.Width, texture.Height);
                        return texture.GetBitmap(image);
                    }
                case PixelFormat.PFID_DXT5:
                    {
                        var image = DxtUtil.DecompressDxt5(texture.SourceData, texture.Width, texture.Height);
                        return texture.GetBitmap(image);
                    }
                default:
                    {
                        List<int> colors = texture.GetImageColorArray();
                        return texture.GetBitmap(colors);
                    }
            }
        }

        /// <summary>
        /// Converts the byte array SourceData into color values per pixel
        /// </summary>
        private static List<int> GetImageColorArray(this RenderSurface texture)
        {
            List<int> colors = new List<int>();
            if (texture.SourceData == null || texture.SourceData.Length == 0) return colors;

            switch (texture.Format)
            {
                case PixelFormat.PFID_R8G8B8: // RGB
                    using (BinaryReader reader = new BinaryReader(new MemoryStream(texture.SourceData)))
                    {
                        for (uint i = 0; i < texture.Height; i++)
                            for (uint j = 0; j < texture.Width; j++)
                            {
                                byte b = reader.ReadByte();
                                byte g = reader.ReadByte();
                                byte r = reader.ReadByte();
                                int color = (r << 16) | (g << 8) | b;
                                colors.Add(color);
                            }
                    }
                    break;
                case PixelFormat.PFID_CUSTOM_LSCAPE_R8G8B8:
                    using (BinaryReader reader = new BinaryReader(new MemoryStream(texture.SourceData)))
                    {
                        for (uint i = 0; i < texture.Height; i++)
                            for (uint j = 0; j < texture.Width; j++)
                            {
                                byte r = reader.ReadByte();
                                byte g = reader.ReadByte();
                                byte b = reader.ReadByte();
                                int color = (r << 16) | (g << 8) | b;
                                colors.Add(color);
                            }
                    }
                    break;
                case PixelFormat.PFID_A8R8G8B8: // ARGB format. Most UI textures fall into this category
                    using (BinaryReader reader = new BinaryReader(new MemoryStream(texture.SourceData)))
                    {
                        for (uint i = 0; i < texture.Height; i++)
                            for (uint j = 0; j < texture.Width; j++)
                                colors.Add(reader.ReadInt32());
                    }
                    break;
                case PixelFormat.PFID_INDEX16: // 16-bit indexed colors. Index references position in a palette;
                    using (BinaryReader reader = new BinaryReader(new MemoryStream(texture.SourceData)))
                    {
                        for (uint y = 0; y < texture.Height; y++)
                            for (uint x = 0; x < texture.Width; x++)
                                colors.Add(reader.ReadInt16());
                    }
                    break;
                case PixelFormat.PFID_A8: // Greyscale, also known as Cairo A8.
                case PixelFormat.PFID_CUSTOM_LSCAPE_ALPHA:
                    using (BinaryReader reader = new BinaryReader(new MemoryStream(texture.SourceData)))
                    {
                        for (uint y = 0; y < texture.Height; y++)
                            for (uint x = 0; x < texture.Width; x++)
                                colors.Add(reader.ReadByte());
                    }
                    break;
                case PixelFormat.PFID_P8: // Indexed
                    using (BinaryReader reader = new BinaryReader(new MemoryStream(texture.SourceData)))
                    {
                        for (uint y = 0; y < texture.Height; y++)
                            for (uint x = 0; x < texture.Width; x++)
                                colors.Add(reader.ReadByte());
                    }
                    break;
                case PixelFormat.PFID_R5G6B5: // 16-bit RGB
                    using (BinaryReader reader = new BinaryReader(new MemoryStream(texture.SourceData)))
                    {
                        for (uint y = 0; y < texture.Height; y++)
                            for (uint x = 0; x < texture.Width; x++)
                            {
                                ushort val = reader.ReadUInt16();
                                List<int> color = get565RGB(val);
                                colors.Add(color[0]); // Red
                                colors.Add(color[1]); // Green
                                colors.Add(color[2]); // Blue
                            }
                    }
                    break;
                case PixelFormat.PFID_A4R4G4B4:
                    using (BinaryReader reader = new BinaryReader(new MemoryStream(texture.SourceData)))
                    {
                        for (uint y = 0; y < texture.Height; y++)
                            for (uint x = 0; x < texture.Width; x++)
                            {
                                ushort val = reader.ReadUInt16();
                                int alpha = (val >> 12) / 0xF * 255;
                                int red = (val >> 8 & 0xF) / 0xF * 255;
                                int green = (val >> 4 & 0xF) / 0xF * 255;
                                int blue = (val & 0xF) / 0xF * 255;

                                colors.Add(alpha);
                                colors.Add(red);
                                colors.Add(green);
                                colors.Add(blue);
                            }
                    }
                    break;
                default:
                    Console.WriteLine("Unhandled SurfacePixelFormat (" + texture.Format.ToString() + ") in RenderSurface " + texture.Id.ToString("X8"));
                    break;
            }

            return colors;
        }

        /// <summary>
        /// Generates Bitmap data from colorArray.
        /// </summary>
        private static Bitmap GetBitmap(this RenderSurface texture, List<int> colorArray)
        {
            Bitmap image = new Bitmap(texture.Width, texture.Height);
            switch (texture.Format)
            {
                case PixelFormat.PFID_R8G8B8:
                case PixelFormat.PFID_CUSTOM_LSCAPE_R8G8B8:
                    for (int i = 0; i < texture.Height; i++)
                        for (int j = 0; j < texture.Width; j++)
                        {
                            int idx = (i * texture.Width) + j;
                            int r = (colorArray[idx] & 0xFF0000) >> 16;
                            int g = (colorArray[idx] & 0xFF00) >> 8;
                            int b = colorArray[idx] & 0xFF;
                            image.SetPixel(j, i, Color.FromArgb(r, g, b));
                        }
                    break;
                case PixelFormat.PFID_A8R8G8B8:
                    for (int i = 0; i < texture.Height; i++)
                        for (int j = 0; j < texture.Width; j++)
                        {
                            int idx = (i * texture.Width) + j;
                            int a = (int)((colorArray[idx] & 0xFF000000) >> 24);
                            int r = (colorArray[idx] & 0xFF0000) >> 16;
                            int g = (colorArray[idx] & 0xFF00) >> 8;
                            int b = colorArray[idx] & 0xFF;
                            image.SetPixel(j, i, Color.FromArgb(a, r, g, b));
                        }
                    break;
                case PixelFormat.PFID_INDEX16:
                case PixelFormat.PFID_P8:
                    DatManager.PortalDat.TryReadFileCache(texture.DefaultPaletteId, out Palette pal);

                    // Apply any custom palette colors, if any, to our loaded palette (note, this may be all of them!)
                    /*if (texture.CustomPaletteColors.Count > 0)
                        foreach (KeyValuePair<int, uint> entry in CustomPaletteColors)
                            if (entry.Key <= pal.Colors.Count)
                                pal.Colors[entry.Key] = entry.Value;

                    for (int i = 0; i < texture.Height; i++)
                        for (int j = 0; j < texture.Width; j++)
                        {
                            int idx = (i * Width) + j;
                            int a = (int)((pal.Colors[colorArray[idx]] & 0xFF000000) >> 24);
                            int r = (int)(pal.Colors[colorArray[idx]] & 0xFF0000) >> 16;
                            int g = (int)(pal.Colors[colorArray[idx]] & 0xFF00) >> 8;
                            int b = (int)pal.Colors[colorArray[idx]] & 0xFF;
                            image.SetPixel(j, i, Color.FromArgb(a, r, g, b));
                        }*/
                    break;
                case PixelFormat.PFID_A8:
                case PixelFormat.PFID_CUSTOM_LSCAPE_ALPHA:
                    for (int i = 0; i < texture.Height; i++)
                        for (int j = 0; j < texture.Width; j++)
                        {
                            int idx = (i * texture.Width) + j;
                            int r = colorArray[idx];
                            int g = colorArray[idx];
                            int b = colorArray[idx];
                            image.SetPixel(j, i, Color.FromArgb(r, g, b));
                        }
                    break;
                case PixelFormat.PFID_R5G6B5: // 16-bit RGB
                    for (int i = 0; i < texture.Height; i++)
                        for (int j = 0; j < texture.Width; j++)
                        {
                            int idx = 3 * ((i * texture.Width) + j);
                            int r = (int)(colorArray[idx]);
                            int g = (int)(colorArray[idx + 1]);
                            int b = (int)(colorArray[idx + 2]);
                            image.SetPixel(j, i, Color.FromArgb(r, g, b));
                        }
                    break;
                case PixelFormat.PFID_A4R4G4B4:
                    for (int i = 0; i < texture.Height; i++)
                        for (int j = 0; j < texture.Width; j++)
                        {
                            int idx = 4 * ((i * texture.Width) + j);
                            int a = (colorArray[idx]);
                            int r = (colorArray[idx + 1]);
                            int g = (colorArray[idx + 2]);
                            int b = (colorArray[idx + 3]);
                            image.SetPixel(j, i, Color.FromArgb(a, r, g, b));
                        }
                    break;
            }
            return image;
        }

        /// <summary>
        /// Generates Bitmap data from byteArray, generated by DXT1, DXT3, and DXT5 image foramts.
        /// </summary>
        private static Bitmap GetBitmap(this RenderSurface texture, byte[] byteArray)
        {
            Bitmap image = new Bitmap(texture.Width, texture.Height);
            for (int i = 0; i < texture.Height; i++)
                for (int j = 0; j < texture.Width; j++)
                {
                    int idx = 4 * ((i * texture.Width) + j);
                    int r = (int)(byteArray[idx]);
                    int g = (int)(byteArray[idx + 1]);
                    int b = (int)(byteArray[idx + 2]);
                    int a = (int)(byteArray[idx + 3]);
                    image.SetPixel(j, i, Color.FromArgb(a, r, g, b));
                }

            return image;
        }

        // https://docs.microsoft.com/en-us/windows/desktop/DirectShow/working-with-16-bit-rgb
        private static List<int> get565RGB(ushort val)
        {
            List<int> color = new List<int>();

            int red_mask = 0xF800;
            int green_mask = 0x7E0;
            int blue_mask = 0x1F;

            int red = ((val & red_mask) >> 11) << 3;
            int green = ((val & green_mask) >> 5) << 2;
            int blue = (val & blue_mask) << 3;

            color.Add(red); // Red
            color.Add(green); // Green
            color.Add(blue); // Blue

            return color;
        }
    }
}
