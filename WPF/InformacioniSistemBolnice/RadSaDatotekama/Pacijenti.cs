using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace RadSaDatotekama
{
    public sealed class Pacijenti : RadSaDatotekama
    {
        private static readonly Lazy<Pacijenti>
           lazy =
           new Lazy<Pacijenti>
               (() => new Pacijenti());

        public static Pacijenti Instance { get { return lazy.Value; } }
        public ObservableCollection<Pacijent> listaPacijenata
        {
            get;
            set;
        }

        public void Deserijalizacija(string putanja)
        {
            listaPacijenata = JsonConvert.DeserializeObject < ObservableCollection<Pacijent>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(listaPacijenata, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Pacijenti()
        {
            listaPacijenata = new ObservableCollection<Pacijent>();
        }
    }
}