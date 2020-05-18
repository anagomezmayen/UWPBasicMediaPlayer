using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPBasicMediaPlayer.Model
{
    public class PlayList
    {
        #region Properties
        public string Title { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();
       // public List<Album> Albums { get; set; } = new List<Album>();
        public string Artist { get; set; }
        #endregion

        #region Methods
        //public PlayList(string title, string artist)
        //{
        //    this.Title = title;
        //    this.Artist = artist;
        //}

        public void AddSong(Song song)
        {
            this.Songs.Add(song);
        }
        #endregion
    }
}
