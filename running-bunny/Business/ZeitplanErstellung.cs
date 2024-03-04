using running_bunny.Modell;

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
                    .GroupBy(idUndSchueler => idUndSchueler.VeranstaltungsId)
                    .ToDictionary(grouping => grouping.Key, grouping => grouping.Select(e => e.SchuelerObj));

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
                    .GroupBy(e => e.Prioritaet)
                    .ToDictionary(grouping => grouping.Key,
                    grouping => grouping.Select(e => e.schueler).ToList());

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
            IEnumerable<WuenscheProVeranstaltung> wuenscheProVeranstaltungen
            , IEnumerable<Raum> raeume)
        {
            /*Schritte: 
             - Zusammenzählen der Wünsche 1 - 5
             - Berechnen der benötigten Veranstaltungen
                - Beachten: Wenn Gesamtanzahl an Leuten überschritten ist, 6. Wunsch miteinpflegen
            - HIER: Nur die Schüler mit Wünschen berücksichtigen
             */

            foreach (var wuenscheProVeranstaltung in wuenscheProVeranstaltungen)
            {
                var veranstaltung = wuenscheProVeranstaltung.Veranstaltung;

                var maxAnzahlMöglicheKurse = Math.Max((int)veranstaltung.FruehsterZeitSlot, veranstaltung.MaxAnzahlVerantstaltungen);
                //var möglicheKurse = 
                for(var i = 0; i < maxAnzahlMöglicheKurse; i++)
                {
                    
                }

                var maxAnzahlSchuelerAlleKurse = maxAnzahlMöglicheKurse * veranstaltung.MaxAnzahlTeilnehmer;

                





                //Auffüllen der maximalen Anzahl, dann Abziehen
                //Zusammenzählen Wünsche Prio 1 - 5

            }

            //Wichtige Aspekte: Fülle des Kurses, Berücksichtigung Wünsche, Raumverteilung möglichst geschickt
            //Backtracking-Algorithmus, Fülle des Kurses berechnen, sonst zurückkehren (Rekursion?)

            return new List<RaumzuweisungVeranstaltungsKurs>();
        }

        //public class MinimalKurs
        //{
        //    public int MöglicheAnzahlTeilnehmer { get; set; }
        //}

        public class WuenscheProVeranstaltung
        {
            public Veranstaltung Veranstaltung { get; set; }
            public Dictionary<int, List<Schueler>> SchuelerProPrio { get; set; }
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

