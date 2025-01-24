namespace AgentWorld.Views;

using AgentWorld.ViewModels;
using Avalonia.Controls;


/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        this.DataContext = new MainViewModel();
    }
}
