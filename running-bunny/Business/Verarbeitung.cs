using Excel = Microsoft.Office.Interop.Excel;
using running_bunny.Model;
using running_bunny.RaumZeitPlan;
using running_bunny.WunschRaumZeitPlanZuweisung;
using System.Diagnostics;
namespace running_bunny.Business
{
    public class Verarbeitung
    {
        public void run(string schuelerFilePath, string veranstalterFilePath, string raumFilePath)
        {
            //TODO: Methoden static machen? Was Vor- und Nachteile?

            var schuelerExcel = ReadExcel(schuelerFilePath);
            var schuelerListe = SchuelerErstellen(schuelerExcel);

            var unternehmensExcel = ReadExcel(veranstalterFilePath);
            var veranstaltungsListe = VeranstaltungsListeErstellen(unternehmensExcel);

            var raumExcel = ReadExcel(raumFilePath);
            var raumListe = RaumErstellen(raumExcel);

            //var wuenscheNachUnternehmen = ZeitplanErstellung.ZaehleWuenscheProVeranstaltung(schuelerListe, veranstaltungsListe);
            //var unternehmenNachPrio = ZeitplanErstellung.ErstellungZeitplanBasierendAufWuenscheMitPrio(wuenscheNachUnternehmen, raumListe);
            RaumZeitPlanErstellung raumZeitPlanErstellung = new RaumZeitPlanErstellung(schuelerListe, veranstaltungsListe, raumListe);

            WunschRaumZeitPlanZuweisungErstellung wunschRaumZeitPlanZuweisungErstellung = new WunschRaumZeitPlanZuweisungErstellung
                                                                                                (raumZeitPlanErstellung.SchuelerListe, raumZeitPlanErstellung.RaumZeitplan);
            List<Schueler> schuelerListeFuerLaufzettel = wunschRaumZeitPlanZuweisungErstellung.SchuelerListe;
            List<ZelleRaumZeitplan> zellenListeFuerAnwesenheitslisteUNDRaumzeitplan = wunschRaumZeitPlanZuweisungErstellung.ZelleRaumZeitplan;

        }

        private List<Schueler> SchuelerErstellen(string[,] excel)
        {
            //Prüfen, ob die Datei das richtige Format besitzt
            var colCount = excel.GetLength(1);

            //Mindestangaben: Klassen, Name, Vorname
            if (colCount < 3 || colCount > 9)
            {
                throw new ArgumentException("Die Schüler-Datei enthält eine falsche Anzahl an Spalten. " +
                    "Mindestangaben sind Klasse, Name und Vorname. Es können höchstens 6 Wünsche pro Schüler angegeben werden.");
            }

            var schuelerListe = new List<Schueler>();

            //Iterieren durch die Zeilen
            for (int zeile = 0; zeile < excel.GetLength(0); zeile++)
            {
                var actualExcelLine = zeile + 2;

                //Eintragen der notwendigen Daten
                var schueler = new Schueler();
                schueler.Klasse = excel[zeile, 0];
                schueler.Nachname = excel[zeile, 1];
                schueler.Vorname = excel[zeile, 2];

                if (string.IsNullOrWhiteSpace(schueler.Klasse)
                    || string.IsNullOrWhiteSpace(schueler.Vorname)
                    || string.IsNullOrWhiteSpace(schueler.Nachname))
                {
                    throw new ArgumentException($"Die Klasse, der Vorname oder der Nachname sind leer. Fehler in Zeile {actualExcelLine}");
                }

                var wuensche = new List<Wunsch>();
                //TODO: Wie darauf reagieren, wenn verschiedene Wahlen gefüllt sind
                for (int spalte = 3; spalte < excel.GetLength(1); spalte++)
                {
                    var wunsch = new Wunsch();
                    var prio = spalte - 2;
                    var unternehmenId = excel[zeile, spalte];
                    
                    if (!string.IsNullOrWhiteSpace(unternehmenId))
                    {
                        if (!int.TryParse(unternehmenId, out var unternehmendIdAsInt))
                        {
                            throw new ArgumentException($"Die Id des Unternehmen konnte nicht in eine gültige Zahl umgewandelt werden. Fehler in Zeile {actualExcelLine}");
                        }
                        wunsch.Prioritaet = prio;
                        wunsch.VeranstaltungsId = unternehmendIdAsInt;
                        wuensche.Add(wunsch);
                    }
                }
                schueler.Wuensche = wuensche;
                schuelerListe.Add(schueler);
            }
            Debug.WriteLine("////////////////////////////");
            Debug.WriteLine("SCHÜLERLISTE");
            foreach(Schueler schueler in schuelerListe)
            {
                Debug.WriteLine($"Vorname: {schueler.Vorname}     Nachname: {schueler.Nachname}");
            }
            return schuelerListe;
        }
        private List<Veranstaltung> VeranstaltungsListeErstellen(string[,] excel)
        {

            List<Veranstaltung> liste = new List<Veranstaltung>();
            for (int row = 0; row < excel.GetLength(0); row++)
            {
                try
                {
                    int id = Int32.Parse(excel[row, 0].Trim());
                    String unternehmensname = excel[row, 1].Trim();
                    String fachrichtung = excel[row, 2]?.Trim();
                    int teilnehmer = Int32.Parse(excel[row, 3].Trim());
                    int veranstaltungen = Int32.Parse(excel[row, 4].Trim());
                    char fruehsteZeit = Char.Parse(excel[row, 5]);
                    liste.Add(new Veranstaltung(id, unternehmensname, fachrichtung, teilnehmer, veranstaltungen, fruehsteZeit));
                }
                catch (ArgumentNullException)
                {


                }
                catch (FormatException)
                {

                    throw;
                }
                catch (OverflowException)
                {

                    throw;
                }
            }

            return liste;
        }
        private List<Raum> RaumErstellen(string[,] excel)
        {
            int maxZeilen = excel.GetLength(0); // maxZeilen gibt an wieviele Zeilen in der Exceldatei benutzt werden
            int maxSpalten = excel.GetLength(1); // maxSpalten gibt an wieviele Spalten in der Exceldatei benutzt werden

            // Mindestangaben: Raum, Kapazität
            if (maxSpalten != 2)
            {
                throw new ArgumentException("Die Raum-Datei enthält zu wenig Spalten. " +
                    "Es muss mindestens eine Spalte \"Raum\" und eine Spalte \"Kapazität\" vorhanden sein.");
            }

            // Kontrolle, ob ein Feld leer ist
            for (int zeile = 0; zeile < maxZeilen; zeile++)
            {
                int aktuelleExcelZeile = zeile + 2;

                for (int spalte = 0; spalte < maxSpalten; spalte++)
                {
                    // Wert der aktuellen Zelle abrufen
                    string zellenInhalt = excel[zeile, spalte];

                    // Überprüfen, ob die Zelle leer ist
                    if (string.IsNullOrEmpty(zellenInhalt) || string.IsNullOrWhiteSpace(zellenInhalt))
                    {
                        throw new ArgumentException($"Die Zelle in Spalte {GetSpaltenBuchstabe(spalte)}, Zeile {aktuelleExcelZeile} ist leer, " +
                            "oder es sind Leerzeichen enthalten.");
                    }
                    else if (zellenInhalt.Equals("0"))
                    {
                        throw new ArgumentException($"In Spalte {GetSpaltenBuchstabe(spalte)}, Zeile {aktuelleExcelZeile}, wurde die Kapazität mit  \"0\" angegeben.");
                    }
                }
            }

            // Liste mit Raum-Objekten erstellen
            List<Raum> raumListe = new List<Raum>();

            for (int zeile = 0; zeile < excel.GetLength(0); zeile++)
            {
                int aktuelleExcelZeile = zeile + 2;

                //Eintragen der Daten ins Objekt "Raum"
                Raum objektRaum = new Raum();
                objektRaum.Bezeichnung = excel[zeile, 0];
                String raumKapazitaet = excel[zeile, 1];
                if (int.TryParse(raumKapazitaet, out int kapazitaetAlsInt)) // prüfen ob die Kapazität in einen Int parsen kann
                {
                    objektRaum.Kapazitaet = kapazitaetAlsInt; // wenn ja, parsen
                }
                else
                {
                    // wenn nein, dann Fehlermeldung
                    throw new ArgumentException($"Die Kapazität des Raumes konnte nicht in eine gültige Zahl umgewandelt werden. Fehler in Spalte {GetSpaltenBuchstabe(1)}, Zeile {aktuelleExcelZeile}");
                }

                raumListe.Add(objektRaum);
            }
            // Debug.WriteLine("fertig");
            return raumListe;
        }

