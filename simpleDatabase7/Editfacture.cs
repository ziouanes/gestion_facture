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
        string st = "";
        

        public Editfacture(string numerof, DateTime date, string garage, string Véhicule, string montant, string nbon, DateTime datepayment  , string KILOMÉTRAGE  )
        {
            InitializeComponent();
            label1.Text = numerof;
            dateTimePicker1.Value = date;
            st  = Véhicule;
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
            comboBox1.SelectedIndex = -1;
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
            if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
            Program.sql_cmd = Program.sql_con.CreateCommand();
            Program.sql_cmd.CommandText = txtQuery;
            Program.sql_cmd.ExecuteNonQuery();
            Program.sql_con.Close();
        }
        public static DataTable dtnew = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox6.Text == "" || textBox8.Text == "" || comboBox1.SelectedIndex == -1)
            {
                this.Alert("all field required", Form_Alert.enmType.Error);
            }

            else
            {
                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                Program.sql_cmd.CommandText = string.Format("update facture set   DateFacture =  '{0}' , GARAGE = '{1}' , VEHICULE = '{2}' ,   MONTANT = '{3}',  N°BON = '{4}'  ,   DatePaiement=  '{5}' , KILOMÉTRAGE = '{6}'   where N°facture = '{7}'  ", dateTimePicker1.Value.ToString("yyyy-MM-dd"), textBox1.Text, comboBox1.SelectedValue.ToString(), textBox8.Text, textBox6.Text, dateTimePicker2.Value.ToString("yyyy-MM-dd"), textBox3.Text, label1.Text);
                Program.sql_cmd.ExecuteNonQuery();
                Program.sql_con.Close();

                textBox1.Text = ""; textBox6.Text = ""; textBox8.Text = ""; comboBox1.SelectedIndex =-1; dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;



                ///////////show data to grid  




                this.Close();
                this.Alert("add facture Success", Form_Alert.enmType.Success);




            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment supprimer  facture N° " + label1.Text + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();

                Program.sql_cmd.CommandText = string.Format("delete from facture where N°facture ='{0}' ", label1.Text);
                Program.sql_cmd.ExecuteNonQuery();




                Program.sql_con.Close();

                textBox1.Text = ""; textBox6.Text = ""; textBox8.Text = ""; comboBox1.SelectedIndex = -1; dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;



                ///////////show data to grid  




                this.Close();
                this.Alert("delete facture Success", Form_Alert.enmType.Info);

            }

        }

        private void Editfacture_Load_1(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();


            Program.sql_cmd.CommandText = string.Format("select  * from vehicules ");
            Program.db  = Program.sql_cmd.ExecuteReader();
            DataTable dts = new DataTable();
            dts.Load(Program.db);
            comboBox1.DataSource = dts;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "vehicule";
            
            Program.sql_con.Close();
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox1.SelectedIndex = -1;
            comboBox1.SelectedText = st;
            this.ActiveControl = comboBox1;
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
              (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
























































        }
    }
}
