using running_bunny.RaumZeitPlan;

namespace running_bunny.Model
{
    public class Wunsch
    {
        public int Prioritaet { get; set; }
        public int VeranstaltungsId { get; set; }
        public ZelleRaumZeitplan zelle { get; set; } = null;
    }
}
