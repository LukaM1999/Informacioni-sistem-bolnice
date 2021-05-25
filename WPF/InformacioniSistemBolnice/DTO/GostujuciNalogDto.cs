using System;

namespace InformacioniSistemBolnice.DTO
{
    public class GostujuciNalogDto
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Jmbg { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public GostujuciNalogDto(string ime, string prezime, string jmbg, DateTime datumRodjenja, string telefon, string email)
        {
            this.Ime = ime;
            this.Prezime = prezime;
            this.Jmbg = jmbg;
            this.DatumRodjenja = datumRodjenja;
            this.Telefon = telefon;
            this.Email = email;
        }
    }
}
