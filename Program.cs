using System.Security.Cryptography.X509Certificates;
using System;

namespace ProjektZahnrad
{
    enum Material { Holz, Metal, Kunstoff, NichtEisenMetalle } //Deklaration des Materials (Eingang)

    class Program
    {
        public static void Main(string[] args)
        {
            double m, d, b, z;                 // Deklaration der Variablen (Eingang)
                                    
            Material M;                     //Deklaration des Materials (Eingang)



            double p, da, df, h, ha, hf, c, a;   // Deklaration der Variablen (Ausgang)            

            double v, G, P;                      //Deklaration der Basisinformationen Volumen, Masse, Preis (Ausgang)


            Console.WriteLine("Geben Sie die Eingangsparameter für die Zahnradberechnung ein.");
            Console.WriteLine();


            //Initialisierung der Variablen a und b

            d = Prüfung("Geben Sie den Teilkreisdurchmesser des Zahnrads in mm an: ");


            b = Prüfung("Geben Sie die Breite des Zahnrads in mm an: ");


            //Initialisierung der Varibalen z und Überprüfung auf ganzzahlige Eingabe
           do
            {
               z = Prüfung("Geben Sie die Zähnezahl des Zahnrads an: ");

                if (z % 1 != 0)
                    Console.WriteLine("Geben Sie für die Zähnezahl nur ganzzahlige Zahlen ein!");
           
            } while (z % 1 != 0);



            //Berechnungen der Ausgangswerte

            m = d / z;
            Console.WriteLine("Modul: " + Math.Round(m,3));

            p = Math.PI * m;
            Console.WriteLine("Teilung: " + Math.Round(p,3));

            da = d + 2 * m;
            Console.WriteLine("Kopfkreisdurchmesser: " + Math.Round(da,3));

            c = 0.167 * m;
            Console.WriteLine("Kopfspiel: " + Math.Round(c,3));

            df = d - 2 * (m + c);
            Console.WriteLine("Fußkreisdurchmesser: " + Math.Round(df,3));

            h = 2 * m + c;
            Console.WriteLine("Kopfspiel: " + Math.Round(h,3));

            ha = m;
            Console.WriteLine("Zahnkopfhöhe: " + Math.Round(ha,3));

            hf = m + c;
            Console.WriteLine("Zahnfußhöhe: " + Math.Round(hf,3));

            v = 0;
            Console.WriteLine("Volumen: " + v);

            G = 0;
            Console.WriteLine("Masse: " + G);

            P = 0;
            Console.WriteLine("Preis: " + P);


        }

        // Funktion zur Überprüfung der Eingangswerte ( Werte größer Null)
        static double Prüfung(string name)
        {
            double xx;
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

