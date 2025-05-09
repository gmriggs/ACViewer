using System.Collections.Generic;

using ACViewer.Entity;

namespace ACViewer.FileTypes
{
    public class SoundTable
    {
        public DatReaderWriter.DBObjs.SoundTable _soundTable;

        public SoundTable(DatReaderWriter.DBObjs.SoundTable soundTable)
        {
            _soundTable = soundTable;
        }

        public TreeNode BuildTree()
        {
            var treeView = new TreeNode($"{_soundTable.Id:X8}");

            var hashTable = new TreeNode("SoundHash:");
            foreach (var hash in _soundTable.Hashes)
            {
                var hashNode = new TreeNode($"{hash.Key}");
                hashNode.Items = new SoundHashData(hash.Value).BuildTree();

                hashTable.Items.Add(hashNode);
            }

            var sounds = new TreeNode("Sounds:");
            foreach (var kvp in _soundTable.Sounds)
            {
                var sound = new TreeNode($"{(ACE.Entity.Enum.Sound)kvp.Key}");
                sound.Items.AddRange(new SoundData(kvp.Value).BuildTree());
                sounds.Items.Add(sound);
            }

            treeView.Items.AddRange(new List<TreeNode>() { hashTable, sounds });

            return treeView;
        }
    }
}
