using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    class BazaPodatakaOZaposlenjuiZanimanjuPacijenata : Repozitorijum
    {
        private static readonly Lazy<BazaPodatakaOZaposlenjuiZanimanjuPacijenata>
          lazy =
          new Lazy<BazaPodatakaOZaposlenjuiZanimanjuPacijenata>
              (() => new BazaPodatakaOZaposlenjuiZanimanjuPacijenata());

        public static BazaPodatakaOZaposlenjuiZanimanjuPacijenata Instance { get { return lazy.Value; } }

        public ObservableCollection<PodaciOZaposlenjuIZanimanju> bazaPodatakaOZaposlenjuIZanimanjuPacijenata
        {
            get;
            set;
        }



        public void Deserijalizacija(string putanja)
        {
            bazaPodatakaOZaposlenjuIZanimanjuPacijenata = JsonConvert.DeserializeObject<ObservableCollection<PodaciOZaposlenjuIZanimanju>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(bazaPodatakaOZaposlenjuIZanimanjuPacijenata, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }


        public BazaPodatakaOZaposlenjuiZanimanjuPacijenata()
        {
           bazaPodatakaOZaposlenjuIZanimanjuPacijenata = new ObservableCollection<PodaciOZaposlenjuIZanimanju>();
        }
    }
}
