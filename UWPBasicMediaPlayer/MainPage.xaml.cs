using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPBasicMediaPlayer.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPBasicMediaPlayer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Song> Songs;
        private List<Feature> Features;
        private Song previousSong;
        private Song currentSong;

        public MainPage()
        {
            this.InitializeComponent();
            Songs = new ObservableCollection<Song>();
            SongManager.GetAllSongs(Songs);
            Features = new List<Feature>();
            Features.Add(new Feature { IconFile = "Assets/Icons/animals.png", Category = SongCategory.Classical });
            Features.Add(new Feature { IconFile = "Assets/Icons/cartoon.png", Category = SongCategory.Pop });
            Features.Add(new Feature { IconFile = "Assets/Icons/taunt.png", Category = SongCategory.Jazz });
            Features.Add(new Feature { IconFile = "Assets/Icons/warning.png", Category = SongCategory.Romantic });

            BackButton.Visibility = Visibility.Collapsed;

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SongManager.GetAllSongs(Songs);
            CategoryTextBlock.Text = "All Sounds";
            FeaturesListView.SelectedItem = null;
            BackButton.Visibility = Visibility.Collapsed;
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            MyMediaElement.Pause();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            MyMediaElement.Play();
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SongGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (Song)e.ClickedItem;
            this.PlayAndupdatePreviousAndCurrentSong(song);
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            this.PlayAndupdatePreviousAndCurrentSong(this.previousSong);
            // Update the selected item in the UI.
        }

        private void PlayAndupdatePreviousAndCurrentSong(Song currentSong)
        {
            this.previousSong = this.currentSong;
            this.currentSong = currentSong;
            if (this.currentSong != null)
            {
                MyMediaElement.Source = new Uri(BaseUri, this.currentSong.SongFile);
            }
        }

        private void FeaturesListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var Feature = (Feature)e.ClickedItem;
            CategoryTextBlock.Text = Feature.Category.ToString();
            SongManager.GetSongsByCategory(Songs, Feature.Category);
            BackButton.Visibility = Visibility.Visible;
        }
    }
}
