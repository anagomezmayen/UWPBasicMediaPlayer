using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPBasicMediaPlayer.Model
{
    public static class SongManager
    {
        public static void GetAllSongs(System.Collections.ObjectModel.ObservableCollection<Song> songs)
        {
            var allSongs = GetSongs();
            songs.Clear();
            allSongs.ForEach(song => songs.Add(song));

        }
        /**
         * This method read the name files in the carpet music and make the list of Songs
         */
        public static List<Song> GetSongs()
        {
            var songs = new List<Song>();
            string[] filePaths = Directory.GetFiles($"/Assets/music");
            foreach (var filepath in filePaths)
            {
                songs.Add(new Song(filepath));
            }
            return songs;
        }

    }
}
