using System;
using PropertyChanged;
namespace Model
{
    [AddINotifyPropertyChangedInterface]
    public class Anamneza
    {
        public string AnamnezaId { get; set; }
        public string SadasnjaBolest { get; set; }
        public string RanijeBolesti { get; set; }
        public string PorodicneAnamneze { get; set; }
        public string Zakljucak { get; set; }
        public string BeleskePacijenta { get; set; }

        public Anamneza()
        {
        }

        public Anamneza(string sadasnja, string ranije, string porodicne, string zakljucak)
        {
            SadasnjaBolest = sadasnja;
            RanijeBolesti = ranije;
            PorodicneAnamneze = porodicne;
            Zakljucak = zakljucak;
        }
    }
}