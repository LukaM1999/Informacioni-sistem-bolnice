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
    public class LekRepo : IRepozitorijum
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

        public Lek NadjiPoNazivu(string naziv)
        {
            foreach (Lek pronadjen in Lekovi)
                if (pronadjen.Naziv == naziv) return pronadjen;
            return null;
        }

        public bool BrisiPoNazivu(string naziv)
        {
            foreach (Lek pronadjen in Lekovi)
            {
                if (pronadjen.Naziv != naziv) continue;
                return Lekovi.Remove(pronadjen);
            }
            return false;
        }

        public void DodajLek(Lek noviLek)
        {
            Lekovi.Add(noviLek);
            Serijalizacija();
        }
    }
}
