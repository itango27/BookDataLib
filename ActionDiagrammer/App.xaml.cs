using ActionDiagrammer.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace ActionDiagrammer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var win = new MainWindow();

            win.Closed += Win_Closed;
            win.Show();

        }

        private void Win_Closed(object? sender, EventArgs e)
        {
            Shutdown(); 
        }
    }

}
