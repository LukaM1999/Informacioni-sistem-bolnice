using System;
using Model;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class BolnickoLecenjeRepo : Repozitorijum
    {
        private const string Putanja = "../../../json/bolnickaLecenja.json";

        private static readonly Lazy<BolnickoLecenjeRepo> Lazy = new(() => new BolnickoLecenjeRepo());
        public static BolnickoLecenjeRepo Instance => Lazy.Value;

        public ObservableCollection<BolnickoLecenje> BolnickaLecenja { get; set; }

        public void Deserijalizacija()
        {
            lock (BolnickaLecenja)
                BolnickaLecenja = JsonConvert.DeserializeObject<ObservableCollection<BolnickoLecenje>>(File.ReadAllText(Putanja));
        }

        public void Serijalizacija()
        {
            lock (BolnickaLecenja)
                File.WriteAllText(Putanja, JsonConvert.SerializeObject(BolnickaLecenja, Formatting.Indented));
        }

        public BolnickoLecenjeRepo()
        {
            BolnickaLecenja = new ObservableCollection<BolnickoLecenje>();
        }

        public BolnickoLecenje NadjiLecenje(string jmbgPacijenta)
        {
            foreach (BolnickoLecenje pronadjeno in BolnickaLecenja)
                if (pronadjeno.JmbgPacijenta.Equals(jmbgPacijenta)) return pronadjeno;
            return null;
        }

        public bool DodajLecenje(BolnickoLecenje lecenje)
        {
            if (NadjiLecenje(lecenje.JmbgPacijenta) != null) return false;
            BolnickaLecenja.Add(lecenje);
            return true;
        }

        public bool ObrisiLecenje(BolnickoLecenje lecenje)
        {
            return BolnickaLecenja.Remove(NadjiLecenje(lecenje.JmbgPacijenta));
        }
    }
}