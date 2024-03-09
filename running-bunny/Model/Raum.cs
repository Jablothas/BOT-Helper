namespace running_bunny.Model
{
    public class Raum
    {
        public string Bezeichnung { get; set; }
        public int Kapazitaet { get; set; }
        public bool IstBelegt { get; set; } = false; //false = nicht belegt

        public Raum() { IstBelegt = false; }
    }
}
