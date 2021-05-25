using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using InformacioniSistemBolnice;

namespace Repozitorijum
{
    public class SpecijalizacijaRepo : Repozitorijum
    {
        private const string Putanja = "../../../json/specijalizacije.json";

        private static readonly Lazy<SpecijalizacijaRepo> Lazy = new(() => new SpecijalizacijaRepo());
        public static SpecijalizacijaRepo Instance => Lazy.Value;

        public ObservableCollection<Specijalizacija> Specijalizacije { get; set; }

        public void Deserijalizacija()
        {
            Specijalizacije = JsonConvert.DeserializeObject<ObservableCollection<Specijalizacija>>(File.ReadAllText(Putanja));
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(Specijalizacije, Formatting.Indented));
        }

        public SpecijalizacijaRepo()
        {
            Specijalizacije = new ObservableCollection<Specijalizacija>();
        }
    }
}
