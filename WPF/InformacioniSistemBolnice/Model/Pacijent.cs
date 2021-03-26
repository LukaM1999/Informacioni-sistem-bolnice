using System;

namespace Model
{
   public class Pacijent : Osoba
   {
      public Pacijent()
      {
         throw new NotImplementedException();
      }
      
      public ZdravstveniKarton zdravstveniKarton;
      public Termin[] termin;
   
   }
}