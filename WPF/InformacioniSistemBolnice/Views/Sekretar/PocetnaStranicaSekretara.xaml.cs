using System.Windows;

namespace InformacioniSistemBolnice
{
    public partial class PocetnaStranicaSekretara : Window
    {
        public PocetnaStranicaSekretara()
        {
            InitializeComponent();   
        }

        private void MenuBtn_Click(object sender, RoutedEventArgs e)
             => contentControl.Content = new UCMenuSekretara(this);
    }
}
