using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using Model;

namespace Repozitorijum
{
    public class Vesti : Repozitorijum
    {
        private string putanja = "../../../json/vesti.json";

        private static readonly Lazy<Vesti>
               lazy =
               new Lazy<Vesti>
                   (() => new Vesti());

        public static Vesti Instance { get { return lazy.Value; } }

        public ObservableCollection<Vest> listaVesti
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            listaVesti = JsonConvert.DeserializeObject<ObservableCollection<Vest>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaVesti, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }
        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }
        public bool DodajVest(Vest vestZaDodavanje)
        {
            if (listaVesti.Contains(vestZaDodavanje)) return false;
            listaVesti.Add(vestZaDodavanje);
            SacuvajPromene();
            return true;
        }

        public bool ObrisiVest(Vest vestZaBrisanje)
        {
            return listaVesti.Remove(NadjiPoId(vestZaBrisanje.Id));
        }

        public Vest NadjiPoId(string Id)
        {
            foreach (Vest pronadjena in listaVesti)
                if (pronadjena.Id == Id) return pronadjena;
            return null;
        }
    }
}