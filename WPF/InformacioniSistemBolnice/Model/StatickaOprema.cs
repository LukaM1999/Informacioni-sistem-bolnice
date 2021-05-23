using System;

namespace Model
{
    public class StatickaOprema : Oprema
    {
        public TipStatickeOpreme Tip { get; set; }
        public int Kolicina { get; set; }
        public StatickaOprema(int kolicina, TipStatickeOpreme tip)
        {
            this.Kolicina = kolicina;
            this.Tip = tip;
        }
    }
}