using System.Diagnostics;
using running_bunny.Model;
using running_bunny.RaumZeitPlan;

namespace running_bunny.WunschRaumZeitPlanZuweisung
{
    public class SchuelerZuweisungZuZeitplan
    {
        public List<Schueler> SchuelerListe { get; set; }
        public List<ZelleRaumZeitplan> ZelleRaumZeitplan { get; set; }

        public SchuelerZuweisungZuZeitplan(List<Schueler> schuelerListe, List<ZelleRaumZeitplan> zellenListe)
        {
            SchuelerListe = schuelerListe;
            ZelleRaumZeitplan = zellenListe;
            Erstellen();
        }

        public void Erstellen()
        {
            Debug.WriteLine("////////////////////////");
            Debug.WriteLine("Zuweisung Wunsch zu Zelle");

            SchülerNachWünschenKursenZuweisen();
            WeiseAlleSchülerMitFreienZeitslotsVeranstaltungenZu();
        }

        private void SchülerNachWünschenKursenZuweisen()
        {
            for (int wunschPrio = 1; wunschPrio <= 6; wunschPrio++)
            {
                //Schülerliste sortiert nach Scores
                var schuelerOrderedByScore = SchuelerListe.OrderBy(schueler => schueler.SummeGewichtung);

                foreach (var schueler in schuelerOrderedByScore)
                {
                    var wunsch = schueler.Wuensche.SingleOrDefault(wunsch => wunsch.Prioritaet == wunschPrio);
                    if (wunsch != null)
                    {
                        var passendeZellen = SuchePassendeZellen(ZelleRaumZeitplan, wunsch, schueler.BelegteZeitslots.Select(slotZelle => slotZelle.Key));
                        if (passendeZellen != null)
                        {
                            WeiseZelleSchuelerWunschZu(wunschPrio, schueler, wunsch, passendeZellen.First());
                        }
                        else
                        {
                            var alteWuensche = schueler.Wuensche.Where(wunsch => wunsch.Zelle != null).OrderByDescending(wunsch => wunsch.Prioritaet);
                            var alleZeitslots = Enum.GetValues<Zeitslot>();

                            var passendeZellenNachTauschen = AlteWuenscheAendernGibPassendeZellenZurueck(wunsch, alteWuensche, ZelleRaumZeitplan, new List<Zeitslot>(), schueler);
                            if (passendeZellen != null)
                            {
                                //Erste passende Zelle eintragen
                                WeiseZelleSchuelerWunschZu(wunschPrio, schueler, wunsch, passendeZellen.First());
                            }
                        }
                    }
                }
            }
        }

        private static void WeiseZelleSchuelerWunschZu(int wunschPrio, Schueler? schueler, Wunsch? wunsch, ZelleRaumZeitplan passendeZelle)
        {
            wunsch.Zelle = passendeZelle;
            wunsch.Zelle.SchuelerListe.Add(schueler);

            schueler.BelegteZeitslots.Add(passendeZelle.Zeitslot, passendeZelle);

            //Setzen von Erfüllungsscore
            schueler.SummeGewichtung = schueler.SummeGewichtung + (7 - wunschPrio);
        }

        private static IEnumerable<ZelleRaumZeitplan>? AlteWuenscheAendernGibPassendeZellenZurueck(
            Wunsch neuerWunsch,
            IOrderedEnumerable<Wunsch> alteWuensche,
            IEnumerable<ZelleRaumZeitplan> zelleRaumZeitplan,
            IEnumerable<Zeitslot> probierteZeitslots,
            Schueler schueler)
        {
            var alleZeitslots = Enum.GetValues<Zeitslot>();
            //Ändern der Wünsche und Zellen, um es erneut zu probieren
            foreach (var alterWunsch in alteWuensche)
            {
                var alterZeitslot = alterWunsch.Zelle.Zeitslot;

                var unzulässigeZeitslots = new[] { alterZeitslot }.Concat(schueler.BelegteZeitslots.Select(e => e.Key)).Concat(probierteZeitslots).Distinct().ToList();

                var alternativeZellen = SuchePassendeZellen(zelleRaumZeitplan, alterWunsch, unzulässigeZeitslots);

                //Probieren anderer Zellen für die vorherigen Wünsche in anderem Zeitslot. Wenn keine alternativen Zellen existieren, wird der nächste Wunsch durchprobiert
                if (alternativeZellen == null)
                {
                    return AlteWuenscheAendernGibPassendeZellenZurueck(neuerWunsch, alteWuensche.Except(new[] { alterWunsch }).OrderByDescending(wunsch => wunsch.Prioritaet), zelleRaumZeitplan, new List<Zeitslot>(), schueler);
                }

                //Wechseln der Zellen für den aktuellen Wunsch
                foreach (var neueZelle in alternativeZellen)
                {
                    alterWunsch.Zelle.SchuelerListe.Remove(schueler);
                    alterWunsch.Zelle = neueZelle;
                    alterWunsch.Zelle.SchuelerListe.Add(schueler);

                    schueler.BelegteZeitslots.Remove(alterZeitslot);
                    schueler.BelegteZeitslots.Add(neueZelle.Zeitslot, neueZelle);

                    var passendeZellenNeuerWunsch = SuchePassendeZellen(zelleRaumZeitplan, neuerWunsch, schueler.BelegteZeitslots.Select(e => e.Key));

                    unzulässigeZeitslots.Add(neueZelle.Zeitslot);
                    return passendeZellenNeuerWunsch ?? AlteWuenscheAendernGibPassendeZellenZurueck(neuerWunsch, alteWuensche, zelleRaumZeitplan, unzulässigeZeitslots, schueler);
                }
            }
            return null;
        }

