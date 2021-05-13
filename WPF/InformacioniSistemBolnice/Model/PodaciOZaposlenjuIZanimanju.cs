using System;

namespace Model
{
    public class PodaciOZaposlenjuIZanimanju
    {
        public string RadnoMjesto { get; set; }
        public string RegistarskiBroj { get; set; }
        public string SifraDelatnosti { get; set; }
        public string PosaoKojiObavlja { get; set; }
        public string OSIZZdrZastite { get; set; }
        public string RadPodPosebnimUslovima { get; set; }
        public string Promjene { get; set; }

        public PodaciOZaposlenjuIZanimanju(string mjestoRada, string regBroj, string sifra, string posao, string OSIZ, string posebniUslovi, string izmjene)
        {
            RadnoMjesto = mjestoRada;
            RegistarskiBroj = regBroj;
            SifraDelatnosti = sifra;
            PosaoKojiObavlja = posao;
            OSIZZdrZastite = OSIZ;
            RadPodPosebnimUslovima = posebniUslovi;
            Promjene = izmjene;

        }

    }
}