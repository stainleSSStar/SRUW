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
    /// Logika interakcji dla klasy ChildWindow_AdmCtr.xaml
    /// </summary>
    public partial class ChildWindow_AdmCtr : Window
    {
        private int usedid;
        public ChildWindow_AdmCtr()
        {
            InitializeComponent();
        }
        public ChildWindow_AdmCtr(int usedid)
        {
            InitializeComponent();
            this.usedid = usedid;
        }
        private void CW_AdmCtr_F_Close(object sender, RoutedEventArgs e) {
            Close();
            var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("SRUW - Logowanie"));
            var existingWindowMain = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("System Rekrutacji Uczelni Wyższych"));
            existingWindow.Show();
        }
        private void CW_Database_Opener(object sender,RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://localhost/phpmyadmin/");
        }
    }
}
