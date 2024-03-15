using running_bunny.RaumZeitPlan;

namespace running_bunny.Model
{
    public class Raum
    {
        public string Bezeichnung { get; set; }
        public int Kapazitaet { get; set; }
        public bool IstBelegt { get; set; } = false; //false = nicht belegt

        public Raum() { IstBelegt = false; }

        private ZelleRaumZeitplan[] belegteSlots = new ZelleRaumZeitplan[5];
        public ZelleRaumZeitplan[] BelegteSlots
        {
            get{return belegteSlots;}
            
        }
        public bool IstRaumVoll()
        {
            for(int i = 0; i < BelegteSlots.Length; i++ )
            {
                if (BelegteSlots[i] == null)
                    return false;

            }
            return true;
        }
        
    }
}
