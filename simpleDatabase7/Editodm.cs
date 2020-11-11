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

        string st="";
       

        public Editodm(string numero, DateTime date, string distination, string VEHICULE, string kelometrage, string bénéficier, string montant, string QUALITÉ)
        {
            InitializeComponent();
            label1.Text = numero;
            dateTimePicker1.Value = date;
            st = VEHICULE;
            textBox1.Text = distination;
            textBox8.Text = montant;
            textBox4.Text = bénéficier;
            textBox6.Text = kelometrage;
            textBox3.Text = QUALITÉ;
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
            if (textBox1.Text == "" || textBox6.Text == "" || textBox8.Text == "" || comboBox1.SelectedIndex == -1 || textBox4.Text == "" || textBox3.Text == "")
            {
                this.Alert("all field required", Form_Alert.enmType.Error);
            }

            else
            {
                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
                Program.sql_cmd.CommandText = string.Format("update ODM  set   DATE =  '{0}' , DESTINATION = '{1}' , VEHICULE = '{2}' ,   KILOMÉTRAGE = '{3}',  BÉNÉFICIANT = '{4}'  ,   MONTANT=  '{5}' , QUALITÉ = '{6}'  where n°ODM = '{7}'  ", dateTimePicker1.Value.ToString("yyyy-MM-dd"), textBox1.Text, comboBox1.SelectedValue.ToString(), textBox6.Text, textBox4.Text, textBox8.Text,textBox3.Text , label1.Text);
                Program.sql_cmd.ExecuteNonQuery();
                Program.sql_con.Close();

                textBox1.Text = ""; textBox6.Text = ""; textBox8.Text = ""; comboBox1.SelectedIndex = -1; dateTimePicker1.Value = DateTime.Now;
                textBox4.Text = "";



                ///////////show data to grid  




                this.Close();
                this.Alert("add ODM Success", Form_Alert.enmType.Success);




            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Voulez-vous vraiment supprimer  ODM N° " + label1.Text + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();

                Program.sql_cmd.CommandText = string.Format("delete from ODM where n°ODM ='{0}' ", label1.Text);
                Program.sql_cmd.ExecuteNonQuery();




                Program.sql_con.Close();

                textBox1.Text = ""; textBox6.Text = ""; textBox8.Text = ""; dateTimePicker1.Value = DateTime.Now;




                ///////////show data to grid  




                this.Close();
                this.Alert("supprimer ODM Succès", Form_Alert.enmType.Info);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            textBox8.Text = null;
            textBox6.Text = null;
            textBox1.Text = null;
            dateTimePicker1.Value = DateTime.Now;
            textBox4.Text = null;
            textBox3.Text = null;
        }

        private void Editodm_Load_1(object sender, EventArgs e)
        {
            this.ActiveControl = textBox3;
            Program.sql_con.Open();

            Program.sql_cmd.CommandText = string.Format("select  * from vehicules ");
            Program.db = Program.sql_cmd.ExecuteReader();
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
            //comboBox1.SelectedItem = st;
        }
    }
}
