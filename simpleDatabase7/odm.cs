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
    public partial class odm : Form
    {
        public odm()
        {
            InitializeComponent();
        }

        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }

        private void ExecuteQuery(string txtQuery)
        {
            //Program.SetConnection();
            Program.sql_con.Open();
            Program.sql_cmd = Program.sql_con.CreateCommand();
            Program.sql_cmd.CommandText = txtQuery;
            Program.sql_cmd.ExecuteNonQuery();
            Program.sql_con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            textBox3.Text = null;
            textBox8.Text = null;
            textBox6.Text = null;
            textBox1.Text = null;
            dateTimePicker1.Value = DateTime.Now;
            textBox4.Text = null;

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox6.Text == "" || textBox3.Text == "" || textBox8.Text == "" || comboBox1.SelectedIndex == -1 || textBox4.Text == "")
            {
                this.Alert("all field required", Form_Alert.enmType.Error);
            }

            else
            {

                


                string textquery = "INSERT INTO ODM(n°ODM , DATE  ,DESTINATION, VEHICULE, KILOMÉTRAGE  , BÉNÉFICIANT , MONTANT ,   QUALITÉ  ) VALUES('" + textBox3.Text + "','" + dateTimePicker1.Value.ToString("dd/MM/yyyy") + "' ,'" + textBox1.Text + "' , '" + comboBox1.SelectedValue + "'  , '" + textBox6.Text + "'  , '" + textBox4.Text + "'  ,'" + textBox8.Text + "','" + textBox5.Text + "'  ) ";
                ExecuteQuery(textquery);
                this.Alert("add facture Success", Form_Alert.enmType.Success);
                textBox1.Text = ""; textBox6.Text = ""; textBox3.Text = ""; textBox8.Text = ""; comboBox1.SelectedIndex = -1; dateTimePicker1.Value = DateTime.Now;
                textBox4.Text = null;
                this.ActiveControl = textBox3;

            }
        }

        private void odm_Load(object sender, EventArgs e)
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
            comboBox1.SelectedIndex = -1;
            Program.sql_con.Close();
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
        }
    }
}
