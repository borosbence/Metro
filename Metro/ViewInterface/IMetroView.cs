using Metro.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.ViewInterface
{
    public interface IMetroView
    {
        List<Allomas> Allomasok { get; set; }
        List<Vonal> MetroVonalak { get; set; }
        //string Indulas { get; set; }
        //string Erkezes { get; set; }
        //List<string> Utvonal { get; set; }
    }
}
