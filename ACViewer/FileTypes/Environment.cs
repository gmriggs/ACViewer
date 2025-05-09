using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class Environment
    {
        public DatReaderWriter.DBObjs.Environment _env;

        public Environment(DatReaderWriter.DBObjs.Environment env)
        {
            _env = env;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_env.Id:X8}");

            foreach (var kvp in _env.Cells)
            {
                var cellStruct = new TreeNode($"{kvp.Key}");
                cellStruct.Items.AddRange(new CellStruct(kvp.Value).BuildTree());
                treeView.Items.Add(cellStruct);
            }
            return treeView;
        }
    }
}
