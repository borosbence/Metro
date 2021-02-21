using Metro.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro.Repository
{
    public class MetroRepository
    {
        private string _filePath;
        public List<Vonal> MetroVonalak = new List<Vonal>();
        public List<Allomas> Allomasok = new List<Allomas>();

        public MetroRepository(string filepath)
        {
            _filePath = filepath;
            ReadAll();
        }

        private void ReadAll()
        {
            using (ExcelPackage package = new ExcelPackage(new FileInfo(_filePath)))
            {
                // Állomások beolvasása
                ExcelWorksheet allomasokMunkalap = package.Workbook.Worksheets[0];
                var start = allomasokMunkalap.Dimension.Start;
                var end = allomasokMunkalap.Dimension.End;
                for (int row = start.Row + 1; row <= end.Row; row++)
                {
                    Allomasok.Add(new Allomas(
                        allomasokMunkalap.Cells[row, 1].Text,
                        allomasokMunkalap.Cells[row, 2].Text,
                        allomasokMunkalap.Cells[row, 3].Text));
                }

                // Vonalak beolvasása
                ExcelWorksheet vonalakMunkalap = package.Workbook.Worksheets[1];
                start = vonalakMunkalap.Dimension.Start;
                end = vonalakMunkalap.Dimension.End;

                for (int row = start.Row + 1; row <= end.Row; row++)
                {
                    var vonal = new Vonal(vonalakMunkalap.Cells[row, 1].Text);
                    //int megalloSzam = 1;
                    for (int col = 2; col <= end.Column; col++)
                    {
                        string allomasNev = vonalakMunkalap.Cells[row, col].Text;
                        // Állomás kikeresése
                        var allomas = Allomasok.SingleOrDefault(x => x.AllomasNev == allomasNev);
                        if (allomas != null)
                        {
                            vonal.Allomasok.Add(allomas);
                            //vonal.Allomasok.Add(megalloSzam, allomas);
                            //megalloSzam++;
                        }
                    }
                    MetroVonalak.Add(vonal);
                }
            }
        }

        public bool VonalonLetezik(Vonal vonal, string allomas)
        {
            return vonal.Allomasok.Select(x => x.AllomasNev).Contains(allomas);
        }
    }
}
