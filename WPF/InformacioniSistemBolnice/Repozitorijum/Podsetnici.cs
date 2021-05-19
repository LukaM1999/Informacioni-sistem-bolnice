using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace Repozitorijum
{
    public class Podsetnici : Repozitorijum
    {
        private const string Putanja = "../../../json/podsetnici.json";

        private static readonly Lazy<Podsetnici> Lazy = new(() => new Podsetnici());
        public static Podsetnici Instance => Lazy.Value;

        public ObservableCollection<Podsetnik> listaPodsetnika;

        public void Deserijalizacija()
        {
            listaPodsetnika = JsonConvert.DeserializeObject<ObservableCollection<Podsetnik>>(File.ReadAllText(Putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaPodsetnika, Formatting.Indented);
            File.WriteAllText(Putanja, json);
        }

        public void DodajPodsetnik(Podsetnik noviPodsetnik)
        {
            listaPodsetnika.Add(noviPodsetnik);
            Serijalizacija();
        }

        public bool PodsetnikVecPostoji(Podsetnik podsetnik)
        {
            foreach (Podsetnik nadjenPodsetnik in listaPodsetnika) if (podsetnik == nadjenPodsetnik) return true;
            return false;
        }

        public ObservableCollection<Podsetnik> NadjiSvePoJmbg(string jmbgPacijenta)
        {
            ObservableCollection<Podsetnik> pacijentoviPodsetnici = new();
            foreach (Podsetnik podsetnik in listaPodsetnika) 
                if (podsetnik.PacijentJmbg == jmbgPacijenta) pacijentoviPodsetnici.Add(podsetnik);
            return pacijentoviPodsetnici;
        }
    }
}
