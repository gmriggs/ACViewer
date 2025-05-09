using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class SceneDesc
    {
        public DatReaderWriter.Types.SceneDesc _sceneDesc;

        public SceneDesc(DatReaderWriter.Types.SceneDesc sceneDesc)
        {
            _sceneDesc = sceneDesc;
        }

        public List<TreeNode> BuildTree()
        {
            var sceneTypes = new TreeNode("SceneTypes:");

            for (var i = 0; i < _sceneDesc.SceneTypes.Count; i++)
            {
                var sceneType = new TreeNode($"{i}");
                sceneType.Items.AddRange(new SceneType(_sceneDesc.SceneTypes[i]).BuildTree());

                sceneTypes.Items.Add(sceneType);
            }
            return new List<TreeNode>() { sceneTypes };
        }
    }
}
