using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Model;

namespace Repozitorijum
{
    class Lekovi:Repozitorijum
    {
        private string putanja = "../../../json/lekovi.json";

        private static readonly Lazy<Lekovi>
           lazy =
           new Lazy<Lekovi>
               (() => new Lekovi());

        public static Lekovi Instance { get { return lazy.Value; } }

        public ObservableCollection<Lek> listaLekova
        {
            get;
            set;
        }

        public void Deserijalizacija()
        {
            listaLekova = JsonConvert.DeserializeObject<ObservableCollection<Lek>>(File.ReadAllText(putanja));
        }

        public void Serijalizacija()
        {
            string json = JsonConvert.SerializeObject(listaLekova, Formatting.Indented);
            File.WriteAllText(putanja, json);
        }

        public Lek getSelected(Lek p)
        {
            Lek prs = null;
            foreach (Lek pr in listaLekova)
            {
                if (pr.Naziv.Equals(p.Naziv))
                {
                    prs = pr;
                }
            }
            return listaLekova.ElementAt(listaLekova.IndexOf(prs));
        }

        public Lek NadjiPoNazivu(string naziv)
        {
            foreach (Lek pronadjen in listaLekova)
            {
                if (pronadjen.Naziv == naziv) return pronadjen;
            }
            return null;
        }

        public bool BrisiPoNazivu(string naziv)
        {
            foreach (Lek pronadjen in listaLekova)
            {
                if (pronadjen.Naziv != naziv) continue;
                return listaLekova.Remove(pronadjen);
            }
            return false;
        }

        public void SacuvajPromene()
        {
            Serijalizacija();
            Deserijalizacija();
        }
    }
}
