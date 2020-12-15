using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sprint2
{
    public class CatiaObj
    {
        public CatiaObj(int z, double b, double m)
        {
            try
            {

                CatiaConnection cc = new CatiaConnection();

                // Finde Catia Prozess
                if (cc.CATIALaeuft())
                {
                    Console.WriteLine("0");

                    // Öffne ein neues Part
                    cc.ErzeugePart();
                    Console.WriteLine("1");

                    // Erstelle eine Skizze
                    cc.ErstelleLeereSkizze();
                    Console.WriteLine("2");

                    // Generiere ein Profil
                    //cc.ErzeugeProfil(20, 10);
                    //Console.WriteLine("3");

                    // Extrudiere Balken
                    //cc.ErzeugeBalken(300);
                    //Console.WriteLine("4");
                    cc.ErzeugeZahnrad(z, b, m);
                }
                else
                {
                    Console.WriteLine("Laufende Catia Application nicht gefunden");
                    MessageBox.Show("Laufende Catia Application nicht gefunden");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception aufgetreten");
            }

        }

        
    }
}
