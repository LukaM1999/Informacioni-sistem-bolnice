using System;

namespace Model
{
    public class StatickaOprema : Oprema
    {
        public TipStatickeOpreme tip { get; set; }
        public int kolicina { get; set; }
        public StatickaOprema(int kolicnia, TipStatickeOpreme tip)
        {
            this.kolicina = kolicina;
            this.tip = tip;
        }
    }
}