using System;

namespace Model
{
    public class StatickaOprema : Oprema
    {



        public TipStatickeOpreme tip
        {
            get;
            set;
        }
        public int kolicina
        {
            get;
            set;
        }

        public StatickaOprema() 
        {
            
        }

        public StatickaOprema(int kol, TipStatickeOpreme tip2)
        {
            kolicina = kol;
            tip = tip2;
        }
        
        

    }
}