using System;
using System.Windows;
using System.Windows.Threading;
using Controller;
using Model;
namespace WPF_Applicatie

{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Data.Initialize();
            Data.NextRaceEvent += OnNextRaceEvent;
            Data.NextRace();
        }

        private void OnDriversChanged(object o, DriversChangedEventArgs e)
        {
            TrackImage.Dispatcher.BeginInvoke(
            DispatcherPriority.Render,
            new Action(() =>
            {
                TrackImage.Source = null;
                TrackImage.Source = WPFVisualization.DrawTrack(e.Track);
            }));
        }

        private void OnNextRaceEvent(object o, NextRaceEventArgs e)
        {
            ImageClass.Dispose();

            if (e.race != null)
            {
                WPFVisualization.Initialize(e.race);

                e.race.DriversChanged += OnDriversChanged!;
            }

        }
    }
}

