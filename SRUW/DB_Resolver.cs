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

        public void DB_Resolver_Login_Queue(String login, String password, out String loginstatus, out String logintype) {
            String db_loginattemptquery = "SELECT type from sruw_accounts WHERE pesel=@usedlogin AND password = @usedpassword";
            MySqlConnection dbconnectionobj = new MySqlConnection(db_connectionstring);
            MySqlCommand execcommand1 = new MySqlCommand(db_loginattemptquery, dbconnectionobj);
            execcommand1.Parameters.AddWithValue("@usedlogin", login);
            execcommand1.Parameters.AddWithValue("@usedpassword", password);
            execcommand1.CommandTimeout = 30;
            MySqlDataReader datareader;
            String Loginstatusholder = "";
            String Typeholder = "";
            try
            {
                dbconnectionobj.Open();
                datareader = execcommand1.ExecuteReader();
                if (datareader.HasRows)
                {
                    while (datareader.Read())
                    {
                        Typeholder = datareader.GetString(0);
                        Loginstatusholder = "IN";
                    }
                }
                else
                {
                    Loginstatusholder = "OUT";
                    Typeholder = null;
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
        }
    }
}