using DatReaderWriter.DBObjs;

namespace ACE.DatLoader.Extensions
{
    public static class SetupModelExtensions
    {
        public static void CreateSimpleSetup(this Setup setup)
        {
            setup.SortingSphere = new DatReaderWriter.Types.Sphere();
            setup.SelectionSphere = new DatReaderWriter.Types.Sphere();
            setup.Flags |= DatReaderWriter.Enums.SetupFlags.AllowFreeHeading;
        }
    }
}
