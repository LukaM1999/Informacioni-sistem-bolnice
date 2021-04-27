using System;
using Model;
using System.IO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class Termini : Repozitorijum
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
            lock (listaTermina)
            {
                listaTermina = JsonConvert.DeserializeObject<ObservableCollection<Termin>>(File.ReadAllText(putanja));
            }
        }

        public void Serijalizacija(string putanja)
        {
            lock (listaTermina)
            {
                string json = JsonConvert.SerializeObject(listaTermina, Formatting.Indented);
                File.WriteAllText(putanja, json);
            }
        }

        public Termini()
        {
            listaTermina = new ObservableCollection<Termin>();
        }

    }
}