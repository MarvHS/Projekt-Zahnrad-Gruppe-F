using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

<<<<<<< HEAD

namespace Sprint3

=======
//z, b, m, p, c, df, hf, h, ha, dh
namespace Sprint2
>>>>>>> b7d050d92e70aed715e72a45c1c67a8bd8c1caa0
{
    public class CatiaObj
    {
        public CatiaObj(int z, double b, double m, double p, double c, double df, double hf, double h, double ha, double da)
        {
            try
            {

                Sprint2.CatiaConnection cc = new CatiaConnection();

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
<<<<<<< HEAD
                   cc.ErzeugeProfil(20, 10);
                    //Console.WriteLine("3");
=======
                    cc.ErzeugeProfil (z,b,m,p,c,df,hf,h,ha,da);
                   

                  //cc.ErzeugeZahnrad(z, b, m);
>>>>>>> b7d050d92e70aed715e72a45c1c67a8bd8c1caa0

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
