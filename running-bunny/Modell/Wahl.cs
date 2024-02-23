using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace running_bunny.Modell
{
    public class Wahl
    {
        public int Prioritaet { get; set; }
        public string Firma { get; set; }

        public Wahl(int Prioritaet, string Firma)
        {
            this.Prioritaet = Prioritaet;
            this.Firma = Firma;
        }
    }
}
