using System.Collections.Generic;

namespace ACViewer.Entity
{
    public class Season
    {
        public DatReaderWriter.Types.Season _season;

        public Season(DatReaderWriter.Types.Season season)
        {
            _season = season;
        }

        public List<TreeNode> BuildTree()
        {
            var startDate = new TreeNode($"StartDate: {_season.Start}");
            var name = new TreeNode($"Name: {_season.Name}");

            return new List<TreeNode>() { startDate, name };
        }
    }
}
