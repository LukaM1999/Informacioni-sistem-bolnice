using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class HitnoZakazivanjeDto
    {
        public string specijalizacija { get; set; }
        
        public string jmbgPacijenta { get; set; }
       
        public string idProstorije { get; set; }

        public HitnoZakazivanjeDto(string specijalizacija, string jmbgPacijenta, string idProstorije)
        {
            this.specijalizacija = specijalizacija;
            this.jmbgPacijenta = jmbgPacijenta;
            this.idProstorije = idProstorije;
        }
    }
}

