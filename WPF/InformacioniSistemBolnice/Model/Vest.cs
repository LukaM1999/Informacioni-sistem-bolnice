using System;

namespace Model
{
   public class Vest
   {
        private string sadrzaj;


      private string id;
      

        public Vest(string sadrzajVesti, string naslov)
        {
            this.Sadrzaj = sadrzajVesti;
            this.Id = naslov;
        }

        public string Sadrzaj { get => sadrzaj; set => sadrzaj = value; }
        public string Id { get => id; set => id = value; }

        public override string ToString()
        {
            return this.Id;
        }

    }
}