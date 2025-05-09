namespace ACViewer.Entity
{
    public class Plane
    {
        public System.Numerics.Plane _plane;

        public Plane(System.Numerics.Plane plane)
        {
            _plane = plane;
        }

        public override string ToString()
        {
            return $"Normal: {_plane.Normal} - Distance: {_plane.D}";
        }
    }
}
