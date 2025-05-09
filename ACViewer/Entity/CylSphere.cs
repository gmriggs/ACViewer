namespace ACViewer.Entity
{
    public class CylSphere
    {
        public DatReaderWriter.Types.CylSphere _cylSphere;

        public CylSphere(DatReaderWriter.Types.CylSphere cylSphere)
        {
            _cylSphere = cylSphere;
        }

        public override string ToString()
        {
            return $"Origin: {_cylSphere.Origin}, Radius: {_cylSphere.Radius}, Height: {_cylSphere.Height}";
        }
    }
}
