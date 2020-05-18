using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
namespace SRUW
{
    /// <summary>
    /// Logika interakcji dla klasy ChildWindow_SysInf.xaml
    /// </summary>
    public partial class ChildWindow_SysInf : Window
    {
        public ChildWindow_SysInf()
        {
            InitializeComponent();
        }
        private void CW_SysInf_F_License(object sender, RoutedEventArgs e)
        {
            try
            {
                var FileToOpen = @"LICENCE.txt";
                var process = new Process();
                process.StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = FileToOpen
                };

                process.Start();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Plik Licencji nie mógł zostać załadowany.", "SRUW - Błąd Programu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void CW_SysInf_F_Close(object sender, RoutedEventArgs e)
        {
            Close();
            var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("System Rekrutacji Uczelni Wyższych"));
            existingWindow.Activate();
        }
        private void CW_SysInf_F_Changes(object sender, RoutedEventArgs e)
        {
            try
            {
                var FileToOpen = @"CHANGELOG.txt";
                var process = new Process();
                process.StartInfo = new ProcessStartInfo()
                {
                    UseShellExecute = true,
                    FileName = FileToOpen
                };

                process.Start();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Plik Zmian nie mógł zostać załadowany.", "SRUW - Błąd Programu", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
