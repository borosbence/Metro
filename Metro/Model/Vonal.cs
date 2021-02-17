using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Model
{
    public class Vonal
    {
        public string VonalNev { get; set; }
        public List<Allomas> Allomasok = new List<Allomas>();
        //public Dictionary<int,Allomas> Allomasok = new Dictionary<int,Allomas>();
        public Vonal(string nev)
        {
            VonalNev = nev;
        }
    }
}
