using System;

namespace Model
{
    public class Alergen
    {
        public string Naziv { get; set; }

        public Alergen(string naziv) => Naziv = naziv;
        public override string ToString() { return Naziv.ToString(); }
    }
}