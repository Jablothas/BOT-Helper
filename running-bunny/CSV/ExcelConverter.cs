using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace running_bunny.CSV
{
    public static class ExcelConverter
    {
        public static string[,] ReadExcel(string filepath)
        {
            //Annahme, dass die Datei existiert
            var xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(@filepath, ReadOnly: true);

            //Nur das erte Worksheet wird eingelesen; TODO: Hinweis, wenn mehrere existieren?
            Excel.Worksheet xlWorksheet = xlWorkbook.Worksheets[1];
            xlWorksheet.Activate();

            Excel.Range usedRowRange = xlWorksheet.UsedRange;
            int maxRowCount = usedRowRange.Row + usedRowRange.Rows.Count - 1 - 1; //Zusätzlich, weil die erste Zeile Header sind

            Excel.Range usedColRange = xlWorksheet.UsedRange;
            int maxColCount = usedColRange.Column + usedColRange.Columns.Count - 1;

            string[,] data = new string[maxRowCount, maxColCount];

            //Durch die Zeilen iterieren
            //Achtung: Einlesen beginnt erst ab zweiter Zeile wegen den Headern
            for (int rowCount = 2; rowCount < maxRowCount; rowCount++)
            {
                //Durch die Spalten iterieren
                for (int colCount = 1; colCount < maxColCount; colCount++)
                {
                    data[rowCount - 2, colCount - 1] = xlWorksheet.Cells[rowCount, colCount].Value.ToString();
                }
            }
            xlWorkbook.Close();
            return data;
        }
    }
}
