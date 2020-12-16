using System;
using System.Windows;
using ProductStructureTypeLib;
using KnowledgewareTypeLib;
using HybridShapeTypeLib;
using INFITF;
using MECMOD;
using PARTITF;

namespace Sprint2
{
    class CatiaConnection
    {

        INFITF.Application hsp_catiaApp;
        MECMOD.PartDocument hsp_catiaPart;
        MECMOD.Sketch hsp_catiaProfil;

        public bool CATIALaeuft()
        {
            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject("CATIA.Application");
                hsp_catiaApp = (INFITF.Application)catiaObject;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean ErzeugePart()
        {
            INFITF.Documents catDocuments1 = hsp_catiaApp.Documents;
            hsp_catiaPart = catDocuments1.Add("Part") as MECMOD.PartDocument;
            return true;
        }

        public void ErstelleLeereSkizze()
        {
            // geometrisches Set auswaehlen und umbenennen
            HybridBodies catHybridBodies1 = hsp_catiaPart.Part.HybridBodies;
            HybridBody catHybridBody1;
            try
            {
                catHybridBody1 = catHybridBodies1.Item("Geometrisches Set.1");
            }
            catch (Exception)
            {
                MessageBox.Show("Kein geometrisches Set gefunden! " + Environment.NewLine +
                    "Ein PART manuell erzeugen und ein darauf achten, dass 'Geometisches Set' aktiviert ist.",
                    "Fehler", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            catHybridBody1.set_Name("Profil");
            // neue Skizze im ausgewaehlten geometrischen Set anlegen
            Sketches catSketches1 = catHybridBody1.HybridSketches;
            OriginElements catOriginElements = hsp_catiaPart.Part.OriginElements;
            Reference catReference1 = (Reference)catOriginElements.PlaneYZ;
            hsp_catiaProfil = catSketches1.Add(catReference1);

            // Achsensystem in Skizze erstellen 
            ErzeugeAchsensystem();

            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }

        private void ErzeugeAchsensystem()
        {
            object[] arr = new object[] {0.0, 0.0, 0.0,
                                         0.0, 1.0, 0.0,
                                         0.0, 0.0, 1.0 };
            hsp_catiaProfil.SetAbsoluteAxisData(arr);
        }

                public void ErzeugeProfil(int z, double b, double m, double p, double c, double df, double hf, double h, double ha, double da)
        {
          
            //Punkte

            //Nullpunkte (koordinatenursprung)

            double x0 = 0;
            double y0 = 0;
            
           

            //HilfsWinkel aus dem Skript

            double alpha = 20;
            double beta = 90 / z;
            double betarad = Math.PI * beta / 180;
            double gamma = 90 - (alpha - beta);
            double gammarad = Math.PI * gamma / 180;
            double totalangle = 360.0 / z;
            double totalanglerad = Math.PI * totalangle / 180;

            // Radien aus dem Skript 

            double teilekreisradius= (m*z) / 2;
            double hilfskreisradius= 0.94*teilekreisradius;
            double fußkreisradius = teilekreisradius - (1.25*m);
            double kopfkreisradius = teilekreisradius +m;
            double verrundungsradius = 0.35*m;

            //Mittelpunkt für Teilstück aus dem Evolventenkreis 

            double mittelpunkt_evolventenkreis_x = hilfskreisradius * Math.Cos(gammarad);
            double mittelpunkt_evolventenkreis_y = hilfskreisradius * Math.Sin(gammarad);

             
            // Schnittpunkte Teilstück Evolventenkreis mit Teilkreisradius 
            double  schnittpunkt_evolventenkreis_x = -teilekreisradius * Math.Sin(betarad);
            double  schnittpunkt_evolventenkreis_y = teilekreisradius * Math.Cos(betarad);

             //Radius für Teilstück Evolventenkreis
            double evolventenkreis_r = Math.Sqrt(Math.Pow(( mittelpunkt_evolventenkreis_x - schnittpunkt_evolventenkreis_x), 2) + Math.Pow((mittelpunkt_evolventenkreis_y - schnittpunkt_evolventenkreis_y), 2));

            
            //Schnittpunkte Evolventenkreis und Kopfkreisradius
            double schnittpunkt_evolventenkopfkreis_x = schnittpunkt_x_Achse(x0, y0, kopfkreisradius, mittelpunkt_evolventenkreis_x, mittelpunkt_evolventenkreis_y, evolventenkreis_r);
            double schnittpunkt_evolventenkopfkreis_y = schnittpunkt_y_Achse(x0, y0, kopfkreisradius, mittelpunkt_evolventenkreis_x, mittelpunkt_evolventenkreis_y, evolventenkreis_r);
                   
            //Mittelpunkt Radius Verrunduung 
            double mittelpunktverrundung_x = schnittpunkt_x_Achse(x0, y0, fußkreisradius + verrundungsradius, mittelpunkt_evolventenkreis_x, mittelpunkt_evolventenkreis_y, evolventenkreis_r + verrundungsradius );
            double mittelpunktverrundung_y = schnittpunkt_y_Achse(x0, y0, fußkreisradius + verrundungsradius, mittelpunkt_evolventenkreis_x, mittelpunkt_evolventenkreis_y, evolventenkreis_r + verrundungsradius );

            //Schnittpunkte Teilstück Evolventenkreis und Radius Verrundungungsradius     NEU
            double schnittpunktevolventenverrundung_x = schnittpunkt_x_Achse(mittelpunkt_evolventenkreis_x, mittelpunkt_evolventenkreis_y, evolventenkreis_r, mittelpunktverrundung_x, mittelpunktverrundung_y, verrundungsradius);
            double schnittpunktevolventenverrundung_y = schnittpunkt_y_Achse(mittelpunkt_evolventenkreis_x, mittelpunkt_evolventenkreis_y, evolventenkreis_r, mittelpunktverrundung_x, mittelpunktverrundung_y, verrundungsradius);
            
            //SchnittPunkt Fußkreis & Verrundungs Radius
            double schnittpunkt_verrundungsradius_x = schnittpunkt_x_Achse(x0, y0, fußkreisradius, mittelpunktverrundung_x, mittelpunktverrundung_y, verrundungsradius);
            double schnittpunkt_verrundungsradius_y = schnittpunkt_y_Achse(x0, y0, fußkreisradius, mittelpunktverrundung_x, mittelpunktverrundung_y, verrundungsradius);


            //StartPunkt Fußkreis Radius
            double phi = totalanglerad - Math.Atan(Math.Abs(schnittpunkt_verrundungsradius_x) / Math.Abs(schnittpunkt_verrundungsradius_y));

            double startpunktfußkreis_x = -fußkreisradius * Math.Sin(phi);
            double startpunktfußkreis_y = fußkreisradius * Math.Cos(phi);



            // öffne das Programm
            Factory2D catFactory2D1 =hsp_catiaProfil.OpenEdition();


            //Punkte 
            
            //  Ursprung
            Point2D catP2D_Ursprung = catFactory2D1.CreatePoint(x0, y0);

            //Fußkreis Start links
            Point2D catP2D_startpunktfußkreis = catFactory2D1.CreatePoint(startpunktfußkreis_x, startpunktfußkreis_y);

            //Verrundung unten links
            Point2D catP2D_schnittpunktfußkreisradius1 = catFactory2D1.CreatePoint(schnittpunkt_verrundungsradius_x, schnittpunkt_verrundungsradius_y);

            //Verrundung unten rechts
            Point2D catP2D_schnittpunktfußkreisradius2 = catFactory2D1.CreatePoint(-schnittpunkt_verrundungsradius_x, schnittpunkt_verrundungsradius_y);

            // außen rechts
            Point2D catP2D_mittelpunktevolventenkreis1 = catFactory2D1.CreatePoint(mittelpunkt_evolventenkreis_x, mittelpunkt_evolventenkreis_y);

            //außen links
            Point2D catP2D_mittelpunktevolventenkreis2 = catFactory2D1.CreatePoint(-mittelpunkt_evolventenkreis_x, mittelpunkt_evolventenkreis_y);
          
            //Evolvente links
            Point2D catP2D_schnittpunktevolventenkreis1 = catFactory2D1.CreatePoint(schnittpunkt_evolventenkopfkreis_x, schnittpunkt_evolventenkopfkreis_y);

            //Evolvente rechts
            Point2D catP2D_schnittpunktevolventenkreis2 = catFactory2D1.CreatePoint(-schnittpunkt_evolventenkopfkreis_x, schnittpunkt_evolventenkopfkreis_y);

            //Verrundung Mittelpunkt links
            Point2D catP2D_mittelpunktverrundung1 = catFactory2D1.CreatePoint(mittelpunktverrundung_x, mittelpunktverrundung_y);

            //Verrundung Mittelpunkt rechts 
            Point2D catP2D_mittelpunktverrundung2 = catFactory2D1.CreatePoint(-mittelpunktverrundung_x, mittelpunktverrundung_y);

            // Verrundung oben links
            Point2D catP2D_schnittpunktevolventenverrundung1 = catFactory2D1.CreatePoint(schnittpunktevolventenverrundung_x, schnittpunktevolventenverrundung_y);

            //Verrundung oben rechts 
            Point2D catP2D_schnittpunktevolventenverrundung2 = catFactory2D1.CreatePoint(-schnittpunktevolventenverrundung_x, schnittpunktevolventenverrundung_y);

        

             //Kreise

            //Fußkreis links 
            Circle2D catC2D_Frußkreis = catFactory2D1.CreateCircle(x0, y0, fußkreisradius, 0, 0);
            catC2D_Frußkreis.CenterPoint = catP2D_Ursprung;
            catC2D_Frußkreis.StartPoint = catP2D_schnittpunktfußkreisradius1;
            catC2D_Frußkreis.EndPoint = catP2D_startpunktfußkreis;

            //Kopfkreis
            Circle2D catC2D_Kopfkreis = catFactory2D1.CreateCircle(x0, y0, kopfkreisradius, 0, 0);
            catC2D_Kopfkreis.CenterPoint = catP2D_Ursprung;
            catC2D_Kopfkreis.StartPoint = catP2D_schnittpunktevolventenkreis2;
            catC2D_Kopfkreis.EndPoint = catP2D_schnittpunktevolventenkreis1;

            //Evolvente links 
            Circle2D catC2D_EvolventenKreis1 = catFactory2D1.CreateCircle(mittelpunkt_evolventenkreis_x, mittelpunkt_evolventenkreis_y, evolventenkreis_r, 0, 0);
            catC2D_EvolventenKreis1.CenterPoint = catP2D_mittelpunktevolventenkreis1;
            catC2D_EvolventenKreis1.StartPoint = catP2D_schnittpunktevolventenkreis1;
            catC2D_EvolventenKreis1.EndPoint = catP2D_schnittpunktevolventenverrundung1;

            //Evolvente rechts 
            Circle2D catC2D_Evolventenkreis2 = catFactory2D1.CreateCircle(-mittelpunkt_evolventenkreis_x, mittelpunkt_evolventenkreis_y, evolventenkreis_r, 0, 0);
            catC2D_Evolventenkreis2.CenterPoint = catP2D_mittelpunktevolventenkreis2;
            catC2D_Evolventenkreis2.StartPoint = catP2D_schnittpunktevolventenverrundung2;
            catC2D_Evolventenkreis2.EndPoint = catP2D_schnittpunktevolventenkreis2;

            //Verrundung links 
            Circle2D catC2D_VerrundungsKreis1 = catFactory2D1.CreateCircle(mittelpunktverrundung_x, mittelpunktverrundung_y,verrundungsradius, 0, 0);
            catC2D_VerrundungsKreis1.CenterPoint = catP2D_mittelpunktverrundung1;
            catC2D_VerrundungsKreis1.StartPoint = catP2D_schnittpunktfußkreisradius1;
            catC2D_VerrundungsKreis1.EndPoint = catP2D_schnittpunktevolventenverrundung1;
         
            //Verrundung rechts 
            Circle2D catC2D_VerrundungsKreis2 = catFactory2D1.CreateCircle(-mittelpunktverrundung_x, mittelpunktverrundung_y, verrundungsradius, 0, 0);
            catC2D_VerrundungsKreis2.CenterPoint = catP2D_mittelpunktverrundung2;
            catC2D_VerrundungsKreis2.StartPoint = catP2D_schnittpunktevolventenverrundung2;
            catC2D_VerrundungsKreis2.EndPoint = catP2D_schnittpunktfußkreisradius2;
         

            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }


      


        public void ErzeugeZahnrad(int Zaehnezahl, double Dicke, double Modul)
        {
            ShapeFactory SF = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;
            HybridShapeFactory HSF = (HybridShapeFactory)hsp_catiaPart.Part.HybridShapeFactory; Part myPart = hsp_catiaPart.Part;
            Factory2D Factory2D1 = hsp_catiaProfil.Factory2D;


            HybridShapePointCoord Ursprung = HSF.AddNewPointCoord(0, 0, 0);
            Reference RefUrsprung = myPart.CreateReferenceFromObject(Ursprung);
            HybridShapeDirection XDir = HSF.AddNewDirectionByCoord(1, 0, 0);
            Reference RefXDir = myPart.CreateReferenceFromObject(XDir); 


            CircPattern Kreismuster = SF.AddNewSurfacicCircPattern(Factory2D1, 1, 2, 0, 0, 1, 1, RefUrsprung, RefXDir, false, 0, true, false);
            Kreismuster.CircularPatternParameters = CatCircularPatternParameters.catInstancesandAngularSpacing;
            AngularRepartition angularRepartition1 = Kreismuster.AngularRepartition;
            Angle angle1 = angularRepartition1.AngularSpacing;
            angle1.Value = Convert.ToDouble(360 / Convert.ToDouble(Zaehnezahl));
            AngularRepartition angularRepartition2 = Kreismuster.AngularRepartition;
            IntParam intParam1 = angularRepartition2.InstancesCount;
            intParam1.Value = Convert.ToInt32(Zaehnezahl) + 1;


            Reference Ref_Kreismuster = myPart.CreateReferenceFromObject(Kreismuster);
            HybridShapeAssemble Verbindung = HSF.AddNewJoin(Ref_Kreismuster, Ref_Kreismuster); Reference Ref_Verbindung = myPart.CreateReferenceFromObject(Verbindung);
          
            HSF.GSMVisibility(Ref_Verbindung, 0);

            myPart.Update();

            Bodies bodies = myPart.Bodies;
            Body myBody = bodies.Add();
            myBody.set_Name("Zahnrad");
            myBody.InsertHybridShape(Verbindung);
            myPart.Update();
            myPart.InWorkObject = myBody;
            Pad myPad = SF.AddNewPadFromRef(Ref_Verbindung, Dicke);

            myPart.Update();
        }


        //Berechnungen der SchnittPunkte
        private double schnittpunkt_x_Achse(double mittelpunkt_Kreis1_x, double mittelpunkt_Kreis1_y, double r1, double mittelpunkt_Kreis2_x, double mittelpunkt_Kreis2_y, double r2)
        {
            double d = Math.Sqrt(Math.Pow((mittelpunkt_Kreis1_x - mittelpunkt_Kreis2_x), 2) + Math.Pow((mittelpunkt_Kreis1_y - mittelpunkt_Kreis2_y), 2));
            double l = (Math.Pow(r1, 2) - Math.Pow(r2, 2) + Math.Pow(d, 2)) / (d * 2);
            double h = Math.Sqrt(Math.Pow(r1, 2) - Math.Pow(l, 2));
        

           
            return l * (mittelpunkt_Kreis2_x - mittelpunkt_Kreis1_x) / d - h * (mittelpunkt_Kreis2_y - mittelpunkt_Kreis1_y) / d + mittelpunkt_Kreis1_x;
        }

        private double schnittpunkt_y_Achse(double mittelpunkt_Kreis1_x, double mittelpunkt_Kreis1_y, double r1, double mittelpunkt_Kreis2_x, double mittelpunkt_Kreis2_y, double r2)
        {
            double d = Math.Sqrt(Math.Pow((mittelpunkt_Kreis1_x - mittelpunkt_Kreis2_x), 2) + Math.Pow((mittelpunkt_Kreis1_y - mittelpunkt_Kreis2_y), 2));
            double l = (Math.Pow(r1, 2) - Math.Pow(r2, 2) + Math.Pow(d, 2)) / (d * 2);
            double h = Math.Sqrt(Math.Pow(r1, 2) - Math.Pow(l, 2));
                        

            return l * (mittelpunkt_Kreis2_y - mittelpunkt_Kreis1_y) / d + h * (mittelpunkt_Kreis2_x - mittelpunkt_Kreis1_x) / d + mittelpunkt_Kreis1_y;
        }
    }
}
