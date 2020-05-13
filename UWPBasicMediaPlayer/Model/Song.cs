using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace UWPBasicMediaPlayer.Model
{
    /** Media element could be music or video*/
    public class Song
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Title { get; set; }
        public bool IsFavorite { get; set; }
        public string SongFile { get; set; }

        public Song(string pathToFile)
        {
            TagLib.File tagFile = TagLib.File.Create(pathToFile);
            Artist = (string)tagFile.Tag.FirstAlbumArtist;
            Album = (string)tagFile.Tag.Album;
            Title = (string)tagFile.Tag.Title;
            SongFile = pathToFile;
            IsFavorite = false; //meanwhile this property has a default value

        }
    }
}
