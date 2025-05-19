using BookDataLib;
using BookDataLib.Model;
using HelperLib.Controllers;
using HelperLib.Events;
using HelperLib.Factories;
using HelperLib.Helpers;
using HelperLib.Repositories;
using HelperLib.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows;
using TryBookDataLib.Layout;
using TryBookDataLib.ViewModels;
using TryBookDataLib.Views;

namespace TryBookDataLib
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var assembly = AssemblyHelper.GetAssemblyInfo();
            ServiceLocator.Initialize();

            Controller controller = new Controller();
            controller.Initialize(assembly.ManifestModule.Name, false);
            ServiceLocator.AddInstance<Controller>(controller);

            EventAggregator2 eventAggregator = new EventAggregator2();
            ServiceLocator.AddInstance<EventAggregator2>(eventAggregator);

            ProxyFactory proxyFactory = new ProxyFactory();
            ServiceLocator.AddInstance<ProxyFactory>(proxyFactory);

            var dbFactory = new BookDataDbContextFactory();
            var dbContext = dbFactory.CreateDbContext();

            ServiceLocator.AddInstance<BookDataDbContextFactory>(dbFactory);
            ServiceLocator.AddInstance<BookDataDbContext>(dbContext);

            var engine = new ForceDirectedLayoutEngine();
            ServiceLocator.AddInstance<ForceDirectedLayoutEngine>(engine);

            GraphViewModel vm = new GraphViewModel();
            ServiceLocator.AddInstance<GraphViewModel>(vm);


            ServiceLocator.Build();


            var path = Path.Combine(Environment.CurrentDirectory, "BookDataLib.DLL");
            Assembly assy = Assembly.LoadFrom(path);

            var product = AssemblyData.Attributes.Product;

            if (TryInitializeDbContext())
            {
                using (var repo = new Repository(ContextConfiguration.GetConnectionString(product, $"{product}DB"))) ;

                MainWindow win = new MainWindow();

                win.Closed += (object sender, EventArgs e) =>
                {
                    Application.Current.Shutdown();
                };

                string title = string.Format("{0} v{1} by {2}",
                    AssemblyData.Attributes.Product,
                    AssemblyData.Attributes.Version,
                    AssemblyData.Attributes.Company);
                win.Title = title;
                win.Show();
            }
        }


        static bool TryInitializeDbContext()
        {
            try
            {
                var dbContext = ServiceLocator.Get<BookDataDbContext>();

                // Create the view manually
                var sql = @"SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'dbo.VRelsView')";
                var rowsAffected = dbContext.Database.ExecuteSqlRaw(sql);
                if (rowsAffected < 0)
                {
                    VRelsView.CreateVRelsView();
                    VRelsView.CreateRelsTreeView(); 
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                Application.Current.Shutdown();
                return false;
            }
        }
    }

}
