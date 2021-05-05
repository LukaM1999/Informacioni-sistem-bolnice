using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class GostujuciNalogDto
    {
        public string ime { get; set; }


        public string prezime { get; set; }


        public string jmbg { get; set; }


        public DateTime datumRodjenja { get; set; }


        public string telefon { get; set; }


        public string email { get; set; }


        public GostujuciNalogDto(string ime, string prezime, string jmbg, DateTime datumRodjenja, string telefon, string email)
        {
            this.ime = ime;
            this.prezime = prezime;
            this.jmbg = jmbg;
            this.datumRodjenja = datumRodjenja;
            this.telefon = telefon;
            this.email = email;
        }
    }
}
