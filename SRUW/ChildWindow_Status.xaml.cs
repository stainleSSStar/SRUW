using System;
using System.Collections.Generic;
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
    /// Logika interakcji dla klasy ChildWindow_Status.xaml
    /// </summary>
    public partial class ChildWindow_Status : Window
    {
        public int usedid;
        public ChildWindow_Status()
        {
            InitializeComponent();
        }
        public ChildWindow_Status(int usedid)
        {
            InitializeComponent();
            this.usedid = usedid;
        }

        private void CW_Status_F_Close(object sender, RoutedEventArgs e)
        {
            Close();
            var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Logowanie"));
            var existingWindowMain = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("System Rekrutacji Uczelni Wyższych"));
            existingWindow.Show();
        }


        private void CW_Status_F_Delete_User(object sender, RoutedEventArgs e)
        {
            MessageBoxResult Result = MessageBox.Show("Czy napewno chcesz usunąć wszystkie dane z systemu?", "SRUW - Usuwanie Użytkownika", MessageBoxButton.YesNo, MessageBoxImage.Information);
            if (Result == MessageBoxResult.Yes)
            {
                DB_Resolver connection = new DB_Resolver();
                connection.DB_ConnectionOpener();
                if (connection.DB_ConnectionChecker())
                {
                    connection.DB_Resolver_Status_User_Deletion(usedid);
                    Close();
                    var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("System Rekrutacji Uczelni Wyższych"));
                    existingWindow.WindowState = WindowState.Normal;
                    existingWindow.Activate();
                }
                else
                {
                    MessageBox.Show("Błąd połączenia z bazą danych systemu. Napewno posiadasz połączenie z internetem?", "SRUW - Błąd Połączenia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else if (Result == MessageBoxResult.No)
            {
                var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Status"));
                existingWindow.WindowState = WindowState.Normal;
                existingWindow.Activate();
            }
        }
    }
}
