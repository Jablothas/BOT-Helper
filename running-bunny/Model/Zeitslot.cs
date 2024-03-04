namespace running_bunny.Model
{
    public enum Zeitslot
    {
        Ungültig = 0, //Default eines Enums ist immer 0, ungültiger Wert beim Parsen soll ersichtlich sein

        //Der int-Wert ist die Anzahl der möglichen Veranstaltungen im optimalen Fall
        A = 5,
        B = 4,
        C = 3,
        D = 2,
        E = 1
    }
}
