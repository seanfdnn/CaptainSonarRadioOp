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

namespace CaptainSonarAi
{
    /// <summary>
    /// Interaction logic for SelectMapDialog.xaml
    /// </summary>
    public partial class SelectMapDialog : Window
    {
        public SelectMapDialog()
        {
            InitializeComponent();
            cmbMap.ItemsSource = BoardFactory.GetAvaliableMaps();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
