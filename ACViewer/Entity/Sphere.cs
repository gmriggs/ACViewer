namespace ACViewer.Entity
{
    public class Sphere
    {
        public DatReaderWriter.Types.Sphere _sphere;

        public Sphere(DatReaderWriter.Types.Sphere sphere)
        {
            _sphere = sphere;
        }

        public override string ToString()
        {
            return $"Origin: {_sphere.Origin}, Radius: {_sphere.Radius}";
        }
    }
}
