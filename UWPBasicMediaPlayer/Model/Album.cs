using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPBasicMediaPlayer.Model
{
   public class  Album
    {
        #region Properties
        public string Name { get; set; }

        public bool Equals(Album album)
        {
            return Name.Equals(album.Name);
        }
       // public List<Song> Songs { get; private set; } = new List<Song>();
       // public string  Artist { get; set; }
        #endregion

        #region Method
        //public Album(string name, string artist)
        ////{
            //this.Name = name;
         //   this.Artist = artist;
        //}

        //public void AddSong(Song song)
        //{
        //    this.Songs.Add(song);
        //}

        #endregion
    }
}
