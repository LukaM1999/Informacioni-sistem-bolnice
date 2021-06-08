using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Servis.UpravljanjeBolnickimLecenjima
{
    public class ZavrsenoBolnickoLecenjeServis
    {
        private static readonly Lazy<ZavrsenoBolnickoLecenjeServis> Lazy = new(() => new ZavrsenoBolnickoLecenjeServis());
        public static ZavrsenoBolnickoLecenjeServis Instance => Lazy.Value;

        public void ProveriZavrsenostLecenja()
        {
            while (true)
            {
                foreach (BolnickoLecenje lecenje in BolnickoLecenjeRepo.Instance.BolnickaLecenja.ToList())
                    if (lecenje.ZavrsetakLecenja < DateTime.Today) BolnickoLecenjeRepo.Instance.ObrisiPoId(lecenje);
                BolnickoLecenjeRepo.Instance.Serijalizacija();
                Thread.Sleep(2000);
            }
        } 
    }
}
