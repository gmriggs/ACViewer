using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class PaletteSet
    {
        public DatReaderWriter.DBObjs.PaletteSet _paletteSet;

        public PaletteSet(DatReaderWriter.DBObjs.PaletteSet paletteSet)
        {
            _paletteSet = paletteSet;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_paletteSet.Id:X8}");

            foreach (var paletteID in _paletteSet.Palettes)
                treeView.Items.Add(new TreeNode($"{paletteID:X8}", clickable: true));

            return treeView;
        }
    }
}
