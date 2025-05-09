namespace ACE.Server.Physics.Common
{
    public class RoadAlphaMap
    {
        public DatReaderWriter.Types.RoadAlphaMap _alphaMap;

        public uint RCode;
        public uint RoadTexGID;
        public ImgTex Texture;

        public static readonly RoadAlphaMap NULL;

        public RoadAlphaMap(DatReaderWriter.Types.RoadAlphaMap alphaMap)
        {
            _alphaMap = alphaMap;

            RCode = alphaMap.RCode;
            RoadTexGID = alphaMap.TexGID;
        }
    }
}
