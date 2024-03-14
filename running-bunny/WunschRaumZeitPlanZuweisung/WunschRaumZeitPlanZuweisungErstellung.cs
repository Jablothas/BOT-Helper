using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using running_bunny.Model;
using running_bunny.RaumZeitPlan;

namespace running_bunny.WunschRaumZeitPlanZuweisung
{
    public class WunschRaumZeitPlanZuweisungErstellung
    {
        //Zuweisung anhand Erfüllungsscore
        //Prüfung in jeder Zuweisungsrunde. Schüler mit kleinen Scores -> Wunscherfüllung
        //Bei erfolgreicher Zuweisung -> Schüler erhält Punkte
        //Zuweisung immer Wunsch = ZelleRaumZeitPlan. Dadurch Informationen für Wunsch zu Zeitslot und Veranstaltung

        public List<Schueler> SchuelerListe { get; set; }
        private List<Veranstaltung> VeranstaltungsListe { get; set; } //nötig?, weil bestenfalls alle Veranstaltungen durch "ZelleRaumZeitPlan"-Objekten abgedeckt werden
        public List<ZelleRaumZeitplan> ZelleRaumZeitplanListe { get; set; } = new List<ZelleRaumZeitplan>();

        public WunschRaumZeitPlanZuweisungErstellung(List<Schueler> schuelerListe, List<ZelleRaumZeitplan> zellenListe)
        {
            SchuelerListe = schuelerListe;
            ZelleRaumZeitplanListe = zellenListe;
        }
    }
}
