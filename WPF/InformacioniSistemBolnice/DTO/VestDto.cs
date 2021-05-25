using System;

namespace InformacioniSistemBolnice.DTO
{
    public class VestDto
    {
        public string Sadrzaj { get; set; }
        public string Id { get; set; }
        public DateTime VremeObjave { get; set; }

        public VestDto(string naslov, string sadrzajVesti)
        {
            Id = naslov;
            Sadrzaj = sadrzajVesti;
        }
    }
}
