namespace ACViewer.Entity
{
    public class PortalPoly
    {
        public DatReaderWriter.Types.PortalPoly _portalPoly;

        public PortalPoly(DatReaderWriter.Types.PortalPoly portalPoly)
        {
            _portalPoly = portalPoly;
        }

        public override string ToString()
        {
            return $"PortalIdx: {_portalPoly.PortalIndex}, PolygonId: {_portalPoly.PolygonId}";
        }
    }
}
