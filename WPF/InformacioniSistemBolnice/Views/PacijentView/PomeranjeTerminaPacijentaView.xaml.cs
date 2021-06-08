using System;
using System.Collections.Generic;
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
using Model;
using Repozitorijum;
using Servis;
using Kontroler;
using System.Collections.ObjectModel;
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.Servis;
using InformacioniSistemBolnice.Utilities;
using InformacioniSistemBolnice.ViewModels.PacijentViewModel;

namespace InformacioniSistemBolnice
{
    public partial class PomeranjeTerminaPacijentaView : Window
    {
        private readonly Termin terminZaPomeranje;

        public PomeranjeTerminaPacijentaView(Termin izabranTermin)
        {
            InitializeComponent();
            terminZaPomeranje = izabranTermin;
            ponudjeniTermini.ItemsSource =
                new PredlogSlobodnihTerminaServis(izabranTermin).PonudiSlobodneTermineZaPomeranje();
        }

        private void PomeriTermin(object sender, RoutedEventArgs e)
        {
            Termin noviTermin = (Termin) ponudjeniTermini.SelectedValue;
            TerminKontroler.Instance.PomeriTermin(terminZaPomeranje, noviTermin);
            Close();
            KalendarViewModel.Appointments.Remove(
                (DTO.TerminDto) KalendarViewModel.Appointments.Select(dto => dto.Pocetak == terminZaPomeranje.Vreme));
            KalendarViewModel.Appointments.Add(new TerminDto(TerminUtility.DobaviFormatiranPrikazTermina
                    (noviTermin.Tip, noviTermin.LekarJmbg, noviTermin.ProstorijaId, noviTermin.Status),
                noviTermin.Vreme, noviTermin.Vreme.AddMinutes(noviTermin.Trajanje),
                new SolidColorBrush(Colors.Red), new SolidColorBrush(Colors.White), noviTermin.LekarJmbg,
                noviTermin.PacijentJmbg, noviTermin.ProstorijaId, noviTermin.Hitan, noviTermin.Tip));
            MessageBox.Show("Uspešno ste pomerili termin!");
        }
    }
}
