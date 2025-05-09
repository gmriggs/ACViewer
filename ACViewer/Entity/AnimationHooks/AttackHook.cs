using System.Collections.Generic;

namespace ACViewer.Entity.AnimationHooks
{
    public class AttackHook : AnimationHook
    {
        public AttackHook(DatReaderWriter.Types.AnimationHook hook)
            : base(hook)
        {
        }

        public override List<TreeNode> BuildTree()
        {
            var treeNode = new List<TreeNode>();

            if (_hook is DatReaderWriter.Types.AttackHook _attackHook)
            {
                var attackCone = new AttackCone(_attackHook.AttackCone);
                treeNode.AddRange(attackCone.BuildTree());
            }
            treeNode.AddRange(base.BuildTree());

            return treeNode;
        }
    }
}
