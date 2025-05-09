using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class ContractTable
    {
        public DatReaderWriter.DBObjs.ContractTable _contractTable;

        public ContractTable(DatReaderWriter.DBObjs.ContractTable contractTable)
        {
            _contractTable = contractTable;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_contractTable.Id:X8}");

            foreach (var kvp in _contractTable.Contracts)
            {
                var contract = new TreeNode($"{kvp.Key} - {kvp.Value.ContractName}");
                contract.Items = new Contract(kvp.Value).BuildTree();
                treeView.Items.Add(contract);
            }
            return treeView;
        }
    }
}
