
using System.Collections;
using running_bunny.Model;
using running_bunny.Business;

namespace running_bunny.Business
{
    public class ZeitplanErstellung
    {
        #region Notizen

        /*
        Teilschritte: 
            - Zusammenzählen aller Schülerwünsche für ein Unternehmen (Gruppierung nach Klasse spielt dabei keine Rolle, oder evtl. doch wegen sozialen Aspekten)
                - Berücksichtigen: Anzahl maximale Veranstaltungen für ein Unternehmen und frühester Zeitslot (A - 5 Veranstaltungen a Angabe Leute möglich)
                  Außerdem: Priorisierung der Schüler (1 - 5 müssen berücksichtigt werden, 6. Wunsch optional)
                Zählen für: Prio der Schüler 1 - 5 und Prio 6, addiert ergibt Gesamtnachfrage
            - Werfen von Fehler, wenn die Anzahl an Veranstaltungen pro Unternehmen mit Prio 1 - 5 nicht realisiert werden kann, dann Umverteilung nötig
            - Ausgabe von Wunsch-Objekt mit: 
                Unternehmen, Anzahl Prios 1 - 5, Anzahl Prios 6                
         */

        #endregion

        public /*IEnumerable<Zeitplan>*/void ErstelleZeitplan(IEnumerable<Schueler> schueler, IEnumerable<Veranstaltung> unternehmen, IEnumerable<Raum> raeume)
        {
            var wuenscheProVeranstaltung = ZaehleWuenscheProVeranstaltung(schueler, unternehmen);
            var kurse = ErmittlungsKurseUndZuweisungZuZeitslotsUndRaum(wuenscheProVeranstaltung, raeume);
            //var zuweisungSchuelerZuKursen =


            //var zeitpläne = ErstellungZeitplanBasierendAufWuenscheMitPrio(wuenscheProVeranstaltung, raeume);

            //Wo die Zuweisung der Personen vornehmen?
            //return new List<Zeitplan>();
        }

        /// <summary>
        /// Liefert für die übergebenen Veranstaltung-Objekte eine Liste aller Schüler zurück, gruppiert nach der Priorität.
        /// </summary>
        /// <param name="schuelerListe"></param>
        /// <param name="veranstaltungen"></param>
        /// <returns></returns>
        public static IEnumerable<WuenscheProVeranstaltung> ZaehleWuenscheProVeranstaltung(IEnumerable<Schueler> schuelerListe, IEnumerable<Veranstaltung> veranstaltungen)
        {
            //Zweck: ZAEHLENWENN-Funktion ersetzen und die Wünsche für eine Veranstaltung zählen (alle Priorität)

            /*TODO: 
             * Code funktioniert zwar, aber ginge wahrscheinlich auch etwas einfacher
             */
            var schuelerNachVeranstalterIds = schuelerListe.Select(schueler =>
                schueler.Wuensche.Select(wunsch =>
                new { wunsch.VeranstaltungsId, SchuelerObj = schueler }))
                    .SelectMany(idUndSchueler => idUndSchueler)
                    .ToLookup(idUndSchueler => idUndSchueler.VeranstaltungsId, idUndSchueler => idUndSchueler.SchuelerObj);

            var alleVeranstaltungIds = veranstaltungen.Select(veranstaltung => veranstaltung.Id);
            var schuelerWuenscheOhneVeranstaltung = schuelerNachVeranstalterIds.Where(grouping => !alleVeranstaltungIds.Contains(grouping.Key));
            if (schuelerWuenscheOhneVeranstaltung.Count() > 0)
            {
                throw new ArgumentException("In der eingelesenen Excel-Liste der Schülerwünsche wurden folgende Veranstaltung-Ids gefunden, " +
                    $"die nicht zugeordnet werden konnten: [{schuelerWuenscheOhneVeranstaltung.Select(e => e.Key)}]");
            }

            var ergebnisList = new List<WuenscheProVeranstaltung>();
            foreach (var veranstaltung in veranstaltungen)
            {
                var schuelerFuerVeranstaltung = schuelerNachVeranstalterIds[veranstaltung.Id];

                var schuelerNachPrio = schuelerFuerVeranstaltung
                    .Select(schueler => schueler.Wuensche.Where(wunsch => wunsch.VeranstaltungsId == veranstaltung.Id)
                        .Select(wunsch => new { wunsch.Prioritaet, schueler }))
                    .SelectMany(e => e)
                    .ToLookup(e => e.Prioritaet, e => e.schueler);

                var wuenscheProVeranstaltung = new WuenscheProVeranstaltung
                {
                    Veranstaltung = veranstaltung,
                    SchuelerProPrio = schuelerNachPrio
                };
                ergebnisList.Add(wuenscheProVeranstaltung);
            }

            return ergebnisList;
        }

