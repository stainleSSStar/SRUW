using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
            else if(db_connectionstatus == "Offline"){
                return false;
            }
            else
            {
                return false;
            }
        }
        public void DB_ConnectionInsert()
        {
            //not implemented
        }
    }
}

