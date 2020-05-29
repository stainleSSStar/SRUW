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
            if (dbinterface.DB_ConnectionChecker())
            {
                var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Logowanie"));

                if (existingWindow == null)
                {
                    Window ChildWindow_Login = new ChildWindow_Login();
                    ChildWindow_Login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    ChildWindow_Login.Owner = this;
                    ChildWindow_Login.Show();
                }
                else
                {
                    existingWindow.WindowState = WindowState.Normal;
                    existingWindow.Activate();
                }
            }
            else
            {
                MessageBox.Show("Błąd połączenia z bazą danych systemu. Napewno posiadasz połączenie z internetem?", "SRUW - Błąd Połączenia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void MW_F_Register(object sender, RoutedEventArgs e)
        {
            DB_Resolver dbinterface = new DB_Resolver();
            dbinterface.DB_ConnectionOpener();
            if (dbinterface.DB_ConnectionChecker())
            {
                var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Rejestracja"));

                if (existingWindow == null)
                {
                    Window ChildWindow_Register = new ChildWindow_Register();
                    ChildWindow_Register.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    ChildWindow_Register.Owner = this;
                    ChildWindow_Register.Show();
                }
                else
                {
                    existingWindow.WindowState = WindowState.Normal;
                    existingWindow.Activate();
                }
            }
            else
            {
                MessageBox.Show("Błąd połączenia z bazą danych systemu. Napewno posiadasz połączenie z internetem?", "SRUW - Błąd Połączenia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
