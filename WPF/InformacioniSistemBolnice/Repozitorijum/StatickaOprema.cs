using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class StatickaOprema : Repozitorijum
    {
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

        public void Deserijalizacija(string putanja)
        {
            listaOpreme = JsonConvert.DeserializeObject<ObservableCollection<Model.StatickaOprema>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(listaOpreme, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

    }
}