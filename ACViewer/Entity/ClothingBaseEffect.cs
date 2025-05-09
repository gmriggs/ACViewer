using System.Collections.Generic;
using System.Linq;

namespace ACViewer.Entity
{
    public class ClothingBaseEffect
    {
        public DatReaderWriter.Types.ClothingBaseEffect _baseEffect;

        public ClothingBaseEffect(DatReaderWriter.Types.ClothingBaseEffect baseEffect)
        {
            _baseEffect = baseEffect;
        }

        public List<TreeNode> BuildTree()
        {
            var objEffects = new TreeNode($"Object Effects:");
            foreach (var objEffect in _baseEffect.CloObjectEffects.OrderBy(i => i.Index))
            {
                var objEffectTree = new ClothingObjectEffect(objEffect).BuildTree();
                var objEffectNode = new TreeNode($"{objEffectTree[0].Name}");
                objEffectTree.RemoveAt(0);
                objEffectNode.Items.AddRange(objEffectTree);
                objEffects.Items.Add(objEffectNode);
            }
            return new List<TreeNode>() { objEffects };
        }
    }
}
