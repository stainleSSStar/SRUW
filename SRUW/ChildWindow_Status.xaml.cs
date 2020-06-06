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
            DB_Resolver informationpreload = new DB_Resolver();
            informationpreload.DB_Resolver_Status_Queue(CW_Status_Pesel_Field,CW_Status_Name_Field,CW_Status_Email_Field,CW_Status_Address1_Field,CW_Status_Address2_Field,CW_Status_Polish_Field,CW_Status_Maths_Field,CW_Status_English_Field,CW_Status_Pol1Add_Field,CW_Status_Mat2Add_Field,CW_Status_Eng3Add_Field,CW_Status_Route_Field,CW_Status_Status_Field,CW_Status_Points_Field, usedid);
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
