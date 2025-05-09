using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class DegradeInfo
    {
        public DatReaderWriter.DBObjs.GfxObjDegradeInfo _degrade;

        public DegradeInfo(DatReaderWriter.DBObjs.GfxObjDegradeInfo degrade)
        {
            _degrade = degrade;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_degrade.Id:X8}");

            foreach (var degrade in _degrade.Degrades)
            {
                var degradeTree = new GfxObjInfo(degrade).BuildTree();
                var degradeNode = new TreeNode($"{degradeTree[0].Name.Replace("ID: ", "")}", clickable: true);
                degradeTree.RemoveAt(0);
                degradeNode.Items.AddRange(degradeTree);
                treeView.Items.Add(degradeNode);
            }
            return treeView;
        }
    }
}
