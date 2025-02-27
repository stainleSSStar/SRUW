﻿using System;
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
    /// Logika interakcji dla klasy ChildWindow_Register.xaml
    /// </summary>
    public partial class ChildWindow_Register : Window
    {
        int emailcorrector = 0;
        int namecorrector = 0;
        int peselcorrector = 0;
        public ChildWindow_Register()
        {
            InitializeComponent();
            CW_Register_ComboBoxFiller();
        }
        private void CW_Register_F_Close(object sender, RoutedEventArgs e)
        {
            Close();
            var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("System Rekrutacji Uczelni Wyższych"));
            existingWindow.Activate();
        }

        private void CW_Register_ComboBoxFiller()
        {
            DB_Resolver comboboxinitializer = new DB_Resolver();
            comboboxinitializer.DB_ConnectionComboBoxUniver(CW_Register_University_ComboBox);
            comboboxinitializer.DB_ConnectionComboBoxUniverAvail(CW_Register_University_Available_ComboBox);
        }

        private void emailcorrector_status_changer(int input)
        {
            emailcorrector = input;
        }
        private void namecorrector_status_changer(int input)
        {
            namecorrector = input;
        }
        private void peselcorrector_status_changer(int input)
        {
            peselcorrector = input;
        }

        private void CW_Register_Email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CW_Register_Email_Field.Text == "")
                {
                    CW_Register_Email_Field.Text = "";
                    emailcorrector_status_changer(0);
                }
                else
                {
                    String emailinput = CW_Register_Email_Field.Text;
                    int foundq = emailinput.IndexOf("@");
                    if (foundq == -1)
                    {
                        CW_Register_emailcheck_Label.Content = "Wprowadzony adres email wygląda na niepoprawny.";
                        emailcorrector_status_changer(0);
                    }
                    else
                    {
                        //Leave As Is
                        CW_Register_emailcheck_Label.Content = "";
                        emailcorrector_status_changer(1);
                    }
                }
            }
        }
        private void CW_Register_Email_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CW_Register_Email_Field.Text == "")
            {
                CW_Register_Email_Field.Text = "";
                emailcorrector_status_changer(0);
            }
            else
            {
                String emailinput = CW_Register_Email_Field.Text;
                int foundq = emailinput.IndexOf("@");
                if (foundq == -1)
                {
                    CW_Register_emailcheck_Label.Content = "Wprowadzony adres email wygląda na niepoprawny.";
                    emailcorrector_status_changer(0);
                }
                else
                {
                    //Leave As Is
                    CW_Register_emailcheck_Label.Content = "";
                    emailcorrector_status_changer(1);
                }
            }
        }

        private void CW_Register_Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CW_Register_Name_Field.Text == "")
                {
                    CW_Register_Name_Field.Text = "";
                    namecorrector_status_changer(0);
                }
                else
                {
                    String nameinput = CW_Register_Name_Field.Text;
                    int foundq = nameinput.IndexOf(" ");
                    if (foundq == -1)
                    {
                        CW_Register_namecheck_Label.Content = "Wprowadzone dane wyglądają na niepoprawne.";
                        namecorrector_status_changer(0);
                    }
                    else
                    {
                        //Leave As Is
                        CW_Register_namecheck_Label.Content = "";
                        namecorrector_status_changer(1);
                    }
                }
            }
        }
        private void CW_Register_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CW_Register_Name_Field.Text == "")
            {
                CW_Register_Name_Field.Text = "";
                namecorrector_status_changer(0);
            }
            else
            {
                String nameinput = CW_Register_Name_Field.Text;
                int foundq = nameinput.IndexOf(" ");
                if (foundq == -1)
                {
                    CW_Register_namecheck_Label.Content = "Wprowadzone dane wyglądają na niepoprawne.";
                    namecorrector_status_changer(0);
                }
                else
                {
                    //Leave As Is
                    CW_Register_namecheck_Label.Content = "";
                    namecorrector_status_changer(1);
                }
            }
        }

        private void CW_Register_Pesel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (CW_Register_Pesel_Field.Text == "")
                {
                    CW_Register_Pesel_Field.Text = "";
                    peselcorrector_status_changer(0);
                }
                else
                {
                    try
                    {
                        String peselinput = CW_Register_Pesel_Field.Text;
                        int length = peselinput.Length;
                        if (length == 11)
                        {
                            double isanumber = Convert.ToDouble(peselinput);
                            CW_Register_peselcheck_Label.Content = "";
                            peselcorrector_status_changer(1);
                        }
                        else
                        {
                            //Leave As Is
                            CW_Register_peselcheck_Label.Content = "Wprowadzony numer PESEL jest niepoprawny.";
                            peselcorrector_status_changer(0);
                        }
                    }
                    catch(Exception exception) {
                        CW_Register_peselcheck_Label.Content = "Wprowadzony numer PESEL jest niepoprawny.";
                        peselcorrector_status_changer(0);
                    };
                }
            }
        }
        private void CW_Register_Pesel_LostFocus(object sender, RoutedEventArgs e)
        {
            if (CW_Register_Pesel_Field.Text == "")
            {
                CW_Register_Pesel_Field.Text = "";
                peselcorrector_status_changer(0);
            }
            else
            {
                try
                {
                    String peselinput = CW_Register_Pesel_Field.Text;
                    int length = peselinput.Length;
                    if (length == 11)
                    {
                        double isanumber = Convert.ToDouble(peselinput);
                        CW_Register_peselcheck_Label.Content = "";
                        peselcorrector_status_changer(1);
                    }
                    else
                    {
                        //Leave As Is
                        CW_Register_peselcheck_Label.Content = "Wprowadzony numer PESEL jest niepoprawny.";
                        peselcorrector_status_changer(0);
                    }
                }
                catch (Exception exception)
                {
                    CW_Register_peselcheck_Label.Content = "Wprowadzony numer PESEL jest niepoprawny.";
                    peselcorrector_status_changer(0);
                };
            }
        }

        private void CW_Register_PrecentageModifier_Polish_LostFocus(object sender, RoutedEventArgs e)
        {
            CW_Register_precentagecheck_Label.Content = "";
            String input = CW_Register_polish_Field.Text;
            try
            {
                int inputvalue = Convert.ToInt32(input);
                if (inputvalue > 100)
                {
                    CW_Register_polish_Field.Text = "100";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                }
                else if (inputvalue < 0)
                {
                    CW_Register_polish_Field.Text = "0";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                }
                else if (inputvalue == 000 || inputvalue == 00)
                {
                    CW_Register_polish_Field.Text = "0";
                }
                else
                {
                    //EXCEPTION OCCURED
                }
            }
            catch (Exception exception)
            {
                CW_Register_polish_Field.Text = "";
                CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
            };
        }
        private void CW_Register_PrecentageModifier_Polish_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CW_Register_precentagecheck_Label.Content = "";
                String input = CW_Register_polish_Field.Text;
                try
                {
                    int inputvalue = Convert.ToInt32(input);
                    if (inputvalue > 100)
                    {
                        CW_Register_polish_Field.Text = "100";
                        CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                    }
                    else if (inputvalue < 0)
                    {
                        CW_Register_polish_Field.Text = "0";
                        CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                    }
                    else if (inputvalue == 000 || inputvalue == 00)
                    {
                        CW_Register_polish_Field.Text = "0";
                    }
                    else
                    {
                        //EXCEPTION OCCURED
                    }
                }
                catch (Exception exception)
                {
                    CW_Register_polish_Field.Text = "";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                };
            }
        }

        private void CW_Register_PrecentageModifier_Maths_LostFocus(object sender, RoutedEventArgs e)
        {
            CW_Register_precentagecheck_Label.Content = "";
            String input = CW_Register_maths_Field.Text;
            try
            {
                int inputvalue = Convert.ToInt32(input);
                if (inputvalue > 100)
                {
                    CW_Register_maths_Field.Text = "100";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                }
                else if (inputvalue < 0)
                {
                    CW_Register_maths_Field.Text = "0";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                }
                else if (inputvalue == 000 || inputvalue == 00)
                {
                    CW_Register_maths_Field.Text = "0";
                }
                else
                {
                    //EXCEPTION OCCURED
                }
            }
            catch (Exception exception)
            {
                CW_Register_maths_Field.Text = "";
                CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
            };
        }
        private void CW_Register_PrecentageModifier_Maths_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CW_Register_precentagecheck_Label.Content = "";
                String input = CW_Register_maths_Field.Text;
                try
                {
                    int inputvalue = Convert.ToInt32(input);
                    if (inputvalue > 100)
                    {
                        CW_Register_maths_Field.Text = "100";
                        CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                    }
                    else if (inputvalue < 0)
                    {
                        CW_Register_maths_Field.Text = "0";
                        CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                    }
                    else if (inputvalue == 000 || inputvalue == 00)
                    {
                        CW_Register_maths_Field.Text = "0";
                    }
                    else
                    {
                        //EXCEPTION OCCURED
                    }
                }
                catch (Exception exception)
                {
                    CW_Register_maths_Field.Text = "";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                };
            }
        }

        private void CW_Register_PrecentageModifier_English_LostFocus(object sender, RoutedEventArgs e)
        {
            CW_Register_precentagecheck_Label.Content = "";
            String input = CW_Register_english_Field.Text;
            try
            {
                int inputvalue = Convert.ToInt32(input);
                if (inputvalue > 100)
                {
                    CW_Register_english_Field.Text = "100";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                }
                else if (inputvalue < 0)
                {
                    CW_Register_english_Field.Text = "0";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                }
                else if (inputvalue == 000 || inputvalue == 00)
                {
                    CW_Register_english_Field.Text = "0";
                }
                else
                {
                    //EXCEPTION OCCURED
                }
            }
            catch (Exception exception)
            {
                CW_Register_english_Field.Text = "";
                CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
            };
        }
        private void CW_Register_PrecentageModifier_English_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CW_Register_precentagecheck_Label.Content = "";
                String input = CW_Register_english_Field.Text;
                try
                {
                    int inputvalue = Convert.ToInt32(input);
                    if (inputvalue > 100)
                    {
                        CW_Register_english_Field.Text = "100";
                        CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                    }
                    else if (inputvalue < 0)
                    {
                        CW_Register_english_Field.Text = "0";
                        CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                    }
                    else if (inputvalue == 000 || inputvalue == 00)
                    {
                        CW_Register_english_Field.Text = "0";
                    }
                    else
                    {
                        //EXCEPTION OCCURED
                    }
                }
                catch (Exception exception)
                {
                    CW_Register_english_Field.Text = "";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                };
            }
        }

        private void CW_Register_PrecentageModifier_Add1Pol_LostFocus(object sender, RoutedEventArgs e)
        {
            CW_Register_precentagecheck_Label.Content = "";
            String input = CW_Register_add1pol_Field.Text;
            try
            {
                int inputvalue = Convert.ToInt32(input);
                if (inputvalue > 100)
                {
                    CW_Register_add1pol_Field.Text = "100";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                }
                else if (inputvalue < 0)
                {
                    CW_Register_add1pol_Field.Text = "0";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                }
                else if (inputvalue == 000 || inputvalue == 00)
                {
                    CW_Register_add1pol_Field.Text = "0";
                }
                else
                {
                    //EXCEPTION OCCURED
                }
            }
            catch (Exception exception)
            {
                CW_Register_add1pol_Field.Text = "";
                CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
            };
        }
        private void CW_Register_PrecentageModifier_Add1Pol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CW_Register_precentagecheck_Label.Content = "";
                String input = CW_Register_add1pol_Field.Text;
                try
                {
                    int inputvalue = Convert.ToInt32(input);
                    if (inputvalue > 100)
                    {
                        CW_Register_add1pol_Field.Text = "100";
                        CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                    }
                    else if (inputvalue < 0)
                    {
                        CW_Register_add1pol_Field.Text = "0";
                        CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                    }
                    else if (inputvalue == 000 || inputvalue == 00)
                    {
                        CW_Register_add1pol_Field.Text = "0";
                    }
                    else
                    {
                        //EXCEPTION OCCURED
                    }
                }
                catch (Exception exception)
                {
                    CW_Register_add1pol_Field.Text = "";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                };
            }
        }

        private void CW_Register_PrecentageModifier_Add2Mat_LostFocus(object sender, RoutedEventArgs e)
        {
            CW_Register_precentagecheck_Label.Content = "";
            String input = CW_Register_add2mat_Field.Text;
            try
            {
                int inputvalue = Convert.ToInt32(input);
                if (inputvalue > 100)
                {
                    CW_Register_add2mat_Field.Text = "100";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                }
                else if (inputvalue < 0)
                {
                    CW_Register_add2mat_Field.Text = "0";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                }
                else if (inputvalue == 000 || inputvalue == 00)
                {
                    CW_Register_add2mat_Field.Text = "0";
                }
                else
                {
                    //EXCEPTION OCCURED
                }
            }
            catch (Exception exception)
            {
                CW_Register_add2mat_Field.Text = "";
                CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
            };
        }
        private void CW_Register_PrecentageModifier_Add2Mat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CW_Register_precentagecheck_Label.Content = "";
                String input = CW_Register_add2mat_Field.Text;
                try
                {
                    int inputvalue = Convert.ToInt32(input);
                    if (inputvalue > 100)
                    {
                        CW_Register_add2mat_Field.Text = "100";
                        CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                    }
                    else if (inputvalue < 0)
                    {
                        CW_Register_add2mat_Field.Text = "0";
                        CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                    }
                    else if (inputvalue == 000 || inputvalue == 00)
                    {
                        CW_Register_add2mat_Field.Text = "0";
                    }
                    else
                    {
                        //EXCEPTION OCCURED
                    }
                }
                catch (Exception exception)
                {
                    CW_Register_add2mat_Field.Text = "";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                };
            }
        }

        private void CW_Register_PrecentageModifier_Add3Eng_LostFocus(object sender, RoutedEventArgs e)
        {
            CW_Register_precentagecheck_Label.Content = "";
            String input = CW_Register_add3eng_Field.Text;
            try
            {
                int inputvalue = Convert.ToInt32(input);
                if (inputvalue > 100)
                {
                    CW_Register_add3eng_Field.Text = "100";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                }
                else if (inputvalue < 0)
                {
                    CW_Register_add3eng_Field.Text = "0";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                }
                else if (inputvalue == 000 || inputvalue == 00)
                {
                    CW_Register_add3eng_Field.Text = "0";
                }
                else
                {
                    //EXCEPTION OCCURED
                }
            }
            catch (Exception exception)
            {
                CW_Register_add3eng_Field.Text = "";
                CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
            };
        }
        private void CW_Register_PrecentageModifier_Add3Eng_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CW_Register_precentagecheck_Label.Content = "";
                String input = CW_Register_add3eng_Field.Text;
                try
                {
                    int inputvalue = Convert.ToInt32(input);
                    if (inputvalue > 100)
                    {
                        CW_Register_add3eng_Field.Text = "100";
                        CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                    }
                    else if (inputvalue < 0)
                    {
                        CW_Register_add3eng_Field.Text = "0";
                        CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                    }
                    else if (inputvalue == 000 || inputvalue == 00)
                    {
                        CW_Register_add3eng_Field.Text = "0";
                    }
                    else
                    {
                        //EXCEPTION OCCURED
                    }
                }
                catch (Exception exception)
                {
                    CW_Register_add3eng_Field.Text = "";
                    CW_Register_precentagecheck_Label.Content = "UWAGA : Wartości procentowe zostały skorygowane przez system.";
                };
            }
        }

        private bool CW_Register_FieldsChecker()
        {
            if (CW_Register_Name_Field.Text != "" && CW_Register_Pesel_Field.Text != "" && CW_Register_Email_Field.Text != "" && CW_Register_Address1_Field.Text != "" && CW_Register_Address2_Field.Text != "" &&
                CW_Register_polish_Field.Text != "" && CW_Register_maths_Field.Text != "" && CW_Register_english_Field.Text != "" && CW_Register_add1pol_Field.Text != "" && CW_Register_add2mat_Field.Text != ""
                && CW_Register_add3eng_Field.Text != "")
            {
                if (emailcorrector == 0 || namecorrector == 0 || peselcorrector == 0 ) {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        private void CW_Register_F_RegisterInitializer(object sender, RoutedEventArgs e)
        {
            bool registerformstatus = CW_Register_FieldsChecker();
            if (registerformstatus == true) {
            DB_Resolver connection = new DB_Resolver();
            connection.DB_ConnectionOpener();
            if (connection.DB_ConnectionChecker() == true)
            {
                    MessageBoxResult Result = MessageBox.Show("Wszystko wygląda poprawnie. Kontynuować?", "SRUW - Akceptacja", MessageBoxButton.YesNo, MessageBoxImage.Information);
                    if (Result == MessageBoxResult.Yes)
                    {
                        connection.DB_ConnectionInsert(CW_Register_polish_Field, CW_Register_maths_Field, CW_Register_english_Field, CW_Register_add1pol_Field, CW_Register_add2mat_Field, CW_Register_add3eng_Field, CW_Register_Name_Field, CW_Register_Pesel_Field, CW_Register_Email_Field, CW_Register_Address1_Field, CW_Register_Address2_Field, CW_Register_University_ComboBox,CW_Register_University_Available_ComboBox);
                        MessageBoxResult Result2 = MessageBox.Show("Użytkownik został dodany do systemu." + System.Environment.NewLine +
                            "Twoje dane do logowania to : " + System.Environment.NewLine +
                            "Login : " + connection.usedlogin + System.Environment.NewLine +
                            "Hasło : " + connection.generatedpassword + System.Environment.NewLine +
                            "Skopiować dane do schowka systemowego?", "SRUW - Pomyślnie zarejestrowano", MessageBoxButton.YesNo, MessageBoxImage.Information);
                        if (Result2 == MessageBoxResult.Yes)
                        {
                            String SRUWCLIPB1= "==============================================" + System.Environment.NewLine;
                            String SRUWCLIPB2 = "SRUW - WYGENEROWANE POŚWIADCZENIA" + System.Environment.NewLine;
                            String SRUWCLIPB3 = "==============================================" + System.Environment.NewLine;
                            String SRUWCLIPB4 = "LOGIN : " + connection.usedlogin + System.Environment.NewLine;
                            String SRUWCLIPB5 = "HASŁO : " + connection.generatedpassword + System.Environment.NewLine;
                            String SRUWCLIPB6 = "==============================================" + System.Environment.NewLine;
                            Clipboard.SetText(SRUWCLIPB1 + SRUWCLIPB2 + SRUWCLIPB3 + SRUWCLIPB4 + SRUWCLIPB5 + SRUWCLIPB6);
                            MessageBox.Show("Pomyślnie skopiowano poświadczenia do schowka systemowego. Rejestracja Pomyślna.", "SRUW - Operacja pomyślna.", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                            var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("System Rekrutacji Uczelni Wyższych"));
                            existingWindow.Activate();
                        }
                        else if(Result2 == MessageBoxResult.No)
                        {
                            MessageBox.Show("Rejestracja Pomyślna. Powracanie do okna głównego.", "SRUW - Operacja pomyślna.", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                            var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("System Rekrutacji Uczelni Wyższych"));
                            existingWindow.Activate();
                        }
                        else
                        {
                            MessageBox.Show("Rejestracja Pomyślna. Powracanie do okna głównego.", "SRUW - Operacja pomyślna.", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                            var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("System Rekrutacji Uczelni Wyższych"));
                            existingWindow.Activate();
                        }
                    }
                    else if (Result == MessageBoxResult.No)
                    {
                        var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Rejestracja"));
                        existingWindow.Activate();
                    }
            }
            else
            {
                MessageBox.Show("Błąd połączenia z bazą danych systemu. Napewno posiadasz połączenie z internetem?", "SRUW - Błąd Połączenia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            }
            else
            {
                var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Rejestracja"));
                existingWindow.Activate();
                MessageBox.Show("Nie wszystkie pola są zapełnione lub poprawnie wypełnione.", "SRUW - Błąd Rejestracji", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void CW_Register_University_Available_Route_Switcher(object sender, RoutedEventArgs e)
        {
            DB_Resolver comboboxinitializer = new DB_Resolver();
            comboboxinitializer.DB_ConnectionComboBoxUniverAvailSwitch(CW_Register_University_Available_ComboBox, CW_Register_University_ComboBox);
        }
    }
}
