namespace running_bunny.Model
{
    public class Schueler
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public List<Wunsch> Wuensche { get; set; }
        public string Klasse { get; set; }
    }
}
