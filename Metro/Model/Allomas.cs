using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Model
{
    public class Allomas
    {
        public string AllomasNev { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Allomas(string nev, string x, string y)
        {
            AllomasNev = nev;
            X = Convert.ToInt32(x);
            Y = Convert.ToInt32(y);
        }
    }
}
