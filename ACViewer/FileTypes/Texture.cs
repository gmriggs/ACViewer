using System.Collections.Generic;
using System.IO;
using System.Text;

using ACE.Entity.Enum;

using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class Texture
    {
        public DatReaderWriter.DBObjs.RenderSurface _texture;

        public Texture(DatReaderWriter.DBObjs.RenderSurface texture)
        {
            _texture = texture;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_texture.Id:X8}");

            //var unknown = new TreeNode($"Unknown: {_texture.Unknown}");
            var width = new TreeNode($"Width: {_texture.Width}");
            var height = new TreeNode($"Height: {_texture.Height}");
            var format = new TreeNode($"Type: {_texture.Format}");
            var size = new TreeNode($"Size: {_texture.SourceData.Length} bytes");

            treeView.Items.AddRange(new List<TreeNode>() { /*unknown,*/ width, height, format, size });

            if (_texture.DefaultPaletteId != 0)
                treeView.Items.Add(new TreeNode($"DefaultPalette: {_texture.DefaultPaletteId:X8}", clickable: true));

            if (_texture.Format == DatReaderWriter.Enums.PixelFormat.PFID_INDEX16)
            {
                var sb = new StringBuilder();

                using (var reader = new BinaryReader(new MemoryStream(_texture.SourceData)))
                {
                    for (var y = 0; y < _texture.Height; y++)
                    {
                        for (var x = 0; x < _texture.Width; x++)
                        {
                            if (x == 0)
                                sb.Append((reader.ReadInt16() / 8).ToString().PadLeft(3, ' '));
                            else
                                sb.Append(", " + (reader.ReadInt16() / 8).ToString().PadLeft(3, ' '));
                        }
                        sb.AppendLine();
                    }
                }
                var colorIndices = new TreeNode("Color indices:");
                colorIndices.Items = new List<TreeNode>();
                colorIndices.Items.Add(new TreeNode(sb.ToString()));
                treeView.Items.Add(colorIndices);
            }
            return treeView;
        }
    }
}
