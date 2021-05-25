using Model;

namespace InformacioniSistemBolnice.DTO
{
    public class RaspodelaDinamickeOpremeDto
    {
        public string IzProstorijeId { get; set; }
        public string UProstorijuId { get; set; }
        public DinamickaOprema Oprema { get; set; }
        public int Kolicina { get; set; }

        public RaspodelaDinamickeOpremeDto(string izProstorije, string uProstoriju, DinamickaOprema oprema, int kolicina)
        {
            IzProstorijeId = izProstorije;
            UProstorijuId = uProstoriju;
            Oprema = oprema;
            Kolicina = kolicina;
        }
    }
}
