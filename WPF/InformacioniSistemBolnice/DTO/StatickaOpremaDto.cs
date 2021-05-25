using Model;

namespace InformacioniSistemBolnice.DTO
{
    public class StatickaOpremaDto
    {
        public TipStatickeOpreme Tip { get; set; }
        public int Kolicina { get; set; }
        public StatickaOpremaDto(int kolicina, TipStatickeOpreme tip)
        {
            this.Kolicina = kolicina;
            this.Tip = tip;
        }
    }
}
