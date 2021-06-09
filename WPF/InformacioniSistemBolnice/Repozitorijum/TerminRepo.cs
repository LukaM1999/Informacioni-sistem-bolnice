using System;
using Model;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class TerminRepo : IRepozitorijum
    {
        private const string Putanja = "../../../json/zakazaniTermini.json";

        private static readonly Lazy<TerminRepo> Lazy = new(() => new TerminRepo());
        public static TerminRepo Instance => Lazy.Value;

        public ObservableCollection<Termin> Termini { get; set; }

        public ObservableCollection<object> Deserijalizacija()
        {
            lock (Termini)
                Termini = JsonConvert.DeserializeObject<ObservableCollection<Termin>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {Termini};
        }

        public void Serijalizacija()
        {
            lock (Termini)
                File.WriteAllText(Putanja, JsonConvert.SerializeObject(Termini, Formatting.Indented));
        }

        public TerminRepo()
        {
            Termini = new ObservableCollection<Termin>();
        }

        public Termin NadjiTermin(DateTime vreme, string jmbgPacijenta, string jmbgLekara)
        {
            foreach (Termin pronadjen in Termini)
                if (JePronadjen(vreme, jmbgPacijenta, jmbgLekara, pronadjen)) return pronadjen;
            return null;
        }

        public bool JePronadjen(DateTime vreme, string jmbgPacijenta, string jmbgLekara, Termin pronadjen)
        {
            return pronadjen.Vreme == vreme && pronadjen.LekarJmbg == jmbgLekara &&
                   pronadjen.PacijentJmbg == jmbgPacijenta;
        }

        public bool DodajTermin(Termin terminZaDodavanje)
        {
            if (NadjiTermin(terminZaDodavanje.Vreme, terminZaDodavanje.PacijentJmbg, 
                terminZaDodavanje.LekarJmbg) != null) return false;
            Termini.Add(terminZaDodavanje);
            return true;
        }

        public bool ObrisiTermin(Termin terminZaBrisanje)
        {
            return Termini.Remove(NadjiTermin(terminZaBrisanje.Vreme, 
                terminZaBrisanje.PacijentJmbg, terminZaBrisanje.LekarJmbg));
        }

        public void BrisiProstorijuIzSvihTermina(string idProstorije)
        {
            foreach (Termin termin in Termini)
            {
                if (termin.ProstorijaId != idProstorije) continue;
                termin.ProstorijaId = null;
                Serijalizacija();
            }
        }
    }
}