using System;
using Model;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class BolnickoLecenjeRepo : IRepozitorijum, IOsnovniUpiti
    {
        private const string Putanja = "../../../json/bolnickaLecenja.json";

        private static readonly Lazy<BolnickoLecenjeRepo> Lazy = new(() => new BolnickoLecenjeRepo());
        public static BolnickoLecenjeRepo Instance => Lazy.Value;

        public ObservableCollection<BolnickoLecenje> BolnickaLecenja { get; set; }

        public ObservableCollection<object> Deserijalizacija()
        {
            lock (BolnickaLecenja)
                BolnickaLecenja = JsonConvert.DeserializeObject<ObservableCollection<BolnickoLecenje>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {BolnickaLecenja};
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

        public object NadjiPoId(object id)
        {
            foreach (BolnickoLecenje pronadjeno in BolnickaLecenja)
                if (pronadjeno.JmbgPacijenta.Equals(id as string)) return pronadjeno;
            return null;
        }

        public bool ObrisiPoId(object lecenje)
        {
            return BolnickaLecenja.Remove(NadjiPoId((lecenje as BolnickoLecenje).JmbgPacijenta) as BolnickoLecenje);
        }

        public bool Dodaj(object lecenje)
        {
            if (NadjiPoId((lecenje as BolnickoLecenje).JmbgPacijenta) != null) return false;
            BolnickaLecenja.Add(lecenje as BolnickoLecenje);
            return true;
        }
    }
}