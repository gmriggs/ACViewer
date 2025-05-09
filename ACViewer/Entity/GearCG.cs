using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class GearCG
    {
        public DatReaderWriter.Types.GearCG _gear;

        public GearCG(DatReaderWriter.Types.GearCG gear)
        {
            _gear = gear;
        }

        public List<TreeNode> BuildTree()
        {
            var name = new TreeNode($"Name: {_gear.Name}");
            var clothingTable = new TreeNode($"Clothing Table: {_gear.ClothingTable:X8}", clickable: true);
            var weenieDefault = new TreeNode($"Weenie Default: {_gear.WeenieDefault}");

            return new List<TreeNode>() { name, clothingTable, weenieDefault };
        }
    }
}
