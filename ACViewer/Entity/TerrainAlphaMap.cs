using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class TerrainAlphaMap
    {
        public DatReaderWriter.Types.TerrainAlphaMap _terrainAlphaMap;

        public TerrainAlphaMap(DatReaderWriter.Types.TerrainAlphaMap terrainAlphaMap)
        {
            _terrainAlphaMap = terrainAlphaMap;
        }

        public List<TreeNode> BuildTree()
        {
            var terrainCode = new TreeNode($"TerrainCode: {_terrainAlphaMap.TCode}");
            var textureGID = new TreeNode($"TextureGID: {_terrainAlphaMap.TexGID:X8}", clickable: true);

            return new List<TreeNode>() { terrainCode, textureGID };
        }

        public override string ToString()
        {
            return $"TerrainCode: {_terrainAlphaMap.TCode}, TextureGID: {_terrainAlphaMap.TexGID:X8}";
        }
    }
}
