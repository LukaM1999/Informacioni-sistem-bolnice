using System;
using Model;
using System.IO;
using Newtonsoft.Json;

namespace RadSaDatotekama
{
    public class Termini : RadSaDatotekama
    {
        public System.Collections.Generic.List<Termin> listaTermina
        {
            get;
            set;
        }

        public void Deserijalizacija(string putanja)
        {
            listaTermina = JsonConvert.DeserializeObject<System.Collections.Generic.List<Termin>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(listaTermina, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Termini()
        {
            listaTermina = new System.Collections.Generic.List<Termin>();
        }

    }
}