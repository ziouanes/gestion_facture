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
    public partial class Editfacture : Form
    {
        

        public Editfacture(string numerof, DateTime date, string garage, string Véhicule, string montant, string nbon, DateTime datepayment  , string KILOMÉTRAGE)
        {
            InitializeComponent();
            label1.Text = numerof;
            dateTimePicker1.Value = date;
            textBox2.Text = Véhicule;
            textBox1.Text = garage;
            textBox8.Text = montant;
            dateTimePicker2.Value = datepayment;
            textBox6.Text = nbon;
            textBox3.Text = KILOMÉTRAGE;
        }

        private void editfacture_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox2.Text = null;
            textBox8.Text = null;
            textBox6.Text = null;
            textBox1.Text = null;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            this.ActiveControl = dateTimePicker1;
        }

        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }


        //setexecutequery
        private void ExecuteQuery(string txtQuery)
        {
            //Program.SetConnection();
            Program.sql_con.Open();
            Program.sql_cmd = Program.sql_con.CreateCommand();
            Program.sql_cmd.CommandText = txtQuery;
            Program.sql_cmd.ExecuteNonQuery();
            Program.sql_con.Close();
        }
        public static DataTable dtnew = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox6.Text == "" || textBox8.Text == "" || textBox2.Text == "")
            {
                this.Alert("all field required", Form_Alert.enmType.Error);
            }

            else
            {
                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                Program.sql_cmd.CommandText = string.Format("update facture set   DateFacture =  '{0}' , GARAGE = '{1}' , VÉHICULE = '{2}' ,   MONTANT = '{3}',  N°BON = '{4}'  ,   DatePaiement=  '{5}' , KILOMÉTRAGE = '{6}'  where N°facture = '{7}'  ", dateTimePicker1.Value.ToString("dd/MM/yyyy"), textBox1.Text, textBox2.Text, textBox8.Text, textBox6.Text, dateTimePicker2.Value.ToString("dd/MM/yyyy"), textBox3.Text, label1.Text);
                Program.sql_cmd.ExecuteNonQuery();
                Program.sql_con.Close();

                textBox1.Text = ""; textBox6.Text = ""; textBox8.Text = ""; textBox2.Text = ""; dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;



                ///////////show data to grid  




                this.Close();
                this.Alert("add facture Success", Form_Alert.enmType.Success);




            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete  facture N° " + label1.Text + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();

                Program.sql_cmd.CommandText = string.Format("delete from facture where N°facture ='{0}' ", label1.Text);
                Program.sql_cmd.ExecuteNonQuery();




                Program.sql_con.Close();

                textBox1.Text = ""; textBox6.Text = ""; textBox8.Text = ""; textBox2.Text = ""; dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;



                ///////////show data to grid  




                this.Close();
                this.Alert("delete facture Success", Form_Alert.enmType.Info);

            }

        }

        private void Editfacture_Load_1(object sender, EventArgs e)
        {

        }
    }
}
