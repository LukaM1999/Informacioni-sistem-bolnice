using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Servis;

namespace Kontroler
{
    public class PodsetnikKontroler
    {
        private static readonly Lazy<PodsetnikKontroler> Lazy = new(() => new PodsetnikKontroler());
        public static PodsetnikKontroler Instance => Lazy.Value;

        public void UkljuciNoviPodsetnik(Podsetnik novPodsetnik)
            => ObavestenjePacijentaServis.Instance.UkljuciNoviPodsetnik(novPodsetnik);
    }
}
