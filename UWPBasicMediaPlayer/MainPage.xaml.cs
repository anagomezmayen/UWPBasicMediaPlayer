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
        private ObservableCollection<Artist> Artists;
        private ObservableCollection<PlayList> PlayLists = new ObservableCollection<PlayList>();
        private List<Feature> Features;
        private Song previousSong;
        private Song currentSong;

        public MainPage()
        {
            this.InitializeComponent();
            Songs = new ObservableCollection<Song>();
            Artists = new ObservableCollection<Artist>();
            SongManager.GetAllSongs(Songs);
            SongManager.GetAllArtist(Artists);//Artists
      
            Features = new List<Feature>();
            Features.Add(new Feature { IconFile = "Assets/Icons/Albums.png", Item = FeatureItems.Albums  });
            Features.Add(new Feature { IconFile = "Assets/Icons/Artists.png", Item = FeatureItems.Artists });
            Features.Add(new Feature { IconFile = "Assets/Icons/Favourite.png", Item = FeatureItems.Favourite });
            Features.Add(new Feature { IconFile = "Assets/Icons/MyMusic.png", Item = FeatureItems.MyMusic });
            Features.Add(new Feature { IconFile = "Assets/Icons/Playlist.png", Item = FeatureItems.Playlist });

            BackButton.Visibility = Visibility.Collapsed;
            PlayListGridView.Visibility = Visibility.Collapsed;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SongManager.GetAllSongs(Songs);
            ItemTextBlock.Text = "All Songs";
            FeaturesListView.SelectedItem = null;
            SongGridView.Visibility = Visibility.Visible;
            PlayListGridView.Visibility = Visibility.Collapsed;
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
            TimeSpan currentPostion = MyMediaElement.Position;
            var newPostion = currentPostion.Add(new TimeSpan(0,0, 5));
            MyMediaElement.Position = newPostion;
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
            if (Feature.Item == FeatureItems.Playlist)
            {
                ItemTextBlock.Text = "All my playlists";
                PlayListManager.GetAllPlayLists(PlayLists, SongManager.MusicFilesPath);//added
                BackButton.Visibility = Visibility.Visible;
                PlayListGridView.Margin = new Thickness(20,0,0,0);
                SongGridView.Visibility = Visibility.Collapsed;
                PlayListGridView.Visibility = Visibility.Visible;
            }
            else { 
                ItemTextBlock.Text = Feature.Item.ToString();
                SongManager.GetSongsByFeature(Songs, Feature.Item);
                BackButton.Visibility = Visibility.Visible;
                SongGridView.Visibility = Visibility.Visible;
                PlayListGridView.Visibility = Visibility.Collapsed;
            }
        }

        private void PlayListGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedPlaylist = (PlayList)e.ClickedItem;
            this.Songs.Clear();
            selectedPlaylist.Songs.ForEach(song => this.Songs.Add(song));
            PlayListGridView.Margin = new Thickness(20, 150, 0, 0);
            SongGridView.Visibility = Visibility.Visible;
            PlayListGridView.Visibility = Visibility.Visible;
        }

        private void SongGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (Song)e.ClickedItem;
            this.PlayAndupdatePreviousAndCurrentSong(song);
            ArtistName.Text = song.Artist;
            SongName.Text = song.Title;
            
        }

        private void ArtistsView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var artist = (Artist)e.ClickedItem;
            SongManager.GetSongsByArtist(Songs, artist.Name.Trim().ToUpper());
            ItemTextBlock.Text = "All Songs by "+artist.Name;
        }

        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {

        }
    }
}
