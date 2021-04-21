using System;

namespace Model
{
    public class Anamneza
    {
        public Anamneza()
        {

        }
        public string id { get; set; }
        public string sadasnjaBolest { get; set; }
        public string ranijeBolesti { get; set; }
        public string porodicneAnamneze { get; set; }
        public string zakljucak { get; set; }
        public string alergije { get; set; }

        public Anamneza(string sadasnja, string ranije, string porodicne, string a, string z)
        {
            sadasnjaBolest = sadasnja;
            ranijeBolesti = ranije;
            porodicneAnamneze = porodicne;
            zakljucak = z;
            alergije = a;
        }

    }
}