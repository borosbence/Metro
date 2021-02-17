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
        private float zoom = 1F;
        public MetroForm()
        {
            InitializeComponent();
            presenter = new MetroPresenter(this);
            presenter.LoadData();
        }

        public List<Allomas> Allomasok { get; set; }
        public List<Vonal> MetroVonalak { get; set; }

        private void DrawMap(float zoom)
        {
            Graphics g = panel1.CreateGraphics();
            if (zoom > 0)
            {
                g.ScaleTransform(zoom, zoom);
            }
            Pen black = new Pen(Color.Black, 1);
            SolidBrush white = new SolidBrush(Color.White);
            Font font = new Font("FreightSans Medium", 7, FontStyle.Regular);
            foreach (var allomas in Allomasok)
            {
                g.DrawEllipse(black, allomas.X - 5, allomas.Y - 5, 10, 10);
                g.FillEllipse(white, allomas.X - 5, allomas.Y - 5, 10, 10);
                g.DrawString(allomas.AllomasNev, font, Brushes.Black,
                new Rectangle(allomas.X + 3, allomas.Y + 2, 61, 50));
            }
        }

        private void DrawRailLines(float zoom)
        {
            Graphics g = panel1.CreateGraphics();
            if (zoom > 0)
            {
                g.ScaleTransform(zoom, zoom);
            }
            Pen redPen = new Pen(Color.Red, 3);

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
                            default:
                                break;
                        }
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawMap(zoom);
            DrawRailLines(zoom);
        }

        private void ZoomPlusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoom += 0.1F;
            panel1.Size = new Size((panel1.Width + 1), (panel1.Height + 1));
            panel1.Invalidate();
        }

        private void ZoomMinusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoom -= 0.1F;
            panel1.Size = new Size((panel1.Width - 1), (panel1.Height - 1));
            panel1.Invalidate();
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            zoom = 0;
            panel1.Size = new Size(1440, 1185);
            panel1.Invalidate();
        }
    }
}
