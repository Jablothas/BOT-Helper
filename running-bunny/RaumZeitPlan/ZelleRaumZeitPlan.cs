using running_bunny.Model;
using running_bunny.RaumZeitPlan;

namespace running_bunny.RaumZeitPlan
{
    public class ZelleRaumZeitplan : IComparable<ZelleRaumZeitplan>
    {
        public Zeitslot Zeitslot { get; set; }
        public Veranstaltung Veranstaltung { get; set; }
        public Raum Raum { get; set; }
        public List<Schueler> SchuelerListe { get; set; } = new List<Schueler>();
        public bool MaxTeilnehmerErreicht => SchuelerListe.Count() >= Veranstaltung.MaxAnzahlTeilnehmer;
        public bool RaumVoll => SchuelerListe.Count() >= Raum.Kapazitaet;
        public ZelleRaumZeitplan(Zeitslot zeitslot, Veranstaltung veranstaltung, Raum raum)
        {
            Zeitslot = zeitslot;
            Veranstaltung = veranstaltung;
            Raum = raum;
        }

        public int CompareTo(ZelleRaumZeitplan? vergleichZelle)
        {
            for (int i = 0; i < this.Veranstaltung.UnternehmensName.Length; i++)
            {
                if ((int)(this.Veranstaltung.UnternehmensName[i]) > (int)(vergleichZelle.Veranstaltung.UnternehmensName[i]))
                    return 1;
                if ((int)(this.Veranstaltung.UnternehmensName[i]) == (int)(vergleichZelle.Veranstaltung.UnternehmensName[i]))
                    continue;
                return -1;
            }
            return 0;

        }
    }
}
