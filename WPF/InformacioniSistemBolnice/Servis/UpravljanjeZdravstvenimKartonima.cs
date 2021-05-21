using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servis
{
    public class UpravljanjeZdravstvenimKartonima
    {
        private static readonly Lazy<UpravljanjeZdravstvenimKartonima>
         lazy =
         new Lazy<UpravljanjeZdravstvenimKartonima>
             (() => new UpravljanjeZdravstvenimKartonima());

    }
}
