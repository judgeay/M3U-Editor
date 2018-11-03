using System;

namespace M3U_Editor
{
    public class Channel : IM3uFileContent
    {
        public const string NAME = "tvg-name";
        public const string GROUP_TITLE = "group-title";

        public string[] Lines { get; }

        public string Name { get; private set; }
        public string GroupTitle { get; private set; }

        public Channel(string[] lines)
        {
            Lines = lines;
            ReadAttribute(lines[0], GetAttribute(NAME), value => Name = value);
            ReadAttribute(lines[0], GetAttribute(GROUP_TITLE), value => GroupTitle = value);
        }

        private static string GetAttribute(string attributeName) => $"{attributeName}=\"";

        private static void ReadAttribute(string line, string attribute, Action<string> writeValue)
        {
            var indexOfAttribute = line.IndexOf(attribute);
            if (indexOfAttribute != -1)
            {
                int startIndex = indexOfAttribute + attribute.Length;
                writeValue(line.Substring(startIndex, line.IndexOf("\"", startIndex) - startIndex));
            }
        }
    }
}