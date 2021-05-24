using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ZahtevDto
    {
        public string Komentar { get; set; }
        public string Potpis { get; set; }

        public ZahtevDto(string komentar, string potpis)
        {
            Komentar = komentar;
            Potpis = potpis;
        }
    }
}
