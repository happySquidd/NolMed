using NolMed.database;
using System.Configuration;
using System.Data;
using System.Windows;

namespace NolMed
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RedisService Redis {get; private set;}

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Redis = new RedisService("localhost:6379");
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Redis?.Dispose();
            base.OnExit(e);
        }
    }   

}
