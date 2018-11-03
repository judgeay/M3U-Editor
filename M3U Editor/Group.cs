using System.Collections.ObjectModel;

namespace M3U_Editor
{
    public class Group
    {
        public string Name { get; }
        public ObservableCollection<Channel> Channels { get; }

        public Group(string name, ObservableCollection<Channel> channels)
        {
            Name = name;
            Channels = channels;
        }
    }
}
