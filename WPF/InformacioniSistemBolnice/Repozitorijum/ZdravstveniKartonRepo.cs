using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class ZdravstveniKartonRepo : IRepozitorijum
    {
        private const string Putanja = "../../../json/kartoni.json";

        private static readonly Lazy<ZdravstveniKartonRepo> Lazy = new(() => new ZdravstveniKartonRepo());
        public static ZdravstveniKartonRepo Instance => Lazy.Value;

        public ObservableCollection<ZdravstveniKarton> ZdravstveniKartoni { get; set; }

        public ObservableCollection<object> Deserijalizacija()
        {
            ZdravstveniKartoni = JsonConvert.DeserializeObject<ObservableCollection<ZdravstveniKarton>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {ZdravstveniKartoni};
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(ZdravstveniKartoni, Formatting.Indented));
        }

        public ZdravstveniKartonRepo()
        {
            ZdravstveniKartoni = new ObservableCollection<ZdravstveniKarton>();
        }

        public bool DodajKarton(ZdravstveniKarton kartonZaDodavanje)
        {
            if (ZdravstveniKartoni.Contains(kartonZaDodavanje)) return false;
            ZdravstveniKartoni.Add(kartonZaDodavanje);
            Serijalizacija();
            return true;
        }
    }
}