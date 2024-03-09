using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows;
using running_bunny.Model;
using running_bunny.Business;

namespace running_bunny.RaumZeitPlan
{
    public class RaumZeitPlanErstellung
    {
        public List<Schueler> SchuelerListe { get; set; }
        public List<Veranstaltung> VeranstaltungsListe { get; set; }
        public List<Raum> RaumListe { get; set; }


        //Fields für zufällige RaumSlot Zuweisung
        private Random random = new Random();
        private int zufallsZahl = 0;
        private Raum freierRaum;
        private String[] arrayFuerZufall = null;

        public RaumZeitPlanErstellung(List<Schueler> schuelerListe, List<Veranstaltung> veranstaltungsListe, List<Raum> raumListe)
        {
            SchuelerListe = schuelerListe;
            VeranstaltungsListe = veranstaltungsListe;
            RaumListe = raumListe;

            WunschProVeranstaltung();
            RaumZeitPlanZuweisung();
        }
        private void WunschProVeranstaltung()
        {

            foreach(Veranstaltung veranstaltung in VeranstaltungsListe)
            {
                foreach(Schueler schueler in SchuelerListe)
                {
                    Wunsch wunsch = schueler.Wuensche.Find(swu => swu.VeranstaltungsId == veranstaltung.Id);
                    if (wunsch != null)
                    {
                        veranstaltung.AnzahlWünsche = veranstaltung.AnzahlWünsche + 1;
                    }
                    //veranstaltung.AnzahlWünsche++; //wird setter aufgerufen?
                    
                }
                Debug.WriteLine(veranstaltung.UnternehmensName + " " + veranstaltung.Fachrichtung + " " + veranstaltung.AnzahlWünsche);
                
                Debug.WriteLine("");
            }
            Debug.WriteLine("///////////////////////////"); Debug.WriteLine("///////////////////////////");
        }
        private void RaumZeitPlanZuweisung()
        {

            List<ZelleRaumZeitplan> RaumZeitplan = new List<ZelleRaumZeitplan>();


            foreach (int zeitslot in Enum.GetValues(typeof(Zeitslot)))
            {
                Zeitslot tmpZeitslot = (Zeitslot)Enum.Parse(typeof(Zeitslot), zeitslot.ToString());

                if (zeitslot == 1 )
                {
                    foreach (Veranstaltung veranstaltung in VeranstaltungsListe)
                    {
                        Random random = new Random();
                            
                        if(zeitslot >= (int)veranstaltung.FruehsterZeitSlot)
                        {

                            Raum freierRaum = SucheFreienRaum();
                            if(freierRaum != null)
                            {
                                ZelleRaumZeitplan zelleRaumZeitplan = new ZelleRaumZeitplan(tmpZeitslot, veranstaltung, freierRaum);
                                veranstaltung.RaeumeBesetzt++;

                                RaumZeitplan.Add(zelleRaumZeitplan);
                            }
                        }
                    }
                    RaumListe.ForEach(raum => raum.IstBelegt = false);
                }
                if(zeitslot > 1)
                {
                    //Zuweisung  Unternehmen mit mehreren Zeitslots = gleiche Räume (1),
                    //Zuweisung Slots an übrigen Veranstaltungen
                    foreach(Veranstaltung veranstaltung in VeranstaltungsListe)
                    {
                        if (zeitslot <= (int)veranstaltung.FruehsterZeitSlot) continue;
                        if(veranstaltung.RaeumeBesetzt < veranstaltung.AnzahlRaeume)
                        {
                            ZelleRaumZeitplan zelle = RaumZeitplan.Find(zelle => zelle.Veranstaltung.Id == veranstaltung.Id); //Suche nach vergangen Zellen für (1) 
                            ZelleRaumZeitplan zelleRaumZeitplan;
                            if(zelle != null) //(1) gefunden
                            {
                                zelleRaumZeitplan = new ZelleRaumZeitplan(tmpZeitslot, veranstaltung, zelle.Raum);
                                RaumListe.Find(raum => raum.Bezeichnung == zelle.Raum.Bezeichnung).IstBelegt = true;
                            }
                            else
                            {
                                Raum freierRaum = SucheFreienRaum();
                                zelleRaumZeitplan = new ZelleRaumZeitplan(tmpZeitslot, veranstaltung, freierRaum);
                                veranstaltung.RaeumeBesetzt++;
                            }
                            
                            RaumZeitplan.Add(zelleRaumZeitplan);
                        }
                    }
                    RaumListe.ForEach(raum => raum.IstBelegt = false);
                }
                
            }
            DebugRaumZeitPlan(RaumZeitplan);
        }
        private void DebugRaumZeitPlan(List<ZelleRaumZeitplan> raumZeitPlan)
        {
            raumZeitPlan.Sort();
            Debug.WriteLine("RAUMZEITPLANUNG");
            foreach(ZelleRaumZeitplan raumZeit in raumZeitPlan)
            {
                
                Debug.WriteLine("UNTERNEHMEN:" + raumZeit.Veranstaltung.UnternehmensName +  "   VERANSTALTUNG: " + raumZeit.Veranstaltung.Fachrichtung + "  RAUM " + raumZeit.Raum.Bezeichnung + "  ZEITSLOT " + raumZeit.Zeitslot);
            }
        }

        private Raum SucheFreienRaum()
        {
            //Zufällige Zuweisung unvollständig!
            //if(arrayFuerZufall == null)
            //{
            //    arrayFuerZufall = new String[RaumListe.Count];
            //    for(int i = 0; i< RaumListe.Count; i++)
            //    {
            //        arrayFuerZufall[i] = RaumListe.ElementAt(i).Bezeichnung;
            //    }
            //}

            //while (true)
            //{
            //    zufallsZahl = random.Next(arrayFuerZufall.Length);
            //    freierRaum = RaumListe.ElementAt(zufallsZahl);
            //    if (freierRaum.IstBelegt == true)
            //        arrayFuerZufall[zufallsZahl] = null;
            //    else
            //    {
            //        freierRaum.IstBelegt = true;
            //        return freierRaum;
            //    }

            //}

            foreach(Raum raum in RaumListe)
            {

                if(!raum.IstBelegt)
                {
                    Raum freierRaum = raum;
                    freierRaum.IstBelegt = true;
                    return freierRaum;
                }
                
            }
            return null;

        }
        
    }
}
