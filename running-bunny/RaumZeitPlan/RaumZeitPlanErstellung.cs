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
using System.Reflection.Metadata.Ecma335;
using Microsoft.Office.Interop.Word;
using System.Security.Policy;

namespace running_bunny.RaumZeitPlan
{
    public class RaumZeitPlanErstellung
    {
        public List<Schueler> SchuelerListe { get; set; }
        public List<Veranstaltung> VeranstaltungsListe { get; set; }
        public List<Raum> RaumListe { get; set; }
        public List<ZelleRaumZeitplan> RaumZeitplan { get; set; } = new List<ZelleRaumZeitplan>();


        //Fields für zufällige RaumSlot Zuweisung
        private Random random = new Random();
        private int zufallsZahl = 0;
        private Raum freierRaum;
        private String[] arrayFuerZufall = null;
        private bool istgemischt = false;

        public RaumZeitPlanErstellung(List<Schueler> schuelerListe, List<Veranstaltung> veranstaltungsListe, List<Raum> raumListe)
        {
            SchuelerListe = schuelerListe;
            VeranstaltungsListe = veranstaltungsListe;
            RaumListe = raumListe;

            WunschProVeranstaltung();
            RaumZeitPlanZeilenweiseZuweisung();
        }
        private void WunschProVeranstaltung()
        {
            int counter = 0;
            foreach(Veranstaltung veranstaltung in VeranstaltungsListe)
            {
                counter++;
                foreach(Schueler schueler in SchuelerListe)
                {
                    Wunsch wunsch = schueler.Wuensche.Find(swu => swu.VeranstaltungsId == veranstaltung.Id && swu.Prioritaet < 6);
                    if (wunsch != null)
                    {
                        veranstaltung.AnzahlWünsche = veranstaltung.AnzahlWünsche + 1;
                    }
                    //veranstaltung.AnzahlWünsche++; //wird setter aufgerufen?
                    
                }
                veranstaltung.BerechneBenoetigteRaeume();
                Debug.WriteLine($"{counter}.     " + veranstaltung.UnternehmensName + " " + veranstaltung.Fachrichtung + " " + veranstaltung.AnzahlWünsche + " Benötigte Räume " + veranstaltung.AnzahlRaeume + "\n");
            }
            Debug.WriteLine("///////////////////////////"); Debug.WriteLine("///////////////////////////");
        }
        //private void RaumZeitPlanSpaltenweiseZuweisung() nicht in Nutzung!
        //{
        //    foreach (int zeitslot in Enum.GetValues(typeof(Zeitslot)))
        //    {
        //        Zeitslot tmpZeitslot = (Zeitslot)Enum.Parse(typeof(Zeitslot), zeitslot.ToString());

        //        if (zeitslot == 1 )
        //        {
        //            foreach (Veranstaltung veranstaltung in VeranstaltungsListe)
        //            {
        //                Random random = new Random();
                            
        //                if(zeitslot >= (int)veranstaltung.FruehsterZeitSlot)
        //                {

        //                    Raum freierRaum = SucheFreienRaum();
        //                    if(freierRaum != null)
        //                    {
        //                        ZelleRaumZeitplan zelleRaumZeitplan = new ZelleRaumZeitplan(tmpZeitslot, veranstaltung, freierRaum);
        //                        veranstaltung.RaeumeBesetzt++;

