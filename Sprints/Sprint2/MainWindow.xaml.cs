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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sprint2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Ende_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btn_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            double m = Convert.ToDouble(m1.Text);
            double d = Convert.ToDouble(d1.Text);
            double b = Convert.ToDouble(b1.Text);
            double z = Convert.ToDouble(z1.Text);

            double p = Math.PI * m;
            double c = 0.167 * m;
            double df = d - 2 * (m + c);
            double hf = m + c;
            double h = 2 * m + c;
            double ha = m;
            double da = d + 2 * m;

            p1.Text = Convert.ToString(p);
            df1.Text = Convert.ToString(df);
            c1.Text = Convert.ToString(c);
            hf1.Text = Convert.ToString(hf);
            h1.Text = Convert.ToString(h);
            ha1.Text = Convert.ToString(ha);
            da1.Text = Convert.ToString(da);
            /**
     
          
            v = 0;
            G = 0;
            P = 0;
            **/
        }

        private void p1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
