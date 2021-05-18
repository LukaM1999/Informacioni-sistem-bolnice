using System;
using PropertyChanged;
namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Anamneza
    {
        public string id { get; set; }
        public string sadasnjaBolest { get; set; }
        public string ranijeBolesti { get; set; }
        public string porodicneAnamneze { get; set; }
        public string zakljucak { get; set; }
        public string BeleskePacijenta { get; set; }

        public Anamneza(string sadasnja, string ranije, string porodicne, string z)
        {
            sadasnjaBolest = sadasnja;
            ranijeBolesti = ranije;
            porodicneAnamneze = porodicne;
            zakljucak = z;
        }
    }
}