using System;

namespace Model
{
    public class Specijalizacija
    {
        public string Naziv { get; set; }

        public Specijalizacija(string naziv)
        {
            Naziv = naziv;
        }

        public override string ToString()
        {
            return Naziv;
        }

    }
}