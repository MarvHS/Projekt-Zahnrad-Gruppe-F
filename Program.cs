using System.Security.Cryptography.X509Certificates;
using System;

namespace ProjektZahnrad
{
    enum Material { Holz, Metal, Kunstoff, NichtEisenMetalle } //Deklaration des Materials (Eingang)
    
    class Program
    {
        public static void Main(string[] args)
        {
            double m, d, b;                  // Deklaration der Variablen (Eingang)
            int z;                          //Deklaration der Variablen (Eingang)

            Material M;                     //Deklaration des Materials (Eingang)

       


			double p, da, df, h, ha, hf, c, a;   // Deklaration der Variablen (Ausgang)            

            double v, G, P;   //Deklaration der Basisinformationen Volumen, Masse, Preis (Ausgang)


			Console.WriteLine("Geben Sie die Eingangsparameter für die Zahnradberechnung ein.");
            Console.WriteLine();


            //Initialisierung der Variablen
           
            d = Prüfung("Geben Sie den Teilkreisdurchmesser des Zahnrads in mm an: ");

			b = Prüfung("Geben Sie die Breite des Zahnrads in mm an: ");

			z = (int)Prüfung("Geben Sie die Zähnezahl des Zahnrads an: ");



        //Berechnungen der Ausgangswerte

			m = d / z;
            Console.WriteLine("Modul: " + m);

			p = Math.PI* m;
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
            v = 0;
            Console.WriteLine("Volumen: "+v);
            G= 0;
            Console.WriteLine("Masse: " + G);

            P = 0;
            Console.WriteLine("Preis: " + P);





        }

        // Funktion zur Überprüfung der Eingangswerte ( Werte größer Null)
        static double Prüfung(string name)
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

