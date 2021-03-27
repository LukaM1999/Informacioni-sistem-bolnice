using System;
using Model;
using Newtonsoft.Json;
using System.IO;

namespace RadSaDatotekama
{
    public class Pacijenti : RadSaDatotekama
    {
        public System.Collections.Generic.List<Pacijent> listaPacijenata
        {
            get;
            set;
        }

        public void Deserijalizacija(string putanja)
        {
            listaPacijenata = JsonConvert.DeserializeObject<System.Collections.Generic.List<Pacijent>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija(string putanja)
        {
            string json = JsonConvert.SerializeObject(listaPacijenata, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Pacijenti()
        {
            listaPacijenata = new System.Collections.Generic.List<Pacijent>();
        }
    }
}