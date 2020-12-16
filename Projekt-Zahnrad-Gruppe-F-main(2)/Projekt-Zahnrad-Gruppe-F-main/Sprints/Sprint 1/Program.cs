using System.Security.Cryptography.X509Certificates;
using System;

namespace ProjektZahnrad
{
    enum Material { Holz, Metal, Kunstoff, NichtEisenMetalle } //Deklaration des Materials (Eingang)

    class Program
    {
        public static void Main(string[] args)
        {
            double m=0, d, b, z;                 // Deklaration der Variablen (Eingang)

            Material M;                     //Deklaration des Materials (Eingang)



            double p, da, df, h, ha, hf, c, a;   // Deklaration der Variablen (Ausgang)            

            double v, G, P;                      //Deklaration der Basisinformationen Volumen, Masse, Preis (Ausgang)


            Console.WriteLine("Geben Sie die Eingangsparameter für die Zahnradberechnung ein.");
            Console.WriteLine();


            //Initialisierung der Variablen a und b

            d = Prüfung("Geben Sie den Teilkreisdurchmesser des Zahnrads in mm an: ");


            b = Prüfung("Geben Sie die Breite des Zahnrads in mm an: ");

            //Initialisierung der Varibalen z und Überprüfung auf ganzzahlige Eingabe
            double s = 0;

            do
            {
                 s = Prüfung("Tippen Sie 1 für Modul oder 2 für Zähnezahl");

                if (s != 1 && s != 2)
                    Console.WriteLine("Wählen Sie 1 oder 2 !");

            } while (s  != 1 && s !=2);

            if(s==1)
            {
                //Initialisierung der Varibalen z und Überprüfung auf ganzzahlige Eingabe
                
                m = Prüfung("Geben Sie das Modul ein: ");

            
                z = d *m;
                Console.WriteLine("Zähnezahl: " + Math.Round(z, 3) );
            }
            

            if (s == 2)
            {
                //Initialisierung der Varibalen z und Überprüfung auf ganzzahlige Eingabe
                do
                {
                    z = Prüfung("Geben Sie die Zähnezahl des Zahnrads an: ");

                    if (z % 1 != 0)
                        Console.WriteLine("Geben Sie für die Zähnezahl nur ganzzahlige Zahlen ein!");

                } while (z % 1 != 0);
                m = d / z;
                Console.WriteLine("Modul: " + Math.Round(m, 3) + "mm");
            }


            //Berechnungen der Ausgangswerte

         

            p = Math.PI * m;
            Console.WriteLine("Teilung: " + Math.Round(p, 3) + "mm");

            da = d + 2 * m;
            Console.WriteLine("Kopfkreisdurchmesser: " + Math.Round(da, 3)+"mm^2");

            c = 0.167 * m;
            Console.WriteLine("Kopfspiel: " + Math.Round(c, 3) + "mm");

            df = d - 2 * (m + c);
            Console.WriteLine("Fußkreisdurchmesser: " + Math.Round(df, 3)+"mm^2");

            h = 2 * m + c;
            Console.WriteLine("Kopfspiel: " + Math.Round(h, 3) + "mm");

            ha = m;
            Console.WriteLine("Zahnkopfhöhe: " + Math.Round(ha, 3) + "mm");

            hf = m + c;
            Console.WriteLine("Zahnfußhöhe: " + Math.Round(hf, 3) + "mm");

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

