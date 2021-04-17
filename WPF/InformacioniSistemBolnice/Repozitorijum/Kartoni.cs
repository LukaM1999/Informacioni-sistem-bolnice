using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class Kartoni : Repozitorijum
    {
        private static readonly Lazy<Kartoni>
           lazy =
           new Lazy<Kartoni>
               (() => new Kartoni());

        public static Kartoni Instance { get { return lazy.Value; } }

        public ObservableCollection<ZdravstveniKarton> listaKartona
        {
            get;
            set;
        }

        public void Deserijalizacija(string putanja)
        {
            listaKartona = JsonConvert.DeserializeObject<ObservableCollection<ZdravstveniKarton>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(listaKartona, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

    }
}