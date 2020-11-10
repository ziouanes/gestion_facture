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
    public partial class Facture : Form
    {
        public Facture()
        {
            InitializeComponent();
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
        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }


        private void facture_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // this.Close();
            //textBox1.Text = null;
            //textBox3.Text = null;
            //textBox5.Text = null;
            //textBox6.Text = null;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {


        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            textBox3.Text = null;
            textBox8.Text = null;
            textBox6.Text = null;
            textBox1.Text = null;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;


        }

        private void facture_Leave(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox6.Text == "" || textBox3.Text == "" || textBox8.Text == "" || comboBox1.SelectedIndex == -1)
            {
                this.Alert("all field required", Form_Alert.enmType.Error);
            }

            else
            {

                string textquery = "INSERT INTO facture(N°facture , DateFacture  ,GARAGE, VEHICULE, MONTANT  , N°BON , DatePaiement  , KILOMÉTRAGE  ) VALUES('" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "' ,'" + textBox1.Text + "' , '" + comboBox1.SelectedValue + "'  , '" + textBox8.Text + "'  ,'" + textBox6.Text + "'  ,  '" + dateTimePicker2.Value.ToString("dd/MM/yyyy") + "' ,'" + textBox4.Text + "' ) ";
                ExecuteQuery(textquery);
                this.Alert("add facture Success", Form_Alert.enmType.Success);
                textBox1.Text = ""; textBox6.Text = ""; textBox3.Text = ""; textBox8.Text = ""; comboBox1.SelectedIndex = -1; dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                this.ActiveControl = textBox3;

            }
        }

        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = textBox1;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {


            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = dateTimePicker1;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = dateTimePicker2;
            }
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = textBox8;
            }
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = textBox6;
            }
        }

        private void textBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = button1;
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = textBox3;
            }
        }

        private void Facture_Load_1(object sender, EventArgs e)
        {
            this.ActiveControl = textBox3;
            try
            {
            Program.sql_con.Open();

            Program.sql_cmd.CommandText = string.Format("select  * from vehicules ");
            Program.db = Program.sql_cmd.ExecuteReader();
            DataTable dts = new DataTable();
            dts.Load(Program.db);
            comboBox1.DataSource = dts;
            comboBox1.ValueMember = "id";
            comboBox1.DisplayMember = "VEHICULE";
            comboBox1.SelectedIndex = -1;
            Program.sql_con.Close();
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;

            }
            catch { }
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            textBox3.Text = null;
            textBox8.Text = null;
            textBox6.Text = null;
            textBox1.Text = null;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox6.Text == "" || textBox3.Text == "" || textBox8.Text == "" || comboBox1.SelectedIndex == -1)
            {
                this.Alert("all field required", Form_Alert.enmType.Error);
            }

            else
            {

                string textquery = "INSERT INTO facture(N°facture , DateFacture  ,GARAGE, VEHICULE, MONTANT  , N°BON , DatePaiement  , KILOMÉTRAGE  ) VALUES('" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "' ,'" + textBox1.Text + "' , '" + comboBox1.SelectedValue + "'  , '" + textBox8.Text + "'  ,'" + textBox6.Text + "'  ,  '" + dateTimePicker2.Value.ToString("dd/MM/yyyy") + "' ,'" + textBox4.Text + "' ) ";
                ExecuteQuery(textquery);
                this.Alert("add facture Success", Form_Alert.enmType.Success);
                textBox1.Text = ""; textBox6.Text = ""; textBox3.Text = ""; textBox8.Text = ""; comboBox1.SelectedIndex = -1; dateTimePicker1.Value = DateTime.Now;
                dateTimePicker2.Value = DateTime.Now;
                this.ActiveControl = textBox3;

            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
