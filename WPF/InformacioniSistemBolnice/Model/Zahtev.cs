using System;

namespace Model
{
    public class Zahtev
    {
        public string Komentar { get; set; }
        public string Potpis { get; set; }

        public Zahtev(string komentar, string potpis)
        {
            Komentar = komentar;
            Potpis = potpis;
        }

        public override string ToString()
        {
            return Potpis;
        }
    }
}