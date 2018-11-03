using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace M3U_Editor
{
    internal class M3uFile : ObservableObject
    {
        public const string EXTINF = "#EXTINF";
        private Group _selectedGroup;

        public List<IM3uFileContent> Lines { get; }

        public ObservableCollection<Group> Groups { get; }
        public Group SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;
                RaisePropertyChanged();
            }
        }

        public M3uFile(string fileName)
        {
            Lines = new List<IM3uFileContent>();
            Groups = new ObservableCollection<Group>();

            var lines = File.ReadAllLines(fileName);

            UpdateM3uFile(lines);
        }

        internal void DeleteChannels(IEnumerable<Channel> selectedChannels)
        {
            var channelsDictionary = selectedChannels.ToDictionary(x => x, x => x);

            foreach (var line in Lines.OfType<Channel>().ToArray())
            {
                if (channelsDictionary.ContainsKey(line)) Lines.Remove(line);
            }

            var selectedGroup = SelectedGroup;
            UpdateM3uFile(Lines.SelectMany(x => x.Lines).ToArray());
            SelectedGroup = Groups.FirstOrDefault(x => x.Name == selectedGroup.Name);
        }

        internal void DeleteGroups(IEnumerable<Group> selectedGroups)
        {
            var groupsDictionary = selectedGroups.ToDictionary(x => x.Name, x => x);

            foreach(var line in Lines.OfType<Channel>().ToArray())
            {
                if (groupsDictionary.ContainsKey(line.GroupTitle)) Lines.Remove(line);
            }

            UpdateM3uFile(Lines.SelectMany(x => x.Lines).ToArray());
        }

        private void UpdateM3uFile(string[] lines)
        {
            Lines.Clear();
            Groups.Clear();

            for (var i = 0; i < lines.Length; ++i)
            {
                if (lines[i].IndexOf(EXTINF) == 0) Lines.Add(new Channel(new[] { lines[i++], lines[i] }));
                else Lines.Add(new Line(lines[i]));
            }

            var groupedChannels = Lines.OfType<Channel>().GroupBy(x => x.GroupTitle);

            foreach (var channels in groupedChannels)
            {
                Groups.Add(new Group(channels.Key, new ObservableCollection<Channel>(channels)));
            }
        }
    }
}