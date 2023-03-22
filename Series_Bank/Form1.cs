using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Series_Bank
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                damped_discharge();
                menustriprun();
                DrawMatrix();
            }
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            damped_discharge();
            menustriprun();
            DrawMatrix();
        }
        void chartmenustrip(object sender, EventArgs e)
        {
            menustriprun();
        }
        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var relativeClickedPosition = e.Location;
                var screenClickedPosition = (sender as Control).PointToScreen(relativeClickedPosition);
                contextMenuStrip1.Show(screenClickedPosition);
            }
        }
        void menustriprun()
        {
            
            if (itToolStripMenuItem.Checked == true)
            {
                chart1.Series[0].Enabled = true;
            }
            else if (itToolStripMenuItem.Checked == false)
            {
                chart1.Series[0].Enabled = false;
            }

            if (itToolStripMenuItem1.Checked == true)
            {
                chart1.Series[1].Enabled = true;
            }
            else if (itToolStripMenuItem1.Checked == false)
            {
                chart1.Series[1].Enabled = false;
            }

            if (ptToolStripMenuItem.Checked == true)
            {
                chart1.Series[2].Enabled = true;
            }
            else if (ptToolStripMenuItem.Checked == false)
            {
                chart1.Series[2].Enabled = false;
            }

            if (etToolStripMenuItem.Checked == true)
            {
                chart1.Series[3].Enabled = true;
            }
            else if (etToolStripMenuItem.Checked == false)
            {
                chart1.Series[3].Enabled = false;
            }

            if (vtToolStripMenuItem.Checked == true)
            {
                chart1.Series[4].Enabled = true;
            }
            else if (vtToolStripMenuItem.Checked == false)
            {
                chart1.Series[4].Enabled = false;
            }
            chart1.ChartAreas[0].RecalculateAxesScale();
        }

        //CODE       
        private void DrawMatrix()
        {
            double Hs1 = Convert.ToDouble(textBox3.Text);
            double Hs2 = Convert.ToDouble(textBox4.Text);
            double Hp1 = Convert.ToDouble(textBox5.Text);
            double Hp2 = Convert.ToDouble(textBox6.Text);

            double Vx = Convert.ToDouble(textBox5.Text);
            double Vy = Convert.ToDouble(textBox3.Text);

            float cx0 = ((panel2.Size.Width / 2)-((float)Vx)*10);
            float cy0 = ((panel2.Size.Height / 2) - ((float)Vy * 10));

            Pen redPen = new Pen(Color.Red, 1);
            Pen bluePen = new Pen(Color.Blue, 1);
            SolidBrush bpen = new SolidBrush(Color.Blue);
            
            Bitmap bmp = new Bitmap(panel2.Size.Width, panel2.Size.Height, PixelFormat.Format32bppArgb);
            Graphics graph1 = Graphics.FromImage(bmp);
            try
            {
                for (int y = 0; y < Vy; y++)
                    for (int x = 0; x < Vx; x++)
                    {
                        //     FillRectangle(Cor, Px, Py, Comp, alt);
                        graph1.DrawString(""+x, new Font("Tahoma", 8), Brushes.Black, cx0 + (x * 20)+2, cy0-26);
                        graph1.DrawString("" + y, new Font("Tahoma", 8), Brushes.Black, cx0 -15, cy0+(y * 20)-8);

                        graph1.FillRectangle(Brushes.Black, cx0 + 6 + (x * 20), cy0 + (y * 20)-11, 2, 9);
                        graph1.FillRectangle(Brushes.Black, cx0 + (x * 20), cy0 + (y * 20), 14, 2);
                        graph1.FillRectangle(Brushes.Black, cx0 + (x * 20), cy0 + (y * 20)-4, 14, 2);
                        graph1.FillRectangle(Brushes.Black, cx0 + 6 + (x * 20), cy0 + (y * 20), 2, 9);
                    }
            }            
            finally
            {
                
            }
            pictureBox1.Image = bmp;           

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // simulation parameters
                textBox1.Text = File.ReadAllLines(openFileDialog.FileName).Skip(0).Take(1).First();     // sample rate
                textBox2.Text = File.ReadAllLines(openFileDialog.FileName).Skip(1).Take(1).First();     // tmax
                textBox10.Text = File.ReadAllLines(openFileDialog.FileName).Skip(2).Take(1).First();     // power frequency

                // bank parameters
                textBox3.Text = File.ReadAllLines(openFileDialog.FileName).Skip(3).Take(1).First();     // bank series 1 branch
                textBox5.Text = File.ReadAllLines(openFileDialog.FileName).Skip(4).Take(1).First();     // bank series 2 branch
                textBox4.Text = File.ReadAllLines(openFileDialog.FileName).Skip(5).Take(1).First();     // bank parallel 1 branch
                textBox6.Text = File.ReadAllLines(openFileDialog.FileName).Skip(6).Take(1).First();     // bank parallel 2 branch
                textBox7.Text = File.ReadAllLines(openFileDialog.FileName).Skip(7).Take(1).First();     // Ulim
                textBox11.Text = File.ReadAllLines(openFileDialog.FileName).Skip(8).Take(1).First();     // discharge frequency
                textBox12.Text = File.ReadAllLines(openFileDialog.FileName).Skip(9).Take(1).First();     // damping

                // capacitor parameters
                textBox8.Text = File.ReadAllLines(openFileDialog.FileName).Skip(10).Take(1).First();     // power
                textBox9.Text = File.ReadAllLines(openFileDialog.FileName).Skip(11).Take(1).First();     // voltage
                textBox13.Text = File.ReadAllLines(openFileDialog.FileName).Skip(12).Take(1).First();     // si
                textBox14.Text = File.ReadAllLines(openFileDialog.FileName).Skip(13).Take(1).First();     // pi
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "";
            save.Filter = "Text File | *.txt";
            if (save.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(save.OpenFile());
                // simulation parameters
                writer.WriteLine(textBox1.Text);    // sample rate
                writer.WriteLine(textBox2.Text);    // tmax
                writer.WriteLine(textBox10.Text);   // power frequency

                // bank parameters
                writer.WriteLine(textBox3.Text);    // bank series 1 branch
                writer.WriteLine(textBox5.Text);    // bank series 2 branch
                writer.WriteLine(textBox4.Text);    // bank parallel 1 branch
                writer.WriteLine(textBox6.Text);    // bank parallel 1 branch
                writer.WriteLine(textBox7.Text);    // Ulim
                writer.WriteLine(textBox11.Text);   // discharge frequency
                writer.WriteLine(textBox12.Text);   // damping

                // capacitor parameters
                writer.WriteLine(textBox8.Text);   // power
                writer.WriteLine(textBox9.Text);   // voltage
                writer.WriteLine(textBox13.Text);   // si
                writer.WriteLine(textBox14.Text);   // pi

                writer.Dispose();
                writer.Close();
            }
        }

        
        double ams, tmax, fn, fd, s, p, si, pi, jQ, Un, C,Cb, L, R, Ulim, D, U1dd;
        double XC,alpha,W0,Wd,B1,B2,I1,I2;
        async void damped_discharge()
        {
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.Series[3].Points.Clear();
            chart1.Series[4].Points.Clear();
            ams = Convert.ToDouble(textBox1.Text);
            tmax = Convert.ToDouble(textBox2.Text);
            fn = Convert.ToDouble(textBox10.Text);
            s = Convert.ToDouble(textBox3.Text) + Convert.ToDouble(textBox5.Text);
            si = Convert.ToDouble(textBox13.Text);            
            p = Convert.ToDouble(textBox4.Text) + Convert.ToDouble(textBox6.Text);
            pi = Convert.ToDouble(textBox14.Text);
            Ulim = Convert.ToDouble(textBox7.Text);
            jQ = Convert.ToDouble(textBox8.Text);
            Un = Convert.ToDouble(textBox9.Text);
            fd = Convert.ToDouble(textBox11.Text);
            if(textBox15.Text != "")
            {
                C = Convert.ToDouble(textBox15.Text);
            }
            else
            {
                C = jQ * 1000 / (2 * Math.PI * fn * Math.Pow(Un, 2));
            }
            Cb = C * si / pi;
            L = (1 / Math.Pow(2 * Math.PI * fd, 2)) / C;
            D = Convert.ToDouble(textBox12.Text);
            R = -2 * (fd * L * Math.Log(D));
            U1dd = Ulim * Un * 1.6;
            alpha = R / (2 * L);
            W0 = 1 / (Math.Sqrt(C * L));
            Wd = Math.Sqrt(Math.Pow(W0, 2) - Math.Pow(alpha, 2));
            B1 = -U1dd;
            B2 = (1 / Wd) * (alpha * B1);
            int np = Convert.ToInt32(1 / ams);
            double[] t = new double[np];
            double[] it = new double[np];
            double[] i2t = new double[np];
            double[] vt = new double[np];
            double[] pt = new double[np];
            double[] et = new double[np];
            for (int i = 0; i < np; i++)
            {
                t[i] = i * ams;
                vt[i] = Math.Exp(-alpha * t[i]) * ((B1 * Math.Cos(Wd * t[i])) + (B2 * Math.Sin(Wd * t[i])));
                it[i] = C*(Math.Exp(-alpha * t[i])*(-B1 * Wd * Math.Sin(Wd * t[i]) + (Wd * B2 * Math.Cos(Wd * t[i]))) - (alpha*Math.Exp(-alpha * t[i])*(B1 * Math.Cos(Wd * t[i]) + (B2 * Math.Sin(Wd * t[i])))));
                pt[i] = vt[i] * it[i];
                et[0] = 0;
                i2t[i] = it[i] * it[i] * ams;
                if(i!=0)
                {
                    i2t[i] = i2t[i] + i2t[i - 1];
                    et[i] = (pt[i] * ams) + et[i - 1];
                }                

                chart1.Series[0].Points.AddXY(t[i], it[i]);
                chart1.Series[1].Points.AddXY(t[i], i2t[i]);
                chart1.Series[2].Points.AddXY(t[i], pt[i]);
                chart1.Series[3].Points.AddXY(t[i], et[i]);
                chart1.Series[4].Points.AddXY(t[i], vt[i]);
                progressBar1.Value = Convert.ToInt32((i / np) * 100);
                if(tmax <= (i*ams))
                {
                    break;
                }
            }
            //MessageBox.Show("" + it.Max());
            label4.Text = "Total phase: " + s * p;
            label5.Text = "Total: " + s * p * 3;
            label10.Text = "Cn: " + Math.Round(C * 1000000, 2)+ "μF";
            label11.Text = label10.Text;
            label12.Text = "Ln: " + Math.Round(L * 1000, 2) + "mH";
            label14.Text = "Rn: " + Math.Round(R, 2) + "Ω";
            label16.Text = "Ue: " + Math.Round(U1dd, 0) + "V";
            label19.Text = "Cb: " + Math.Round(Cb * 1000000, 2) + "µF";
            await Task.Delay(500);
            progressBar1.Value = 0;
            chart1.ChartAreas[0].AxisX.Maximum = tmax;
        }
    }    
}
