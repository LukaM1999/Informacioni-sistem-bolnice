namespace InformacioniSistemBolnice.DTO
{
    public class AlergenDto
    {
        public string Naziv { get; set; }

        public AlergenDto(string naziv)
        {
            Naziv = naziv;
        }

        public override string ToString()
        {
            return Naziv;
        }
    }
}
