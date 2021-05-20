using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repozitorijum
{
    public class DinamickaOpremaRepo:Repozitorijum
    {
        private string putanja = "../../../json/dinamickaOprema.json";

        private static readonly Lazy<DinamickaOpremaRepo>
           lazy =
           new Lazy<DinamickaOpremaRepo>
               (() => new DinamickaOpremaRepo());

        public static DinamickaOpremaRepo Instance { get { return lazy.Value; } }

        public ObservableCollection<DinamickaOprema> listaOpreme
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            listaOpreme = JsonConvert.DeserializeObject<ObservableCollection<DinamickaOprema>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaOpreme, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public DinamickaOprema getSelected(DinamickaOprema p)
        {
            Model.DinamickaOprema prs = null;
            foreach (DinamickaOprema pr in listaOpreme)
            {
                if (pr.tip.Equals(p.tip))
                {
                    prs = pr;
                }
            }
            return listaOpreme.ElementAt(listaOpreme.IndexOf(prs));
        }

        public DinamickaOprema NadjiPoTipu(TipDinamickeOpreme tip)
        {
            foreach (DinamickaOprema pronadjena in listaOpreme)
            {
                if (!pronadjena.tip.Equals(tip)) continue;
                return pronadjena;
            }
            return null;
        }

        public bool BrisiPoTipu(TipDinamickeOpreme tip)
        {
            foreach (DinamickaOprema pronadjena in listaOpreme)
            {
                if (pronadjena.tip != tip) continue;
                return listaOpreme.Remove(pronadjena);
            }
            return false;
        }

        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }
    }
}
