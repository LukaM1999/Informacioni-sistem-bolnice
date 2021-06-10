using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.Servis;
using InformacioniSistemBolnice.Utilities;
using InformacioniSistemBolnice.ViewModels;
using InformacioniSistemBolnice.ViewModels.PacijentViewModel;
using Kontroler;
using Model;
using Repozitorijum;
using Servis;

namespace InformacioniSistemBolnice
{
    public partial class IzborTerminaPacijentaView : Window
    {
        public ICommand ZakazivanjeTermina { get; set; }
        public IzborTerminaPacijentaView(ZakazivanjeTerminaDto zakazivanje)
        {
            InitializeComponent();
            DataContext = this;
            ponudjeniTermini.ItemsSource = new PredlogSlobodnihTerminaServis(zakazivanje).PonudiSlobodneTermine();
            ZakazivanjeTermina = new Command(o => ZakaziTermin(), o => ponudjeniTermini.SelectedIndex > -1);
        }

        private void ZakaziTermin()
        {
            Termin izabranTermin = (Termin) ponudjeniTermini.SelectedValue;
            TerminKontroler.Instance.ZakaziTermin(izabranTermin);
            Owner?.Close();
            Close();
            KalendarViewModel.Appointments.Add(new TerminDto(TerminUtility.DobaviFormatiranPrikazTermina
                    (izabranTermin.Tip, izabranTermin.LekarJmbg, izabranTermin.ProstorijaId, izabranTermin.Status),
                izabranTermin.Vreme, izabranTermin.Vreme.AddMinutes(izabranTermin.Trajanje),
                new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.White), izabranTermin.LekarJmbg,
                izabranTermin.PacijentJmbg, izabranTermin.ProstorijaId, izabranTermin.Hitan, izabranTermin.Tip));
            MessageBox.Show("Uspešno ste zakazali termin!");
        }
    }
}
