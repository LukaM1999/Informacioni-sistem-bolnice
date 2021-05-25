using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Model
{
   public class Inventar
   {
        public ObservableCollection<DinamickaOprema> DinamickaOprema { get; set; }
        public ObservableCollection<StatickaOprema> StatickaOprema { get; set; }

        public Inventar()
        {
            DinamickaOprema = new ObservableCollection<DinamickaOprema>();
            StatickaOprema = new ObservableCollection<StatickaOprema>();
        }

        public DinamickaOprema NadjiDinamickuOpremuPoTipu(TipDinamickeOpreme tipOpreme)
        {
            foreach (DinamickaOprema oprema in DinamickaOprema) if (oprema.Tip == tipOpreme) return oprema;
            return null;
        }

        public StatickaOprema NadjiStatickuOpremuPoTipu(TipStatickeOpreme tipOpreme)
        {
            foreach (StatickaOprema oprema in StatickaOprema) if (oprema.Tip == tipOpreme) return oprema;
            return null;
        }

        public void DodajDinamickuOpremu(DinamickaOprema novaOprema)
        {
            DinamickaOprema.Add(novaOprema);
        }

        public void DodajStatickuOpremu(StatickaOprema novaOprema)
        {
            StatickaOprema.Add(novaOprema);
        }
    }
}