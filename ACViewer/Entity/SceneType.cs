using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class SceneType
    {
        public DatReaderWriter.Types.SceneType _sceneType;

        public SceneType(DatReaderWriter.Types.SceneType sceneType)
        {
            _sceneType = sceneType;
        }

        public List<TreeNode> BuildTree()
        {
            var sceneTableIdx = new TreeNode($"SceneTableIdx: {_sceneType.StbIndex}");

            var scenes = new TreeNode($"Scenes:");

            foreach (var scene in _sceneType.Scenes)
                scenes.Items.Add(new TreeNode($"{scene:X8}", clickable: true));

            return new List<TreeNode>() { sceneTableIdx, scenes };
        }
    }
}
