using running_bunny.Modell;

namespace running_bunny.Business
{
    public class ZeitplanErstellung
    {
        public static void ErstelleZeitplan(IEnumerable<Schueler> schueler, IEnumerable<Unternehmen> unternehmen, IEnumerable<Raum> raeume)
        {
            var wuenscheProVeranstaltung = WuenscheProVeranstaltung(schueler, unternehmen);
            var zeitpläne = ErstellungZeitplanBasierendAufWuenscheMitPrio(wuenscheProVeranstaltung, raeume);

            //Wo die Zuweisung der Personen vornehmen?
        }

        public static IEnumerable<WuenscheProUnternehmen> WuenscheProVeranstaltung(IEnumerable<Schueler> schueler, IEnumerable<Unternehmen> unternehmen)
        {
            /*Ausgabeobjekt
            Teilschritte: 
                - Zusammenzählen aller Schülerwünsche für ein Unternehmen (Gruppierung nach Klasse spielt dabei keine Rolle)
                    - Berücksichtigen: Anzahl maximale Veranstaltungen für ein Unternehmen und frühester Zeitslot (A - 5 Veranstaltungen a Angabe Leute möglich
                      Außerdem: Priorisierung der Schüler (1 - 5 müssen berücksichtigt werden, 6. Wunsch optional)
                    Zählen für: Prio der Schüler 1 - 5 und Prio 6, addiert ergibt Gesamtnachfrage
                - Werfen von Fehler, wenn die Anzahl an Veranstaltungen pro Unternehmen mit Prio 1 - 5 nicht realisiert werden kann, dann Umverteilung nötig
                - Ausgabe von Wunsch-Objekt mit: 
                    Unternehmen, Anzahl Prios 1 - 5, Anzahl Prios 6
                
             */
            return new List<WuenscheProUnternehmen>();
        }

        public static IEnumerable<Zeitplan> ErstellungZeitplanBasierendAufWuenscheMitPrio(IEnumerable<WuenscheProUnternehmen> wuenscheProUnternehmen, IEnumerable<Raum> raeume)
        {
            /* Zuteilung Unternehmen zu Raum und Verteilung der Veranstaltungen: 
             *     - Räume 
             *  Berücksichtigen von leeren Schülerwünschen -> Freihalten  
             */
            return new List<Zeitplan>();
        }

        //Zeitplan Unternehmensorientiert gestalten
        //Frage: kann ein Unternehmen den Raum wechseln?

        public class Zeitplan
        {
            public Unternehmen Unternehmen { get; set; }
            public IEnumerable<ZeitslotInfo> ZeitslotInfo { get; set; }
        }

        public class ZeitslotInfo
        {
            public Zeitslot Zeitslot { get; set; }
            public IEnumerable<Schueler> Schueler { get; set; }
            public Raum Raum { get; set; }
        }

        public class WuenscheProUnternehmen
        {
            public Unternehmen Unternehmen { get; set; }
            public int AnzahlWuenscheFirstPriority { get; set; }
            public int AnzahlWuenscheLastPriority { get; set; }
        }
    }
}
