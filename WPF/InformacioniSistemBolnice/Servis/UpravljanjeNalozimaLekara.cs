using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servis
{
    public class UpravljanjeNalozimaLekara
    {
        private static readonly Lazy<UpravljanjeNalozimaLekara>
           lazy =
           new Lazy<UpravljanjeNalozimaLekara>
               (() => new UpravljanjeNalozimaLekara());

        public static UpravljanjeNalozimaLekara Instance { get { return lazy.Value; } }
    }
}
