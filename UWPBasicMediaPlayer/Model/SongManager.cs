using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPBasicMediaPlayer.Model
{
    public static class SongManager
    {
        public const string MusicFilesPath = "Assets/Music";

        public static void GetAllSongs(System.Collections.ObjectModel.ObservableCollection<Song> songs)
        {
            var allSongs = GetSongs();
            songs.Clear();
            allSongs.ForEach(song => songs.Add(song));
        }

        public static void GetSongsByFeature(ObservableCollection<Song> songs, FeatureItems item)
        {
            var allSongs = GetSongs();
            var filteredSongs = allSongs.Where(song => song.Item == item).ToList();
            songs.Clear();
            filteredSongs.ForEach(song => songs.Add(song)); //lambda expression
        }

        public static void GetSongsByArtist(ObservableCollection<Song> songs, string artist)
        {
            var allSongs = GetSongs();
            var filteredSongs = allSongs.Where(song => song.Artist.Trim().ToUpper().Equals(artist)).ToList();
            songs.Clear();
            filteredSongs.ForEach(song => songs.Add(song));
        }

        /**
         * This method read the name files in the carpet music and make the list of Songs
         */
        public static List<Song> GetSongs()
        {
            var songs = new List<Song>();
            string[] filePaths = Directory.GetFiles(MusicFilesPath, "*.mp3");//read only mp3 files
            foreach (var filepath in filePaths)
            {
                songs.Add(new Song(filepath));
            }
            return songs;
        }

        public static void GetAllArtist(ObservableCollection<Artist> artists)
        {
            var allSongs = GetSongs();
            artists.Clear();
            foreach (var s in allSongs)
            {
                artists.Add(new Artist { Name = s.Artist }); // We have to check that name is unique
            } 
        }

        public static Song GetSongByTitle(string title)
        {
            var allSongs = GetSongs();
            return allSongs.Where(song => song.Title == title).FirstOrDefault();
        }
    }
}
