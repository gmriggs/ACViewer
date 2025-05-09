using System.Collections.Generic;

using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class SurfaceTexture
    {
        public DatReaderWriter.DBObjs.SurfaceTexture _surfaceTexture;

        public SurfaceTexture(DatReaderWriter.DBObjs.SurfaceTexture surfaceTexture)
        {
            _surfaceTexture = surfaceTexture;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_surfaceTexture.Id:X8}");

            //var unknown = new TreeNode($"Unknown: {_surfaceTexture.Unknown}");
            //var unknownByte = new TreeNode($"UnknownByte: {_surfaceTexture.UnknownByte}");
            var type = new TreeNode($"Type: {_surfaceTexture.Type}");

            var textures = new TreeNode("Textures:");
            foreach (var textureID in _surfaceTexture.Textures)
                textures.Items.Add(new TreeNode($"{textureID:X8}", clickable: true));

            treeView.Items.AddRange(new List<TreeNode>() { type, textures });

            return treeView;
        }
    }
}
