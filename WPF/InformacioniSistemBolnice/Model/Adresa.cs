using System;

namespace Model
{
    public class Adresa
    {


        private string drzava;
        private string grad;
        private string ulica;
        private string broj;

        public string Broj { get => broj; set => broj = value; }
        public string Ulica { get => ulica; set => ulica = value; }
        public string Grad { get => grad; set => grad = value; }
        public string Drzava { get => drzava; set => drzava = value; }

        public Adresa()
        {

        }
        public Adresa(string drz, string g, string ul, string br)
        {
            drzava = drz;
            grad = g;
            ulica = ul;
            broj = br;
            
        }

    }
}