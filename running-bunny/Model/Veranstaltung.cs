
using running_bunny.Business;

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
        public bool IstVoll { get; set; } = false;

        private int teilnehmerzahlMomentan = 0;
        public int TeilnehmerzahlMomentan
        {
            get
            {
                return teilnehmerzahlMomentan; 
            }
            set
            {
                teilnehmerzahlMomentan = value;
                if (teilnehmerzahlMomentan == MaxAnzahlTeilnehmer)
                    IstVoll = true;
            }
        }

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
        public int AnzahlRaeume { get; set; } = 0;
        public int RaeumeBesetzt { get; set; }

        public void BerechneBenoetigteRaeume()
        {
            double tmp = AnzahlWünsche;
            int tmpAnzahlRaeume = (int)(tmp / 20);

            if(tmpAnzahlRaeume <= MaxAnzahlVerantstaltungen)
                AnzahlRaeume = tmpAnzahlRaeume;

            if (tmp % 20 >= 5)
                tmpAnzahlRaeume++;
            if (tmpAnzahlRaeume <= MaxAnzahlVerantstaltungen)
                AnzahlRaeume = tmpAnzahlRaeume;
        }
    }
}
