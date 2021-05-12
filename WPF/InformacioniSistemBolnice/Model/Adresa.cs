using System;

namespace Model
{
    public class Adresa
    {
        public string Broj { get; set; }
        public string Ulica { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }
        
        public Adresa(string drz, string g, string ul, string br)
        {
            Drzava = drz;
            Grad = g;
            Ulica = ul;
            Broj = br;
        }
    }
}