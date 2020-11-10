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
using System.Data.SqlClient;

namespace simpleDatabase7
{
    public partial class Showallfacture : Form
    {
        public Showallfacture()
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
        public void LoadData()
        {
            Program.sql_con.Open();
            DataTable dt = new DataTable();
            Program.sql_cmd = new SQLiteCommand("SELECT f.[N°facture] ,f.[DateFacture] , f.[GARAGE] , v.[vehicule] , f.[MONTANT] , f.[N°BON] ,f.[DatePaiement],f.[KILOMÉTRAGE]    from facture f inner join vehicules v on f.VEHICULE = v.id order by DateFacture asc ", Program.sql_con);
            Program.db = Program.sql_cmd.ExecuteReader();
            dt.Load(Program.db);

            //hide column 
            //dt.Columns[0].ColumnMapping = MappingType.Hidden;


            dataGridView1.DataSource = null;

            dataGridView1.DataSource = dt;
            dataGridView1.ClearSelection();

            Program.sql_con.Close();
        }


        private void showALll_Load(object sender, EventArgs e)
        {
            LoadData();
            dataGridView1.ClearSelection();


            comboBox1.Text = "Tout";


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
                if (dataGridView1.CurrentRow.Selected == false )
            {
                this.Alert("please select the row to update", Form_Alert.enmType.Warning);
            }
            else
            {
                

                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    string numerof = Convert.ToString(selectedRow.Cells[0].Value);
                        System.Globalization.CultureInfo p = System.Globalization.CultureInfo.InvariantCulture;
                        DateTime date = DateTime.ParseExact(selectedRow.Cells[1].Value.ToString(), "dd/MM/yyyy", p);
                        string garage = Convert.ToString(selectedRow.Cells[2].Value);
                    string Véhicule = Convert.ToString(selectedRow.Cells[3].Value);
                    string montant = Convert.ToString(selectedRow.Cells[4].Value);
                    string nbon = Convert.ToString(selectedRow.Cells[5].Value);
                        System.Globalization.CultureInfo pp = System.Globalization.CultureInfo.InvariantCulture;
                        DateTime datepayment = DateTime.ParseExact(selectedRow.Cells[6].Value.ToString(), "dd/MM/yyyy", pp);
                        string KILOMÉTRAGE = Convert.ToString(selectedRow.Cells[7].Value);
                        Editfacture editf = new Editfacture(numerof, date, garage, Véhicule, montant, nbon, datepayment, KILOMÉTRAGE ); editf.ShowDialog();
                    dataGridView1.DataSource = null;
                    LoadData();

                }


            }
        }


            }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.DataSource = null;
            LoadData();
            dataGridView1.ClearSelection();
        }

        private void btnAddNewBooks_Click(object sender, EventArgs e)
        {
            Facture factre1 = new Facture();
            factre1.ShowDialog();
            dataGridView1.DataSource = null;
            LoadData();
        }

        private void button3_Click(object sender, EventArgs e)
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

       

        private void pictureBox2_Click(object sender, EventArgs e)
        {

            

            string req, req1 = "", req2 = "", req3 = "";

            req = "SELECT f.[N°facture] ,f.[DateFacture] , f.[GARAGE] , v.[VEHICULE] , f.[MONTANT] , f.[N°BON] ,f.[DatePaiement],f.[KILOMÉTRAGE]    from facture f inner join vehicules v on f.VEHICULE = v.id  where 1=1 ";

            if (comboBox1.SelectedIndex == -1)
            {

                req = " SELECT f.[N°facture] ,f.[DateFacture] , f.[GARAGE] , v.[VEHICULE] , f.[MONTANT] , f.[N°BON] ,f.[DatePaiement],f.[KILOMÉTRAGE]    from facture f inner join vehicules v on f.VEHICULE = v.id  asc where 1=1";


            }
            if (comboBox1.SelectedIndex == 1)
            {
                if (textBox1.Text != null)
                {
                    req1 = "and  f.[N°facture] LIKE '%" + textBox1.Text + "%'";


                }

            }

            if (comboBox1.SelectedIndex == 2)
            {
                if (comboBox2.SelectedIndex != -1)
                {
                    req2 = "and  v.[VEHICULE]  LIKE '%" + comboBox2.Text + "%'";
                   

                }

            }

            if (comboBox1.SelectedIndex == 3)
            {
                if (textBox1.Text != null)
                {
                    req3 = "and  f.[N°BON] LIKE '%" + textBox1.Text + "%'";


                }

            }

            req += req1+req2+req3+ "order by DateFacture asc";
                Program.sql_con.Open();
                DataTable dt = new DataTable();
                Program.sql_cmd.CommandText = req;

                
                Program.db = Program.sql_cmd.ExecuteReader();
                if (Program.db.HasRows)
                {

                    dt.Load(Program.db);
                    
                   

                    dataGridView1.DataSource = null;

                    dataGridView1.DataSource = dt;
                    dataGridView1.ClearSelection();

                }
                else
                {

                    this.Alert("Data note found  test ", Form_Alert.enmType.Error);
                }
                Program.db.Close();

                Program.sql_con.Close();







          


        }


        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //readonly
            e.SuppressKeyPress = true;
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {







        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string numerof = Convert.ToString(selectedRow.Cells[0].Value);
                System.Globalization.CultureInfo p = System.Globalization.CultureInfo.InvariantCulture;
                DateTime date = DateTime.ParseExact(selectedRow.Cells[1].Value.ToString(), "dd/MM/yyyy", p); 
                string garage = Convert.ToString(selectedRow.Cells[2].Value);
                string Véhicule = Convert.ToString(selectedRow.Cells[3].Value);
                string montant = Convert.ToString(selectedRow.Cells[4].Value);
                string nbon = Convert.ToString(selectedRow.Cells[5].Value);
                System.Globalization.CultureInfo pp = System.Globalization.CultureInfo.InvariantCulture;
                DateTime datepayment = DateTime.ParseExact(selectedRow.Cells[6].Value.ToString(), "dd/MM/yyyy", pp);
                string KILOMÉTRAGE = Convert.ToString(selectedRow.Cells[7].Value);
              
                Editfacture editf = new Editfacture(numerof, date, garage, Véhicule, montant, nbon, datepayment , KILOMÉTRAGE );
                editf.ShowDialog();
                dataGridView1.DataSource = null;
                LoadData();

            }
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void Showallfacture_Load(object sender, EventArgs e)
        {
            LoadData();
            dataGridView1.ClearSelection();


            comboBox1.Text = "Tout";

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

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string numerof = Convert.ToString(selectedRow.Cells[0].Value);
                System.Globalization.CultureInfo p = System.Globalization.CultureInfo.InvariantCulture;
                DateTime date = DateTime.ParseExact(selectedRow.Cells[1].Value.ToString(), "dd/MM/yyyy", p); 
                string garage = Convert.ToString(selectedRow.Cells[2].Value);
                string Véhicule = Convert.ToString(selectedRow.Cells[3].Value);
                string montant = Convert.ToString(selectedRow.Cells[4].Value);
                string nbon = Convert.ToString(selectedRow.Cells[5].Value);
                System.Globalization.CultureInfo pp = System.Globalization.CultureInfo.InvariantCulture;
                DateTime datepayment = DateTime.ParseExact(selectedRow.Cells[6].Value.ToString(), "dd/MM/yyyy", pp); 
                string KILOMÉTRAGE = Convert.ToString(selectedRow.Cells[7].Value);
               
                Editfacture editf = new Editfacture(numerof, date, garage, Véhicule, montant, nbon, datepayment, KILOMÉTRAGE );
                editf.ShowDialog();
                dataGridView1.DataSource = null;
                LoadData();

            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_KeyDown_1(object sender, KeyEventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            

           
            
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            string numero = "";
            if (e.KeyCode == Keys.Delete)
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    numero = Convert.ToString(selectedRow.Cells[0].Value);





                    if (MessageBox.Show("Do you really want to delete  facture N° " + Convert.ToString(selectedRow.Cells[0].Value) + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();

                        Program.sql_cmd.CommandText = string.Format("delete from facture where N°facture ='{0}' ", numero);
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

        private void textBox7_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void textBox7_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void textBox7_KeyDown_2(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

           




        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 2)
            {
                textBox1.Visible = false;
                comboBox2.Visible = true;
                comboBox2.SelectedIndex = -1;
            }
            else
            {
                textBox1.Visible = true;
                comboBox2.Visible = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
