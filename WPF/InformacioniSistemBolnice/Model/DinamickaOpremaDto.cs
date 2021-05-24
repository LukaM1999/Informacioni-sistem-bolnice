using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DinamickaOpremaDto
    {
        public TipDinamickeOpreme Tip { get; set; }
        public int Kolicina { get; set; }
        public DinamickaOpremaDto() { }
        public DinamickaOpremaDto(int kolicina, TipDinamickeOpreme tip)
        {
            Kolicina = kolicina;
            Tip = tip;
        }
    }
}
