using Metro.Repository;
using Metro.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Presenter
{
    public class MetroPresenter
    {
        private IMetroView view;
        private MetroRepository repo;
        public MetroPresenter(IMetroView param)
        {
            view = param;
            repo = new MetroRepository("../../Data/metro.xlsx");
        }

        public void LoadData()
        {
            view.Allomasok = repo.Allomasok;
            view.MetroVonalak = repo.MetroVonalak;
        }

    }
}
