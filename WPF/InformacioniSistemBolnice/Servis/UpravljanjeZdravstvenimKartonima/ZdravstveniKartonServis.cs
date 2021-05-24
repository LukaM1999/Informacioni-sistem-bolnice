using InformacioniSistemBolnice;
using Model;
using Repozitorijum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Servis
{
    public class ZdravstveniKartonServis
    {
        private static readonly Lazy<ZdravstveniKartonServis>
          Lazy =
          new Lazy<ZdravstveniKartonServis>
              (() => new ZdravstveniKartonServis());
        public static ZdravstveniKartonServis Instance { get { return Lazy.Value; } }

        public void KreirajZdravstveniKarton(ZdravstveniKartonDto zdravstveniKartonDto,
                                                PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto)
        {
            ZdravstveniKarton zdravstveniKarton = new(zdravstveniKartonDto.BrojKartona, zdravstveniKartonDto.BrojKnjizice,
                                                      zdravstveniKartonDto.Jmbg, zdravstveniKartonDto.ImeJednogRoditelja,
                                                      zdravstveniKartonDto.LiceZaZdravstvenuZastitu, zdravstveniKartonDto.PolPacijenta,
                                                      zdravstveniKartonDto.BracnoStanje, zdravstveniKartonDto.KategorijaZdravstveneZastite,
                                                      VratiPodatkeOZaposlenju(podaciOZaposlenjuIZanimanjuDto));
            zdravstveniKarton.DodajAlergen(zdravstveniKartonDto.Alergen);
            ZdravstveniKartonRepo.Instance.DodajKarton(zdravstveniKarton);
        }

        private static PodaciOZaposlenjuIZanimanju VratiPodatkeOZaposlenju(PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuDto)
        {
            return new(podaciOZaposlenjuDto.RadnoMjesto, podaciOZaposlenjuDto.RegistarskiBroj, podaciOZaposlenjuDto.SifraDelatnosti,
                        podaciOZaposlenjuDto.PosaoKojiObavlja, podaciOZaposlenjuDto.OSIZZdrZastite,
                        podaciOZaposlenjuDto.RadPodPosebnimUslovima, podaciOZaposlenjuDto.Promjene);
        }

        public void DodeliZdravstveniKartonPacijentu()
        {
            foreach (ZdravstveniKarton zdravstveniKarton in ZdravstveniKartonRepo.Instance.ZdravstveniKartoni)
                DodjelaZdravstvenogKartonaPacijentu(zdravstveniKarton);
        }

        public void DodjelaZdravstvenogKartonaPacijentu(ZdravstveniKarton zdravstveniKarton)
        {
            Pacijent pacijent = PacijentRepo.Instance.NadjiPoJmbg(zdravstveniKarton.Jmbg);
            pacijent.zdravstveniKarton = zdravstveniKarton;
            PacijentRepo.Instance.Serijalizacija();
        }

        public void IzmeniZdravstveniKarton(ZdravstveniKartonDto zdravstveniKartonDto,
                                                PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto,
                                                ZdravstveniKarton zdravstveniKarton)
        {
            IzmeniOsnovnePodatke(zdravstveniKartonDto, zdravstveniKarton);
            IzmeniPodatkeOZanimanju(podaciOZaposlenjuIZanimanjuDto, zdravstveniKarton);
            PacijentRepo.Instance.SacuvajPromene();
        }

        private static void IzmeniOsnovnePodatke(ZdravstveniKartonDto zdravstveniKartonDto, ZdravstveniKarton zdravstveniKarton)
        {
            zdravstveniKarton.BrojKartona = zdravstveniKartonDto.BrojKartona;
            zdravstveniKarton.BrojKnjizice = zdravstveniKartonDto.BrojKnjizice;
            zdravstveniKarton.Jmbg = zdravstveniKartonDto.Jmbg;
            zdravstveniKarton.ImeJednogRoditelja = zdravstveniKartonDto.ImeJednogRoditelja;
            zdravstveniKarton.LiceZaZdravstvenuZastitu = zdravstveniKartonDto.LiceZaZdravstvenuZastitu;
            zdravstveniKarton.BracnoStanje = zdravstveniKartonDto.BracnoStanje;
            zdravstveniKarton.PolPacijenta = zdravstveniKartonDto.PolPacijenta;
            zdravstveniKarton.KategorijaZdravstveneZastite = zdravstveniKartonDto.KategorijaZdravstveneZastite;
        }

        private static void IzmeniPodatkeOZanimanju(PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto,
                                                    ZdravstveniKarton zdravstveniKarton)
        {
            PodaciOZaposlenjuIZanimanju zaposlenjeIZanimanje = zdravstveniKarton.PodaciOZaposlenjuIZanimanju;
            zaposlenjeIZanimanje.RadnoMjesto = podaciOZaposlenjuIZanimanjuDto.RadnoMjesto;
            zaposlenjeIZanimanje.RegistarskiBroj = podaciOZaposlenjuIZanimanjuDto.RegistarskiBroj;
            zaposlenjeIZanimanje.SifraDelatnosti = podaciOZaposlenjuIZanimanjuDto.SifraDelatnosti;
            zaposlenjeIZanimanje.RadnoMjesto = podaciOZaposlenjuIZanimanjuDto.RadnoMjesto;
            zaposlenjeIZanimanje.PosaoKojiObavlja = podaciOZaposlenjuIZanimanjuDto.PosaoKojiObavlja;
            zaposlenjeIZanimanje.Promjene = podaciOZaposlenjuIZanimanjuDto.Promjene;
        }

        public void DodajAlergenPacijentu(Alergen alergen, string jmbg)
        {
            Pacijent pacijent = PacijentRepo.Instance.NadjiPoJmbg(jmbg);
            ZdravstveniKarton zdravstveniKarton = pacijent.zdravstveniKarton;
            zdravstveniKarton.DodajAlergen(alergen);
            PacijentRepo.Instance.SacuvajPromene();
        }

        public void ObrisiAlergenPacijentu(Alergen alergen, string jmbg)
        {
            Pacijent pacijent = PacijentRepo.Instance.NadjiPoJmbg(jmbg);
            ZdravstveniKarton zdravstveniKarton = pacijent.zdravstveniKarton;
            zdravstveniKarton.ObrisiAlergen(alergen);
            PacijentRepo.Instance.SacuvajPromene();
        }
    }
}

