using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class AlergenRepo : Repozitorijum
    {
        private const string Putanja = "../../../json/alergeni.json";

        private static readonly Lazy<AlergenRepo> Lazy = new(() => new AlergenRepo());

        public static AlergenRepo Instance => Lazy.Value;
        public ObservableCollection<Alergen> Alergeni { get; set; }

        public void Deserijalizacija()
        {
            Alergeni = JsonConvert.DeserializeObject<ObservableCollection<Alergen>>(File.ReadAllText(Putanja));
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(Alergeni, Formatting.Indented));
        }


        public AlergenRepo()
        {
            Alergeni = new ObservableCollection<Alergen>();
        }

        public Alergen NadjiPoNazivu(string naziv)
        {
            foreach (Alergen pronadjen in Alergeni)
            {
                if (pronadjen.Naziv != naziv) continue;
                return pronadjen;
            }
            return null;
        }

        public bool DodajAlergen(Alergen alergenZaDodavanje)
        {
            if (Alergeni.Contains(alergenZaDodavanje)) return false;
            Alergeni.Add(alergenZaDodavanje);
            SacuvajPromene();
            return true;
        }

        public bool BrisiPoNazivu(string naziv)
        {
            foreach (Alergen pronadjen in Alergeni)
            {
                if (pronadjen.Naziv != naziv) continue;
                return Alergeni.Remove(pronadjen);
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