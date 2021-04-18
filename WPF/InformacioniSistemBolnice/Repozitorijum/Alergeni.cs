using System;
using Model;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace Repozitorijum
{
    public class Alergeni : Repozitorijum
    {
        private static readonly Lazy<Alergeni>
           lazy =
           new Lazy<Alergeni>
               (() => new Alergeni());

        public static Alergeni Instance { get { return lazy.Value; } }

        public ObservableCollection<Alergen> listaAlergena
        {
            get;
            set;
        }

        public void Deserijalizacija(string putanja)
        {
            listaAlergena = JsonConvert.DeserializeObject<ObservableCollection<Alergen>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(listaAlergena, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }


        public Alergeni()
        {
            listaAlergena = new ObservableCollection<Alergen>();
        }



    }
}