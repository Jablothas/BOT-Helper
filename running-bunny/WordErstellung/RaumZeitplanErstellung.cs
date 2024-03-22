using running_bunny.RaumZeitPlan;
using System.Runtime.InteropServices;
using running_bunny.Model;
using Word = Microsoft.Office.Interop.Word;

namespace running_bunny.WordErstellung
{
    public class RaumZeitplanErstellung
    {
        public List<Veranstaltung> VeranstaltungsListe { get; set; }
        public List<ZelleRaumZeitplan> RaumZeitplanListe { get; set; } = new List<ZelleRaumZeitplan>();
        private string wordFilesPath { get; set; }
        private Word.Application wordApp { get; set; }

        public RaumZeitplanErstellung(Word.Application wordApp, List<Veranstaltung> veranstalungsListe, List<ZelleRaumZeitplan> raumZeitplanListe, string wordFilesPath)
        {
            VeranstaltungsListe = veranstalungsListe;
            RaumZeitplanListe = raumZeitplanListe;
            this.wordFilesPath = wordFilesPath;
            this.wordApp = wordApp;
        }

        public void ErstelleWordDatei()
        {
            string filename = "RaumZeitplan"; //Hard coded
            Word.Document doc = wordApp.Documents.Add();
            try
            {
                // Neues Dokument erstellen

                Word.Paragraph paragraphUeberschrift = doc.Paragraphs.Add();
                Word.Paragraph paragraph = doc.Paragraphs.Add();
                doc.Content.Font.Name = "Arial";

                //Ueberschrift
                string ueberschriftStil = "ueberschriftStil";
                ErstelleUeberschriftStil(wordApp, ueberschriftStil);
                paragraphUeberschrift.set_Style(ueberschriftStil);
                String text = "Organisationsplan für den Berufsorientierungstag \n\n";
                paragraphUeberschrift.Range.Text = text;


                //Paragraph
                string paragraphStil = "paragraphStil";
                ErstelleParagraphStil(wordApp, paragraphStil);
                paragraph.set_Style(paragraphStil);
                String paragraphText = "8:30 bis 8:45 Uhr Begrüßung und Einführung in der Aula\n";
                String paragraphText2 = "13:10 bis 13:20 Uhr Abschluss im Klassenverbund\n\n";
                paragraph.Range.Text = paragraphText;
                paragraph.Range.Text = paragraphText2;

                // Tabelle erstellen
                int numRows = VeranstaltungsListe.Count;
                int numCols = Enum.GetNames(typeof(Zeitslot)).Length + 1;
                Word.Table table = doc.Tables.Add(doc.Paragraphs[doc.Paragraphs.Count].Range, numRows, numCols);
                table.AllowAutoFit = true;

                table.AutoFitBehavior(Word.WdAutoFitBehavior.wdAutoFitWindow);


                //Tabelle erste Zelle oben links = 1 x 1
                //Erste Zeile in Tabelle
                table.Cell(1, 1).Range.Text = "Veranstalter";
                for (int i = 1; i <= Enum.GetNames(typeof(Zeitslot)).Length; i++)
                {
                    table.Cell(1, i + 1).Range.Text = ((Zeitslot)i).ToString();
                    table.Cell(1, i + 1).Range.Text += LaufzettelErstellung._uhrzeitenZuZeitslot[((Zeitslot)i)];
                }

                string tabellenStil = "TabellenStil";
                ErstelleTabellenStil(wordApp, tabellenStil);
                table.set_Style(tabellenStil);

                // Tabelle füllen
                for (int row = 0; row < VeranstaltungsListe.Count; row++)
                {
                    table.Cell(row + 2, 1).Range.Text = (row + 1).ToString() + ". " + VeranstaltungsListe[row].UnternehmensName;
                    for (int col = 1; col <= Enum.GetNames(typeof(Zeitslot)).Length; col++)
                    {
                        // Zelle setzen
                        ZelleRaumZeitplan zelle = RaumZeitplanListe.Find(zelle => zelle.Zeitslot == (Zeitslot)col && VeranstaltungsListe[row].Id == zelle.Veranstaltung.Id); //Sucht  zelle mit gleicher VeranstaltungsID und Zeitslot
                        if (zelle != null)
                        {
                            table.Cell(2 + row, 1 + col).Range.Text = zelle.Raum.Bezeichnung != null ? zelle.Raum.Bezeichnung : "";
                        }
                    }
                }
            }
            catch (COMException e)
            {

                throw new Exception("Bitte alle Dokumente mit gleicher Bezeichnung schließen und das Programm neu starten");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            doc.SaveAs2($@"{wordFilesPath}\{filename}.docx", ReadOnlyRecommended: false);
            doc.Close();
        }

        private void ErstelleUeberschriftStil(Word.Application wordApp, string styleName)
        {
            Word.Style style = wordApp.ActiveDocument.Styles.Add(styleName, Word.WdStyleType.wdStyleTypeParagraphOnly);
            style.Font.Size = 16;
            style.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            style.Shading.BackgroundPatternColor = Word.WdColor.wdColorGray10;
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
        private void ErstelleParagraphStil(Word.Application wordApp, string styleName)
        {
            Word.Style style = wordApp.ActiveDocument.Styles.Add(styleName, Word.WdStyleType.wdStyleTypeParagraphOnly);
            style.Font.Size = 11;
            style.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

        }
    }
}
