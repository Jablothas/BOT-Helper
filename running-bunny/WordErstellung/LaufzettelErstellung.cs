using running_bunny.Model;
using Microsoft.Office.Interop.Word;
using running_bunny.RaumZeitPlan;

namespace running_bunny.WordErstellung
{
    public class LaufzettelErstellung
    {
        private List<Schueler> SchuelerListe { get; set; }
        private string wordFilesPath { get; set; }
        private Microsoft.Office.Interop.Word.Application wordApp { get; set; }

        public LaufzettelErstellung(Microsoft.Office.Interop.Word.Application wordApp, IEnumerable<Schueler> schueler, string wordFilesPath)
        {
            SchuelerListe = schueler.ToList();
            this.wordFilesPath = wordFilesPath;
            this.wordApp = wordApp;
        }

        public static Dictionary<Zeitslot, string> _uhrzeitenZuZeitslot = new Dictionary<Zeitslot, string>()
        {
            {Zeitslot.A, "8:45 - 9:30" },
            {Zeitslot.B, "9:50 - 10:35" },
            {Zeitslot.C, "10:35 - 11:20" },
            {Zeitslot.D, "11:40 - 12:25" },
            {Zeitslot.E, "12:25 - 13:10" },
        };

        public void ErstelleWordDatei()
        {
            var laufzettelDir = Directory.CreateDirectory($@"{wordFilesPath}\Laufzettel");

            var topAndBottomPaddingCell = wordApp.CentimetersToPoints(0.2f);

            //Für jede Klasse ein eigenes Dokument
            var schülerGroupedByClass = SchuelerListe.ToLookup(schueler => schueler.Klasse.ToUpper());

            foreach (var klasse in schülerGroupedByClass)
            {                
                //neues leeres Dokument erstellen
                Document document = wordApp.Documents.Add();

                SetMarginsOfWordDocument(document, 25);
                SetStyle(document, 11, "Arial");

                var anzahlSchülerAufSeite = 0;
                var übrigeSchüler = klasse.Count();

                var schülerNachNachnamenSortiert = klasse.OrderBy(schueler => schueler.Nachname);
                //Durch die Schüler iterieren
                foreach (var schueler in schülerNachNachnamenSortiert)
                {
                    anzahlSchülerAufSeite++;
                    übrigeSchüler--;
                    Paragraph klasseUndNamePara = document.Content.Paragraphs.Add();
                    klasseUndNamePara.Range.Text = schueler.Klasse + Environment.NewLine
                        + $"{schueler.Nachname}, {schueler.Vorname}";
                    document.Range().InsertParagraphAfter();

                    //Zeitplan erstellen
                    var tabellePara = document.Content.Paragraphs.Add();

                    Table zeitplan = document.Tables.Add(tabellePara.Range, 6, 6);
                    zeitplan.Range.Paragraphs.SpaceAfter = 0;
                    zeitplan.Borders.Enable = 1;
                    zeitplan.AllowAutoFit = true;
                    SetAutoFitTable(zeitplan, wordApp);

                    foreach (Row zeile in zeitplan.Rows)
                    {
                        //Header für erste Zeile schreiben
                        if (zeile.Index == 1)
                        {
                            FillRowAndSetTopPadding(zeile, topAndBottomPaddingCell, string.Empty, "Zeit", "Raum", "Veranstaltung", "Fachrichtung", "Wunsch");
                            zeile.Range.Font.Bold = 1;

                            zeile.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                            foreach (Cell headerZelle in zeile.Cells)
                            {
                                headerZelle.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;          // Vertikale Ausrichtung
                                headerZelle.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;  // horizontale Ausrichtung
                            }
                        }
                        else
                        {
                            //Hier sind die Indizes 2 bis 6
                            var zelleRaumZeitPlanSchueler = schueler.BelegteZeitslots.Single(zelle => (int)zelle.Key == zeile.Index - 1);
                            var zugehörigerWunsch =
                                schueler.Wuensche.FirstOrDefault(wunsch => wunsch.VeranstaltungsId == zelleRaumZeitPlanSchueler.Value.Veranstaltung.Id);

                            FillRowAndSetTopPadding(zeile,
                                topAndBottomPaddingCell,
                                zelleRaumZeitPlanSchueler.Key.ToString(),
                                _uhrzeitenZuZeitslot[zelleRaumZeitPlanSchueler.Key],
                                zelleRaumZeitPlanSchueler.Value.Raum.Bezeichnung,
                                zelleRaumZeitPlanSchueler.Value.Veranstaltung.UnternehmensName,
                                zelleRaumZeitPlanSchueler.Value.Veranstaltung.Fachrichtung,
                                zugehörigerWunsch?.Prioritaet.ToString() ?? "-");
                        }
                    }

                    document.Content.InsertParagraphAfter();
                    if (anzahlSchülerAufSeite == 3 && übrigeSchüler != 0)
                    {
                        //neue Seite
                        document.Words.Last.InsertBreak(WdBreakType.wdPageBreak);
                        anzahlSchülerAufSeite = 0;
                    }
                }

                document.SaveAs2(@$"{laufzettelDir}\{klasse.Key}.docx", ReadOnlyRecommended: false);
                document.Close();
            }
        }

