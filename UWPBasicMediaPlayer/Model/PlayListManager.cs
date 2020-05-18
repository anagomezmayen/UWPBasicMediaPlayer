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
    public static class PlayListManager
    {
        public static void GetAllPlayLists(ObservableCollection<PlayList> playlists)
        {
            var allPlayLists = GetPlayLists();
            allPlayLists.Clear();
            allPlayLists.ForEach(pl => playlists.Add(pl));
        }

      
        public static List<PlayList>  GetPlayLists()
        {
            var playlists = new List<PlayList>();
            StreamReader file = new StreamReader("Assets/Music/_playlists.txt");
            string line, titlePlaylist;
            List<Song> songs=new List<Song>();

            while ((line = file.ReadLine()) != null)
            {
                if (line.StartsWith("MP"))// a new playlist
                {
                    titlePlaylist = line.Substring(3);
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
                else
                {
                    songs.Add(new Song(line.Trim()));
                }
                
            }

            return playlists;
        }
    }
}
