using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace SRUW
{
    class DB_Resolver
    {
        private String db_connectionstring = "datasource = 127.0.0.1; port=3306;username=root;password=;database=sruwdb;";
        private String db_connectionstatus = "unknown";
        public String generatedpassword = "";
        public String usedlogin = "";
        public void DB_ConnectionOpener()
        {
            String db_networkingquery = "SELECT id from sruw_accounts";
            MySqlConnection dbconnectionobj = new MySqlConnection(db_connectionstring);
            MySqlCommand execcommand = new MySqlCommand(db_networkingquery, dbconnectionobj);
            execcommand.CommandTimeout = 30;
            MySqlDataReader datareader;
            try
            {
                dbconnectionobj.Open();
                datareader = execcommand.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        string[] row = { datareader.GetString(0) };
                        db_connectionstatus = "Online";
                    }
                }
                else
                {
                    MessageBox.Show("Baza systemu nie posiada rekordów", "SRUW - Błąd Bazodanowy", MessageBoxButton.OK, MessageBoxImage.Warning);
                    db_connectionstatus = "Online";
                }
                DB_ConnectionCloser(dbconnectionobj);
            }
            catch (Exception exception)
            {
                db_connectionstatus = "Offline";
            };
        }
        public void DB_ConnectionCloser(MySqlConnection obj) {
            obj.Close();
        }
        public bool DB_ConnectionChecker()
        {
            if (db_connectionstatus == "Online")
            {
                return true;
            }
            else if (db_connectionstatus == "Offline") {
                return false;
            }
            else
            {
                return false;
            }
        }

        public void DB_ConnectionComboBoxUniver(ComboBox cbname)
        {

            String db_comboboxpull = "SELECT id,name FROM sruw_univer WHERE id != 1";
            MySqlConnection dbconnectionobj = new MySqlConnection(db_connectionstring);
            MySqlCommand execcommand = new MySqlCommand(db_comboboxpull, dbconnectionobj);
            execcommand.CommandTimeout = 30;
            MySqlDataReader datareader;
            try
            {
                dbconnectionobj.Open();
                datareader = execcommand.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        string[] row = { datareader.GetString(0), datareader.GetString(1) };
                        cbname.Items.Add(datareader["name"]);
                    }
                }
                cbname.SelectedIndex = 0;
                DB_ConnectionCloser(dbconnectionobj);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Nie można załadować uczelni", "SRUW - Błąd Bazodanowy", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
        }

        public void DB_ConnectionComboBoxUniverAvail(ComboBox cbname)
        {
            cbname.Items.Clear();
            String db_comboboxpull = "SELECT id,name FROM sruw_univer_avail WHERE iduniver = 2";
            MySqlConnection dbconnectionobj = new MySqlConnection(db_connectionstring);
            MySqlCommand execcommand = new MySqlCommand(db_comboboxpull, dbconnectionobj);
            execcommand.CommandTimeout = 30;
            MySqlDataReader datareader;
            try
            {
                dbconnectionobj.Open();
                datareader = execcommand.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        string[] row = { datareader.GetString(0), datareader.GetString(1) };
                        cbname.Items.Add(datareader["name"]);
                    }
                }
                cbname.SelectedIndex = 0;
                DB_ConnectionCloser(dbconnectionobj);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Nie można załadować przedmiotów.", "SRUW - Błąd Bazodanowy", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
        }

        public void DB_ConnectionComboBoxUniverAvailSwitch(ComboBox cbname2, ComboBox cbname1)
        {
            cbname2.Items.Clear();
            String db_comboboxpull = "SELECT id,name FROM sruw_univer_avail WHERE iduniver = " + (cbname1.SelectedIndex + 2).ToString();
            MySqlConnection dbconnectionobj = new MySqlConnection(db_connectionstring);
            MySqlCommand execcommand = new MySqlCommand(db_comboboxpull, dbconnectionobj);
            execcommand.CommandTimeout = 30;
            MySqlDataReader datareader;
            try
            {
                dbconnectionobj.Open();
                datareader = execcommand.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        string[] row = { datareader.GetString(0), datareader.GetString(1) };
                        cbname2.Items.Add(datareader["name"]);
                    }
                }
                cbname2.SelectedIndex = 0;
                DB_ConnectionCloser(dbconnectionobj);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Nie można załadować przedmiotów.", "SRUW - Błąd Bazodanowy", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
        }
        public void DB_ConnectionInsert(TextBox polp, TextBox matp, TextBox engp, TextBox polpadd, TextBox matpadd, TextBox engpadd, TextBox name, TextBox pesel, TextBox email, TextBox address1, TextBox address2,ComboBox cbuniver,ComboBox cbuniverroutes)
        {
            String randomizedauthcode = "";
            String randomizedpassword = "";
            DB_Resolver_String_Randomizer(out randomizedauthcode);
            DB_Resolver_String_Randomizer(out randomizedpassword);
            generatedpassword = randomizedpassword;
            usedlogin = pesel.Text;
            int idgradeslookup = 0;
            int cbuniverselindex = cbuniver.SelectedIndex + 2;

            int idavailablerouteslookup = 0;
            String cbuniveravailitem = cbuniverroutes.SelectedItem.ToString();

            String db_insertquery1 = "INSERT INTO sruw_grades (polish, english, maths, add1, add2, add3, idauth) VALUES (@pol,@eng,@mat,@poladd,@matadd,@engadd,@randomizedauthcode)";
            String db_lookupidquery = "SELECT id from sruw_grades WHERE idauth=@idauth";
            String db_lookupidavailquery = "SELECT id from sruw_univer_avail WHERE name=@nameavail";
            String db_insertquery2 = "INSERT INTO sruw_accounts (name, password, type, pesel, email, address1, address2, iduniver, idgrades, iduniveravail) VALUES (@name,@password,@type,@pesel,@email,@address1,@address2,@cbuniversel,@idgradeslookup,@cbuniverroutessel)";
            MySqlConnection dbconnectionobj = new MySqlConnection(db_connectionstring);

            MySqlCommand execcommand1 = new MySqlCommand(db_insertquery1, dbconnectionobj);
            execcommand1.Parameters.AddWithValue("@pol", polp.Text);
            execcommand1.Parameters.AddWithValue("@eng", engp.Text);
            execcommand1.Parameters.AddWithValue("@mat", matp.Text);
            execcommand1.Parameters.AddWithValue("@poladd", polpadd.Text);
            execcommand1.Parameters.AddWithValue("@matadd", matpadd.Text);
            execcommand1.Parameters.AddWithValue("@engadd", engpadd.Text);
            execcommand1.Parameters.AddWithValue("@randomizedauthcode", randomizedauthcode);

            MySqlCommand execcommand2 = new MySqlCommand(db_lookupidquery, dbconnectionobj);
            execcommand2.Parameters.AddWithValue("@idauth", randomizedauthcode);

            MySqlCommand execcommand3 = new MySqlCommand(db_lookupidavailquery, dbconnectionobj);
            execcommand3.Parameters.AddWithValue("@nameavail", cbuniveravailitem);

            MySqlCommand execcommand4 = new MySqlCommand(db_insertquery2, dbconnectionobj);
            execcommand4.Parameters.AddWithValue("@name", name.Text);
            execcommand4.Parameters.AddWithValue("@password", randomizedpassword);
            execcommand4.Parameters.AddWithValue("@type", "STANDARD");
            execcommand4.Parameters.AddWithValue("@pesel", pesel.Text);
            execcommand4.Parameters.AddWithValue("@email", email.Text);
            execcommand4.Parameters.AddWithValue("@address1", address1.Text);
            execcommand4.Parameters.AddWithValue("@address2", address2.Text);
            execcommand4.Parameters.AddWithValue("@cbuniversel", cbuniverselindex);

            execcommand1.CommandTimeout = 30;
            execcommand2.CommandTimeout = 30;
            execcommand3.CommandTimeout = 30;
            execcommand4.CommandTimeout = 30;

            MySqlDataReader datareader;
            try
            {
                dbconnectionobj.Open();
                execcommand1.ExecuteNonQuery();
                datareader = execcommand2.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        idgradeslookup = Convert.ToInt32(datareader.GetString(0));
                        execcommand4.Parameters.AddWithValue("@idgradeslookup", idgradeslookup);
                    }
                }
                datareader.Close();
                datareader = execcommand3.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        idavailablerouteslookup = Convert.ToInt32(datareader.GetString(0));
                        execcommand4.Parameters.AddWithValue("@cbuniverroutessel", idavailablerouteslookup);
                    }
                }
                datareader.Close();
                execcommand4.ExecuteNonQuery();
                DB_ConnectionCloser(dbconnectionobj);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Błąd dodawania użytkownika. Czy posiadasz połączenie z internetem?", "SRUW - Błąd Bazodanowy", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
        }
        public void DB_Resolver_String_Randomizer(out string randomstring) {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[15];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            String randomizedstring = new string(stringChars);
            randomstring = randomizedstring;
        }

        public void DB_Resolver_Login_Queue(String login, String password, out String loginstatus, out String logintype, out int loginid) {
            String db_loginattemptquery = "SELECT type,id from sruw_accounts WHERE pesel=@usedlogin AND password = @usedpassword";
            MySqlConnection dbconnectionobj = new MySqlConnection(db_connectionstring);
            MySqlCommand execcommand1 = new MySqlCommand(db_loginattemptquery, dbconnectionobj);
            execcommand1.Parameters.AddWithValue("@usedlogin", login);
            execcommand1.Parameters.AddWithValue("@usedpassword", password);
            execcommand1.CommandTimeout = 30;
            MySqlDataReader datareader;
            String Loginstatusholder = "";
            String Typeholder = "";
            int Idholder =0;
            try
            {
                dbconnectionobj.Open();
                datareader = execcommand1.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        Typeholder = datareader.GetString(0);
                        Idholder = datareader.GetInt32(1);
                        Loginstatusholder = "IN";
                    }
                }
                else
                {
                    Loginstatusholder = "OUT";
                    Typeholder = null;
                    Idholder = -1;
                    MessageBox.Show("Błąd logowania. Podano złe poświadczenia.", "SRUW - Błąd Logowania", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                datareader.Close();
                DB_ConnectionCloser(dbconnectionobj);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Błąd logowania. Czy posiadasz połączenie z internetem?", "SRUW - Błąd Połączenia", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
            loginstatus = Loginstatusholder;
            logintype = Typeholder;
            loginid = Idholder;
        }


        public void DB_Resolver_Status_User_Deletion(int id)
        {
            MySqlConnection dbconnectionobj = new MySqlConnection(db_connectionstring);
            int Idgradesholder = 0;
            String db_deleteaccountquery = "DELETE FROM sruw_accounts WHERE id = @userid";
            MySqlCommand execcommand1 = new MySqlCommand(db_deleteaccountquery, dbconnectionobj);
            execcommand1.Parameters.AddWithValue("@userid", id);
            execcommand1.CommandTimeout = 30;
            String db_gradesidlookupquery = "SELECT idgrades from sruw_accounts WHERE id = @userid";
            MySqlCommand execcommand2 = new MySqlCommand(db_gradesidlookupquery, dbconnectionobj);
            execcommand2.Parameters.AddWithValue("@userid", id);
            execcommand2.CommandTimeout = 30;
            String db_deletegradesquery = "DELETE FROM sruw_grades WHERE id = @removedusergradesid";
            MySqlCommand execcommand3 = new MySqlCommand(db_deletegradesquery, dbconnectionobj);
            execcommand3.CommandTimeout = 30;

            MySqlDataReader datareader;
            try
            {
                dbconnectionobj.Open();
                datareader = execcommand2.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        Idgradesholder = datareader.GetInt32(0);
                    }
                }
                else
                {
                    Idgradesholder = -1;
                }
                datareader.Close();
                execcommand1.ExecuteNonQuery();
                execcommand3.Parameters.AddWithValue("@removedusergradesid", Idgradesholder);
                execcommand3.ExecuteNonQuery();
                DB_ConnectionCloser(dbconnectionobj);
                MessageBox.Show("Pomyślnie usunięto użytkownika z systemu.", "SRUW - Usuwanie Użytkownika", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Błąd Bazodanowy. Operacja nie powiodła się.", "SRUW - Błąd Bazodanowy", MessageBoxButton.OK, MessageBoxImage.Error);
            };
        }
        public void DB_Resolver_Status_Queue(TextBox pesel,TextBox Name,TextBox Email,TextBox Address1,TextBox Address2,TextBox pol,TextBox mat,TextBox eng,TextBox pol1add,TextBox mat2add,TextBox eng3add,TextBox route,TextBox status,TextBox points, int usedid)
        {
            MySqlConnection dbconnectionobj = new MySqlConnection(db_connectionstring);;
            String peselholder = "";
            String nameholder = "";
            String emailholder = "";
            String address1holder = "";
            String address2holder = "";
            String polholder = "";
            String matholder = "";
            String engholder = "";
            String pol1addholder = "";
            String mat2addholder = "";
            String eng3addholder = "";
            String universitynameholder = "";
            String routeholder = "";
            String statusholder = "";
            String pointsholder = "";
            int calculatedpointsholder = 0;
            int iduniverholder = 0;
            int idgradesholder = 0;
            int iduniveravailholder = 0;

            String db_selectionquery1 = "SELECT name,pesel,email,address1,address2,iduniver,idgrades,iduniveravail from sruw_accounts WHERE id = @userid";
            MySqlCommand execcommand1 = new MySqlCommand(db_selectionquery1, dbconnectionobj);
            execcommand1.Parameters.AddWithValue("@userid", usedid);
            execcommand1.CommandTimeout = 30;

            String db_selectionquery2 = "SELECT polish,english,maths,add1,add2,add3 from sruw_grades WHERE id = @gradesid";
            MySqlCommand execcommand2 = new MySqlCommand(db_selectionquery2, dbconnectionobj);
            execcommand2.CommandTimeout = 30;

            String db_selectionquery3 = "SELECT name from sruw_univer WHERE id = @univerid";
            MySqlCommand execcommand3 = new MySqlCommand(db_selectionquery3, dbconnectionobj);
            execcommand3.CommandTimeout = 30;

            String db_selectionquery4 = "SELECT name,pointtresh from sruw_univer_avail WHERE iduniver = @univerid AND id=@iduniveravail" ;
            MySqlCommand execcommand4 = new MySqlCommand(db_selectionquery4, dbconnectionobj);
            execcommand4.CommandTimeout = 30;

            MySqlDataReader datareader;
            try
            {
                dbconnectionobj.Open();
                datareader = execcommand1.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        nameholder = datareader.GetString(0);
                        peselholder = datareader.GetString(1);
                        emailholder = datareader.GetString(2);
                        address1holder = datareader.GetString(3);
                        address2holder = datareader.GetString(4);
                        iduniverholder = datareader.GetInt32(5);
                        idgradesholder = datareader.GetInt32(6);
                        iduniveravailholder = datareader.GetInt32(7);
                    }
                }
                datareader.Close();
                execcommand2.Parameters.AddWithValue("@gradesid", idgradesholder);

                datareader = execcommand2.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        polholder = datareader.GetString(0);
                        engholder = datareader.GetString(1);
                        matholder = datareader.GetString(2);
                        pol1addholder = datareader.GetString(3);
                        mat2addholder = datareader.GetString(4);
                        eng3addholder = datareader.GetString(5);
                    }
                }
                datareader.Close();
                execcommand3.Parameters.AddWithValue("@univerid", iduniverholder);
                datareader = execcommand3.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        universitynameholder = datareader.GetString(0);
                    }
                }
                datareader.Close();
                execcommand4.Parameters.AddWithValue("@univerid", iduniverholder);
                execcommand4.Parameters.AddWithValue("@iduniveravail", iduniveravailholder);
                datareader = execcommand4.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        routeholder = datareader.GetString(0);
                        pointsholder = datareader.GetString(1);
                    }
                }
                datareader.Close();
                DB_ConnectionCloser(dbconnectionobj);
                pesel.Text = peselholder;
                Name.Text = nameholder;
                Email.Text = emailholder;
                Address1.Text = address1holder;
                Address2.Text = address2holder;
                pol.Text = polholder;
                mat.Text = matholder;
                eng.Text = engholder;
                pol1add.Text = pol1addholder;
                mat2add.Text = mat2addholder;
                eng3add.Text = eng3addholder;
                route.Text = universitynameholder + " - " + routeholder;
                DB_Resolver calculation = new DB_Resolver();
                calculation.DB_Resolver_StatusPointsCalculator(polholder, matholder, engholder, pol1addholder, mat2addholder, eng3addholder,out calculatedpointsholder);
                // ZMIENIC
                if (pointsholder == "NIEUSTAWIONE")
                {
                    statusholder = "OCZEKUJĄCY";
                }
                else if (Convert.ToInt32(pointsholder) < calculatedpointsholder)
                {
                    statusholder = "PRZYJĘTY";
                }
                else if(Convert.ToInt32(pointsholder) > calculatedpointsholder)
                {
                    statusholder = "ODRZUCONY";
                }
                else if (Convert.ToInt32(pointsholder) == calculatedpointsholder)
                {
                    statusholder = "PRZYJETY";
                }
                status.Text = statusholder;
                points.Text = Convert.ToString(calculatedpointsholder);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Błąd Bazodanowy. Operacja nie powiodła się.", "SRUW - Błąd Bazodanowy", MessageBoxButton.OK, MessageBoxImage.Error);
            };
        }

        public void DB_Resolver_StatusPointsCalculator(String o1, String o2, String o3, String o4, String o5, String o6, out int calculatedpoints)
        {
            calculatedpoints = Convert.ToInt32(o1) + Convert.ToInt32(o2) + Convert.ToInt32(o3) + Convert.ToInt32(o4) + Convert.ToInt32(o5) + Convert.ToInt32(o6);
        }
    }
}