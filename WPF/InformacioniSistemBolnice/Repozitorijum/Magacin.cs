using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class Magacin : Repozitorijum
    {
        private static readonly Lazy<Magacin>
           lazy =
           new Lazy<Magacin>
               (() => new Magacin());

        public static Magacin Instance { get { return lazy.Value; } }

        public ObservableCollection<Oprema> listaOpreme
        {
            get;
            set;
        }

        public void Deserijalizacija(string putanja)
        {
            listaOpreme = JsonConvert.DeserializeObject<ObservableCollection<Oprema>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(listaOpreme, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

    }
}