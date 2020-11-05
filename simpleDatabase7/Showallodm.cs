using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Globalization;

namespace simpleDatabase7
{
    public partial class Showallodm : Form
    {
        public Showallodm()
        {
            InitializeComponent();
        }

        //alert enum
        public void Alert(string msg, Form_Alert.enmType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }

        //load data
        private void LoadData()
        {
            Program.sql_con.Open();
            DataTable dt = new DataTable();
            Program.sql_cmd = new SQLiteCommand("SELECT f.[n°ODM] ,f.[DATE] , f.[DESTINATION] , v.[vehicule] , f.[KILOMÉTRAGE] , f.[BÉNÉFICIANT] ,f.[MONTANT],f.[QUALITÉ]  from ODM F inner join vehicules v on f.VEHICULE = v.id", Program.sql_con);
            Program.db = Program.sql_cmd.ExecuteReader();
            dt.Load(Program.db);

            //hide column 
      //      dt.Columns[0].ColumnMapping = MappingType.Hidden;



            dataGridView1.DataSource = dt;

            Program.sql_con.Close();
        }


        private void showALll_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            LoadData();
            dataGridView1.ClearSelection();
            comboBox1.Text = "Tout";

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnAddNewBooks_Click(object sender, EventArgs e)
        {
            odm odm = new odm();
            odm.ShowDialog();
            dataGridView1.DataSource = null;
            LoadData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;


        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {


                if (dataGridView1.CurrentRow.Selected == false)
            {
                this.Alert("please select the row to update", Form_Alert.enmType.Warning);
            }
            else
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {


                        int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                        string numero = Convert.ToString(selectedRow.Cells[0].Value);
                        System.Globalization.CultureInfo p = System.Globalization.CultureInfo.InvariantCulture;
                        DateTime date = DateTime.ParseExact(selectedRow.Cells[1].Value.ToString(), "dd/MM/yyyy", p);
                        string distination = Convert.ToString(selectedRow.Cells[2].Value);
                        string Véhicule = Convert.ToString(selectedRow.Cells[3].Value);
                        string kelometrage = Convert.ToString(selectedRow.Cells[4].Value);
                        string bénéficier = Convert.ToString(selectedRow.Cells[5].Value);
                        string montant = Convert.ToString(selectedRow.Cells[6].Value);
                        string QUALITÉ = Convert.ToString(selectedRow.Cells[7].Value);
                        Editodm edit = new Editodm(numero, date, distination, Véhicule, kelometrage, bénéficier, montant , QUALITÉ);
                        edit.ShowDialog();
                        dataGridView1.DataSource = null;
                        LoadData();
                    }

            }



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }
        public void Searchdataodm(string textsr, string textbox)
        {
            Program.sql_con.Open();
            DataTable dt = new DataTable();
            Program.sql_cmd = new SQLiteCommand("select * from  ODM where " + textsr + " LIKE '%" + textbox + "%' ", Program.sql_con);
            Program.db = Program.sql_cmd.ExecuteReader();
            if (!Program.db.HasRows)
            {
                this.Alert("Data note found", Form_Alert.enmType.Error);




            }
            dt.Load(Program.db);

            //hide column id
      //      dt.Columns[0].ColumnMapping = MappingType.Hidden;

            dataGridView1.DataSource = null;

            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();

            Program.sql_con.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.SelectedIndex == -1)
                {

                    LoadData();
                    //textBox7.Enabled = false;

                }
                else if (comboBox1.SelectedIndex == 0)
                {

                    LoadData();
                   
                }
                else if (comboBox1.SelectedIndex == 1)
                {
                    if (textBox7.Text != null)
                    {
                        Searchdataodm("n°ODM", textBox7.Text);

                    }
                    else { this.Alert("text required", Form_Alert.enmType.Info); }


                }
                else if (comboBox1.SelectedIndex == 2)
                {
                    if (textBox7.Text != null)
                    {
                        Searchdataodm("VÉHICULE", textBox7.Text);

                    }
                    else { this.Alert("text required", Form_Alert.enmType.Info); }


                }

                else
                {
                    this.Alert("data note found", Form_Alert.enmType.Info);

                }
            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
            }
           
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Selected == false)
            {
                this.Alert("please select the row to update", Form_Alert.enmType.Warning);
            }
            else
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    string numero = Convert.ToString(selectedRow.Cells[0].Value);

                    System.Globalization.CultureInfo p = System.Globalization.CultureInfo.InvariantCulture;
                    DateTime date = DateTime.ParseExact(selectedRow.Cells[1].Value.ToString(), "dd/MM/yyyy", p);
                    string distination = Convert.ToString(selectedRow.Cells[2].Value);
                    string Véhicule = Convert.ToString(selectedRow.Cells[3].Value);
                    string kelometrage = Convert.ToString(selectedRow.Cells[4].Value);
                    string bénéficier = Convert.ToString(selectedRow.Cells[5].Value);
                    string montant = Convert.ToString(selectedRow.Cells[6].Value);
                    string QUALITÉ = Convert.ToString(selectedRow.Cells[7].Value);
                    Editodm edit = new Editodm(numero, date, distination, Véhicule, kelometrage, bénéficier, montant, QUALITÉ);
                    edit.ShowDialog();
                    dataGridView1.DataSource = null;
                    LoadData();
                }


            }
        }

        private void Showallodm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            LoadData();
            dataGridView1.ClearSelection();
            comboBox1.Text = "Tout";
        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Selected == false)
            {
                this.Alert("please select the row to update", Form_Alert.enmType.Warning);
            }
            else
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    string numero = Convert.ToString(selectedRow.Cells[0].Value);
                    System.Globalization.CultureInfo p = System.Globalization.CultureInfo.InvariantCulture;
                    DateTime date = DateTime.ParseExact(selectedRow.Cells[1].Value.ToString(), "dd/MM/yyyy", p);
                    string distination = Convert.ToString(selectedRow.Cells[2].Value);
                    string Véhicule = Convert.ToString(selectedRow.Cells[3].Value);
                    string kelometrage = Convert.ToString(selectedRow.Cells[4].Value);
                    string bénéficier = Convert.ToString(selectedRow.Cells[5].Value);
                    string montant = Convert.ToString(selectedRow.Cells[6].Value);
                    string QUALITÉ = Convert.ToString(selectedRow.Cells[7].Value);
                    Editodm edit = new Editodm(numero, date, distination, Véhicule, kelometrage, bénéficier, montant, QUALITÉ);
                    edit.ShowDialog();
                    dataGridView1.DataSource = null;
                    LoadData();
                }
            }
        }

        private void textBox7_KeyDown_1(object sender, KeyEventArgs e)
        {

            
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Exported from gridview";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView1.Rows.Count ; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            // save the application  
            //workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            string numero  = "";
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    numero = Convert.ToString(selectedRow.Cells[0].Value);





                    if (MessageBox.Show("Do you really want to delete  ODM N° " + numero + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();

                        Program.sql_cmd.CommandText = string.Format("delete from ODM where n°ODM ='{0}' ", numero);
                        Program.sql_cmd.ExecuteNonQuery();




                        Program.sql_con.Close();




                        ///////////show data to grid  



                        dataGridView1.DataSource = null;
                        LoadData();

                        this.Alert("delete facture Success", Form_Alert.enmType.Info);

                    }
                }
            }
        }

        private void textBox7_KeyDown_2(object sender, KeyEventArgs e)
        {

        }
    }
}
