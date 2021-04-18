using System;

namespace Model
{
    public class Alergen
    {
        public string nazivAlergena
        {
            get;
            set;
        }

        public Alergen()
        {

        }


        public Alergen(string naziv)
        {
            nazivAlergena = naziv;
        }


        public override string ToString()
        {
            return nazivAlergena.ToString();
          
        }




    }
}