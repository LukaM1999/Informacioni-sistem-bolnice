using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repozitorijum
{
    public class StatickaOpremaRepo : Repozitorijum
    {
        private string putanja = "../../../json/statickaOprema.json";
        private static readonly Lazy<StatickaOpremaRepo> lazy = new Lazy<StatickaOpremaRepo> (() => new StatickaOpremaRepo());
        public static StatickaOpremaRepo Instance { get { return lazy.Value; } }
        public ObservableCollection<Model.StatickaOprema> ListaOpreme { get; set; }
        public void Deserijalizacija()
        {
            ListaOpreme = JsonConvert.DeserializeObject<ObservableCollection<Model.StatickaOprema>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
                string json = JsonConvert.SerializeObject(ListaOpreme, Formatting.Indented);
                File.WriteAllText(putanja, json);
        }

        public StatickaOpremaRepo()
        {
            ListaOpreme = new ObservableCollection<Model.StatickaOprema>();
        }

        public Model.StatickaOprema getSelected(Model.StatickaOprema p)
        {
            Model.StatickaOprema prs = null;
            foreach (Model.StatickaOprema pr in ListaOpreme)
            {
                if (pr.Tip.Equals(p.Tip))
                {
                    prs = pr;
                }
            }
            return ListaOpreme.ElementAt(ListaOpreme.IndexOf(prs));
        }
        public StatickaOprema NadjiPoTipu(TipStatickeOpreme tip)
        {
            foreach (StatickaOprema pronadjena in ListaOpreme)
            {
                if (!pronadjena.Tip.Equals(tip)) continue;
                return pronadjena;
            }
            return null;
        }
        public bool BrisiPoTipu(TipStatickeOpreme tip)
        {
            foreach (StatickaOprema pronadjena in ListaOpreme)
            {
                if (pronadjena.Tip != tip) continue;
                return ListaOpreme.Remove(pronadjena);
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