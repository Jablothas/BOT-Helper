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
    public class ScoreErstellungTxt
    {
        private string WordFilesPath { get; set; }
        private List<Schueler> ListeSchueler { get; set; }

        public ScoreErstellungTxt(string wordFilesPath, List<Schueler> schuelerListe)
        {
            WordFilesPath = wordFilesPath;
            ListeSchueler = schuelerListe;
        }

        public void ErstelleScoreTxt()
        {
            object filename = "SchülerScore";
            var binDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);            

            using (StreamWriter sw = File.CreateText($@"{binDir}\{filename}{DateTime.Now:_yyyy_MM_dd}.txt"))
            {
                ListeSchueler.OrderBy(schueler => schueler.SummeGewichtung);
                double summeErzielterScore = 0;
                int summeMoeglicherScore = 0;
                sw.WriteLine("SchülerScore");
                sw.WriteLine("");
                foreach(Schueler schueler in ListeSchueler)
                {
                    sw.WriteLine(schueler.Vorname + " " + schueler.Nachname + "     Score: " + schueler.SummeGewichtung);

                    summeErzielterScore = summeErzielterScore + schueler.SummeGewichtung;
                    summeMoeglicherScore += schueler.OptimalerScore;
                }

                sw.WriteLine("");
                sw.WriteLine("Gesamtsumme erzielt: " + summeErzielterScore);
                sw.WriteLine("Gesamtsumme möglich: " + summeMoeglicherScore);
                sw.WriteLine("Anteil erfüllter Wünsche in %: " + Math.Round(summeErzielterScore/ summeMoeglicherScore * 100, 2) + "%");
            }
        }
    }
}
