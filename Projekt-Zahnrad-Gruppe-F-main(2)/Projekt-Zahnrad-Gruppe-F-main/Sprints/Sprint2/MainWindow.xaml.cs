﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


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




        // Button "Ende" 
        private void btn_Ende_Click(object sender, RoutedEventArgs e)
        {

            int answer = Convert.ToInt32(MessageBox.Show("Sind Sie sich sicher, dass Sie das Programm beenden wollen?",
                "Ende", MessageBoxButton.YesNo, MessageBoxImage.Warning));


            if (answer == 6)
                System.Windows.Application.Current.Shutdown();

        }

        //Button "Berechne"
        private void btn_Berechnen_Click(object sender, RoutedEventArgs e)
        {
            // Einlesen von Eingabefeldern
            double d_test = Convert.ToDouble(d1.Text);
            double b_test = Convert.ToDouble(b1.Text);
            double z_test = Convert.ToDouble(z1.Text);


            // Kontrolle auf Zahlen größer Null
            if (z_test <= 0)
            {
                MessageBox.Show("Der Wert 'z' muss größer Null sein!", "Ungültige Eingabe", MessageBoxButton.OK, MessageBoxImage.Error);
                z1.Focus();
                z1.SelectAll();
            }


            else if (d_test <= 0)
            {
                MessageBox.Show("Der Wert 'd' muss größer Null sein!", "Ungültige Eingabe", MessageBoxButton.OK, MessageBoxImage.Error);
                d1.Focus();
                d1.SelectAll();
            }

            else if (b_test <= 0)
            {
                MessageBox.Show("Der Wert 'b' muss größer Null sein!", "Ungültige Eingabe", MessageBoxButton.OK, MessageBoxImage.Error);
                b1.Focus();
                b1.SelectAll();
            }


            else
            {
                     
                
                

                    double z = z_test;
                    double d = d_test;
                    double b = b_test;


                    double p = Math.Round(d / z * Math.PI);
                    double c = Math.Round(0.167 * (d / z), 3);
                    double df = Math.Round(d - 2 * ((d / z) + c), 3);
                    double hf = Math.Round((d / z) + c, 3);
                    double h = Math.Round(2 * (d / z) + c, 3);
                    double ha = Math.Round((d / z), 3);
                    double da = Math.Round(d + 2 * (d / z), 3);
                    double m = Math.Round(d / z);

                    p1.Text = Convert.ToString(p);
                    df1.Text = Convert.ToString(df);
                    c1.Text = Convert.ToString(c);
                    hf1.Text = Convert.ToString(hf);
                    h1.Text = Convert.ToString(h);
                    ha1.Text = Convert.ToString(ha);
                    da1.Text = Convert.ToString(da);
                    m1.Text = Convert.ToString(m);
                
            }

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

        private void z1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void z1_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void txtblock_Zähnezahl_MouseEnter(object sender, MouseEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Catia_Click(object sender, RoutedEventArgs e)
        {

            CatiaConnection catia = new CatiaConnection();
            catia.ErstelleLeereSkizze();
            catia.ErzeugeBalken(5);

        }
        }
            

        
        

    }  
        
            
   

