using Model;

namespace InformacioniSistemBolnice.DTO
{
    public class TerminiLekaraZaPomeranjeDto
    {
        public Termin TerminZaPomeranje { get; set; }
        public Termin NoviTermin { get; set; }
        public string PacijentJmbg { get; set; }

        public TerminiLekaraZaPomeranjeDto(Termin terminZaPomeranje, Termin noviTermin, string jmbgPacijenta)
        {
            this.TerminZaPomeranje = terminZaPomeranje;
            this.NoviTermin = noviTermin;
            this.PacijentJmbg = jmbgPacijenta;
        }
    }
}
