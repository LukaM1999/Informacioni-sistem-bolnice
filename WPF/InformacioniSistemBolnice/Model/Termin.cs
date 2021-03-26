using System;

namespace Model
{
   public class Termin
   {
      private DateTime vreme;
      private double trajanje;
      private TipTermina tipTermina;
      private StatusTermina status;
      
      public Termin()
      {
         throw new NotImplementedException();
      }
      
      public Pacijent pacijent;
      public Lekar lekar;
      public Prostorija prostorija;
   
   }
}