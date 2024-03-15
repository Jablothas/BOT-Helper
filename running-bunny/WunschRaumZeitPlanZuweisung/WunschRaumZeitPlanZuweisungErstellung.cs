using System.Diagnostics;
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

            for (int wunschPrio = 1; wunschPrio <= 6; wunschPrio++)
            {
                //Schülerliste sortiert nach Scores
                var schuelerOrderedByScore = SchuelerListe.OrderBy(schueler => schueler.SummeGewichtung);

                //Gleichmäßige Verteilung
                foreach (var schueler in schuelerOrderedByScore)
                {
                    var wunsch = schueler.Wuensche.SingleOrDefault(wunsch => wunsch.Prioritaet == wunschPrio);
                    if (wunsch != null)
                    {
                        var passendeZelle = SuchePassendeZelle(wunsch, schueler.BelegteZeitslots.Select(slotZelle => slotZelle.Zeitslot));
                        if (passendeZelle != null)
                        {
                            wunsch.Zelle = passendeZelle;
                            passendeZelle.SchuelerListe.Add(schueler);

                            schueler.BelegteZeitslots.Add(new ZeitslotMitZelle { Zeitslot = passendeZelle.Zeitslot, Zelle = passendeZelle });

                            //Setzen von Erfüllungsscore
                            schueler.SummeGewichtung = schueler.SummeGewichtung + (7 - wunschPrio);
                        }
                    }
                }
            }
            WeiseAlleSchülerMitFreienZeitslotsVeranstaltungenZu();
        }

        private void WeiseAlleSchülerMitFreienZeitslotsVeranstaltungenZu()
        {
            var alleZeitslots = Enum.GetValues<Zeitslot>();
            var freieZellen = ZelleRaumZeitplanListe.Where(zelle => !zelle.RaumVoll && !zelle.MaxTeilnehmerErreicht).ToList();

            //Schüler ohne Wünsche irgendwelchen Veranstaltungen zuweisen
            var schuelerOhneBelegteZeitslots = SchuelerListe.Where(schueler => schueler.BelegteZeitslots.Count() != 5);
            foreach (var schueler in schuelerOhneBelegteZeitslots)
            {
                var blub = freieZellen.Where(zelle => zelle.Zeitslot == Zeitslot.E);
                var freieZellenNachZeitslot = freieZellen.ToLookup(zelle => zelle.Zeitslot, zelle => zelle);

                var fehlendeZeitslots = alleZeitslots.Except(schueler.BelegteZeitslots.Select(slotZelle => slotZelle.Zeitslot));
                foreach (var fehlenderZeitslot in fehlendeZeitslots)
                {
                    //TODO: Was wenn keine Zellen mehr frei sind, neuen Raum erstellen? Wirft aktuell noch Fehler
                    if (!freieZellenNachZeitslot.Contains(fehlenderZeitslot))
                    {
                        throw new NotImplementedException("Hier müssten prolly neue Kurse erstellt werden");
                    }

                    var ersteFreieZelle = freieZellenNachZeitslot[fehlenderZeitslot].OrderBy(zelle => zelle.SchuelerListe.Count).First();

                    ersteFreieZelle.SchuelerListe.Add(schueler);
                    schueler.BelegteZeitslots.Add(new ZeitslotMitZelle { Zeitslot = fehlenderZeitslot, Zelle = ersteFreieZelle });

                    //Wenn ein Kurs voll ist, wird er aus den freien Zellen gestrichen
                    if (ersteFreieZelle.RaumVoll || ersteFreieZelle.MaxTeilnehmerErreicht)
                    {
                        freieZellen.Remove(ersteFreieZelle);
                    }
                }
            }
        }

        public ZelleRaumZeitplan SuchePassendeZelle(Wunsch wunsch, IEnumerable<Zeitslot> belegteZeitslots)
        {
            //Suchen von Kursen, mit selber VeranstaltungsId und noch nicht in belegtem Zeitslot
            var zellenFuerVeranstaltung = ZelleRaumZeitplanListe.Where(zelle => zelle.Veranstaltung.Id == wunsch.VeranstaltungsId && !belegteZeitslots.Contains(zelle.Zeitslot));

            var möglicheZellen = new List<ZelleRaumZeitplan>();
            foreach (var zelle in zellenFuerVeranstaltung)
            {
                if (!zelle.MaxTeilnehmerErreicht && !zelle.RaumVoll)
                {
                    //Rückgabe von erstem Kurs, der infrage kommt
                    möglicheZellen.Add(zelle);
                }
            }

            if (möglicheZellen.Count() != 0)
            {
                //Zelle mit geringster Teilnehmeranzahl suchen
                return möglicheZellen.OrderBy(zelle => zelle.SchuelerListe.Count).First();
            }

            //Wenn kein Kurs gefunden wird, wird null zurückgegeben
            return null;
        }
    }
}
