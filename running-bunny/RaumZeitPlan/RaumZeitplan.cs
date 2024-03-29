﻿using System;
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
using Word = Microsoft.Office.Interop.Word;
using System.Security.Policy;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Office.Interop.Word;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics.Metrics;
using System.Runtime.InteropServices;
using System.Reflection;

namespace running_bunny.RaumZeitPlan
{
    public class RaumZeitplan
    {
        public List<Schueler> SchuelerListe { get; set; }
        public List<Veranstaltung> VeranstaltungsListe { get; set; }
        public List<Raum> RaumListe { get; set; }
        public List<ZelleRaumZeitplan> RaumZeitplanListe { get; set; } = new List<ZelleRaumZeitplan>();


        //Fields für zufällige RaumSlot Zuweisung
        private Random random = new Random();
        private int zufallsZahl = 0;
        private Raum freierRaum;
        private String[] arrayFuerZufall = null;
        private bool istgemischt = false;

        public RaumZeitplan(List<Schueler> schuelerListe, List<Veranstaltung> veranstaltungsListe, List<Raum> raumListe)
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
            foreach (Veranstaltung veranstaltung in VeranstaltungsListe) //Durchsucht alle Veranstaltungen
            {
                counter++;
                foreach (Schueler schueler in SchuelerListe) //Durchsucht alle Schüler
                {
                    Wunsch wunsch = schueler.Wuensche.Find(swu => swu.VeranstaltungsId == veranstaltung.Id && swu.Prioritaet < 6); //Sucht ersten Wunsch mit gleicher VeranstaltungsID und einer Priorität < 6
                    if (wunsch != null) //Falls kein wunsch gefunden wurde, wird Wunsch übersprungen
                    {
                        veranstaltung.AnzahlWünsche = veranstaltung.AnzahlWünsche + 1; //Erhöht die Anzahl der Wünsche in Veranstaltung um 1
                    }
                    //veranstaltung.AnzahlWünsche++; //wird setter aufgerufen?

                }

                veranstaltung.BerechneBenoetigteKurse();

            }
        }

        public void RaumZeitPlanZeilenweiseZuweisung()
        {
            MischeRaumListe();
            foreach (Raum raum in RaumListe) //Durchsucht Raum in Raumliste
            {
                if (!raum.IstRaumVoll()) //Fragt ab, ob Raum voll ist
                {
                    foreach (Veranstaltung ver in VeranstaltungsListe) //Durchsucht Veranstaltungen in Veranstaltungsliste
                    {

                        if (ver.RaeumeBesetzt < ver.AnzahlKurse && !raum.IstRaumVoll()) //Fragt ob, die Anzahl der reservierten Räume kleiner als die Anahl der benötigten Räumen in Veranstaltung sind
                        {
                            int index = FindeFreieStelle(ver, raum); //Übergibt Veranstaltung und Raum um eine freie Stelle zu suchen
                            if (index != -1) //Bei -1 keine Stelle gefunden
                            {
                                for (int i = 0; i < ver.AnzahlKurse; i++)
                                {
                                    ver.RaeumeBesetzt++; //erhöhung anzahl besesetzte räume um 1
                                    ver.Zeitslots.Add((Zeitslot)(index + i + 1)); //speichert alle slots ab, die von der veranstaltung besetzt werden
                                    ZelleRaumZeitplan zelle = new ZelleRaumZeitplan((Zeitslot)(index + i + 1), ver, raum); //zelle wird erstellt
                                    raum.BelegteSlots[index + i] = zelle;  //raumbelegung wird abgespeichert
                                    RaumZeitplanListe.Add(zelle); //zelle wird in RaumZeitPlanliste hinzugefügt

                                }
                            }
                        }

                    }
                }
            }
        }


        public int FindeFreieStelle(Veranstaltung ver, Raum raum)
        {

            int benoetigteRaueme = ver.AnzahlKurse;

            int index = -1; // -1 => keine freie Stelle gefunden
            for (int i = 0; i < raum.BelegteSlots.Length; i++) //iteriert durch alle Slots
            {
                for (int k = 0; k < benoetigteRaueme; k++) //iteriert durch benötigte Räume
                {
                    int indexMomentan = i + k; //Fasst i und k zusammmen um aktuellen index zu speichern

                    if (indexMomentan <= raum.BelegteSlots.Length - 1 && raum.BelegteSlots[indexMomentan] == null && indexMomentan >= (int)ver.FruehsterZeitSlot - 1) //Fragt ob, index gültigkeit hat
                    {
                        if (k == benoetigteRaueme - 1) return index = i; //falls das ende erreicht wurde, gibt index zurück
                        continue;
                    }
                    else
                        break; //Falls Bedingungen nicht gelten, wird innere schleife abgebrochen
                }
            }
            return index;
        }
        private void MischeRaumListe() //Durchmischt räume in Raumliste 
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


            foreach (Raum raum in RaumListe)
            {

                if (!raum.IstBelegt)
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
