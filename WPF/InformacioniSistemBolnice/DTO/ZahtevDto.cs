namespace InformacioniSistemBolnice.DTO
{
    public class ZahtevDto
    {
        public string Komentar { get; set; }
        public string Potpis { get; set; }

        public ZahtevDto(string komentar, string potpis)
        {
            Komentar = komentar;
            Potpis = potpis;
        }
    }
}
