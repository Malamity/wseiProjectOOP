using ConnectDataBase;
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

namespace wpf_semestralny
{
    /// <summary>
    /// Logika interakcji dla klasy Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnItems_Click(object o,EventArgs e)
        {
            new Window1().Show();
            this.Close();
        }
        private void btnPerformance_Click(object o, EventArgs e)
        {
            new Performances().Show();
            this.Close();
        }
        private void btnStaff_Click(object o, EventArgs e)
        {
            new Staff().Show();
            this.Close();
        }
        
    }
}
