using System.Linq;

using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class BadData
    {
        public DatReaderWriter.DBObjs.BadDataTable _badData;

        public BadData(DatReaderWriter.DBObjs.BadDataTable badData)
        {
            _badData = badData;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_badData.Id:X8}");

            var wcids = _badData.BadIds.Keys.OrderBy(i => i).ToList();

            foreach (var wcid in wcids)
                treeView.Items.Add(new TreeNode($"{wcid}"));

            return treeView;
        }
    }
}
