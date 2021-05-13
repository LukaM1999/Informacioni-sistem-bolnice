using System;

namespace Model
{
    public class DinamickaOprema : Oprema
    {
        public TipDinamickeOpreme tip { get; set; }
        public int kolicina { get; set; }
        public DinamickaOprema()
        {

        }
        public DinamickaOprema(int kol, TipDinamickeOpreme tip2)
        {
            kolicina = kol;
            tip = tip2;
        }

    }
}