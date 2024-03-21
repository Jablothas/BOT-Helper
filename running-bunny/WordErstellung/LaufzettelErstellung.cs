using running_bunny.Model;
using Microsoft.Office.Interop.Word;
using running_bunny.RaumZeitPlan;

namespace running_bunny.WordErstellung
{
    public class LaufzettelErstellung
    {
        private List<Schueler> SchuelerListe { get; set; }
        public LaufzettelErstellung(IEnumerable<Schueler> schueler)
        {
            SchuelerListe = schueler.ToList();
        }

        public static Dictionary<Zeitslot, string> _uhrzeitenZuZeitslot = new Dictionary<Zeitslot, string>()
        {
            {Zeitslot.A, "8:45 - 9:30" },
            {Zeitslot.B, "9:50 - 10:35" },
            {Zeitslot.C, "10:35 - 11:20" },
            {Zeitslot.D, "11:40 - 12:25" },
            {Zeitslot.E, "12:25 - 13:10" },
        };

        //TODO: Alles in ein Try-Catch stopfen
        public void ErstelleWordDatei()
        {
            var anzahlDokumente = Math.Ceiling((double)SchuelerListe.Count() / 3);

            //Word öffnen
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();

            wordApp.Visible = true;
            wordApp.ShowAnimation = false;

            //neues leeres Dokument erstellen
            Document document = wordApp.Documents.Add();
            SetMarginsOfWordDocument(document, 25);
            SetStyle(document, 11, "Calibri");

            var anzahlSchülerAufSeite = 0;
            var übrigeSchüler = SchuelerListe.Count;
            //Durch die Schüler iterieren
            foreach (var schueler in SchuelerListe)
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
                zeitplan.Borders.Enable = 1;
                zeitplan.AllowAutoFit = true;
                SetAutoFitTable(zeitplan, wordApp);

                foreach (Row zeile in zeitplan.Rows)
                {
                    //Header für erste Zeile schreiben
                    if (zeile.Index == 1)
                    {
                        FillRow(zeile, string.Empty, "Zeit", "Raum", "Veranstaltung", string.Empty, "Wunsch");
                        zeile.Range.Font.Bold = 1;

                        //TODO: Hier noch Font ändern maybe
                        zeile.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                        foreach (Cell headerZelle in zeile.Cells)
                        {
                            headerZelle.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                            headerZelle.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                        }
                    }
                    else
                    {
                        //Hier sind die Indizes 2 bis 6
                        var zelleRaumZeitPlanSchueler = schueler.BelegteZeitslots.Single(zelle => (int)zelle.Key == zeile.Index - 1);
                        var zugehörigerWunsch =
                            schueler.Wuensche.FirstOrDefault(wunsch => wunsch.VeranstaltungsId == zelleRaumZeitPlanSchueler.Value.Veranstaltung.Id);

                        FillRow(zeile,
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
            //TODO: Pfad anpassen oder gar nicht speichern?
            //document.SaveAs2(@"D:\SchülerLaufZettel.docx");
            //wordApp.Quit();
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

        private void FillRow(Row row, string val1, string val2, string val3, string val4, string val5, string val6)
        {
            //row.Cells[0].Range.Text = val1;
            row.Cells[1].Range.Text = val1;
            row.Cells[2].Range.Text = val2;
            row.Cells[3].Range.Text = val3;
            row.Cells[4].Range.Text = val4;
            row.Cells[5].Range.Text = val5;
            row.Cells[6].Range.Text = val6;
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
            zeitplan.Columns[4].SetWidth(app.CentimetersToPoints(5.5f), WdRulerStyle.wdAdjustFirstColumn);
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
