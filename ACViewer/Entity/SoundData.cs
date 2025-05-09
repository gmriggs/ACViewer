using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class SoundData
    {
        public DatReaderWriter.Types.SoundData _soundData;

        public SoundData(DatReaderWriter.Types.SoundData soundData)
        {
            _soundData = soundData;
        }

        public List<TreeNode> BuildTree()
        {
            var soundTable = new TreeNode("SoundTable:");

            foreach (var sound in _soundData.Entries)
            {
                var soundTree = new SoundEntry(sound).BuildTree();
                var soundNode = new TreeNode(soundTree[0].Name.Replace("Sound ID: ", ""), clickable: true);
                soundTree.RemoveAt(0);
                soundNode.Items.AddRange(soundTree);
                soundTable.Items.Add(soundNode);
            }
            //var unknown = new TreeNode($"Unknown: {_soundData.Unknown}");

            return new List<TreeNode>() { soundTable/*, unknown*/ };
        }
    }
}
