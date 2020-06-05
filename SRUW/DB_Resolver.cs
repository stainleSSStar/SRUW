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
        private string db_connectionstring = "datasource = 127.0.0.1; port=3306;username=root;password=;database=sruwdb;";
        private string db_networkingquery = "SELECT id from sruw_accounts";
        private string db_connectionstatus = "unknown";
        public void DB_ConnectionOpener()
        {
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
            String db_comboboxpull = "SELECT id,name FROM sruw_univer_avail WHERE iduniver = "+(cbname1.SelectedIndex+2).ToString();
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
        public void DB_ConnectionInsert()
        {
            //not implemented
        }
    }
}
