using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class Kartoni : Repozitorijum
    {
        private string putanja = "../../../json/kartoni.json";

        private static readonly Lazy<Kartoni>
           lazy =
           new Lazy<Kartoni>
               (() => new Kartoni());

        public static Kartoni Instance { get { return lazy.Value; } }

        public ObservableCollection<ZdravstveniKarton> listaKartona
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            listaKartona = JsonConvert.DeserializeObject<ObservableCollection<ZdravstveniKarton>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaKartona, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Kartoni()
        {
            listaKartona = new ObservableCollection<ZdravstveniKarton>();
        }
        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }

        public bool DodajKarton(ZdravstveniKarton kartonZaDodavanje)
        {
            if (listaKartona.Contains(kartonZaDodavanje)) return false;
            listaKartona.Add(kartonZaDodavanje);
            SacuvajPromene();
            return true;
        }
    }
}