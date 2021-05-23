using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class VestDto
    {
        public string Sadrzaj { get; set; }
        public string Id { get; set; }
        public DateTime VremeObjave { get; set; }

        public VestDto(string naslov, string sadrzajVesti)
        {
            Id = naslov;
            Sadrzaj = sadrzajVesti;
        }

    }
}
