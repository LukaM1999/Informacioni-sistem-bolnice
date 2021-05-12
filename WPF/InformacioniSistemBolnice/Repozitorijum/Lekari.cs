using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class Lekari : Repozitorijum
    {
        private string putanja = "../../../json/lekari.json";

        private static readonly Lazy<Lekari>
           lazy =
           new Lazy<Lekari>
               (() => new Lekari());

        public static Lekari Instance { get { return lazy.Value; } }

        public ObservableCollection<Lekar> listaLekara
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            listaLekara = JsonConvert.DeserializeObject<ObservableCollection<Lekar>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaLekara, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Lekar NadjiPoJmbg(string jmbg)
        {
            foreach (Lekar pronadjen in listaLekara)
            {
                if (pronadjen.jmbg == jmbg) return pronadjen;
            }
            return null;
        }

        public bool BrisiPoJmbg(string jmbg)
        {
            foreach (Lekar pronadjen in listaLekara)
            {
                if (pronadjen.jmbg != jmbg) continue;
                return listaLekara.Remove(pronadjen);
            }
            return false;
        }

        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }

    }
}