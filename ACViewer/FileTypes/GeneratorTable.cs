using System.Collections.Generic;

using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class GeneratorTable
    {
        public DatReaderWriter.DBObjs.ObjectHierarchy _generatorTable;

        public GeneratorTable(DatReaderWriter.DBObjs.ObjectHierarchy generatorTable)
        {
            _generatorTable = generatorTable;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_generatorTable.Id:X8}");

            var generators = new TreeNode($"Generators");
            generators.Items = new Generator(_generatorTable).BuildTree();

            var playDayItems = new TreeNode($"PlayDayItems");
            //playDayItems.Items = BuildGenerators(_generatorTable.PlayDayItems);

            var weenieObjectsItems = new TreeNode($"WeenieObjectsItems");
            //weenieObjectsItems.Items = BuildGenerators(_generatorTable.WeenieObjectsItems);

            treeView.Items = new List<TreeNode>() { generators, playDayItems, weenieObjectsItems };

            return treeView;
        }

        public List<TreeNode> BuildGenerators(List<DatReaderWriter.DBObjs.ObjectHierarchy> generators)
        {
            var nodes = new List<TreeNode>();
            
            foreach (var item in generators)
            {
                /*var heading = item.Id != 0 ? $"{item.Id} - {item.Name}" : item.Name;

                var node = new TreeNode(heading);
                node.Items = new Generator(item).BuildTree();

                nodes.Add(node);*/
            }
            return nodes;
        }
    }
}
