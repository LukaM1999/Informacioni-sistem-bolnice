using Model;

namespace InformacioniSistemBolnice.DTO
{
    public class DinamickaOpremaDto
    {
        public TipDinamickeOpreme Tip { get; set; }
        public int Kolicina { get; set; }
        public DinamickaOpremaDto() { }
        public DinamickaOpremaDto(int kolicina, TipDinamickeOpreme tip)
        {
            Kolicina = kolicina;
            Tip = tip;
        }
    }
}
