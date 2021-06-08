using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.DTO;
using Servis;

namespace Kontroler
{
    public class BolnickoLecenjeKontroler
    {
        private static readonly Lazy<BolnickoLecenjeKontroler> Lazy = new(() => new BolnickoLecenjeKontroler());
        public static BolnickoLecenjeKontroler Instance => Lazy.Value;

        public void KreirajBolnickoLecenje(BolnickoLecenjeDto lecenje)
            => BolnickoLecenjeServis.Instance.KreirajBolnickoLecenje(lecenje);

        public void IzmeniBolnickoLecenje(BolnickoLecenjeDto lecenje)
            => BolnickoLecenjeServis.Instance.IzmeniBolnickoLecenje(lecenje);
    }
}
