using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Navigation;
using Newtonsoft.Json;
using Model;

namespace Repozitorijum
{
    public class LekRepo : IRepozitorijum, IOsnovniUpiti
    {
        private const string Putanja = "../../../json/lekovi.json";

        private static readonly Lazy<LekRepo> Lazy = new(() => new LekRepo());
        public static LekRepo Instance => Lazy.Value;

        public ObservableCollection<Lek> Lekovi { get; set; }

        public ObservableCollection<object> Deserijalizacija()
        {
            Lekovi = JsonConvert.DeserializeObject<ObservableCollection<Lek>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {Lekovi};
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(Lekovi, Formatting.Indented));
        }

        public object NadjiPoId(object id)
        {
            foreach (Lek pronadjen in Lekovi)
                if (pronadjen.Naziv == id as string) return pronadjen;
            return null;
        }

        public bool ObrisiPoId(object id)
        {
            foreach (Lek pronadjen in Lekovi)
            {
                if (pronadjen.Naziv != id as string) continue;
                return Lekovi.Remove(pronadjen);
            }
            return false;
        }

        public bool Dodaj(object noviLek)
        {
            foreach (var lek in Lekovi)
                if (NadjiPoId((noviLek as Lek).Naziv).ToString() == lek.Naziv) return false;
            Lekovi.Add(noviLek as Lek);
            Serijalizacija();
            return true;
        }
    }
}