        /// <summary>
        /// Setzt alle Seitenränder eines Dokuments auf den übergebenen Wert.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="margin"></param>
        private void SetMarginsOfWordDocument(Document document, int margin)
        {
            document.PageSetup.LeftMargin = margin;
            document.PageSetup.TopMargin = margin;
            document.PageSetup.RightMargin = margin;
            document.PageSetup.BottomMargin = margin;
        }

        private void FillRowAndSetTopPadding(Row row, float topPadding, string val1, string val2, string val3, string val4, string val5, string val6)
        {
            SetTopPaddingOfAllCells(row, topPadding);

            row.Cells[1].Range.Text = val1;
            row.Cells[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            row.Cells[2].Range.Text = val2;
            row.Cells[2].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            row.Cells[3].Range.Text = val3;
            row.Cells[3].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            row.Cells[4].Range.Text = val4;
            row.Cells[5].Range.Text = val5;
            row.Cells[6].Range.Text = val6;
            row.Cells[6].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        }

        private void SetTopPaddingOfAllCells(Row row, float topAndBottomPadding)
        {
            foreach (Cell cell in row.Cells)
            {
                cell.TopPadding = topAndBottomPadding;
                cell.BottomPadding = topAndBottomPadding;
            }
        }

        private void SetAutoFitTable(Table zeitplan, Microsoft.Office.Interop.Word.Application app)
        {
            //Zeitslot
            zeitplan.Columns[1].SetWidth(app.CentimetersToPoints(0.6f), WdRulerStyle.wdAdjustFirstColumn);
            //Uhrzeit
            zeitplan.Columns[2].SetWidth(app.CentimetersToPoints(2.7f), WdRulerStyle.wdAdjustFirstColumn);
            //Raum
            zeitplan.Columns[3].SetWidth(app.CentimetersToPoints(1.5f), WdRulerStyle.wdAdjustFirstColumn);
            //Unternehmen
            zeitplan.Columns[4].SetWidth(app.CentimetersToPoints(4.5f), WdRulerStyle.wdAdjustFirstColumn);
            //Prio-Nr.
            zeitplan.Columns[6].SetWidth(app.CentimetersToPoints(1.4f), WdRulerStyle.wdAdjustFirstColumn);

            zeitplan.AutoFitBehavior(WdAutoFitBehavior.wdAutoFitWindow);
        }

        private void SetStyle(Document document, int fontSize, string style)
        {
            var start = document.Content.Start;
            var end = document.Content.End;

            var docRange = document.Range(start, end);

            docRange.Font.Size = fontSize;
            docRange.Font.Name = style;
        }
    }
}
