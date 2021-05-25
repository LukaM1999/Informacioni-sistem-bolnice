namespace InformacioniSistemBolnice.DTO
{
    public class HitnoZakazivanjeDto
    {
        public string Specijalizacija { get; set; }
        public string PacijentJmbg { get; set; }
        public string ProstorijaId { get; set; }
        public HitnoZakazivanjeDto(string specijalizacija, string jmbgPacijenta, string idProstorije)
        {
            this.Specijalizacija = specijalizacija;
            this.PacijentJmbg = jmbgPacijenta;
            this.ProstorijaId = idProstorije;
        }
    }
}

