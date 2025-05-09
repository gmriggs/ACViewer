using ACE.DatLoader.Extensions;
using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class Palette
    {
        public DatReaderWriter.DBObjs.Palette _palette;

        public Palette(DatReaderWriter.DBObjs.Palette palette)
        {
            _palette = palette;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_palette.Id:X8}");

            foreach (var color in _palette.Colors)
                treeView.Items.Add(new TreeNode(Color.ToRGBA(color.ToUint32())));

            return treeView;
        }
    }
}
