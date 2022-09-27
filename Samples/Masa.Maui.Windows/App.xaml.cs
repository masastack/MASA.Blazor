using Application = Microsoft.Maui.Controls.Application;

namespace Masa.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}