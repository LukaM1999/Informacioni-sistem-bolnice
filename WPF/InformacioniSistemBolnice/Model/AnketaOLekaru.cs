using System;

namespace Model
{
    public class AnketaOLekaru
    {
        public int Ljubaznost { get; set; }
        public int Profesionalizam { get; set; }
        public int Strpljenje { get; set; }
        public int Komunikativnost { get; set; }
        public int Azurnost { get; set; }
        public int Korisnost { get; set; }
        public string GeneralniKomentari { get; set; }

        public AnketaOLekaru(int ljubaznost, int profesionalizam, int strpljenje, int komunikativnost, int azurnost,
            int korisnost, string generalniKomentari)
        {
            Ljubaznost = ljubaznost;
            Profesionalizam = profesionalizam;
            Strpljenje = strpljenje;
            Komunikativnost = komunikativnost;
            Azurnost = azurnost;
            Korisnost = korisnost;
            GeneralniKomentari = generalniKomentari;
        }

        public AnketaOLekaru()
        {
            
        }

        public override string ToString()
        {
            return "Popunjena";
        }
    }
}