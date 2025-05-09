using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class SoundEntry
    {
        public DatReaderWriter.Types.SoundEntry _data;

        public SoundEntry(DatReaderWriter.Types.SoundEntry data)
        {
            _data = data;
        }

        public List<TreeNode> BuildTree()
        {
            var soundID = new TreeNode($"Sound ID: {_data.Id:X8}", clickable: true);
            var priority = new TreeNode($"Priority: {_data.Priority}");
            var probability = new TreeNode($"Probability: {_data.Probability}");
            var volume = new TreeNode($"Volume: {_data.Volume}");

            return new List<TreeNode>() { soundID, priority, probability, volume };
        }
    }
}
