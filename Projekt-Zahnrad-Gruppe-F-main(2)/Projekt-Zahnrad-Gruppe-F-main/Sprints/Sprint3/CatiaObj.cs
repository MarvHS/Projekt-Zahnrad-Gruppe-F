using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//z, b, m, p, c, df, hf, h, ha, dh
namespace Sprint2
{
    public class CatiaObj
    {
        public CatiaObj(int z, double b, double m, double p, double c, double df, double hf, double h, double ha, double da)
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
                    cc.ErzeugeProfil (z,b,m,p,c,df,hf,h,ha,da);
                   

                 cc.ErzeugeZahnrad(z, b, m);

                    // Extrudiere Balken
                    //cc.ErzeugeBalken(5);
                    //Console.WriteLine("4");
                    
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
