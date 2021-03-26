using System;

namespace Model
{
   public class ZdravstveniKarton
   {
      private string brojKartona;
      private string brojKnjizice;
      private string jmbg;
      private string prezime;
      private string imeRoditelja;
      private string ime;
      private string liceZaZdravZastitu;
      private DateTime datumRodjenja;
      private string adresaStanovanja;
      private string ulicaBroj;
      private string telefon;
      private Pol pol;
      private BracnoStanje bracnoStanje;
      private KategorijaZdravstveneZastite kategorijaZdravZastite;
      
      public ZdravstveniKarton()
      {
         throw new NotImplementedException();
      }
      
      public PodaciOZaposlenjuIZanimanju[] podaciOZaposlenjuIZanimanju;
   
   }
}