        private void Algorithmus()
        {

            //Aufrufe von externen Klassen -> Zeitplanerstellung
            //Zuordnung Raum-Schüler


        }

        private static string[,] ReadExcel(string filepath)
        {
            //Annahme, dass die Datei existiert
            var xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@filepath, ReadOnly: true);

            //Nur das erste Worksheet wird eingelesen; TODO: Hinweis, wenn mehrere existieren?
            Excel.Worksheet xlWorksheet = xlWorkbook.Worksheets[1];
            xlWorksheet.Activate();

            Excel.Range range = xlWorksheet.UsedRange;
            int maxRowCount = range.Rows.Count;
            int maxColCount = range.Columns.Count;

            //Speichern von Workbook in Objekt, damit keine Mehrfachiteration über die Zeilen stattfindet bei jedem Auslesen der Zellen -> bessere Performance
            object[,] xlWorksheetStored = range.Value2;
            xlWorkbook.Close();

            //Abzug von erster Zeile wegen Headern
            string[,] data = new string[maxRowCount - 1, maxColCount];

            //Durch die Zeilen iterieren
            //Achtung: Einlesen beginnt erst ab zweiter Zeile wegen den Headern
            for (int rowCount = 2; rowCount <= maxRowCount; rowCount++)
            {
                //Durch die Spalten iterieren
                for (int colCount = 1; colCount <= maxColCount; colCount++)
                {
                    object entry = xlWorksheetStored[rowCount, colCount];
                    if (entry != null)
                    {
#pragma warning disable CS8601 // Mögliche Nullverweiszuweisung.
                        data[rowCount - 2, colCount - 1] = entry.ToString();
#pragma warning restore CS8601 // Mögliche Nullverweiszuweisung.
                    }
                }
            }
            return data;
        }

        private Excel.Workbook CreateSchuelerExcel()
        {

            return new Excel.Workbook();

            //Option: Excel in Word konvertieren
        }
        private Excel.Workbook UnternehmenAnwesenheitsExcel()
        {

            return new Excel.Workbook();

            //Option: Excel in Word konvertieren
        }

        private string GetSpaltenBuchstabe(int index)
        {
            const string buchstaben = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var wert = "";

            if (index >= buchstaben.Length)
                wert += buchstaben[index / buchstaben.Length - 1];

            wert += buchstaben[index % buchstaben.Length];

            return wert;
        }

    }
}
