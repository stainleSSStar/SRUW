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
    /// Logika interakcji dla klasy ChildWindow_Register.xaml
    /// </summary>
    public partial class ChildWindow_Register : Window
    {
        public ChildWindow_Register()
        {
            InitializeComponent();
        }
        private void CW_Register_F_Close(object sender, RoutedEventArgs e)
        {
            Close();
            var existingWindow = Application.Current.Windows.Cast<Window>().SingleOrDefault(x => x.Title.Equals("System Rekrutacji Uczelni Wyższych"));
            existingWindow.Activate();
        }
    }
}
