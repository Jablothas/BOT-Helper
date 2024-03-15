using running_bunny.RaumZeitPlan;

namespace running_bunny.Model
{
    public class Schueler
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public List<Wunsch> Wuensche { get; set; }
        public string Klasse { get; set; }
        public int SummeGewichtung { get; set; }
        public List<ZeitslotMitZelle> BelegteZeitslots { get; set; } = new List<ZeitslotMitZelle>();
    }

    public class ZeitslotMitZelle
    {
        public Zeitslot Zeitslot { get; set; }
        public ZelleRaumZeitplan Zelle { get; set; }
    }
}
