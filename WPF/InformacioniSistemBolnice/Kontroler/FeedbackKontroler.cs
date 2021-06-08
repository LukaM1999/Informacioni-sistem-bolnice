using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.Servis.UpravljanjeAnketama;

namespace Kontroler
{
    public class FeedbackKontroler
    {
        private static readonly Lazy<FeedbackKontroler> Lazy = new(() => new FeedbackKontroler());
        public static FeedbackKontroler Instance => Lazy.Value;

        public void PosaljiFeedback(FeedbackDto feedback)
        {
            FeedbackServis.Instance.PosaljiFeedback(feedback);
        }
    }
}
