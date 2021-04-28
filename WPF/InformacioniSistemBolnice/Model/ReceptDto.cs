using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice;

namespace Model
{
    public class ReceptDto
    {
        public Pacijent Pacijent { get; set; }
        public string Id { get; set; }
        public DateTime PocetakTerapije { get; set; }
        public DateTime KrajTerapije { get; set; }
        public double MeraLeka { get; set; }
        public double RedovnostUzimanjaLeka { get; set; }

        public ReceptDto(DateTime pocetakTerpaije, DateTime krajTerapije, double meraLeka,
            double redovnostUzimanjaLeka, string id, Pacijent pacijent)
        {
            PocetakTerapije = pocetakTerpaije;
            KrajTerapije = krajTerapije;
            MeraLeka = meraLeka;
            RedovnostUzimanjaLeka = redovnostUzimanjaLeka;
            Id = id;
            Pacijent = pacijent;
        }
    }
}
