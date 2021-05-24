using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AlergenDto
    {
        public string Naziv { get; set; }

        public AlergenDto(string naziv)
        {
            Naziv = naziv;
        }

        public override string ToString()
        {
            return Naziv;
        }
    }
}