        private void WeiseAlleSchülerMitFreienZeitslotsVeranstaltungenZu()
        {
            var alleZeitslots = Enum.GetValues<Zeitslot>();
            var freieZellen = ZelleRaumZeitplan.Where(zelle => !zelle.RaumVoll && !zelle.MaxTeilnehmerErreicht).ToList();

            //Schüler ohne Wünsche irgendwelchen Veranstaltungen zuweisen
            var schuelerOhneBelegteZeitslots = SchuelerListe.Where(schueler => schueler.BelegteZeitslots.Count() != alleZeitslots.Count());
            foreach (var schueler in schuelerOhneBelegteZeitslots)
            {
                var freieZellenNachZeitslot = freieZellen.ToLookup(zelle => zelle.Zeitslot, zelle => zelle);

                var fehlendeZeitslots = alleZeitslots.Except(schueler.BelegteZeitslots.Select(slotZelle => slotZelle.Key));
                foreach (var fehlenderZeitslot in fehlendeZeitslots)
                {
                    //TODO: Was wenn keine Zellen mehr frei sind, neuen Raum erstellen? Wirft aktuell noch Fehler
                    if (!freieZellenNachZeitslot.Contains(fehlenderZeitslot))
                    {
                        throw new NotImplementedException("Hier müssten prolly neue Kurse erstellt werden");
                    }

                    var ersteFreieZelle = freieZellenNachZeitslot[fehlenderZeitslot].OrderBy(zelle => zelle.SchuelerListe.Count).First();

                    ersteFreieZelle.SchuelerListe.Add(schueler);
                    schueler.BelegteZeitslots.Add(fehlenderZeitslot, ersteFreieZelle);

                    //Wenn ein Kurs voll ist, wird er aus den freien Zellen gestrichen
                    if (ersteFreieZelle.RaumVoll || ersteFreieZelle.MaxTeilnehmerErreicht)
                    {
                        freieZellen.Remove(ersteFreieZelle);
                    }
                }
            }
        }

        private static IOrderedEnumerable<ZelleRaumZeitplan>? SuchePassendeZellen(IEnumerable<ZelleRaumZeitplan> zeitplan, Wunsch wunsch, IEnumerable<Zeitslot> belegteZeitslots)
        {
            //Suchen von Kursen, mit selber VeranstaltungsId und noch nicht in belegtem Zeitslot
            var zellenFuerVeranstaltung = zeitplan.Where(zelle => zelle.Veranstaltung.Id == wunsch.VeranstaltungsId && !belegteZeitslots.Contains(zelle.Zeitslot));

            var möglicheZellen = new List<ZelleRaumZeitplan>();
            foreach (var zelle in zellenFuerVeranstaltung)
            {
                if (!zelle.MaxTeilnehmerErreicht && !zelle.RaumVoll)
                {
                    //Rückgabe von erstem Kurs, der infrage kommt
                    möglicheZellen.Add(zelle);
                }
            }

            if (möglicheZellen.Count() != 0)
            {
                //Zellen sortiert nach kleinster Teilnehmerzahl zurückgeben
                return möglicheZellen.OrderBy(zelle => zelle.SchuelerListe.Count);
            }

            //Wenn kein Kurs gefunden wird, wird null zurückgegeben
            return null;
        }
    }
}
