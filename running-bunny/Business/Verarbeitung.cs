using running_bunny.Modell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private List<Schueler> SchuelerErstellen(string[,] excel)
        {
            //Anna, bis zum 23.02
            return new List<Schueler>();
        }
        private List<Unternehmen> UnternehmenErstellen(string[,] excel) {
            //Kevin, bis zum 23.02
            return new List<Unternehmen>(); }
        private List<Raum> RaumErstellen(string[,] excel) {
            //Emmanuel, bis zum 23.02
            return new List<Raum>(); }

        private void Algorithmus()
        {

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
