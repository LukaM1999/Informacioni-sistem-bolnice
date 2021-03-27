using System;

namespace Model
{
    public class Pacijent : Osoba
    {
        public Pacijent()
        {

        }

        public Pacijent(Osoba o)
        {

        }

        public ZdravstveniKarton zdravstveniKarton;
        public Termin[] termin;

    }
}