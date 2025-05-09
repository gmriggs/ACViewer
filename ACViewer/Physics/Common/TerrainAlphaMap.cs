namespace ACE.Server.Physics.Common
{
    public class TerrainAlphaMap
    {
        public DatReaderWriter.Types.TerrainAlphaMap _alphaMap;

        public uint TCode;
        public uint TexGID;
        public ImgTex Texture;

        public static readonly TerrainAlphaMap NULL;

        public TerrainAlphaMap() { }

        public TerrainAlphaMap(DatReaderWriter.Types.TerrainAlphaMap alphaMap)
        {
            _alphaMap = alphaMap;

            TCode = alphaMap.TCode;
            TexGID = alphaMap.TexGID;
        }
    }
}
