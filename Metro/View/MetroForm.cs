using Metro.Model;
using Metro.Presenter;
using Metro.Repository;
using Metro.ViewInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Metro.View
{
    public partial class MetroForm : Form, IMetroView
    {
        private MetroPresenter presenter;
        public MetroForm()
        {
            InitializeComponent();
            presenter = new MetroPresenter(this);
            presenter.LoadData();
        }

        public List<Allomas> Allomasok { get; set; }
        public List<Vonal> MetroVonalak { get; set; }

        private void DrawMap()
        {
            Graphics g = panel1.CreateGraphics();
            Pen black = new Pen(Color.Black, 1);
            SolidBrush white = new SolidBrush(Color.White);
            Font font = new Font("FreightSans Medium", 7, FontStyle.Regular);
            foreach (var allomas in Allomasok)
            {
                g.DrawEllipse(black, allomas.X - 5, allomas.Y - 5, 10, 10);
                g.FillEllipse(white, allomas.X - 5, allomas.Y - 5, 10, 10);
                g.DrawString(allomas.AllomasNev, font, Brushes.Black,
                new Rectangle(allomas.X + 3, allomas.Y + 2, 61, 50));
                IndulasComboBox.Items.Add(allomas.AllomasNev);
                ErkezesComboBox.Items.Add(allomas.AllomasNev);
            }
        }

        private void DrawRailLines()
        {
            Graphics g = panel1.CreateGraphics();
            Pen redPen = new Pen(Color.Red, 3);
            Pen greenPen = new Pen(Color.Green, 3);

            foreach (var metroVonal in MetroVonalak)
            {
                for (int i = 0; i < metroVonal.Allomasok.Count; i++)
                {
                    if (i < metroVonal.Allomasok.Count - 1)
                    {
                        int startX = metroVonal.Allomasok[i].X;
                        int startY = metroVonal.Allomasok[i].Y;
                        int endX = metroVonal.Allomasok[i + 1].X;
                        int endY = metroVonal.Allomasok[i + 1].Y;

                        switch (metroVonal.VonalNev)
                        {
                            case "M2":
                                g.DrawLine(redPen, startX, startY, endX, endY);
                                break;
                            case "M4":
                                g.DrawLine(greenPen, startX, startY, endX, endY);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawMap();
            DrawRailLines();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            int X = e.X;
            int Y = e.Y;

            foreach (var allomas in Allomasok)
            {
                if (Math.Abs(allomas.X - X) < 5 && Math.Abs(allomas.Y - Y) < 5)
                {
                    if (IndulasComboBox.Text == "")
                    {
                        IndulasComboBox.Text = allomas.AllomasNev;
                    }
                    else
                    {
                        ErkezesComboBox.Text = allomas.AllomasNev;
                    }
                }
            }
        }

        private void KiuritesButton_Click(object sender, EventArgs e)
        {
            IndulasComboBox.Text = null;
            ErkezesComboBox.Text = null;
            UtvonalListBox.Items.Clear();
        }

        private void TervezesButton_Click(object sender, EventArgs e)
        {
            string induloAllomasNev = IndulasComboBox.Text;
            string vegAllomasNev = ErkezesComboBox.Text;
            UtvonalListBox.Items.Clear();
            UtvonalListBox.Items.Add("Indulás innen: " + induloAllomasNev);
            UtvonalListBox.Items.Add("");
            UtvonalListBox.Items.Add("Érkezés ide: " + vegAllomasNev);

            bool indulasLetezik, vegLetezik;
            bool direktVonal = false;
            foreach (var induloVonal in MetroVonalak)
            {
                string metroVonal = induloVonal.VonalNev;
                indulasLetezik = presenter.VonalonLetezik(induloVonal, induloAllomasNev);
                vegLetezik = presenter.VonalonLetezik(induloVonal, vegAllomasNev);
                if (indulasLetezik == true && vegLetezik == true)
                {
                    UtvonalListBox.Items.Add("");
                    UtvonalListBox.Items.Add("Induljon el átszállás nélkül az " + metroVonal + " vonalon");
                    direktVonal = true;
                }

                if (direktVonal == false)
                {
                    // Megkeresi a végállomást a vonalak közül
                    foreach (var vegVonal in MetroVonalak)
                    {
                        vegLetezik = presenter.VonalonLetezik(vegVonal, vegAllomasNev);
                        // Ha megtaláta az egyik vonalon
                        if (vegLetezik)
                        {
                            // Megkeresi az induló vonalon szerepel e közös átszálló állomás a végvonalon
                            foreach (var allomas in vegVonal.Allomasok)
                            {
                                bool vanAtszallas = presenter.VonalonLetezik(induloVonal, allomas.AllomasNev);
                                if (indulasLetezik && vegLetezik && vanAtszallas)
                                {
                                    string vegvonal = vegVonal.VonalNev;
                                    UtvonalListBox.Items.Add("");
                                    UtvonalListBox.Items.Add("Induljon el az " + metroVonal + " vonalon");
                                    UtvonalListBox.Items.Add("szálljon át a(z) " + allomas.AllomasNev + " állomáson");
                                    UtvonalListBox.Items.Add("az " + vegvonal + " vonalra");
                                }
                            }
                            
                        }
                    }
                }
            }
        }
    }
}
