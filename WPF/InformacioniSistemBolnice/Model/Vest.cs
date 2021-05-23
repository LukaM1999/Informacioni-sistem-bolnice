using System;
using System.Text;

namespace Model
{
    public class Vest
    {
        public string Sadrzaj { get; set; }
        public string Id { get; set; }
        public DateTime VremeObjave { get; set; }

        public Vest(string sadrzajVesti, string naslov, DateTime vremeObjave)
        {
            Sadrzaj = sadrzajVesti;
            Id = naslov;
            VremeObjave = vremeObjave;
        }

        public override string ToString()
        {
            return Id;
        }
    }
}