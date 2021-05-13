using System;

namespace Model
{
    public class Adresa
    {
        public string Broj { get; set; }
        public string Ulica { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }
        
        public Adresa(string drzava, string grad, string ulica, string broj)
        {
            Drzava = drzava;
            Grad = grad;
            Ulica = ulica;
            Broj = broj;
        }
    }
}