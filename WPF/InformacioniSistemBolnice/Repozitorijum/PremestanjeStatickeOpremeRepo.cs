using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using Model;

namespace Repozitorijum
{
    public class PremestanjeStatickeOpremeRepo : IRepozitorijum
    {
        private const string Putanja = "../../../json/statickaOpremaTermini.json";

        private static readonly Lazy<PremestanjeStatickeOpremeRepo> Lazy = new(() => new PremestanjeStatickeOpremeRepo());
        public static PremestanjeStatickeOpremeRepo Instance => Lazy.Value;

        public ObservableCollection<StatickaOpremaTermin> TerminiPremestanja { get; set; }

        public ObservableCollection<object> Deserijalizacija()
        {
            TerminiPremestanja = JsonConvert.DeserializeObject<ObservableCollection<StatickaOpremaTermin>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {TerminiPremestanja};
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(TerminiPremestanja, Formatting.Indented));
        }

        public PremestanjeStatickeOpremeRepo()
        {
            TerminiPremestanja = new ObservableCollection<StatickaOpremaTermin>();
        }

        public void DodajStatickuOpremu(StatickaOpremaTermin novaOprema)
        {
            TerminiPremestanja.Add(novaOprema);
        }

        public bool BrisiTermin(StatickaOpremaTermin izabranTermin)
        {
            return TerminiPremestanja.Remove(izabranTermin);
        }
    }
}
