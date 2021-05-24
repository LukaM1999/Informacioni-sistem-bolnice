using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class LekDto
    {
        public string Naziv { get; set; }
        public string Proizvodjac { get; set; }
        public string Sastojci { get; set; }
        public string Zamena { get; set; }
        public ObservableCollection<Alergen> Alergeni { get; set; }
        public LekDto(String naziv, String proizvodjac, String sastojci, String zamena, ObservableCollection<Alergen> alergeni)
        {
            Naziv = naziv;
            Proizvodjac = proizvodjac;
            Sastojci = sastojci;
            Zamena = zamena;
            Alergeni = alergeni;
        }
    }
}
