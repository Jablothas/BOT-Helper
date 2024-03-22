using Microsoft.Office.Interop.Word;
using running_bunny.Model;
using running_bunny.RaumZeitPlan;
using Word = Microsoft.Office.Interop.Word;

namespace running_bunny.WordErstellung
{
    public class AnwesenheitslisteUnternehmenErstellung
    {
        public List<Veranstaltung> VeranstaltungsListe { get; set; }
        public List<ZelleRaumZeitplan> RaumZeitplanListe { get; set; } = new List<ZelleRaumZeitplan>();
        private string wordFilesPath { get; set; }
        private Word.Application wordApp { get; set; }

        public AnwesenheitslisteUnternehmenErstellung(List<Veranstaltung> veranstalungsListe, List<ZelleRaumZeitplan> raumZeitplanListe, string wordFilesPath)
        {
            VeranstaltungsListe = veranstalungsListe;
            RaumZeitplanListe = raumZeitplanListe;
            this.wordFilesPath = wordFilesPath;

            Word.Application wordApp = new Word.Application();
            wordApp.Visible = true;
            wordApp.ShowAnimation = false;

            this.wordApp = wordApp;
        }

        public void ErstelleWordDatei()
        {
            object filename = "Anwesenheitslisten"; //Hard coded
            // Neues Dokument erstellen
            Word.Document doc = wordApp.Documents.Add();           

            Word.Paragraph paragraphUeberschrift = doc.Paragraphs.Add();

            doc.Content.Font.Name = "Arial";

            //Ueberschrift
            string ueberschriftStil = "ueberschriftStil";
            ErstelleUeberschriftStil(wordApp, ueberschriftStil);
            paragraphUeberschrift.set_Style(ueberschriftStil);

            String ueberschriftText = "Anwesenheitsliste" + "\n";
            paragraphUeberschrift.Range.Text = ueberschriftText;

            //Firmennamen
            string firmennamenStil = "firmennamenStil";
            ErstelleFirmennamenStil(wordApp, firmennamenStil);
            String firmennamenText;

            //Tabellenstiel
            string tabellenStil = "TabellenStil";
            ErstelleTabellenStil(wordApp, tabellenStil);

            //Uhrzeit
            string uhrzeitStil = "uhrzeitStil";
            ErstelleUhrzeitStil(wordApp, uhrzeitStil);

            String uhrzeitText = null;

            // Tabelle erstellen
            int numRows = 0;
            int numCols = 4;


            foreach (Veranstaltung veranstaltung in VeranstaltungsListe)
            {
                var sindWuenscheVorhanden = RaumZeitplanListe.Where(zelle => zelle.Veranstaltung.Id == veranstaltung.Id).ToList();

                    if (sindWuenscheVorhanden != null)
                    {
                        Word.Paragraph paragraphFirmennamen = doc.Paragraphs.Add();
                        paragraphFirmennamen.set_Style("firmennamenStil");
                        firmennamenText = "\n" + veranstaltung.UnternehmensName + " - " + veranstaltung.Fachrichtung + "\n";
                        paragraphFirmennamen.Range.Text = firmennamenText;
                        

                    ZelleRaumZeitplan zelleZuVeranstaltungUndSlot = null;
                    Zeitslot slot = Zeitslot.A; //temporäre Übergabe, nur für Initialisierung

                    foreach (Zeitslot slotJetzt in veranstaltung.Zeitslots)
                    {
                        zelleZuVeranstaltungUndSlot = RaumZeitplanListe.Find(zelle => zelle.Veranstaltung.Id == veranstaltung.Id && slotJetzt == zelle.Zeitslot);
                        numRows = zelleZuVeranstaltungUndSlot.SchuelerListe.Count;
                        slot = slotJetzt;

                        //Slot Uhrzeit Print
                        Word.Paragraph paragraphUhrzeit = doc.Paragraphs.Add();
                        paragraphUhrzeit.set_Style("uhrzeitStil");
                        uhrzeitText = "\n" + LaufzettelErstellung._uhrzeitenZuZeitslot[slot] + "\n";
                        paragraphUhrzeit.Range.Text = uhrzeitText;


                        //Tabellenerzeugung

                        Word.Table table = doc.Tables.Add(paragraphUhrzeit.Range, 1, numCols); // Beispiel: 2 Zeilen, 2 Spalten
                        table.set_Style("TabellenStil");


                        //Tabellenheader hard codiert
                        table.Cell(1, 1).Range.Text = "Klasse";
                        table.Cell(1, 2).Range.Text = "Name";
                        table.Cell(1, 3).Range.Text = "Vorname";
                        table.Cell(1, 4).Range.Text = "Anwesend?";

                        // Tabelle füllen
                        foreach (Schueler schueler in zelleZuVeranstaltungUndSlot.SchuelerListe)
                        {
                            FügeEineZeileEinerTabelleHinzu(table, new string[] { schueler.Klasse, schueler.Nachname, schueler.Vorname });
                        }
                    }
                }
                doc.Words.Last.InsertBreak(WdBreakType.wdPageBreak);//Seitenumbruch
            }

            doc.SaveAs2($@"{wordFilesPath}\{filename}.docx", ReadOnlyRecommended: false);
            doc.Close();
            wordApp.Quit();
        }

        private void ErstelleUeberschriftStil(Word.Application wordApp, string styleName)
        {
            Word.Style style = wordApp.ActiveDocument.Styles.Add(styleName, Word.WdStyleType.wdStyleTypeParagraphOnly);
            style.Font.Size = 16;
            style.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
        }
        private void ErstelleFirmennamenStil(Word.Application wordApp, string styleName)
        {
            Word.Style style = wordApp.ActiveDocument.Styles.Add(styleName, Word.WdStyleType.wdStyleTypeParagraphOnly);
            style.Font.Size = 15;
            style.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;            
        }
        private void ErstelleTabellenStil(Word.Application wordApp, string styleName)
        {
            Word.Style style = wordApp.ActiveDocument.Styles.Add(styleName, Word.WdStyleType.wdStyleTypeTable);
            style.Font.Size = 11;
            style.Font.Bold = 1;
            style.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            style.Table.Borders.Enable = 1;
            style.Table.Borders.InsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            style.Table.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;


        }
        private void ErstelleUhrzeitStil(Word.Application wordApp, string styleName)
        {
            Word.Style style = wordApp.ActiveDocument.Styles.Add(styleName, Word.WdStyleType.wdStyleTypeParagraphOnly);
            style.Font.Size = 15;
            style.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

        }
        static void FügeEineZeileEinerTabelleHinzu(Word.Table table, string[] zeilenDaten)
        {
            // Füge eine Zeile am Ende der Tabelle hinzu
            table.Rows.Add();

            // Setze die Werte für jede Zelle in der neuen Zeile
            for (int i = 0; i < zeilenDaten.Length; i++)
            {
                table.Cell(table.Rows.Count, i + 1).Range.Text = zeilenDaten[i];
            }
        }
    }
}
