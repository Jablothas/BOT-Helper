using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using running_bunny.Model;
using running_bunny.RaumZeitPlan;

namespace running_bunny.WunschRaumZeitPlanZuweisung
{
    public class WunschRaumZeitPlanZuweisungErstellung
    {
        //Zuweisung anhand Erfüllungsscore
        //Prüfung in jeder Zuweisungsrunde. Schüler mit kleinen Scores -> Wunscherfüllung
        //Bei erfolgreicher Zuweisung -> Schüler erhält Punkte
        //Zuweisung immer "Wunsch.zelle = ZelleRaumZeitPlan". Dadurch Informationen für Wunsch zu Zeitslot und Veranstaltung

        public List<Schueler> SchuelerListe { get; set; }
        private List<Veranstaltung> VeranstaltungsListe { get; set; } //nötig?, weil bestenfalls alle Veranstaltungen durch "ZelleRaumZeitPlan"-Objekten abgedeckt werden
        public List<ZelleRaumZeitplan> ZelleRaumZeitplanListe { get; set; }

        public WunschRaumZeitPlanZuweisungErstellung(List<Schueler> schuelerListe, List<ZelleRaumZeitplan> zellenListe)
        {
            SchuelerListe = schuelerListe;
            ZelleRaumZeitplanListe = zellenListe;
            Erstellen();
        }
        public void Erstellen()
        {
            Debug.WriteLine("////////////////////////");
            Debug.WriteLine("Zuweisung Wunsch zu Zelle");

            while(true)
            {
                Schueler tmpSchueler = (Schueler)(SchuelerListe.OrderBy(schueler => schueler.SummeGewichtung).FirstOrDefault());
                int kleinsteGewichtung = tmpSchueler.SummeGewichtung;
                List<Schueler> SchuelerMitKleinstenScores = SchuelerListe.Where(schueler => schueler.SummeGewichtung == kleinsteGewichtung).ToList();


                foreach (Schueler schueler in SchuelerMitKleinstenScores)
                {
                    for(int i = 0; i < schueler.Wuensche.Count; i++)
                    {
                        if (schueler.Wuensche[i].zelle is null)
                        {

                            schueler.Wuensche[i].zelle = SuchePassendeZelle(schueler.Wuensche[i]);
                        }    
                            
                    }
                }

                break;
            }
        }
        public ZelleRaumZeitplan SuchePassendeZelle(Wunsch wunsch)
        {
            //Soll Veranstaltung zunächst komplett gefüllt werden?
            return new ZelleRaumZeitplan(RaumZeitPlan.Zeitslot.A, null, null);
        }
    }
}
