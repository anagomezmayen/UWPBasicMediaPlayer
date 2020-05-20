using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using TagLib;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPBasicMediaPlayer.Model
{
    public enum FeatureItems
    {
        Albums,
        Artists,
        Favourite,
        MyMusic,
        Playlist,
    }

    /** Media element could be music or video*/
    public class Song
    {
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Title { get; set; }
        public bool IsFavorite { get; set; }
        public string SongFile { get; set; }
        public string Genre { get; set; } 
        public System.TimeSpan Duration { get; set; }
        public BitmapImage CoverImage { get; set; }
        public FeatureItems Item { get; set; }

       
        public Song(string pathToFile)
        {
            TagLib.File tagFile = TagLib.File.Create(pathToFile);
            Artist = (string)tagFile.Tag.FirstAlbumArtist;
            Album = (string)tagFile.Tag.Album;
            Title = (string)tagFile.Tag.Title;
            Genre = (string)tagFile.Tag.FirstGenre;
            if(Genre.ToUpper().Equals("GENRE") || Genre.Trim().Equals(""))
            {
                Genre = "Unknown";
            }
            Duration = (System.TimeSpan)tagFile.Properties.Duration;
            SongFile = pathToFile;
            if (tagFile.Tag.Pictures.Length >= 1)
            {
                CoverImage = GetCoverImage(tagFile.Tag.Pictures[0].Data.Data);
            }
            else
            {
                Uri uri= new Uri("ms-appx:///Assets/Images/songGeneral.png");
                CoverImage = new BitmapImage(uri);
            }
        }
        private BitmapImage GetCoverImage(byte[] pic)
        {
            using(InMemoryRandomAccessStream ms= new InMemoryRandomAccessStream())
            {
                using(DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes((byte[])pic);
                    //the getResults here forces to wait until the operation cpmpletes
                    writer.StoreAsync().GetResults();
                }
                BitmapImage image = new BitmapImage();
                image.SetSource(ms);
                return image;
            }
        }
    }
}
