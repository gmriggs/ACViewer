using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class SoundHashData
    {
        public DatReaderWriter.Types.SoundHashData _data;

        public SoundHashData(DatReaderWriter.Types.SoundHashData data)
        {
            _data = data;
        }

        public List<TreeNode> BuildTree()
        {
            var priority = new TreeNode($"Priority: {_data.Priority}");
            var probability = new TreeNode($"Probability: {_data.Probability}");
            var volume = new TreeNode($"Volume: {_data.Volume}");

            return new List<TreeNode>() { priority, probability, volume };
        }
    }
}
