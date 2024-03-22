using Excel = Microsoft.Office.Interop.Excel;
using running_bunny.Model;
using running_bunny.RaumZeitPlan;
using running_bunny.WunschRaumZeitPlanZuweisung;
using System.Diagnostics;
using running_bunny.WordErstellung;
using Word = Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;
using System.IO.Compression;
namespace running_bunny.Business
{
    public class Verarbeitung
    {
        public void run(string schuelerFilePath, string veranstalterFilePath, string raumFilePath)
        {
            var schuelerExcel = ReadExcel(schuelerFilePath);
            var schuelerListe = SchuelerErstellen(schuelerExcel);

            var unternehmensExcel = ReadExcel(veranstalterFilePath);
            var veranstaltungsListe = VeranstaltungsListeErstellen(unternehmensExcel);

            var raumExcel = ReadExcel(raumFilePath);
            var raumListe = RaumErstellen(raumExcel);

            RaumZeitplan raumZeitPlan = new RaumZeitplan(schuelerListe, veranstaltungsListe, raumListe);

            SchuelerZuweisungZuZeitplan wunschRaumZeitPlanZuweisungErstellung =
                new SchuelerZuweisungZuZeitplan(raumZeitPlan.SchuelerListe, raumZeitPlan.RaumZeitplanListe);

            List<Schueler> schuelerListeFuerLaufzettel = wunschRaumZeitPlanZuweisungErstellung.SchuelerListe;

            //Werden im Bin-Verzeichnis erstellt
            var wordFilesPath = CreateWordFiles(veranstaltungsListe, raumZeitPlan, schuelerListeFuerLaufzettel).GetAwaiter().GetResult();
            ZipFilesToDownloadDeleteFolder(wordFilesPath);
        }

        private void ZipFilesToDownloadDeleteFolder(string wordFilesPath)
        {
            var downloadPath = DownloadFolder.GetDownloadPath();
            var zipFilePathWithoutExtension = Path.Combine(downloadPath, "Bot-Helper");
            var zipFileEnding = ".zip";
            if (File.Exists($"{zipFilePathWithoutExtension}{zipFileEnding}"))
            {
                zipFilePathWithoutExtension += "_" + DateTime.Now.ToString("dd.MM.yyyy_HH.mm");
            }

            ZipFile.CreateFromDirectory(wordFilesPath, zipFilePathWithoutExtension + zipFileEnding);

            if (File.Exists(zipFilePathWithoutExtension)) { }
            Directory.Delete(wordFilesPath, recursive: true);
        }

        public static class DownloadFolder
        {
            private static readonly Guid downloadGuid = new("374DE290-123F-4565-9164-39C4925E467B");

            public static string GetDownloadPath()
            {
                return SHGetKnownFolderPath(downloadGuid, 0);
            }

            [DllImport("shell32",
                CharSet = CharSet.Unicode, ExactSpelling = true, PreserveSig = false)]
            private static extern string SHGetKnownFolderPath(
                [MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags,
                nint hToken = 0);
        }

        private static async Task<string> CreateWordFiles(List<Veranstaltung> veranstaltungsListe, RaumZeitplan raumZeitPlan, List<Schueler> schuelerListeFuerLaufzettel)
        {
            //Erstellung Word-Dateien-Verzeichnis in bin und temporär Speichern der erzeugten Dateien
            //Bin-Verzeichnis holen
            var workingDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var wordFilesDir = Directory.CreateDirectory(@$"{workingDir}\Wordfiles");

            try
            {
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                RaumZeitplanErstellung raumzeitplanWord =
                    new RaumZeitplanErstellung(raumZeitPlan.VeranstaltungsListe, raumZeitPlan.RaumZeitplanListe, wordFilesDir.FullName);
                var raumzeitPlanTask = Task.Run(() => raumzeitplanWord.ErstelleWordDatei());

                var laufzettelErstellung = new LaufzettelErstellung(schuelerListeFuerLaufzettel, wordFilesDir.FullName);
                var laufzettelTask = Task.Run(() => laufzettelErstellung.ErstelleWordDatei());

                AnwesenheitslisteUnternehmenErstellung anwesenheitsliste
                    = new AnwesenheitslisteUnternehmenErstellung(veranstaltungsListe, raumZeitPlan.RaumZeitplanListe, wordFilesDir.FullName);
                var anwesenheitsListeTask = Task.Run(() => anwesenheitsliste.ErstelleWordDatei());

                await raumzeitPlanTask;
                await laufzettelTask;
                await anwesenheitsListeTask;

                stopWatch.Stop();
                Debug.WriteLine("------------------------------------------------------------------------");
                Debug.WriteLine("ERSTELLUNG WORD-DATEIEN DAUER: " + stopWatch.ElapsedMilliseconds.ToString() + " ms");
                Debug.WriteLine("------------------------------------------------------------------------");

                return wordFilesDir.FullName;
            }
            catch (Exception)
            {
                Directory.Delete(wordFilesDir.FullName, recursive: true);
                throw;
            }
            finally
            {
                //wordApp.Quit(SaveChanges: Word.WdSaveOptions.wdDoNotSaveChanges);
            }
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
                catch (Exception)
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
            return raumListe;
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
