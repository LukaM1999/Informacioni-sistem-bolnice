using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class AlergenRepo : IRepozitorijum, IOsnovniUpiti
    {
        private const string Putanja = "../../../json/alergeni.json";

        private static readonly Lazy<AlergenRepo> Lazy = new(() => new AlergenRepo());

        public static AlergenRepo Instance => Lazy.Value;
        public ObservableCollection<Alergen> Alergeni { get; set; }

        public ObservableCollection<object> Deserijalizacija()
        {
            Alergeni = JsonConvert.DeserializeObject<ObservableCollection<Alergen>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {Alergeni};
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(Alergeni, Formatting.Indented));
        }


        public AlergenRepo()
        {
            Alergeni = new ObservableCollection<Alergen>();
        }

        public object NadjiPoId(object id)
        {
            foreach (Alergen pronadjen in Alergeni)
            {
                if (pronadjen.Naziv != id as string) continue;
                return pronadjen;
            }
            return null;
        }

        public bool ObrisiPoId(object id)
        {
            foreach (Alergen pronadjen in Alergeni)
            {
                if (pronadjen.Naziv != id as string) continue;
                return Alergeni.Remove(pronadjen);
            }
            return false;
        }

        public bool Dodaj(object objekat)
        {
            foreach (var alergen in Alergeni)
                if (alergen.Naziv == (objekat as Alergen).Naziv) return false;
            Alergeni.Add(objekat as Alergen);
            Serijalizacija();
            return true;
        }
    }
}