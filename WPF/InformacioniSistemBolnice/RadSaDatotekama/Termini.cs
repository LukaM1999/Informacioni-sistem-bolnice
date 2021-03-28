using System;
using Model;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace RadSaDatotekama
{
    public class Termini : RadSaDatotekama
    {
        private static readonly Lazy<Termini>
            lazy =
            new Lazy<Termini>
                (() => new Termini());

        public static Termini Instance { get { return lazy.Value; } }

        public ObservableCollection<Termin> listaTermina
        {
            get;
            set;
        }

        public void Deserijalizacija(string putanja)
        {
            listaTermina = JsonConvert.DeserializeObject<ObservableCollection<Termin>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(listaTermina, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Termini()
        {
            listaTermina = new ObservableCollection<Termin>();
        }

    }
}