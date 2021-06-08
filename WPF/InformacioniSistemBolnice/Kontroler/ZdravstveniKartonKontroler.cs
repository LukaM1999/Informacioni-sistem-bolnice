using InformacioniSistemBolnice.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Servis;

namespace Kontroler
{
    public class ZdravstveniKartonKontroler
    {
        private static readonly Lazy<ZdravstveniKartonKontroler> Lazy = new(() => new ZdravstveniKartonKontroler());
        public static ZdravstveniKartonKontroler Instance => Lazy.Value;

        public void KreiranjeZdravstvenogKartona(ZdravstveniKartonDto zdravstveniKartonDto,
                                              PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto)
              => ZdravstveniKartonServis.Instance.KreirajZdravstveniKarton(zdravstveniKartonDto,
                                                                           podaciOZaposlenjuIZanimanjuDto);

        public void DodelaZdravstvenogKartonaPacijentu()
            => ZdravstveniKartonServis.Instance.DodeliZdravstveniKartonPacijentu();



        public void IzmenaZdravstvenogKartona(ZdravstveniKartonDto zdravstveniKartonDto, ZdravstveniKarton zdravstveniKarton,
                                              PodaciOZaposlenjuIZanimanjuDto podaciOZaposlenjuIZanimanjuDto)
            => ZdravstveniKartonServis.Instance.IzmeniZdravstveniKarton(zdravstveniKartonDto, podaciOZaposlenjuIZanimanjuDto,
                                                                        zdravstveniKarton);

        public void DodavanjeAlergenaIzZdravstvenogKartona(Alergen alergen, string jmbg)
              => ZdravstveniKartonServis.Instance.DodajAlergenPacijentu(alergen, jmbg);

        public void UklanjanjeAlergenaIzZdravstvenogKartona(Alergen alergen, string jmbg)
                => ZdravstveniKartonServis.Instance.ObrisiAlergenPacijentu(alergen, jmbg);

        public void IzdavanjeRecepta(ReceptDto recept) => ReceptServis.Instance.IzdajRecept(recept);

        public void DodavanjeAnamneze(AnamnezaDto anamneza) => AnamnezaServis.Instance.DodajAnamnezu(anamneza);

        public void DodajAnamnezaBeleske(Pacijent ulogovanPacijent, string beleske)
            => AnamnezaServis.Instance.DodajBeleske(ulogovanPacijent, beleske);

    }
}
