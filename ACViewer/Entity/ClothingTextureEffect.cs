namespace ACViewer.Entity
{
    public class ClothingTextureEffect
    {
        public DatReaderWriter.Types.CloTextureEffect _effect;

        public ClothingTextureEffect(DatReaderWriter.Types.CloTextureEffect effect)
        {
            _effect = effect;
        }

        public override string ToString()
        {
            return $"OldTex: {_effect.OldTexture:X8}, NewTex: {_effect.NewTexture:X8}";
        }
    }
}
