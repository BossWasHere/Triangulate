using System;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Triangulate
{
    public partial class Form1 : Form
    {

        private Point pos1 = new Point(5, 5), pos2 = new Point(5, 5), pos3 = new Point(5, 5);
        private double angle = 0.0;
        private PaintEventArgs pea = null;

        public Form1()
        {
            InitializeComponent();
            pea = new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle);
        }

        private void NumsOnly(TextBox tb)
        {
            string text = tb.Text;
            string regex = Regex.Replace(text, @"\d", "");
            if (regex.Length > 0)
            {
                int pos = tb.SelectionStart;
                tb.Text = "" + Regex.Replace(text, @"[^\d]", "");
                tb.SelectionStart = (pos - 1 < 0 ? 0 : pos - 1);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            NumsOnly(textBox1);
            DrawLines();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            NumsOnly(textBox2);
            DrawLines();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            NumsOnly(textBox3);
            DrawLines();

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            NumsOnly(textBox4);
            DrawLines();
        }

        private void textBox4_LostFocus(object sender, EventArgs e)
        {
            if (textBox4.Text.Length < 1) textBox4.Text = "1";
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            pea = new PaintEventArgs(panel1.CreateGraphics(), panel1.ClientRectangle);
            if (!(pos2.X == 5 && pos2.Y == 5 && pos3.X == 5 && pos3.Y == 5)) panel1_Paint(null, pea);
        }

        private void DrawLines()
        {
            if (textBox1.Text.Length < 1 || textBox2.Text.Length < 1 || textBox3.Text.Length < 1) return;
            int mult = textBox4.Text.Length > 0 ? Int32.Parse(textBox4.Text) : 1;
            int A = Int32.Parse(textBox1.Text) * mult;
            int B = Int32.Parse(textBox2.Text) * mult;
            int C = Int32.Parse(textBox3.Text) * mult;

            angle = TrigMath.GetAngle(A, B, C);
            if (angle < 0)
            {
                SetError(true);
                return;
            }
            pos2.X = A + 1;
            // +/-2 Due to calculation offset
            if (angle < 90)
            {
                pos3.X = (int) (pos2.X - TrigMath.GetAdjCos(B, angle)) + 2;
            } else
            {
                pos3.X = (int) (pos2.X + TrigMath.GetAdjCos(B, angle)) - 2;
            }
            pos3.Y = (int) (pos2.Y + TrigMath.GetOppSin(B, angle));
            panel1_Paint(null, pea);
            SetError(false);
        }

        private void SetError(bool check)
        {
            if (check)
            {
                label4.Text = "Check Lines!";
            }
            else
            {
                label4.Text = "";
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.ControlDark);
            var p = new Pen(Color.Black, 2);
            e.Graphics.DrawLine(p, pos1, pos2);
            e.Graphics.DrawLine(p, pos2, pos3);
            e.Graphics.DrawLine(p, pos3, pos1);
        }
    }
}
