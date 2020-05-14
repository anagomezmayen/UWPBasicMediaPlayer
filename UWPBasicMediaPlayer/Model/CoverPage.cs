using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPBasicMediaPlayer.Model
{
    class CoverPage
    {
        public List<CoverPage> coverPages { get; set; } = new List<CoverPage>();

        public void AddCoverPage(CoverPage coverPage)
        {
            this.coverPages.Add(coverPage);
        }
    }
}
