using InformacioniSistemBolnice;
using Model;
using Repozitorijum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servis
{
    public class UpravljanjeNalozimaLekara
    {
        private static readonly Lazy<UpravljanjeNalozimaLekara>
           lazy =
           new Lazy<UpravljanjeNalozimaLekara>
               (() => new UpravljanjeNalozimaLekara());

        public static UpravljanjeNalozimaLekara Instance { get { return lazy.Value; } }

        public void KreiranjeNaloga(LekarDto lekarDto)
        {
            Korisnik korisnik = KreiranjeKorisnika(lekarDto);
            KreiranjeLekara(lekarDto, korisnik);
            SacuvajURepozitorijum();
        }
        
        private static Korisnik KreiranjeKorisnika(LekarDto lekarDto)
        {
            Korisnik korisnik = new(lekarDto.korisnickoIme, lekarDto.lozinka, 
                                    (Model.UlogaKorisnika)Enum.Parse(typeof(Model.UlogaKorisnika), "lekar"));
            Korisnici.Instance.listaKorisnika.Add(korisnik);
            return korisnik;
        }
        private static void KreiranjeLekara(LekarDto lekarDto, Korisnik korisnik)
        {
            Lekar lekar = new Lekar(new Osoba(lekarDto.ime, lekarDto.prezime, lekarDto.jmbg,
                                    DateTime.Parse(lekarDto.datumRodjenja.ToString()), lekarDto.telefon, lekarDto.email, korisnik,
                                    new Adresa(lekarDto.drzava, lekarDto.grad, lekarDto.ulica, lekarDto.broj)),
                                    lekarDto.specijalizacija);
            ObservableCollection<Termin> zauzetiTermini = new ObservableCollection<Termin>();
            lekar.zauzetiTermini = zauzetiTermini;
            Lekari.Instance.listaLekara.Add(lekar);

        }

        private static void SacuvajURepozitorijum()
        {
            Korisnici.Instance.Serijalizacija();
            Lekari.Instance.Serijalizacija();
        }
    }
}
