using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repozitorijum
{
    public interface IOsnovniUpiti
    {
        public object NadjiPoId(object id);

        public bool ObrisiPoId(object id);

        public bool Dodaj(object objekat);
    }
}
