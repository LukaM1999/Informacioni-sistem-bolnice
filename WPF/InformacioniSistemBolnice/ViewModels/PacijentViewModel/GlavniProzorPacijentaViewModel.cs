using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.Utilities;
using InformacioniSistemBolnice.Views.PacijentView;
using Kontroler;
using MahApps.Metro.Controls.Dialogs;

namespace InformacioniSistemBolnice.ViewModels.PacijentViewModel
{
    public class GlavniProzorPacijentaViewModel
    {
        public ICommand OtvaranjeFeedbacka { get; set; }
        public ICommand Odjavljivanje { get; set; }

        public GlavniProzorPacijentaViewModel()
        {
            OtvaranjeFeedbacka = new Command(o => OtvoriFeedback());
            Odjavljivanje = new Command(o => OdjaviSe());
        }

        private void OtvoriFeedback()
        {
            new FeedbackPacijentaView().ShowDialog();
        }

        private void OdjaviSe()
        {
            MessageBoxResult odluka = MessageBox.Show("Da li ste sigurni da želite da se odjavite", "Potvrda odjavljivanja",
                MessageBoxButton.YesNo);
            if (odluka is MessageBoxResult.No) return;
            new Login().Show();
            PronadjiProzorUtility.PronadjiProzor(this).Close();
        }
    }
}
