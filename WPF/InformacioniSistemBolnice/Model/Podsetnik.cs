using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Podsetnik
    {
        public string PacijentJmbg { get; set; }
        public string Sadrzaj { get; set; }
        public string Vreme { get; set; }

        public Podsetnik(string sadrzaj, string vreme, string pacijentJmbg)
        {
            Sadrzaj = sadrzaj;
            Vreme = vreme;
            PacijentJmbg = pacijentJmbg;
        }
    }
}
