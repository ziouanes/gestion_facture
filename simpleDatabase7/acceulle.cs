using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simpleDatabase7
{
    public partial class acceulle : Form
    {
        public acceulle()
        {
            InitializeComponent();
        }
        private void button10_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            odm odm = new odm();
            odm.ShowDialog();
        }

        private void button12_Click(object sender, EventArgs e)
        {

            System.Diagnostics.Process.Start("mailto:contact@hamza-ziouane.tech");


        }

        private void button11_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/ziouanes/gestion_facture");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Facture fc = new Facture();
            fc.ShowDialog();




































































































































































































































        }

        private void button7_Click(object sender, EventArgs e)
        {
            
           

            try
            {
                using (Showallodm showALllodm = new Showallodm())
                { 
                    showALllodm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                    MessageBox.Show(ex.Message);
            }
            finally
            {
                //this.Dispose();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Showallfacture ALllFacture = new Showallfacture();
            ALllFacture.ShowDialog();
        }

        private void acceulle_Load(object sender, EventArgs e)
        {


        }

        private void button8_Click(object sender, EventArgs e)
        {
            vehicules VS = new vehicules();
            VS.ShowDialog();
        }

        private void acceulle_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black, 6), this.DisplayRectangle);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
