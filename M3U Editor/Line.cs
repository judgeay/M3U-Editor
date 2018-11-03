namespace M3U_Editor
{
    public class Line : IM3uFileContent
    {
        public string[] Lines { get; }

        public Line(string line)
        {
            Lines = new[] { line };
        }
    }
}