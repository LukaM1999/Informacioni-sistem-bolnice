using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class Alergeni : Repozitorijum
    {
        private string putanja = "../../../json/alergeni.json";

        private static readonly Lazy<Alergeni>
           lazy =
           new Lazy<Alergeni>
               (() => new Alergeni());

        public static Alergeni Instance { get { return lazy.Value; } }

        public ObservableCollection<Alergen> listaAlergena
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            listaAlergena = JsonConvert.DeserializeObject<ObservableCollection<Alergen>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaAlergena, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }


        public Alergeni()
        {
            listaAlergena = new ObservableCollection<Alergen>();
        }

        public Alergen NadjiPoNazivu(string naziv)
        {
            foreach (Alergen pronadjen in listaAlergena)
            {
                if (pronadjen.Naziv != naziv) continue;
                return pronadjen;
            }
            return null;
        }

        public bool DodajAlergen(Alergen alergenZaDodavanje)
        {
            if (listaAlergena.Contains(alergenZaDodavanje)) return false;
            listaAlergena.Add(alergenZaDodavanje);
            SacuvajPromene();
            return true;
        }

        public bool BrisiPoNazivu(string naziv)
        {
            foreach (Alergen pronadjen in listaAlergena)
            {
                if (pronadjen.Naziv != naziv) continue;
                return listaAlergena.Remove(pronadjen);
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