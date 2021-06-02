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
    public class TransformacijaProstorijaRepo : IRepozitorijum

    {
        private const string Putanja = "../../../json/transformacijaProstorijeTermini.json";

        private static readonly Lazy<TransformacijaProstorijaRepo> Lazy = new(() => new TransformacijaProstorijaRepo());
        public static TransformacijaProstorijaRepo Instance => Lazy.Value;

        public ObservableCollection<TransformacijaProstorija> Termini { get; set; }

        public ObservableCollection<object> Deserijalizacija()
        {
            Termini = JsonConvert.DeserializeObject<ObservableCollection<TransformacijaProstorija>>(File.ReadAllText(Putanja));
            return new ObservableCollection<object> {Termini} ;
        }

        public void Serijalizacija()
        {
            File.WriteAllText(Putanja, JsonConvert.SerializeObject(Termini, Formatting.Indented));
        }

        public Prostorija NadjiProstoriju(string iDIzabraneProstorije)
        {
            return ProstorijaRepo.Instance.NadjiPoId(iDIzabraneProstorije);
        }

        public void DodajTerminTransformacijeProstorija(TransformacijaProstorija noviTermin)
        {
            Termini.Add(noviTermin);
        }

        public bool SeProstorijaVecRenovira(string idProstorije)
        {
            foreach (TransformacijaProstorija termin in Termini)
            {
                if (termin.IdPrveStareProstorije.Equals(idProstorije) || termin.PrvaNovaProstorija.Id.Equals(idProstorije))
                {
                    return true;
                }

                if (termin.IdDrugeStareProstorije != null)
                {
                    if (termin.IdDrugeStareProstorije.Equals(idProstorije)) return true;
                }

                if (termin.DrugaNovaProstorija != null)
                {
                    if (termin.DrugaNovaProstorija.Id.Equals(idProstorije)) return true;
                }
            }

            return false;
        }

        public bool BrisiTerminTransformacijeProstorije(TransformacijaProstorija izabraniTermin)
        {
            return Termini.Remove(izabraniTermin);
        }
    }
}
