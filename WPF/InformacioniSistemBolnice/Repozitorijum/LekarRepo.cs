using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class LekarRepo : IRepozitorijum
    {
        private const string Putanja = "../../../json/lekari.json";

        private static readonly Lazy<LekarRepo> Lazy = new(() => new LekarRepo());
        public static LekarRepo Instance => Lazy.Value;

        public ObservableCollection<Lekar> Lekari { get; set; }

        public ObservableCollection<object> Deserijalizacija()
        {
            Lekari = JsonConvert.DeserializeObject<ObservableCollection<Lekar>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {Lekari};
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(Lekari, Formatting.Indented));
        }

        public Lekar NadjiLekara(string jmbg)
        {
            foreach (Lekar pronadjen in Lekari)
                if (pronadjen.Jmbg == jmbg) return pronadjen;
            return null;
        }

        public Lekar NadjiLekaraIsteSpecijalizacije(Lekar lekarSpecijalista)
        {
            foreach (Lekar pronadjen in Lekari)
                if (JeIsteSpecijalizacije(lekarSpecijalista, pronadjen)) return pronadjen;
            return null;
        }

        public bool JeIsteSpecijalizacije(Lekar lekarSpecijalista, Lekar pronadjen)
        {
            return pronadjen.Jmbg != lekarSpecijalista.Jmbg &&
                   lekarSpecijalista.Specijalizacija.Naziv == pronadjen.Specijalizacija.Naziv;
        }

        public bool DodajLekara(Lekar lekarZaDodavanje)
        {
            if (NadjiLekara(lekarZaDodavanje.Jmbg) != null) return false;
            Lekari.Add(lekarZaDodavanje);
            Serijalizacija();
            return true;
        }

        public bool ObrisiLekara(Lekar lekarZaBrisanje)
        {
            return Lekari.Remove(NadjiLekara(lekarZaBrisanje.Jmbg));
        }
    }
}