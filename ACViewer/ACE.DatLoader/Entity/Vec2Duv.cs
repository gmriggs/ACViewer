using System.IO;

namespace ACE.DatLoader.Entity
{
    /// <summary>
    /// Info on texture UV mapping
    /// </summary>
    public class Vec2Duv : IUnpackable
    {
        public float U { get; set; }
        public float V { get; set; }

        public void Unpack(BinaryReader reader)
        {
            U = reader.ReadSingle();
            V = reader.ReadSingle();
        }
    }
}
