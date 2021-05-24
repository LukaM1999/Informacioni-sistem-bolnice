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
    public class PodsetnikRepo : Repozitorijum
    {
        private const string Putanja = "../../../json/podsetnici.json";

        private static readonly Lazy<PodsetnikRepo> Lazy = new(() => new PodsetnikRepo());
        public static PodsetnikRepo Instance => Lazy.Value;

        public ObservableCollection<Podsetnik> Podsetnici { get; set; }

        public void Deserijalizacija()
        {
            Podsetnici = JsonConvert.DeserializeObject<ObservableCollection<Podsetnik>>(File.ReadAllText(Putanja));
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(Podsetnici, Formatting.Indented));
        }

        public void DodajPodsetnik(Podsetnik noviPodsetnik)
        {
            Podsetnici.Add(noviPodsetnik);
            Serijalizacija();
        }

        public bool PodsetnikVecPostoji(Podsetnik podsetnik)
        {
            foreach (Podsetnik nadjenPodsetnik in Podsetnici) if (podsetnik == nadjenPodsetnik) return true;
            return false;
        }

        public ObservableCollection<Podsetnik> NadjiSvePoJmbg(string jmbgPacijenta)
        {
            ObservableCollection<Podsetnik> pacijentoviPodsetnici = new();
            foreach (Podsetnik podsetnik in Podsetnici) 
                if (podsetnik.PacijentJmbg == jmbgPacijenta) pacijentoviPodsetnici.Add(podsetnik);
            return pacijentoviPodsetnici;
        }
    }
}
