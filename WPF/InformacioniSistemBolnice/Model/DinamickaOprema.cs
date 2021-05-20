using System;

namespace Model
{
    public class DinamickaOprema : Oprema
    {
        public TipDinamickeOpreme Tip { get; set; }
        public int Kolicina { get; set; }
        public DinamickaOprema() {}
        public DinamickaOprema(int kol, TipDinamickeOpreme tip2)
        {
            Kolicina = kol;
            Tip = tip2;
        }

    }
}