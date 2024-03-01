using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace running_bunny.Modell
{
    public class Unternehmen
    {
        public Unternehmen(int nummer, String unternehmensName, int teilnehmer, int veranstaltungen, Char fruehsteZeit)
        {
            Nummer = nummer;
            UnternehmensName = unternehmensName;
            Teilnehmer = teilnehmer;
            Verantstaltung = veranstaltungen;
            FruehsterZeit = fruehsteZeit;
        }
        public int Nummer { get; set; }
        public String UnternehmensName { get; set; }
        public int Teilnehmer { get; set; }
        public int Verantstaltung { get; set; }
        public Char FruehsterZeit { get; set; }

    }
}
