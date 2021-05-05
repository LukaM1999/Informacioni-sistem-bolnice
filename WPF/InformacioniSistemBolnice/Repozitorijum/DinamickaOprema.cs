using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repozitorijum
{
    class DinamickaOprema:Repozitorijum
    {
        private string putanja = "../../../json/dinamickaOprema.json";

        private static readonly Lazy<DinamickaOprema>
           lazy =
           new Lazy<DinamickaOprema>
               (() => new DinamickaOprema());

        public static DinamickaOprema Instance { get { return lazy.Value; } }

        public ObservableCollection<Model.DinamickaOprema> listaOpreme
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            listaOpreme = JsonConvert.DeserializeObject<ObservableCollection<Model.DinamickaOprema>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaOpreme, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Model.DinamickaOprema getSelected(Model.DinamickaOprema p)
        {
            Model.DinamickaOprema prs = null;
            foreach (Model.DinamickaOprema pr in listaOpreme)
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
