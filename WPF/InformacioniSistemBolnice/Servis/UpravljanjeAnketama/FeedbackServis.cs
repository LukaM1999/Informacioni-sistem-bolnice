using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformacioniSistemBolnice.DTO;
using Model;
using Repozitorijum;

namespace InformacioniSistemBolnice.Servis.UpravljanjeAnketama
{
    public class FeedbackServis
    {
        private static readonly Lazy<FeedbackServis> Lazy = new(() => new FeedbackServis());
        public static FeedbackServis Instance => Lazy.Value;

        public void PosaljiFeedback(FeedbackDto feedback)
        {
            FeedbackRepo.Instance.DodajFeedback(new Feedback(feedback.ImeKorisnika, feedback.UlogaKorisnika,
                feedback.Poruka, DateTime.Now));
        }
    }
}
