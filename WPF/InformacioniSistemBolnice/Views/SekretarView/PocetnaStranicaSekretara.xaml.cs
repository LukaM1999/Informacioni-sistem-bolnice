using InformacioniSistemBolnice.ViewModels.SekretarViewModel;
using InformacioniSistemBolnice.Views.SekretarView;
using System.Windows;

namespace InformacioniSistemBolnice
{
    public partial class PocetnaStranicaSekretara : Window
    {
        public PocetnaStranicaSekretara()
        {
            InitializeComponent();
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e) => contentControl.Content = new UCMenuSekretara(this);

        private void OdjaviSe_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }

        private void SekretarovProfil_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new SekretarovProfil(this);
        }

        private void Feedback_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new Feedback() { DataContext = new FeedbackViewModel(this, new UCMenuSekretara(this)) }; ;
        }
    }
}
