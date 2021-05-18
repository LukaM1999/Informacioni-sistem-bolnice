using System;
using Model;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class Termini : Repozitorijum
    {
        private string putanja = "../../../json/zakazaniTermini.json";

        private static readonly Lazy<Termini>
            lazy =
            new Lazy<Termini>
                (() => new Termini());

        public static Termini Instance { get { return lazy.Value; } }

        public ObservableCollection<Termin> listaTermina
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            lock (listaTermina)
            {
                listaTermina = JsonConvert.DeserializeObject<ObservableCollection<Termin>>(File.ReadAllText(putanja));
            }
        }

        public void Serijalizacija()
        {
            lock (listaTermina)
            {
                string json = JsonConvert.SerializeObject(listaTermina, Formatting.Indented);
                File.WriteAllText(putanja, json);
            }
        }

        public Termini()
        {
            listaTermina = new ObservableCollection<Termin>();
        }

        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }

        public Termin NadjiTermin(DateTime vreme, string jmbgPacijenta, string jmbgLekara)
        {
            foreach (Termin pronadjen in listaTermina)
                if (JePronadjen(vreme, jmbgPacijenta, jmbgLekara, pronadjen)) return pronadjen;
            return null;
        }
        public bool JePronadjen(DateTime vreme, string jmbgPacijenta, string jmbgLekara, Termin pronadjen)
        {
            return pronadjen.vreme == vreme && pronadjen.lekarJMBG == jmbgLekara &&
                   pronadjen.pacijentJMBG == jmbgPacijenta;
        }

        public bool DodajTermin(Termin terminZaDodavanje)
        {
            if (listaTermina.Contains(terminZaDodavanje)) return false;
            listaTermina.Add(terminZaDodavanje);
            System.Diagnostics.Debug.WriteLine("dodao sam termin");
            return true;
        }

        /*public bool ObrisiTermin(DateTime vreme, string jmbgPacijenta, string jmbgLekara)
        {
            foreach (Termin pronadjen in listaTermina)
                if (JePronadjen(vreme, jmbgPacijenta, jmbgLekara, pronadjen)) return listaTermina.Remove(pronadjen);
            return false;
        }*/

        public bool ObrisiTermin(Termin terminZaBrisanje)
        {
            return listaTermina.Remove(terminZaBrisanje);
        }

    }
}