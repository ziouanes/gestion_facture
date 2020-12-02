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
    public partial class vehicules : Form
    {
        public vehicules()
        {
            InitializeComponent();
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

        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }
        private void button1_Click(object sender, EventArgs e)
        {

            //ALTER TABLE odm ADD COLUMN  vehicule_id INTEGER REFERENCES vehicules(id)
            


            if (textBox2.Text !="")
            {
                if (MessageBox.Show("Do you really want to add   vehicule  Matricule° " + textBox2.Text + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string textquery = "INSERT INTO vehicules(vehicule)VALUES('" + textBox2.Text + "')";
                    ExecuteQuery(textquery);
                    this.Alert("add facture vehicule Success", Form_Alert.enmType.Success);
                    textBox2.Text = "";




                }



            }
            else
            {
                this.Alert("Please add  new vehicule", Form_Alert.enmType.Warning);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
