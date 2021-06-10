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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace InformacioniSistemBolnice.Views.UpravnikView
{
    /// <summary>
    /// Interaction logic for Demo.xaml
    /// </summary>
    public partial class Demo : Page
    {
        public Demo()
        {
            InitializeComponent();
        }
        private List<BitmapImage> Images = new List<BitmapImage>();
        private int ImageNumber = 0;
        private DispatcherTimer PictureTimer = new DispatcherTimer();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Images.Add(new BitmapImage(new Uri("../images/DemoSlike/prva.png", UriKind.Relative)));
            Images.Add(new BitmapImage(new Uri("../images/DemoSlike/druga.png", UriKind.Relative)));
            Images.Add(new BitmapImage(new Uri("../images/DemoSlike/treca.png", UriKind.Relative)));
            Images.Add(new BitmapImage(new Uri("../images/DemoSlike/cetvrta.png", UriKind.Relative)));
            Images.Add(new BitmapImage(new Uri("../images/DemoSlike/peta.png", UriKind.Relative)));
            Images.Add(new BitmapImage(new Uri("../images/DemoSlike/sesta.png", UriKind.Relative)));
            Images.Add(new BitmapImage(new Uri("../images/DemoSlike/sedma.png", UriKind.Relative)));

            // Display the first image.
            Slika.Source = Images[0];

            // Install a timer to show each image.
            PictureTimer.Interval = TimeSpan.FromSeconds(8);
            PictureTimer.Tick += Tick;
            PictureTimer.Start();
        }

        private void Tick(object sender, System.EventArgs e)
        {
            if (ImageNumber == 6)
            {
                PictureTimer.Stop();
            }
            else
            {
                ImageNumber++;
                Slika.Source = Images[ImageNumber];
            }
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MeniProzor());
        }
    }

}
