using Controller;
using System.Windows;

namespace WPF;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var currentRace = Data.CurrentRace;
    }
}
