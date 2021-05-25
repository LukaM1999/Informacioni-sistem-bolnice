using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class ZahtevRepo : Repozitorijum
    {
        private const string Putanja = "../../../json/zahtevi.json";

        private static readonly Lazy<ZahtevRepo> Lazy = new(() => new ZahtevRepo());
        public static ZahtevRepo Instance => Lazy.Value;

        public ObservableCollection<Zahtev> Zahtevi { get; set; }

        public void Deserijalizacija()
        {
            Zahtevi = JsonConvert.DeserializeObject<ObservableCollection<Zahtev>>(File.ReadAllText(Putanja));
        }
        
        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(Zahtevi, Formatting.Indented));
        }

        public ZahtevRepo()
        {
            Zahtevi = new ObservableCollection<Zahtev>();
        }

        public void DodajZahtev(Zahtev noviZahtev)
        {
            Zahtevi.Add(noviZahtev);
        }

        public bool BrisiPoKomentaru(string komentar)
        {
            foreach (Zahtev pronadjen in Zahtevi)
                if (pronadjen.Komentar == komentar) return Zahtevi.Remove(pronadjen);
            return false;
        }
    }
}