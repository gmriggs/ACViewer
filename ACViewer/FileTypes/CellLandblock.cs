using System.Collections.Generic;

using ACE.DatLoader;
using ACE.DatLoader.Extensions;

using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class CellLandblock
    {
        public DatReaderWriter.DBObjs.LandBlock _landblock;
        
        public CellLandblock(DatReaderWriter.DBObjs.LandBlock landblock)
        {
            _landblock = landblock;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_landblock.Id:X8}");

            var hasObjects = new TreeNode($"HasObjects: {_landblock.HasObjects}");
            var terrain = new TreeNode("Terrain:");
            for (var i = 0; i < _landblock.Terrain.Length; i++)
            {
                var t = _landblock.Terrain[i];
                var typename = DatManager.PortalDat.RegionDesc().TerrainInfo.TerrainTypes[(int)t.Type].TerrainName;
                terrain.Items.Add(new TreeNode($"{i}: Road: {t.Road}, Type: {typename}, Scenery: {t.Scenery}"));
                
            }

            var heights = new TreeNode("Heights:");
            for (var i = 0; i < _landblock.Height.Length; i++)
                heights.Items.Add(new TreeNode($"{i}: {_landblock.Height[i]}"));

            treeView.Items.AddRange(new List<TreeNode>() { hasObjects, terrain, heights });

            return treeView;
        }
    }
}
