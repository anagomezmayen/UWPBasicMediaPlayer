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
        public int SongsNumber { get; set; } = 0;

        public Artist Artist { get; set; }

        public bool Equals(Album album)
        {
            return (Name.Equals(album.Name) && Artist.Name==album.Artist.Name);
        }

        #endregion

        #region Method
        public Album(string name, Artist artist)
        {
           this.Name = name;
           this.Artist = artist;
        }

        public void AddSong()
        {
            this.SongsNumber++;
        }

        #endregion
    }
}
