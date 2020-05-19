using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;

namespace UWPBasicMediaPlayer.Model
{

    // Play List
    public static class PlayListManager
    {

        public static void GetAllPlayLists(ObservableCollection<PlayList> playlists, string musicFilesPath)
        {
            var allPlayLists = GetPlayLists(musicFilesPath);
            allPlayLists.Clear();
            allPlayLists.ForEach(pl => playlists.Add(pl));
        }

      
        public static List<PlayList>  GetPlayLists(string musicFilesPath)
        {
            var playlists = new List<PlayList>();
            string[] linesFile = File.ReadAllLines(musicFilesPath+"/_playlists.txt");
            string titlePlaylist;
            int lineNumber = 1;
            lineNumber--;
            List<Song> songs=new List<Song>();
            if (linesFile.Length > 0) { //there is a playlist 
                while (lineNumber < linesFile.Length)
                {
                    if (linesFile[lineNumber].StartsWith("MP"))// a new playlist
                    {
                        titlePlaylist = linesFile[lineNumber].Substring(3);
                        if (songs.Count == 0)
                        {
                            playlists.Add(new PlayList
                            {
                                Title = titlePlaylist,
                                Songs = songs
                            });
                        }
                        songs.Clear();

                    }
                    lineNumber++;
                } }
            else
            {
                songs.Add(new Song(linesFile[lineNumber].Trim()));
            }
            return playlists;
        }
    }
}
