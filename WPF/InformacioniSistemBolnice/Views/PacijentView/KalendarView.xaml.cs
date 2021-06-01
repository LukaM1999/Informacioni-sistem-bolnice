using System.Windows.Controls;
using InformacioniSistemBolnice.DTO;
using InformacioniSistemBolnice.ViewModels.PacijentViewModel;

namespace InformacioniSistemBolnice.Views.PacijentView
{
    public partial class KalendarView : UserControl
    {
        public KalendarView()
        {
            InitializeComponent();
        }

        private void Schedule_AppointmentTapped(object sender, Syncfusion.UI.Xaml.Scheduler.AppointmentTappedArgs e)
        {
            KalendarViewModel kalendarViewModel = (KalendarViewModel) DataContext;
            kalendarViewModel.IzabranTermin = (TerminDto) e.Appointment?.Data;
        }
    }
}
