using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace running_bunny.Business
{
    //LeereZelleException -> Leere Zellen
    //Allgemeine Exception -> ProcessException
    //SpaltenZeilenException


    public class ProzessException: Exception
    {
        public ProzessException(String message): base(message) { }
    }

    public class LeereZelleException: Exception
    {
        public int Zeile { get; set; } = 0;
        public int Spalte { get; set; } = 0;
        public LeereZelleException(int zeile, int spalte, String message = "Die Zelle ist leer"): base(message)
        {
            Zeile = zeile;
            Spalte = spalte;
            

        }
        public override String Message
        {
            get
            {
                return "Leere Zelle: Zeile: " + Zeile + "; Spalte: " + Spalte;
            }
        }
       
    }
    public class SpaltenZeilenException: Exception
    {
        public int ExcelZeilen { get; set; }
        public int ExcelSpalten { get; set; }
        public SpaltenZeilenException(int zeilen, int spalten)
        {
            ExcelZeilen = zeilen;
            ExcelSpalten = spalten;
        }
        public override String Message
        {
            get
            {
                return "ExcelFormat ist nicht kompatibel: " + ExcelSpalten + " Spalten; " + ExcelZeilen + " Zeilen";
            }
        }
    }
}
