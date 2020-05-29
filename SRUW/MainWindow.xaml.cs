using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SRUW
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            String processname = Process.GetCurrentProcess().ProcessName;
            Process[] prorun = Process.GetProcessesByName(processname);
            if (prorun.Length > 1)
            {
                MessageBox.Show("Aplikacja jest już uruchomiona", "SRUW - Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
            else
            {
                InitializeComponent();
            }
        }
        private void MW_F_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void MW_F_Changes(object sender, RoutedEventArgs e)
        {
            var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Informacje o aplikacji"));

            if (existingWindow == null)
            {
                Window ChildWindow_SysInf = new ChildWindow_SysInf();
                ChildWindow_SysInf.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                ChildWindow_SysInf.Owner = this;
                ChildWindow_SysInf.Show();
            }
            else
            {

                existingWindow.WindowState = WindowState.Normal;
                existingWindow.Activate();
            }
        }
        private void MW_F_Login(object sender, RoutedEventArgs e)
        {
            DB_Resolver dbinterface = new DB_Resolver();
            dbinterface.DB_ConnectionOpener();
        }
    }
}
