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
          
            //                                                Punkte
            //Nullpunkte
            double x0 = 0;
            double y0 = 0;
            double radius =20;
            
            //Startpunkte
            double StartPkt_Fußkreis_x = x0;
            double StartPkt_Fußkreis_y = df/2;
            double StartPkt_Kopfkreis_x = x0;
            double StartPkt_Kopfkreis_y = da/2;
            //Endpunkte
            double EndPkt_Kopfkreis_x = p/2;
            double EndPkt_Kopfkreis_y = da/2;
            double EndPkt_Fußkreis_x =  p/2;
            double EndPkt_Fußkreis_y = df/2;
            //Winkelpunkt
            double Alpha = z / 2 * Math.PI;
            double EndPkt_Radius_x = Math.Sin (90 * Math.PI/180 - Alpha) * df/2;
            double EndPkt_Radius_y = Math.Cos (90 * Math.PI/180 - Alpha) * df/2;

          

            Factory2D catFactory2D1 =hsp_catiaProfil.OpenEdition();

            Point2D catP2D_MPkt_Radius = catFactory2D1.CreatePoint(x0, y0 );
            
            Point2D catP2D_StartPkt_Fußkreis = catFactory2D1.CreatePoint(StartPkt_Fußkreis_x, StartPkt_Fußkreis_y);
            Point2D catP2D_StartPkt_Kopfkreis = catFactory2D1.CreatePoint(StartPkt_Kopfkreis_x, StartPkt_Kopfkreis_y);
            Point2D catP2D_EndPkt_Kopfkreis = catFactory2D1.CreatePoint(EndPkt_Kopfkreis_x, EndPkt_Kopfkreis_y);
            Point2D catP2D_EndPkt_Fußkreis = catFactory2D1.CreatePoint(EndPkt_Fußkreis_x, EndPkt_Fußkreis_y);
            Point2D catP2D_EndPkt_Radius = catFactory2D1.CreatePoint(EndPkt_Radius_x, EndPkt_Radius_y );
          

             //                                               Linien

            Line2D catLine2D1 = catFactory2D1.CreateLine(StartPkt_Fußkreis_x, StartPkt_Fußkreis_y, StartPkt_Kopfkreis_x, StartPkt_Kopfkreis_y );
            Line2D catLine2D2 = catFactory2D1.CreateLine(StartPkt_Kopfkreis_x, StartPkt_Kopfkreis_y, EndPkt_Kopfkreis_x, EndPkt_Kopfkreis_y);
            Line2D catLine2D3 = catFactory2D1.CreateLine(EndPkt_Kopfkreis_x, EndPkt_Kopfkreis_y, EndPkt_Fußkreis_x, EndPkt_Fußkreis_y);
          //  Line2D catLine2D4 = catFactory2D1.CreateLine(EndPkt_Fußkreis_x, EndPkt_Fußkreis_y,EndPkt_Radius_x, EndPkt_Radius_y);

             Circle2D catC2D_Fußkreis = catFactory2D1.CreateCircle(catP2D_MPkt_Radius,radius);

            // Skizzierer verlassen
            hsp_catiaProfil.CloseEdition();
            // Part aktualisieren
            hsp_catiaPart.Part.Update();
        }

        public void ErzeugeBalken(Double l)
        {
            // Hauptkoerper in Bearbeitung definieren
            hsp_catiaPart.Part.InWorkObject = hsp_catiaPart.Part.MainBody;

            // Block(Balken) erzeugen
            ShapeFactory catShapeFactory1 = (ShapeFactory)hsp_catiaPart.Part.ShapeFactory;
            Pad catPad1 = catShapeFactory1.AddNewPad(hsp_catiaProfil, l);

            // Block umbenennen
            catPad1.set_Name("Balken");

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
    }
}
