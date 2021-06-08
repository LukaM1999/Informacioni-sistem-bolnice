using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repozitorijum
{
    public class DinamickaOpremaRepo:IRepozitorijum, IOsnovniUpiti
    {
        private const string Putanja = "../../../json/dinamickaOprema.json";

        private static readonly Lazy<DinamickaOpremaRepo> Lazy = new(() => new DinamickaOpremaRepo());
        public static DinamickaOpremaRepo Instance => Lazy.Value;

        public ObservableCollection<DinamickaOprema> DinamickaOprema {get; set;}

        public ObservableCollection<object> Deserijalizacija()
        {
            DinamickaOprema = JsonConvert.DeserializeObject<ObservableCollection<DinamickaOprema>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {DinamickaOprema};
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(DinamickaOprema, Formatting.Indented));
        }

        public object NadjiPoId(object id)
        {
            foreach (DinamickaOprema pronadjena in DinamickaOprema)
            {
                if (!pronadjena.Tip.Equals(id as TipDinamickeOpreme?)) continue;
                return pronadjena;
            }
            return null;
        }

        public bool ObrisiPoId(object id)
        {
            foreach (DinamickaOprema pronadjena in DinamickaOprema)
            {
                if (pronadjena.Tip != id as TipDinamickeOpreme?) continue;
                return DinamickaOprema.Remove(pronadjena);
            }
            return false;
        }

        public bool Dodaj(object objekat)
        {
            if(NadjiPoId((objekat as DinamickaOprema).Tip) != null) return false;
            DinamickaOprema.Add(objekat as DinamickaOprema);
            Serijalizacija();
            return true;
        }
    }
}
