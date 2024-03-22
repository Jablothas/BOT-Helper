using running_bunny.RaumZeitPlan;

namespace running_bunny.Model
{
    public class Veranstaltung
    {
        public Veranstaltung(int id, string unternehmensName, string fachrichtung, int teilnehmer, int veranstaltungen, char fruehsteZeit)
        {
            Id = id;
            Fachrichtung = fachrichtung;
            UnternehmensName = unternehmensName;
            MaxAnzahlTeilnehmer = teilnehmer;
            MaxAnzahlVerantstaltungen = veranstaltungen;
            FruehsterZeitSlot = Enum.TryParse<RaumZeitPlan.Zeitslot>(char.ToString(fruehsteZeit), out var zeitslot) ? zeitslot : RaumZeitPlan.Zeitslot.A;
        }

        public int Id { get; set; }
        public string UnternehmensName { get; set; }
        public string Fachrichtung { get; set; }
        public int MaxAnzahlTeilnehmer { get; set; }
        public int MaxAnzahlVerantstaltungen { get; set; }
        public RaumZeitPlan.Zeitslot FruehsterZeitSlot { get; set; }
        public List<Zeitslot> Zeitslots { get; set; } = new List<Zeitslot>();
        

        private int anzahlWünsche = 0;
        public int AnzahlWünsche 
        {
            get
            {
                return anzahlWünsche;
            }
            set
            {
                anzahlWünsche = value;
                
            }
        }
        public int AnzahlKurse { get; set; } = 0;
        public int RaeumeBesetzt { get; set; }

        public void BerechneBenoetigteKurse()
        {
            double tmp = AnzahlWünsche;
            int tmpAnzahlKurse = (int)(tmp / 20);

            if(tmpAnzahlKurse <= MaxAnzahlVerantstaltungen)
                AnzahlKurse = tmpAnzahlKurse;

            if (tmp % 20 > 0)
                tmpAnzahlKurse++;
            if (tmpAnzahlKurse <= MaxAnzahlVerantstaltungen)
                AnzahlKurse = tmpAnzahlKurse;
        }
    }
}
