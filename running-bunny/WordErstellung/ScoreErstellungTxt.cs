using running_bunny.Business;
using running_bunny.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace running_bunny.WordErstellung
{
    public class ScoreErstellungTxt : IWordErstellung
    {
        private string WordFilesPath { get; set; }
        private List<Schueler> ListeSchueler { get; set; }

        public ScoreErstellungTxt(string wordFilesPath, List<Schueler> schuelerListe)
        {
            WordFilesPath = wordFilesPath;
            ListeSchueler = schuelerListe;
        }
        public void ErstelleWordDatei()
        {
            object filename = "SchülerScore";

            using (StreamWriter sw = File.CreateText($@"{WordFilesPath}\{filename}.txt"))
            {
                ListeSchueler.OrderBy(schueler => schueler.SummeGewichtung);
                int summe = 0;
                sw.WriteLine("SchülerScore");
                sw.WriteLine("");
                foreach(Schueler schueler in ListeSchueler)
                {
                    sw.WriteLine(schueler.Vorname + " " + schueler.Nachname + "     Score: " + schueler.SummeGewichtung);
                    summe = summe + schueler.SummeGewichtung;
                }

                sw.WriteLine("");
                sw.WriteLine("Gesamtsumme: " + summe);
            }

        }
    }
}
