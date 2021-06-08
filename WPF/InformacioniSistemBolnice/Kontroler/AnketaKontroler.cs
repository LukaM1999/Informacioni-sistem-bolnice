using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Servis;

namespace Kontroler
{
    public class AnketaKontroler
    {
        private static readonly Lazy<AnketaKontroler> Lazy = new(() => new AnketaKontroler());
        public static AnketaKontroler Instance => Lazy.Value;

        public void PosaljiAnketuOLekaru(Termin zavrsenTermin, AnketaDto novaAnketa)
            => SlanjeAnketeServis.Instance.PosaljiAnketuOLekaru(zavrsenTermin, novaAnketa);

        public void PosaljiAnketuOBolnici(AnketaDto anketa)
            => SlanjeAnketeServis.Instance.PosaljiAnketuOBolnici(anketa);
    }
}
