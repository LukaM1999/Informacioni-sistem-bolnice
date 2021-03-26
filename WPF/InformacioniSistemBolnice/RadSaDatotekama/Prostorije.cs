using System;
using Model;
using Newtonsoft.Json;
using System.IO;

namespace RadSaDatotekama
{
   public class Prostorije : RadSaDatotekama
   {
      public System.Collections.Generic.List<Prostorija> listaProstorija
      {
            get;
            set;
      }
      
      public void Deserijalizacija(string putanja)
      {
           listaProstorija =  JsonConvert.DeserializeObject<System.Collections.Generic.List<Prostorija>>(File.ReadAllText(putanja));
      }
      
      public void Serijalizacija(string putanja)
      {
            string json = JsonConvert.SerializeObject(listaProstorija, Formatting.Indented);
            File.WriteAllText(putanja, json);
      }

        public Prostorije() 
        {
            listaProstorija = new System.Collections.Generic.List<Prostorija>();
        }
   
   }
}