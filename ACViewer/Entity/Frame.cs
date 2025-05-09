namespace ACViewer.Entity
{
    public class Frame
    {
        public DatReaderWriter.Types.Frame _frame;

        public Frame(DatReaderWriter.Types.Frame frame)
        {
            _frame = frame;
        }

        public override string ToString()
        {
            return $"{_frame.Origin} - {_frame.Orientation}";
        }
    }
}
