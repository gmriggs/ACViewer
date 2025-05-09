namespace ACViewer.Entity
{
    public class LocationType
    {
        public DatReaderWriter.Types.LocationType _locationType;

        public LocationType(DatReaderWriter.Types.LocationType locationType)
        {
            _locationType = locationType;
        }

        public override string ToString()
        {
            return $"PartID: {_locationType.PartId}, Frame: {new Frame(_locationType.Frame)}";
        }
    }
}
