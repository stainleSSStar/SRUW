using System;
using System.Collections.Generic;
using System.Configuration;
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


        private void CW_Status_F_Modify_User(object sender, RoutedEventArgs e)
        {
            CW_Status_Pesel_Field.IsReadOnly = false;
            CW_Status_Pesel_Field.BorderBrush = Brushes.Green;
            CW_Status_Name_Field.IsReadOnly = false;
            CW_Status_Name_Field.BorderBrush = Brushes.Green;
            CW_Status_Email_Field.IsReadOnly = false;
            CW_Status_Email_Field.BorderBrush = Brushes.Green;
            CW_Status_Address1_Field.IsReadOnly = false;
            CW_Status_Address1_Field.BorderBrush = Brushes.Green;
            CW_Status_Address2_Field.IsReadOnly = false;
            CW_Status_Address2_Field.BorderBrush = Brushes.Green;
            CW_Status_Polish_Field.IsReadOnly = false;
            CW_Status_Polish_Field.BorderBrush = Brushes.Green;
            CW_Status_Maths_Field.IsReadOnly = false;
            CW_Status_Maths_Field.BorderBrush = Brushes.Green;
            CW_Status_English_Field.IsReadOnly = false;
            CW_Status_English_Field.BorderBrush = Brushes.Green;
            CW_Status_Pol1Add_Field.IsReadOnly = false;
            CW_Status_Pol1Add_Field.BorderBrush = Brushes.Green;
            CW_Status_Mat2Add_Field.IsReadOnly = false;
            CW_Status_Mat2Add_Field.BorderBrush = Brushes.Green;
            CW_Status_Eng3Add_Field.IsReadOnly = false;
            CW_Status_Eng3Add_Field.BorderBrush = Brushes.Green;
            CW_Status_Modify_Lock.Visibility = Visibility.Visible;
            CW_Status_Modify_UnLock.Visibility = Visibility.Hidden;
        }

        private void CW_Status_F_Modify_User_Locker(object sender, RoutedEventArgs e)
        {
            int peselcorrector = 0;
            int namecorrector = 0;
            int emailcorrector = 0;
            int address1corrector = 0;
            int address2corrector = 0;
            int polishcorrector = 0;
            int mathscorrector = 0;
            int englishcorrector = 0;
            int pol1addcorrector = 0;
            int mat2addcorrector = 0;
            int eng3addcorrector = 0;

            if (CW_Status_Pesel_Field.Text == "")
            {
                CW_Status_Pesel_Field.Text = "";
                peselcorrector = 0;
            }
            else
            {
                try
                {
                    String peselinput = CW_Status_Pesel_Field.Text;
                    int length = peselinput.Length;
                    if (length == 11)
                    {
                        double isanumber = Convert.ToDouble(peselinput);
                        peselcorrector = 1;
                    }
                    else
                    {
                        CW_Status_Pesel_Field.Text = "";
                        peselcorrector = 0;
                    }
                }
                catch (Exception exception)
                {
                    CW_Status_Pesel_Field.Text = "";
                    peselcorrector = 0;
                };
            }
            //napraw

            if (CW_Status_Name_Field.Text == "")
            {
                CW_Status_Name_Field.Text = "";
                namecorrector = 0;
            }
            else
            {
                String nameinput = CW_Status_Name_Field.Text;
                int foundq = nameinput.IndexOf(" ");
                if (foundq == -1)
                {
                    CW_Status_Name_Field.Text = "";
                    namecorrector = 0;
                }
                else
                {
                    namecorrector = 1;
                }
            }

            if (CW_Status_Email_Field.Text == "")
            {
                CW_Status_Email_Field.Text = "";
                emailcorrector = 0;
            }
            else
            {
                String emailinput = CW_Status_Email_Field.Text;
                int foundq = emailinput.IndexOf("@");
                if (foundq == -1)
                {
                    CW_Status_Email_Field.Text = "";
                    emailcorrector = 0;
                }
                else
                {
                    emailcorrector = 1;
                }
            }

            if (CW_Status_Address1_Field.Text == "")
            {
                CW_Status_Address1_Field.Text = "";
                address1corrector = 0;
            }
            else
            {
                address1corrector = 1;
            }

            if (CW_Status_Address2_Field.Text == "")
            {
                CW_Status_Address2_Field.Text = "";
                address2corrector = 0;
            }
            else
            {
                address2corrector = 1;
            }




            String input = CW_Status_Polish_Field.Text;
            try
            {
                int inputvalue = Convert.ToInt32(input);
                if (inputvalue > 100)
                {
                    CW_Status_Polish_Field.Text = "";
                    polishcorrector = 0;
                }
                else if (inputvalue < 0)
                {
                    CW_Status_Polish_Field.Text = "";
                    polishcorrector = 0;
                }
                else if (inputvalue == 000 || inputvalue == 00)
                {
                    CW_Status_Polish_Field.Text = "0";
                    polishcorrector = 1;
                }
                else
                {
                    polishcorrector = 1;
                }
            }
            catch (Exception exception)
            {
                CW_Status_Polish_Field.Text = "";
                polishcorrector = 0;
            };

            input = CW_Status_Maths_Field.Text;
            try
            {
                int inputvalue = Convert.ToInt32(input);
                if (inputvalue > 100)
                {
                    CW_Status_Maths_Field.Text = "";
                    mathscorrector = 0;
                }
                else if (inputvalue < 0)
                {
                    CW_Status_Maths_Field.Text = "";
                    mathscorrector = 0;
                }
                else if (inputvalue == 000 || inputvalue == 00)
                {
                    CW_Status_Maths_Field.Text = "0";
                    mathscorrector = 1;
                }
                else
                {
                    mathscorrector = 1;
                }
            }
            catch (Exception exception)
            {
                CW_Status_Maths_Field.Text = "";
                mathscorrector = 0;
            };

            input = CW_Status_English_Field.Text;
            try
            {
                int inputvalue = Convert.ToInt32(input);
                if (inputvalue > 100)
                {
                    CW_Status_English_Field.Text = "";
                    englishcorrector = 0;
                }
                else if (inputvalue < 0)
                {
                    CW_Status_English_Field.Text = "";
                    englishcorrector = 0;
                }
                else if (inputvalue == 000 || inputvalue == 00)
                {
                    CW_Status_English_Field.Text = "0";
                    englishcorrector = 1;
                }
                else
                {
                    englishcorrector = 1;
                }
            }
            catch (Exception exception)
            {
                CW_Status_English_Field.Text = "";
                englishcorrector = 0;
            };

            input = CW_Status_Pol1Add_Field.Text;
            try
            {
                int inputvalue = Convert.ToInt32(input);
                if (inputvalue > 100)
                {
                    CW_Status_Pol1Add_Field.Text = "";
                    pol1addcorrector = 0;
                }
                else if (inputvalue < 0)
                {
                    CW_Status_Pol1Add_Field.Text = "";
                    pol1addcorrector = 0;
                }
                else if (inputvalue == 000 || inputvalue == 00)
                {
                    CW_Status_Pol1Add_Field.Text = "0";
                    pol1addcorrector = 1;
                }
                else
                {
                    pol1addcorrector = 1;
                }
            }
            catch (Exception exception)
            {
                CW_Status_Pol1Add_Field.Text = "";
                pol1addcorrector = 0;
            };

            input = CW_Status_Mat2Add_Field.Text;
            try
            {
                int inputvalue = Convert.ToInt32(input);
                if (inputvalue > 100)
                {
                    CW_Status_Mat2Add_Field.Text = "";
                    mat2addcorrector = 0;
                }
                else if (inputvalue < 0)
                {
                    CW_Status_Mat2Add_Field.Text = "";
                    mat2addcorrector = 0;
                }
                else if (inputvalue == 000 || inputvalue == 00)
                {
                    CW_Status_Mat2Add_Field.Text = "0";
                    mat2addcorrector = 1;
                }
                else
                {
                    mat2addcorrector = 1;
                }
            }
            catch (Exception exception)
            {
                CW_Status_Mat2Add_Field.Text = "";
                mat2addcorrector = 0;
            };

            input = CW_Status_Eng3Add_Field.Text;
            try
            {
                int inputvalue = Convert.ToInt32(input);
                if (inputvalue > 100)
                {
                    CW_Status_Eng3Add_Field.Text = "";
                    eng3addcorrector = 0;
                }
                else if (inputvalue < 0)
                {
                    CW_Status_Eng3Add_Field.Text = "";
                    eng3addcorrector = 0;
                }
                else if (inputvalue == 000 || inputvalue == 00)
                {
                    CW_Status_Eng3Add_Field.Text = "0";
                    eng3addcorrector = 1;
                }
                else
                {
                    eng3addcorrector = 1;
                }
            }
            catch (Exception exception)
            {
                CW_Status_Eng3Add_Field.Text = "";
                eng3addcorrector = 0;
            };


            if(peselcorrector == 0 || namecorrector == 0 || emailcorrector == 0 || address1corrector == 0 || address2corrector == 0 || polishcorrector == 0 || mathscorrector == 0 || englishcorrector == 0 || address1corrector == 0 || pol1addcorrector == 0 || mat2addcorrector == 0 || eng3addcorrector == 0)
            {
                var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Status"));
                existingWindow.Activate();
                MessageBox.Show("Niektóre pola nie przeszły testów walidacyjnych i zostały wyczyszczone przez system bądź zostały pozostawione puste. Wypełnij je poprawnie.", "SRUW - Modyfikacja Profilu", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                DB_Resolver updaterconnection = new DB_Resolver();
                updaterconnection.DB_ConnectionOpener();
                if (updaterconnection.DB_ConnectionChecker())
                {
                    CW_Status_Pesel_Field.IsReadOnly = true;
                    CW_Status_Pesel_Field.BorderBrush = Brushes.Gray;
                    CW_Status_Name_Field.IsReadOnly = true;
                    CW_Status_Name_Field.BorderBrush = Brushes.Gray;
                    CW_Status_Email_Field.IsReadOnly = true;
                    CW_Status_Email_Field.BorderBrush = Brushes.Gray;
                    CW_Status_Address1_Field.IsReadOnly = true;
                    CW_Status_Address1_Field.BorderBrush = Brushes.Gray;
                    CW_Status_Address2_Field.IsReadOnly = true;
                    CW_Status_Address2_Field.BorderBrush = Brushes.Gray;
                    CW_Status_Polish_Field.IsReadOnly = true;
                    CW_Status_Polish_Field.BorderBrush = Brushes.Gray;
                    CW_Status_Maths_Field.IsReadOnly = true;
                    CW_Status_Maths_Field.BorderBrush = Brushes.Gray;
                    CW_Status_English_Field.IsReadOnly = true;
                    CW_Status_English_Field.BorderBrush = Brushes.Gray;
                    CW_Status_Pol1Add_Field.IsReadOnly = true;
                    CW_Status_Pol1Add_Field.BorderBrush = Brushes.Gray;
                    CW_Status_Mat2Add_Field.IsReadOnly = true;
                    CW_Status_Mat2Add_Field.BorderBrush = Brushes.Gray;
                    CW_Status_Eng3Add_Field.IsReadOnly = true;
                    CW_Status_Eng3Add_Field.BorderBrush = Brushes.Gray;
                    CW_Status_Modify_Lock.Visibility = Visibility.Hidden;
                    CW_Status_Modify_UnLock.Visibility = Visibility.Visible;
                    updaterconnection.DB_Resolver_Status_Modify_Updater(CW_Status_Pesel_Field, CW_Status_Name_Field, CW_Status_Email_Field, CW_Status_Address1_Field, CW_Status_Address2_Field, CW_Status_Polish_Field, CW_Status_Maths_Field, CW_Status_English_Field, CW_Status_Pol1Add_Field, CW_Status_Mat2Add_Field, CW_Status_Eng3Add_Field, usedid);
                    updaterconnection.DB_Resolver_Status_Queue(CW_Status_Pesel_Field, CW_Status_Name_Field, CW_Status_Email_Field, CW_Status_Address1_Field, CW_Status_Address2_Field, CW_Status_Polish_Field, CW_Status_Maths_Field, CW_Status_English_Field, CW_Status_Pol1Add_Field, CW_Status_Mat2Add_Field, CW_Status_Eng3Add_Field, CW_Status_Route_Field, CW_Status_Status_Field, CW_Status_Points_Field, usedid);
                }
                else
                {
                    var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Status"));
                    if (existingWindow != null)
                    {
                        existingWindow.WindowState = WindowState.Normal;
                        existingWindow.Activate();
                    }
                    MessageBox.Show("Błąd połączenia z bazą danych systemu. Napewno posiadasz połączenie z internetem?", "SRUW - Błąd Połączenia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
