using System;

namespace Model
{
    public class PodaciOZaposlenjuIZanimanju
    {
      
        private string radnoMjesto;
        private string registarskiBroj;
        private string sifraDelatnosti;
        private string posaoKojiObavlja;
        private string oSIZZdrZastite;
        private string radPodPosebnimUslovima;
        private string promjene;

        public PodaciOZaposlenjuIZanimanju()
        {
           
        }

        public PodaciOZaposlenjuIZanimanju(string mjestoRada, string regBroj, string sifra, string posao, string oSIZ, string posebniUslovi, string izmjene)
        {
            radnoMjesto = mjestoRada;
            registarskiBroj = regBroj;
            sifraDelatnosti = sifra;
            posaoKojiObavlja = posao;
            oSIZZdrZastite = oSIZ;
            radPodPosebnimUslovima = posebniUslovi;
            promjene = izmjene;

        }

        public string MestoRada { get => radnoMjesto; set => radnoMjesto = value; }
        public string RegBroj { get => registarskiBroj; set => registarskiBroj = value; }
        public string SifraDelatnosti { get => sifraDelatnosti; set => sifraDelatnosti = value; }
        public string PosaoKojiObavlja { get => posaoKojiObavlja; set => posaoKojiObavlja = value; }
        public string OSIZZdrZastite { get => oSIZZdrZastite; set => oSIZZdrZastite = value; }
        public string RadPodPosebnimUslovima { get => radPodPosebnimUslovima; set => radPodPosebnimUslovima = value; }
        public string Promene { get => promjene; set => promjene = value; }
    }
}