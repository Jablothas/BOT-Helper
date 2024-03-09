using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using running_bunny.Model;
using running_bunny.Business;

namespace running_bunny.RaumZeitPlan
{
    public class ZelleRaumZeitplan: IComparable<ZelleRaumZeitplan>
    {
        public Zeitslot Zeitslot { get; set; }
        public Veranstaltung Veranstaltung { get; set; }
        public Raum Raum { get; set; }

        public ZelleRaumZeitplan(Zeitslot zeitslot, Veranstaltung veranstaltung, Raum raum)
        {
            Zeitslot = zeitslot;
            Veranstaltung = veranstaltung;
            Raum = raum;
        }

        public int CompareTo(ZelleRaumZeitplan? vergleichZelle)
        {
            // A null value means that this object is greater.
            if (this.Veranstaltung.UnternehmensName[0] > (int)(vergleichZelle.Veranstaltung.UnternehmensName[0]))
                return 1;

            else
                return -1;
        }
    }
}
