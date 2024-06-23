using Microsoft.Maui.Controls;

namespace Ranksterr.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }
}
