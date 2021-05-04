using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repozitorijum
{
    public class StatickaOprema : Repozitorijum
    {
        private string putanja = "../../../json/statickaOprema.json";

        private static readonly Lazy<StatickaOprema>
           lazy =
           new Lazy<StatickaOprema>
               (() => new StatickaOprema());

        public static StatickaOprema Instance { get { return lazy.Value; } }

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

        public StatickaOprema()
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
    }
}