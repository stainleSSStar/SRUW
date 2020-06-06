using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Configuration;
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
            String usedlogin = CW_Login_Pesel_Field.Text;
            String usedauthcode = CW_Login_AuthCode_Field.Password;
            String loginstatus = "";
            String logintype = "";
            int loginid = 0;
            DB_Resolver connection = new DB_Resolver();
            connection.DB_ConnectionOpener();
            if(connection.DB_ConnectionChecker()) {
            connection.DB_Resolver_Login_Queue(usedlogin,usedauthcode,out loginstatus,out logintype,out loginid);
            if(loginstatus == "OUT")
            {
                var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Logowanie"));
                    existingWindow.WindowState = WindowState.Normal;
                    existingWindow.Activate();
            }
            else
            {
                if(logintype == "STANDARD")
                {
                    Hide();
                    var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Status"));

                    if (existingWindow == null)
                    {
                        Window ChildWindow_Status = new ChildWindow_Status(loginid);
                        ChildWindow_Status.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        ChildWindow_Status.Owner = this;
                        ChildWindow_Status.Show();
                    }
                    else
                    {
                        existingWindow.WindowState = WindowState.Normal;
                        existingWindow.Activate();
                    }
                }
                else
                {
                    Hide();
                    var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Panel Administratora"));

                    if (existingWindow == null)
                    {
                        Window ChildWindow_AdmCtr = new ChildWindow_AdmCtr(loginid);
                        ChildWindow_AdmCtr.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        ChildWindow_AdmCtr.Owner = this;
                        ChildWindow_AdmCtr.Show();
                    }
                    else
                    {
                        existingWindow.WindowState = WindowState.Normal;
                        existingWindow.Activate();
                    }
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
                MessageBox.Show("Błąd połączenia z bazą danych systemu. Napewno posiadasz połączenie z internetem?", "SRUW - Błąd Połączenia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CW_Login_Pesel_Field_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CW_Login_AuthCode_Field.Focus();
            }
        }

        private void CW_Login_AuthCode_Field_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CW_Login_F_Login_Attempt(sender, e);
            }
        }
    }
}
