using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPBasicMediaPlayer.Model
{
    public class Artist
    {
        public string Name { get; set; }

        public bool Equals(Artist artist)
        {
            return Name.Equals(artist.Name);

        }
    }

}
