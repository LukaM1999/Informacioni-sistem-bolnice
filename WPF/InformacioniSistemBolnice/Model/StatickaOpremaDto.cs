using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class StatickaOpremaDto
    {
        public TipStatickeOpreme Tip { get; set; }
        public int Kolicina { get; set; }
        public StatickaOpremaDto(int kolicina, TipStatickeOpreme tip)
        {
            this.Kolicina = kolicina;
            this.Tip = tip;
        }
    }
}
