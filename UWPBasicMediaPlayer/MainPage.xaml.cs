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
        private Song previousSong;
        private Song currentSong;

        public MainPage()
        {
            this.InitializeComponent();
            Songs = new ObservableCollection<Song>();
            SongManager.GetAllSongs(Songs);
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {

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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SongGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var song = (Song)e.ClickedItem;

            // update current song and previous song
            this.updatePreviousAndCurrentSong(song);

            MyMediaElement.Source = new Uri(BaseUri, this.currentSong.SongFile);
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            this.updatePreviousAndCurrentSong(this.previousSong);
            if (this.currentSong != null)
            {
                MyMediaElement.Source = new Uri(BaseUri, this.currentSong.SongFile);
            }
        }

        private void updatePreviousAndCurrentSong(Song currentSong)
        {
            this.previousSong = this.currentSong;
            this.currentSong = currentSong;
        }
    }
}
