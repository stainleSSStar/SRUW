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
        public ChildWindow_AdmCtr()
        {
            InitializeComponent();
        }
        public ChildWindow_AdmCtr(int usedid)
        {
            InitializeComponent();
            this.usedid = usedid;
        }
        private int usedid;
    }
}
