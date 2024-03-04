using running_bunny.Model;
using Excel = Microsoft.Office.Interop.Excel;

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
            var unternehmensListe = UnternehmenErstellen(unternehmensExcel);

            var raumExcel = ReadExcel(raumFilePath);
            var raumListe = RaumErstellen(raumExcel);

            var wuenscheNachUnternehmen = ZeitplanErstellung.ZaehleWuenscheProVeranstaltung(schuelerListe, unternehmensListe);
            //var unternehmenNachPrio = ZeitplanErstellung.ErstellungZeitplanBasierendAufWuenscheMitPrio(wuenscheNachUnternehmen, raumListe);
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
                schueler.Vorname = excel[zeile, 1];
                schueler.Nachname = excel[zeile, 2];

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
                    var prio = spalte - 2;
                    var unternehmenId = excel[zeile, spalte];
                    if (!string.IsNullOrWhiteSpace(unternehmenId))
                    {
                        if (!int.TryParse(unternehmenId, out var unternehmendIdAsInt))
                        {
                            throw new ArgumentException($"Die Id des Unternehmen konnte nicht in eine gültige Zahl umgewandelt werden. Fehler in Zeile {actualExcelLine}");
                        }
                        wuensche.Add(new Wunsch { VeranstaltungsId = unternehmendIdAsInt, Prioritaet = prio });
                    }
                }
                schueler.Wuensche = wuensche;
                schuelerListe.Add(schueler);
            }
            return schuelerListe;
        }
        private List<Veranstaltung> UnternehmenErstellen(string[,] excel)
        {
            
            List<Veranstaltung> liste = new List<Veranstaltung>();
            for (int row = 0; row < excel.GetLength(0); row++)
            {
                try
                {
                    liste.Add(new Veranstaltung(Int32.Parse(excel[row, 0]), excel[row, 1], excel[row, 2], Int32.Parse(excel[row, 3]), Int32.Parse(excel[row, 4]), Char.Parse(excel[row, 5])));
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
            // maxZeilen gibt an wieviele Zeilen in der Exceldatei benutzt werden
            // maxSpalten gibt an wieviele Spalten in der Exceldatei benutzt werden
            // // var colCount = excel.GetLength(1);
            int maxZeilen = excel.GetLength(0);
            int maxSpalten = excel.GetLength(1);

            // Mindestangaben: Raum, Kapazität
            if (maxSpalten < 2)
            {
                throw new ArgumentException("Die Raum-Datei enthält zu wenig Spalten. " +
                    "Es muss mindestens eine Spalte \"Raum\" und eine Spalte \"Kapazität\" vorhanden sein.");
            }

            // Kontrolle, ob ein Feld leer ist
            for (int zeile = 0; zeile < maxZeilen; zeile++)
            {
                for (int spalte = 0; spalte < maxSpalten; spalte++)
                {
                    // Wert der aktuellen Zelle abrufen
                    string zellenInhalt = excel[zeile, spalte];
                    //Excel.Range zelle = (Excel.Range)range.Cells[zeile, spalte];
                    //string cellValue = zelle.Value != null ? zelle.Value.ToString() : "";

                    // Überprüfen, ob die Zelle leer ist
                    // TODO: leere Felder, Felder mit "" und Felder mit "0" erstellen und testen obs knallt
                    if (string.IsNullOrEmpty(zellenInhalt) || zellenInhalt.Equals("0"))
                    {
                        Console.WriteLine($"Die Zelle in Zeile {zeile}, Spalte {spalte} ist leer " + 
                            "oder die Kapazität wurde mit \"0\" angegeben.");
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
                    // TODO: aktuelle Spalte soll auch angezeigt werden
                    throw new ArgumentException($"Die Kapazität des Raumes konnte nicht in eine gültige Zahl umgewandelt werden. Fehler in Zeile {aktuelleExcelZeile}");
                }

                //if (string.IsNullOrWhiteSpace(objektRaum.Bezeichnung)
                //    || objektRaum.Kapazitaet == 0)
                //{
                //    throw new ArgumentException($"Die Bezeichnung ist leer oder die Kapazität ist 0. Fehler in Zeile {aktuelleExcelZeile}");
                //}

                raumListe.Add(objektRaum);
            }
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

            //Nur das erte Worksheet wird eingelesen; TODO: Hinweis, wenn mehrere existieren?
            Excel.Worksheet xlWorksheet = xlWorkbook.Worksheets[1];
            xlWorksheet.Activate();

            Excel.Range usedRowRange = xlWorksheet.UsedRange;
            int maxRowCount = usedRowRange.Row + usedRowRange.Rows.Count - 1;

            Excel.Range usedColRange = xlWorksheet.UsedRange;
            int maxColCount = usedColRange.Column + usedColRange.Columns.Count - 1;

            //Abzug von erster Zeile wegen Headern
            string[,] data = new string[maxRowCount - 1, maxColCount];

            //Durch die Zeilen iterieren
            //Achtung: Einlesen beginnt erst ab zweiter Zeile wegen den Headern
            for (int rowCount = 2; rowCount <= maxRowCount; rowCount++)
            {
                //Durch die Spalten iterieren
                for (int colCount = 1; colCount <= maxColCount; colCount++)
                {
                    object entry = xlWorksheet.Cells[rowCount, colCount].Value;
                    if (entry != null)
                    {
#pragma warning disable CS8601 // Mögliche Nullverweiszuweisung.
                        data[rowCount - 2, colCount - 1] = entry.ToString();
#pragma warning restore CS8601 // Mögliche Nullverweiszuweisung.
                    }
                }
            }
            xlWorkbook.Close();
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



    }
}
