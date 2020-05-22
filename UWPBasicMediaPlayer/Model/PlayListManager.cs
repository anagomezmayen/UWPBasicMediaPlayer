using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;

namespace UWPBasicMediaPlayer.Model
{

    // Play List
    public static class PlayListManager
    {
        private static List<PlayList> playLists = new List<PlayList>();

        static PlayListManager()
        {
            playLists = GetPlayLists(Constants.MusicFilesPath);
        }

        public static List<PlayList> GetAllPlayLists()
        {
            return PlayListManager.playLists;
        }

        public static PlayList GetPlayListByTitle(string title)
        {
            return PlayListManager.playLists.Where(playList => playList.Title == title).SingleOrDefault();
        }

        public static void SavePlayLists()
        {

        }

        public static List<PlayList> ReLoadPlayLists()
        {
            playLists = GetPlayLists(Constants.MusicFilesPath);
            return playLists;
        }

        private static List<PlayList> GetPlayLists(string musicFilesPath)
        {
            var playlists = new List<PlayList>();

            string[] linesFile = File.ReadAllLines(musicFilesPath + "/_playlists.txt");
            string titlePlaylist;
            int lineNumber = 1;
            lineNumber--;

            string line;

            List<Song> songs = null;
            if (linesFile.Length > 0)
            { //there is a playlist 
                while (lineNumber < linesFile.Length)
                {
                    if (linesFile[lineNumber].StartsWith("MP"))// a new playlist
                    {
                        titlePlaylist = linesFile[lineNumber].Substring(3);
                        songs = new List<Song>();
                        playlists.Add(new PlayList
                        {
                            Title = titlePlaylist,
                            Songs = songs
                        });
                    }
                    else
                    {
                        songs.Add(SongManager.GetSongByTitle(linesFile[lineNumber].Trim()));
                    }

                    lineNumber++;
                }
            }
            return playlists;
        }
    }
}
