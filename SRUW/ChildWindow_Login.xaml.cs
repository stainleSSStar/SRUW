using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Windows.Shapes;

namespace SRUW
{
    /// <summary>
    /// Logika interakcji dla klasy ChildWindow_Login.xaml
    /// </summary>
    public partial class ChildWindow_Login : Window
    {
        public ChildWindow_Login()
        {
            InitializeComponent();
        }
        private void CW_Login_F_Close(object sender, RoutedEventArgs e)
        {
            Close();
            var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("System Rekrutacji Uczelni Wyższych"));
            existingWindow.Activate();
        }
        private void CW_Login_F_Login_Attempt(object sender, RoutedEventArgs e)
        {
            DB_Resolver dbinterface = new DB_Resolver();
            dbinterface.DB_ConnectionOpener();
            if (dbinterface.DB_ConnectionChecker())
            {
                var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Status"));
                var existingWindowadmin = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Panel Administratora"));

                //Sprawdzenie poswiadczen

                if (existingWindow == null && existingWindowadmin == null)
                {
                    // zaleznosc od poswiadczen
                    Hide();
                    Window ChildWindow_Status = new ChildWindow_Status();
                    ChildWindow_Status.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    ChildWindow_Status.Owner = this;
                    ChildWindow_Status.Show();
                }
                else
                {
                    if (existingWindow != null)
                    {
                        existingWindow.WindowState = WindowState.Normal;
                        existingWindow.Activate();
                    }
                    if (existingWindowadmin != null)
                    {
                        existingWindowadmin.WindowState = WindowState.Normal;
                        existingWindowadmin.Activate();
                    }
                }
            }
            else
            {
                var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Logowanie"));
                if (existingWindow != null)
                {
                    existingWindow.WindowState = WindowState.Normal;
                    existingWindow.Activate();
                }
                else {
                    var existingWindowbackup = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("System Rekrutacji Uczelni Wyższych"));
                    existingWindowbackup.WindowState = WindowState.Normal;
                    existingWindowbackup.Activate();
                }
                MessageBox.Show("Błąd połączenia z bazą danych systemu. Napewno posiadasz połączenie z internetem?", "SRUW - Błąd Połączenia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
