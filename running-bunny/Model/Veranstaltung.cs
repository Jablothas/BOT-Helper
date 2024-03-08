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
            FruehsterZeitSlot = Enum.TryParse<Zeitslot>(char.ToString(fruehsteZeit), out var zeitslot) ? zeitslot : Zeitslot.A;
        }

        public int Id { get; set; }
        public string UnternehmensName { get; set; }
        public string Fachrichtung { get; set; }
        public int MaxAnzahlTeilnehmer { get; set; }
        public int MaxAnzahlVerantstaltungen { get; set; }
        public Zeitslot FruehsterZeitSlot { get; set; }
    }
}