        //                        RaumZeitplan.Add(zelleRaumZeitplan);
        //                    }
        //                }
        //            }
        //            RaumListe.ForEach(raum => raum.IstBelegt = false);
        //        }
        //        if(zeitslot > 1)
        //        {
        //            //Zuweisung  Unternehmen mit mehreren Zeitslots = gleiche Räume (1),
        //            //Zuweisung  an übrigen Veranstaltungen ohne Slot bisher (2)
        //            foreach(Veranstaltung veranstaltung in VeranstaltungsListe)
        //            {
        //                if (zeitslot <= (int)veranstaltung.FruehsterZeitSlot) continue;
        //                if(veranstaltung.RaeumeBesetzt < veranstaltung.AnzahlRaeume)
        //                {
        //                    ZelleRaumZeitplan zelle = RaumZeitplan.Find(zelle => zelle.Veranstaltung.Id == veranstaltung.Id && zelle.Raum != null); //Suche nach vergangen Zellen für (1) 
        //                    ZelleRaumZeitplan zelleRaumZeitplan;
        //                    if(zelle != null) //(1) gefunden
        //                    {
        //                        zelleRaumZeitplan = new ZelleRaumZeitplan(tmpZeitslot, veranstaltung, zelle.Raum);
        //                        RaumListe.Find(raum => raum.Bezeichnung == zelle.Raum.Bezeichnung).IstBelegt = true;
        //                    }
        //                    else //(2)
        //                    {
        //                        Raum freierRaum = SucheFreienRaum();
        //                        if (freierRaum != null)
        //                            zelleRaumZeitplan = new ZelleRaumZeitplan(tmpZeitslot, veranstaltung, freierRaum);
        //                        else
        //                            continue;
        //                    }
        //                    veranstaltung.RaeumeBesetzt++;
        //                    RaumZeitplan.Add(zelleRaumZeitplan);
        //                    //if(zelleRaumZeitplan.Veranstaltung.)
        //                }
        //            }
        //            RaumListe.ForEach(raum => raum.IstBelegt = false);
        //        }
                
        //    }
        //    DebugRaumZeitPlan(RaumZeitplan);
        //}

        public void RaumZeitPlanZeilenweiseZuweisung()
        {
            MischeRaumListe();
            foreach(Raum raum in RaumListe)
            {
                if(!raum.IstRaumVoll())
                {
                    foreach (Veranstaltung ver in VeranstaltungsListe)
                    {
                        if(ver.RaeumeBesetzt < ver.AnzahlRaeume && !raum.IstRaumVoll())
                        {
                            int index = FindeFreieStelle(ver, raum);
                            if (index != -1)
                            {
                                for (int i = 0; i < ver.AnzahlRaeume; i++)
                                {
                                    ver.RaeumeBesetzt++;
                                    ZelleRaumZeitplan zelle = new ZelleRaumZeitplan((Zeitslot)(index + i + 1), ver, raum);
                                    raum.BelegteSlots[index + i] = zelle;
                                    RaumZeitplan.Add(zelle);

                                }
                            }
                        }
                       
                    }
                }
            }
            DebugRaumZeitPlan(RaumZeitplan);
        }

        private void DebugRaumZeitPlan(List<ZelleRaumZeitplan> raumZeitPlan)
        {
            int counter = 0;
           // raumZeitPlan.Sort();
            Debug.WriteLine("RAUMZEITPLANUNG");
            foreach(ZelleRaumZeitplan raumZeit in raumZeitPlan)
            {
                counter++;
                Debug.WriteLine($"{counter}."+"  UNTERNEHMEN:" + raumZeit.Veranstaltung.UnternehmensName +  "   VERANSTALTUNG: " + raumZeit.Veranstaltung.Fachrichtung + "  RAUM " + raumZeit.Raum.Bezeichnung + "  ZEITSLOT " + raumZeit.Zeitslot);
            }
        }


        public int FindeFreieStelle(Veranstaltung ver, Raum raum)
        {
            int benoetigteRaueme = ver.AnzahlRaeume;
            int index = -1; // -1 => keine freie Stelle gefunden
            for(int i = 0; i < raum.BelegteSlots.Length; i++)
            {
                for (int k = 0; k < benoetigteRaueme; k++)
                {
                    int indexMomentan = i + k;

                    if (indexMomentan <= raum.BelegteSlots.Length - 1 && raum.BelegteSlots[indexMomentan] == null && indexMomentan >= (int)ver.FruehsterZeitSlot - 1)
                    {
                        if (k == benoetigteRaueme - 1) return index = i;
                        continue;
                    }
                    else
                        break;
                }
            }
            return index;
        }
        private void MischeRaumListe()
        {
            //mischt die Raumliste vorab
            if (istgemischt == false)
            {
                int n = RaumListe.Count;
                while (n > 1)
                {
                    n--;
                    int k = random.Next(n + 1);
                    Raum raum = RaumListe[k];
                    RaumListe[k] = RaumListe[n];
                    RaumListe[n] = raum;
                }
                istgemischt = true;
            }
        }
        private Raum SucheFreienRaum()
        {
            

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
