using System.Security.Cryptography.X509Certificates;
using System;

namespace ProjektZahnrad
{
    class Program
    {
         static void main(string[] args)
        {
           double m, d, b, z;                   // Deklaration der Variablen (Eingang)
           double p, da, df, h, ha, hf, c, a;   // Deklaration der Variablen (Ausgang)            
            

            Console.WriteLine("Geben Sie die Eingangsparameter für die Zahnradberechnung ein.");
            Console.WriteLine();


            //Initialisierung der Variablen
           
            d = Prüfung("Geben Sie den Teilkreisdurchmesser des Zahnrads in mm an: ");
           
            b = Prüfung("Geben Sie die Breite des Zahnrads in mm an: ");
      
            z = Prüfung("Geben Sie die Zähnezahl des Zahnrads an: ");
          


            //Berechnungen der Ausgangswerte

            m = d / z;
            Console.WriteLine("Modul: " + m);

            p = Math.PI * m;
            Console.WriteLine("Teilung: "+p);

            da = d + 2 * m;
            Console.WriteLine("Kopfkreisdurchmesser: " + da);

            c = 0.167 * m;
            Console.WriteLine("Kopfspiel: " + c);

            df = d - 2 * (m + c);
            Console.WriteLine("Fußkreisdurchmesser: " + df);

            h = 2 * m + c;
            Console.WriteLine("Kopfspiel: " + h);

            ha = m;
            Console.WriteLine("Zahnkopfhöhe: " + ha);

            hf = m + c;
            Console.WriteLine("Zahnfußhöhe: " + hf);

        }

        // Funktion zur Überprüfung der Eingangswerte ( Werte größer Null)
        static double Prüfung (string name)
        {
            double xx = 0;
            do
            {
                Console.WriteLine(name);
                xx = Convert.ToDouble(Console.ReadLine());
                if (xx <= 0)
                    Console.WriteLine("Geben Sie eine Zahl größer Null ein!");
            } while (xx <= 0);


            return (xx);
           
        }
        
        
        
    }
}
