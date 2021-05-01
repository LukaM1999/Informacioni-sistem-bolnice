using System;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;
using Model;

namespace Repozitorijum
{
    class StatickaOpremaTermini
    {
        private string putanja = "../../../json/statickaOpremaTermini.json";

        private static readonly Lazy<StatickaOpremaTermini>
           lazy =
           new Lazy<StatickaOpremaTermini>
               (() => new StatickaOpremaTermini());

        public static StatickaOpremaTermini Instance { get { return lazy.Value; } }

        public ObservableCollection<StatickaOpremaTermin> listaTermina
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            //lock (listaTermina)
            //{
                listaTermina = JsonConvert.DeserializeObject<ObservableCollection<StatickaOpremaTermin>>(File.ReadAllText(putanja));
            //}
        }

        public void Serijalizacija()
        {
            //lock (listaTermina)
            //{
                string json = JsonConvert.SerializeObject(listaTermina, Formatting.Indented);
                File.WriteAllText(putanja, json);
            //}
        }

        public StatickaOpremaTermini()
        {
            listaTermina = new ObservableCollection<StatickaOpremaTermin>();            
        }
    }
}
