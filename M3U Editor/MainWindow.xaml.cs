using System.Linq;
using System.Windows;

namespace M3U_Editor
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = new MainWindowViewModel();

            InitializeComponent();
        }

        private void DeleteGroupsMenuItemClick(object sender, RoutedEventArgs e)
        {
            var viewModel = ((MainWindowViewModel)DataContext);
            if (viewModel.M3uFile == null) return;
            if (GroupsListBox.SelectedItems.Count == 0) return;

            viewModel.M3uFile.DeleteGroups(GroupsListBox.SelectedItems.OfType<Group>());
        }

        private void DeleteChannelsMenuItemClick(object sender, RoutedEventArgs e)
        {
            var viewModel = ((MainWindowViewModel)DataContext);
            if (viewModel.M3uFile == null) return;
            if (ChannelsListBox.SelectedItems.Count == 0) return;

            viewModel.M3uFile.DeleteChannels(ChannelsListBox.SelectedItems.OfType<Channel>());
        }
    }
}
