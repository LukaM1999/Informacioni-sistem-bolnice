using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using InformacioniSistemBolnice.DTO;
using Model;
using Repozitorijum;

namespace Servis
{
    public class BolnickoLecenjeServis
    {
        private static readonly Lazy<BolnickoLecenjeServis> Lazy = new(() => new BolnickoLecenjeServis());
        public static BolnickoLecenjeServis Instance => Lazy.Value;

        private BolnickoLecenjeDto lecenje;

        public void KreirajBolnickoLecenje(BolnickoLecenjeDto lecenjeDto)
        {
            lecenje = lecenjeDto;
            int brojac = 0;
            if (ProveriTerminRenoviranjaProstorije()) return;
            brojac = PrebrojBolnickaLecenjaUProstoriji(brojac);
            ZakaziLecenje(brojac);
        }

        private void ZakaziLecenje(int brojac)
        {
            foreach (StatickaOprema oprema in lecenje.Prostorija.Inventar.StatickaOprema)
            {
                if (oprema.Tip == TipStatickeOpreme.krevet)
                {
                    KreirajLecenje(brojac, oprema);
                    return;
                }
            }
        }

        private void KreirajLecenje(int brojac, StatickaOprema oprema)
        {
            if (brojac >= oprema.Kolicina)
            {
                MessageBox.Show("Zauzeti svi kreveti u prostoriji");
                return;
            }
            BolnickoLecenjeRepo.Instance.Dodaj(new BolnickoLecenje(lecenje.Pocetak, lecenje.Zavrsetak,
                lecenje.Prostorija.Id, lecenje.Pacijent.Jmbg));
            BolnickoLecenjeRepo.Instance.Serijalizacija();
        }

        private int PrebrojBolnickaLecenjaUProstoriji(int brojac)
        {
            foreach (BolnickoLecenje staroLecenje in BolnickoLecenjeRepo.Instance.BolnickaLecenja)
            {
                if (JeZauzetKrevet(staroLecenje))
                {
                    brojac++;
                }
            }
            return brojac;
        }

        private bool JeZauzetKrevet(BolnickoLecenje staroLecenje)
        {
            return staroLecenje.NazivProstorije == lecenje.Prostorija.Id &&
                   ((lecenje.Pocetak >= staroLecenje.PocetakLecenja && lecenje.Pocetak <= staroLecenje.ZavrsetakLecenja)
                    || (lecenje.Zavrsetak >= staroLecenje.PocetakLecenja && lecenje.Zavrsetak <= staroLecenje.ZavrsetakLecenja));
        }

        private bool ProveriTerminRenoviranjaProstorije()
        {
            if (lecenje.Prostorija.Renoviranje == null) return false;
            if (!SePreklapaSaTerminomRenoviranja()) return false;
            MessageBox.Show("Prostorija ima zakazano renoviranje izmedju "
                            + lecenje.Prostorija.Renoviranje.PocetakRenoviranja.ToString("g") +
                            " i " + lecenje.Prostorija.Renoviranje.KrajRenoviranja.Date.ToString("g"));
            return true;
        }

        private bool SePreklapaSaTerminomRenoviranja()
        {
            return (lecenje.Pocetak >= lecenje.Prostorija.Renoviranje.PocetakRenoviranja &&
                    lecenje.Pocetak <= lecenje.Prostorija.Renoviranje.KrajRenoviranja) ||
                   (lecenje.Zavrsetak >= lecenje.Prostorija.Renoviranje.PocetakRenoviranja &&
                    lecenje.Zavrsetak <= lecenje.Prostorija.Renoviranje.KrajRenoviranja);
        }

        public void IzmeniBolnickoLecenje(BolnickoLecenjeDto lecenjeDto)
        {
            BolnickoLecenje lecenje = BolnickoLecenjeRepo.Instance.NadjiPoId(lecenjeDto.Pacijent.Jmbg) as BolnickoLecenje;
            lecenje.ZavrsetakLecenja = lecenjeDto.Zavrsetak;
            BolnickoLecenjeRepo.Instance.Serijalizacija();
        }
    }
}
