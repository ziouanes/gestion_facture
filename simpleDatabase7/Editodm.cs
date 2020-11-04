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
    public partial class Editodm : Form
    {
        
        public Editodm(string numero, DateTime date, string distination, string Véhicule, string kelometrage, string bénéficier, string montant)
        {
            InitializeComponent();
            label1.Text = numero;
            dateTimePicker1.Value = date;

            textBox2.Text = Véhicule;
            textBox1.Text = distination;
            textBox8.Text = montant;
            textBox4.Text = bénéficier;
            textBox6.Text = kelometrage;
        }
        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;


        }

        private void editodm_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox6.Text == "" || textBox8.Text == "" || textBox2.Text == "" || textBox4.Text == "")
            {
                this.Alert("all field required", Form_Alert.enmType.Error);
            }

            else
            {
                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                Program.sql_cmd.CommandText = string.Format("update ODM  set   DATE =  '{0}' , DESTINATION = '{1}' , VÉHICULE = '{2}' ,   KILOMÉTRAGE = '{3}',  BÉNÉFICIANT = '{4}'  ,   MONTANT=  '{5}'  where n°ODM = '{6}'  ", dateTimePicker1.Value.ToString("dd/MM/yyyy"), textBox1.Text, textBox2.Text, textBox6.Text, textBox4.Text, textBox8.Text, label1.Text);
                Program.sql_cmd.ExecuteNonQuery();
                Program.sql_con.Close();

                textBox1.Text = ""; textBox6.Text = ""; textBox8.Text = ""; textBox2.Text = ""; dateTimePicker1.Value = DateTime.Now;
                textBox4.Text = "";



                ///////////show data to grid  




                this.Close();
                this.Alert("add ODM Success", Form_Alert.enmType.Success);




            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete  ODM N° " + label1.Text + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();

                Program.sql_cmd.CommandText = string.Format("delete from ODM where n°ODM ='{0}' ", label1.Text);
                Program.sql_cmd.ExecuteNonQuery();




                Program.sql_con.Close();

                textBox1.Text = ""; textBox6.Text = ""; textBox8.Text = ""; textBox2.Text = ""; dateTimePicker1.Value = DateTime.Now;




                ///////////show data to grid  




                this.Close();
                this.Alert("delete ODM Success", Form_Alert.enmType.Info);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = null;
            textBox8.Text = null;
            textBox6.Text = null;
            textBox1.Text = null;
            dateTimePicker1.Value = DateTime.Now;
            textBox4.Text = null;
        }

        private void Editodm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
