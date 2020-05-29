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
        private string db_networkingquery = "SELECT * from sruw_accounts";
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
                        string[] row = { datareader.GetString(0), datareader.GetString(1), datareader.GetString(2), datareader.GetString(3) };
                    }
                }
                else
                {
                    MessageBox.Show("Baza systemu nie posiada rekordów", "SRUW - Błąd Bazodanowy", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                DB_ConnectionCloser(dbconnectionobj);
            }
            catch (Exception exception)
            {
                MessageBox.Show("Błąd połączenia z bazą danych systemu. Napewno posiadasz połączenie z internetem?", "SRUW - Błąd Połączenia", MessageBoxButton.OK, MessageBoxImage.Warning);
            };
        }
        public void DB_ConnectionCloser(MySqlConnection obj) {
            obj.Close();
        } 
    }
}

