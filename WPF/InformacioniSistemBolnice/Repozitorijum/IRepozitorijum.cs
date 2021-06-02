using System;
using System.Collections.ObjectModel;
using Model;

namespace Repozitorijum
{
   public interface IRepozitorijum
   {
      public void Serijalizacija();
      public ObservableCollection<object> Deserijalizacija();
   }
}