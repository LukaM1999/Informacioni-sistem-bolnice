using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Win32;

namespace InformacioniSistemBolnice.Views.PacijentView
{
    public partial class PomocView : UserControl
    {
        private DispatcherTimer timer;
        bool isDragging;

        public PomocView()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(200);
            timer.Tick += new EventHandler(timer_Tick);
            Player.Source = new Uri(@"D:\Predavanja\Tutorijal.mp4", UriKind.Absolute);
            Player.Play();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!isDragging)
            {
                timelineSlider.Value = Player.Position.TotalSeconds;
            }
        }

        private void timelineSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Player.Position = TimeSpan.FromSeconds(timelineSlider.Value);
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Player.Play();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Player.Stop();
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            Player.Pause();
        }

        private void volumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Player.Volume = volumeSlider.Value;
        }

        private void Player_MediaEnded(object sender, RoutedEventArgs e)
        {
            Player.Stop();
        }

        private void timelineSlider_DragStarted(object sender, DragStartedEventArgs e)
        {
            isDragging = true;
        }

        private void timelineSlider_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            isDragging = false;
            Player.Position = TimeSpan.FromSeconds(timelineSlider.Value);
        }

        private void Player_MediaOpened(object sender, RoutedEventArgs e)
        {
            if (Player.NaturalDuration.HasTimeSpan)
            {
                TimeSpan ts = Player.NaturalDuration.TimeSpan;
                timelineSlider.Maximum = ts.TotalSeconds;
                timelineSlider.SmallChange = 1;
                timelineSlider.LargeChange = Math.Min(10, ts.Seconds / 10);
            }
            timer.Start();
        }
    }
}
