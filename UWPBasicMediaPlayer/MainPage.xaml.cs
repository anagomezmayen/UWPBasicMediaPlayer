using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UWPBasicMediaPlayer.Model;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Pickers;
using Windows.Storage.Search;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

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
        private ObservableCollection<Album> Albums;
        private ObservableCollection<PlayList> PlayLists = new ObservableCollection<PlayList>();
        private List<Feature> Features;
        private Song previousSong;
        private Song currentSong;
        private string MusicFolderPath;

        public MainPage()
        {
            this.InitializeComponent();
            Songs = new ObservableCollection<Song>();
            Artists = new ObservableCollection<Artist>();
            Albums = new ObservableCollection<Album>();
            SongManager.GetAllSongs(Songs);
            SongManager.GetAllArtist(Artists);//Artists
            SongManager.GetAllAlbums(Albums);//Albums

            Features = new List<Feature>();
            Features.Add(new Feature { IconFile = "Assets/Icons/Albums.png", Item = FeatureItems.Albums });
            Features.Add(new Feature { IconFile = "Assets/Icons/Artists.png", Item = FeatureItems.Artists });
            Features.Add(new Feature { IconFile = "Assets/Icons/Playlist.png", Item = FeatureItems.Playlist });

            BackButton.Visibility = Visibility.Collapsed;
            PlayListGridView.Visibility = Visibility.Collapsed;
            ArtistsGridView.Visibility = Visibility.Collapsed;
            AlbumsGridView.Visibility = Visibility.Collapsed;

            MusicFolderPath = "";
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
            AlbumsGridView.Visibility = Visibility.Collapsed;
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
                ItemTextBlock.Text = "All Playlists";
                var allPlayLists = PlayListManager.GetAllPlayLists();
                PlayLists.Clear();
                allPlayLists.ForEach(pl => PlayLists.Add(pl));

                BackButton.Visibility = Visibility.Visible;
                PlayListGridView.Margin = new Thickness(20, 0, 0, 0);
                SongGridView.Visibility = Visibility.Collapsed;
                PlayListGridView.Visibility = Visibility.Visible;
                ArtistsGridView.Visibility = Visibility.Collapsed;
                AlbumsGridView.Visibility = Visibility.Collapsed;
            }
            else
            {

                if (Feature.Item == FeatureItems.Artists)
                {
                    SongGridView.Visibility = Visibility.Collapsed;
                    AlbumsGridView.Visibility = Visibility.Collapsed;
                    ArtistsGridView.Visibility = Visibility.Visible;
                    ItemTextBlock.Text = "All Artists";
                    PlayListGridView.Visibility = Visibility.Collapsed;
                }
                else //Albums
                {
                    ItemTextBlock.Text = Feature.Item.ToString();
                    BackButton.Visibility = Visibility.Visible;
                    SongGridView.Visibility = Visibility.Collapsed;
                    PlayListGridView.Visibility = Visibility.Collapsed;
                    ArtistsGridView.Visibility = Visibility.Collapsed;
                    AlbumsGridView.Visibility = Visibility.Visible;
                    ItemTextBlock.Text = "All Albums";

                }
            }
        }

        private void PlayListGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedPlaylist = (PlayList)e.ClickedItem;
            this.Songs.Clear();
            selectedPlaylist.Songs.ForEach(song => this.Songs.Add(song));
            PlayListGridView.Margin = new Thickness(20, 150, 0, 0);
            ItemTextBlock.Text = "All Songs on the playlist " + selectedPlaylist.Title;
            SongGridView.Visibility = Visibility.Visible;
            PlayListGridView.Visibility = Visibility.Visible;
            ArtistsGridView.Visibility = Visibility.Collapsed;
        }

        private void SongGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (Song)e.ClickedItem;
            this.PlayAndupdatePreviousAndCurrentSong(song);
            ArtistName.Text = song.Artist;
            SongName.Text = song.Title;
            ArtistsGridView.Visibility = Visibility.Collapsed;
                   }

        private void ArtistsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var artist = (Artist)e.ClickedItem;
            SongManager.GetSongsByArtist(Songs, artist.Name.Trim().ToUpper());
            ItemTextBlock.Text = "All Songs by " + artist.Name;
            PlayListGridView.Visibility = Visibility.Collapsed;
            ArtistsGridView.Visibility = Visibility.Collapsed;
            SongGridView.Visibility = Visibility.Visible;
        }


        private void StackPanel_RightTapped(object sender, Windows.UI.Xaml.Input.RightTappedRoutedEventArgs e)
        {
            var songMenuFlyoutSubItem = new MenuFlyoutSubItem()
            {
                Text = "AddToPlayList"
            };

            var icon = new BitmapIcon() { UriSource = new Uri("ms-appx:///Assets/Images/songGeneral.png") };
            
            var playLists = PlayListManager.GetAllPlayLists();
            foreach (var playlist in playLists)
            {
                var playListMenuFlyoutItem = new MenuFlyoutItem() { Text = playlist.Title };
                playListMenuFlyoutItem.Click += AddToPlaylistMenu_ItemClick;
                songMenuFlyoutSubItem.Items.Add(playListMenuFlyoutItem);
            }

            var addToPlaylistFlyout = new MenuFlyout();
            addToPlaylistFlyout.Items.Add(songMenuFlyoutSubItem);
            FrameworkElement senderElement = sender as FrameworkElement;
            addToPlaylistFlyout.ShowAt(sender as UIElement, e.GetPosition(sender as UIElement));
        }

        private void AddToPlaylistMenu_ItemClick(object sender, RoutedEventArgs e)
        {
            var flyoutItem = (MenuFlyoutItem)e.OriginalSource;

            // find playlist
            var playList = PlayListManager.GetPlayListByTitle(flyoutItem.Text);

            FrameworkElement senderElement = sender as FrameworkElement;
            var song = senderElement.DataContext as Song;

            playList.AddSong(song);
        }

        private void AlbumsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var album = (Album)e.ClickedItem;
            SongManager.GetSongsByAlbum(Songs, album.Name.Trim().ToUpper());
            ItemTextBlock.Text = "Songs Album " + album.Name;
            PlayListGridView.Visibility = Visibility.Collapsed;
            ArtistsGridView.Visibility = Visibility.Collapsed;
            SongGridView.Visibility = Visibility.Visible;
            AlbumsGridView.Visibility = Visibility.Collapsed;
        }

    }
}
