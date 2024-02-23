using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace running_bunny.Modell
{
    public class Schueler
    {
        public string Vorname { get; set; }
        public string Nachname { get; set; }

        public List<Wahl> Wuensche { get; set; } = new List<Wahl>();
        public string Klasse { get; set; }
       
    }
}
