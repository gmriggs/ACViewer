using System.Collections.Generic;

using ACE.DatLoader.Extensions;

namespace ACViewer.Entity
{
    public class LightInfo
    {
        public DatReaderWriter.Types.LightInfo _lightInfo;

        public LightInfo(DatReaderWriter.Types.LightInfo lightInfo)
        {
            _lightInfo = lightInfo;
        }

        public List<TreeNode> BuildTree()
        {
            var viewerSpaceLocation = new TreeNode($"Viewer space location: {_lightInfo.ViewSpaceLocation}");

            var color = new TreeNode($"Color: {Color.ToRGBA(_lightInfo.Color.ToUint32())}");
            
            var intensity = new TreeNode($"Intensity: {_lightInfo.Intensity}");

            var falloff = new TreeNode($"Falloff: {_lightInfo.Falloff}");

            var coneAngle = new TreeNode($"ConeAngle: {_lightInfo.ConeAngle}");

            return new List<TreeNode>() { viewerSpaceLocation, color, intensity, falloff, coneAngle };
        }

        public override string ToString()
        {
            return $"Viewer space location: {_lightInfo.ViewSpaceLocation}, Color: {Color.ToRGBA(_lightInfo.Color.ToUint32())}, Intensity: {_lightInfo.Intensity}, Falloff: {_lightInfo.Falloff}, ConeAngle: {_lightInfo.ConeAngle}";
        }
    }
}
