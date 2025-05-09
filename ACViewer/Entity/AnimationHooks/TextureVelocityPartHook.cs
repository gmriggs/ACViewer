using System.Collections.Generic;

namespace ACViewer.Entity.AnimationHooks
{
    public class TextureVelocityPartHook : AnimationHook
    {
        public TextureVelocityPartHook(DatReaderWriter.Types.AnimationHook hook)
            : base(hook)
        {
        }

        public override List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_hook is DatReaderWriter.Types.TextureVelocityPartHook _textureVelocityPartHook)
            {
                treeNode.Add(new TreeNode($"PartIndex: {_textureVelocityPartHook.PartIndex}"));
                treeNode.Add(new TreeNode($"USpeed: {_textureVelocityPartHook.USpeed}"));
                treeNode.Add(new TreeNode($"VSpeed: {_textureVelocityPartHook.VSpeed}"));
            }
            treeNode.AddRange(base.BuildTree());

            return treeNode;
        }
    }
}
