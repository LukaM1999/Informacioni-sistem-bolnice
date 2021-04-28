using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{

    public sealed class Pacijenti : Repozitorijum
    {

        private string putanja = "../../../json/pacijenti.json";

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

        public void Deserijalizacija()
        {
            lock (listaPacijenata)
            {
                listaPacijenata = JsonConvert.DeserializeObject<ObservableCollection<Pacijent>>(File.ReadAllText(putanja));
            }
        }

        public void Serijalizacija()
        {
            lock (listaPacijenata)
            {
                string json = JsonConvert.SerializeObject(listaPacijenata, Formatting.Indented);
                File.WriteAllText(putanja, json);
            }
        }

        public Pacijenti()
        {
            listaPacijenata = new ObservableCollection<Pacijent>();
        }
    }
}