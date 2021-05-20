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

        private static readonly Lazy<StatickaOpremaRepo>
           lazy =
           new Lazy<StatickaOpremaRepo>
               (() => new StatickaOpremaRepo());

        public static StatickaOpremaRepo Instance { get { return lazy.Value; } }

        public ObservableCollection<Model.StatickaOprema> listaOpreme
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            //lock (listaOpreme)
            //{
                listaOpreme = JsonConvert.DeserializeObject<ObservableCollection<Model.StatickaOprema>>(File.ReadAllText(putanja));
            //}
        }

        public void Serijalizacija()
        {
            //lock (listaOpreme)
            //{
                string json = JsonConvert.SerializeObject(listaOpreme, Formatting.Indented);
                File.WriteAllText(putanja, json);
            //}
        }

        public StatickaOpremaRepo()
        {
            listaOpreme = new ObservableCollection<Model.StatickaOprema>();
        }

        public Model.StatickaOprema getSelected(Model.StatickaOprema p)
        {
            Model.StatickaOprema prs = null;
            foreach (Model.StatickaOprema pr in listaOpreme)
            {
                if (pr.tip.Equals(p.tip))
                {
                    prs = pr;
                }
            }
            return listaOpreme.ElementAt(listaOpreme.IndexOf(prs));
        }
        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }
    }
}