        public static IEnumerable<RaumzuweisungVeranstaltungsKurs> ErmittlungsKurseUndZuweisungZuZeitslotsUndRaum(
            IEnumerable<WuenscheProVeranstaltung> wuenscheProVeranstaltungen, IEnumerable<Raum> raeume)
        {
            /*Schritte: 
             - Zusammenzählen der Wünsche 1 - 5
             - Berechnen der benötigten Veranstaltungen
                - Beachten: Wenn Gesamtanzahl an Leuten überschritten ist, 6. Wunsch miteinpflegen
            - HIER: Nur die Schüler mit Wünschen berücksichtigen
             */

            foreach (var wuenscheProVeranstaltung in wuenscheProVeranstaltungen)
            {

                //for(var i = 0; i < maxAnzahlMöglicheKurse; i++)
                //{

                //}

                //var maxAnzahlSchuelerAlleKurse = maxAnzahlMöglicheKurse * veranstaltung.MaxAnzahlTeilnehmer;

                //Auffüllen der maximalen Anzahl, dann Abziehen
                //Zusammenzählen Wünsche Prio 1 - 5

            }

            //Wichtige Aspekte: Fülle des Kurses, Berücksichtigung Wünsche, Raumverteilung möglichst geschickt
            //Backtracking-Algorithmus, Fülle des Kurses berechnen, sonst zurückkehren (Rekursion?)

            return new List<RaumzuweisungVeranstaltungsKurs>();
        }

        private bool PassenderWegRaum(WuenscheProVeranstaltung aktuelleVeranstaltung,
            Queue<WuenscheProVeranstaltung> restlicheVeranstaltungen,
            IEnumerable<RaumZeit> verfügbareRaumZeit,
            int priosZuBerücksichtigen)
        {
            //Wenn weniger als die ersten 3 Prios berücksichtigt werden, gibt es keinen Weg
            if (priosZuBerücksichtigen < 3) { return false; }

            var veranstaltung = aktuelleVeranstaltung.Veranstaltung;
            var wünscheProPrio = aktuelleVeranstaltung.SchuelerProPrio.Where(e => e.Key <= priosZuBerücksichtigen).SelectMany(schueler => schueler).Count();

            var ersteRaumZeit = verfügbareRaumZeit.First();
            if (!PassenAlleSchuelerInVerbleibendeRaumzeit(ersteRaumZeit.Raum, new Queue<Zeitslot>(ersteRaumZeit.ZeitSlots.OrderByDescending(e => (int)e).ToList()), wünscheProPrio))
            {
                restlicheVeranstaltungen.Enqueue(aktuelleVeranstaltung);
                var nextTryVeranstaltung = restlicheVeranstaltungen.Dequeue();
                return PassenderWegRaum(nextTryVeranstaltung, restlicheVeranstaltungen, verfügbareRaumZeit, priosZuBerücksichtigen);
            }
            
            return false;
        }

        private bool PassenAlleSchuelerInVerbleibendeRaumzeit(Raum raum, Queue<Zeitslot> verbleibendeZeitslots, int verbleibendeSchueler)
        {
            if (!verbleibendeZeitslots.Any() && verbleibendeSchueler > 0)
            {
                return false;
            }
            var kapazität = raum.Kapazitaet;
            if (verbleibendeSchueler < kapazität)
            {
                return true;
            }
            verbleibendeZeitslots.Dequeue();
            return PassenAlleSchuelerInVerbleibendeRaumzeit(raum, verbleibendeZeitslots, verbleibendeSchueler - kapazität);
        }

        private class RaumZeit
        {
            public Raum Raum { get; set; }
            public List<Zeitslot> ZeitSlots { get; set; }
        }

        public class WuenscheProVeranstaltung
        {
            public Veranstaltung Veranstaltung { get; set; }
            public ILookup<int, Schueler> SchuelerProPrio { get; set; }
        }

        public class RaumzuweisungVeranstaltungsKurs
        {
            public Raum Raum { get; set; }
            public Zeitslot Zeitslot { get; set; }
            public Veranstaltung Veranstaltung { get; set; }
            public int MogelicheAnzahlSchueler { get; set; }
        }

        public class Kurs
        {
            public Veranstaltung Veranstaltung { get; set; }
            public List<Schueler> Schueler { get; set; }
            public bool IstKursVoll => Veranstaltung.MaxAnzahlTeilnehmer == Schueler.Count;
        }
    }
}

//public static IEnumerable<Zeitplan> ErstellungZeitplanBasierendAufWuenscheMitPrio(IEnumerable<WuenscheProVeranstaltung> wuenscheProUnternehmen, IEnumerable<Raum> raeume)
//{
//    /* Zuteilung Unternehmen zu Raum und Verteilung der Veranstaltungen: 
//     *     - Räume 
//     *  Berücksichtigen von leeren Schülerwünschen -> Freihalten  
//     */
//    return new List<Zeitplan>();
//}

//Zeitplan Unternehmensorientiert gestalten
//Frage: kann ein Unternehmen den Raum wechseln?

//public class Zeitplan
//{
//    public Veranstaltung Unternehmen { get; set; }
//    public IEnumerable<ZeitslotInfo> ZeitslotInfo { get; set; }
//}

//public class ZeitslotInfo
//{
//    public Zeitslot Zeitslot { get; set; }
//    public IEnumerable<Schueler> Schueler { get; set; }
//    public Raum Raum { get; set; }
//}

