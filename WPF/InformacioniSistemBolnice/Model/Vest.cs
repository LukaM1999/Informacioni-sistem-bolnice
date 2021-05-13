using System;

namespace Model
{

    public class Vest
    {

        public string Sadrzaj { get; set; }
        public string Id { get; set; }

        public Vest(string sadrzajVesti, string naslov)
        {
            this.Sadrzaj = sadrzajVesti;
            this.Id = naslov;
        }

        public override string ToString()
        {
            return this.Id;
        }

    }
}