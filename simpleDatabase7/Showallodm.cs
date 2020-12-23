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
using DGVPrinterHelper;
using System.Drawing.Printing;

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

        //sum Montant
        private void Montant_sum()
        {
            double sum = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)


                sum += Convert.ToDouble(row.Cells[6].Value);
            textBox1.Text = sum.ToString("F");
        }

        //load data
        private void LoadData()
        {
            if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();
            DataTable dt = new DataTable();
            Program.sql_cmd = new SQLiteCommand("SELECT f.[n°ODM] ,f.[DATE] , f.[DESTINATION] , v.[VEHICULE] , f.[KILOMÉTRAGE] , f.[BÉNÉFICIANT] ,f.[MONTANT],f.[QUALITÉ]  from ODM F inner join vehicules v on f.VEHICULE = v.id order by date DESC", Program.sql_con);
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
                this.Alert("sélectionnez la ligne à mettre à jour", Form_Alert.enmType.Warning);
            }
            else
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {


                        int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                        DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                        string numero = Convert.ToString(selectedRow.Cells[0].Value);
                        DateTime date = Convert.ToDateTime(selectedRow.Cells[1].Value.ToString());
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
                        Montant_sum();
                    }

            }



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }
       

        private void pictureBox2_Click(object sender, EventArgs e)
        {


            string req, req1 = "", req2 = "", req3 = "";

            req = "SELECT f.[n°ODM] ,f.[DATE] , f.[DESTINATION] , v.[VEHICULE] , f.[KILOMÉTRAGE] , f.[BÉNÉFICIANT] ,f.[MONTANT],f.[QUALITÉ]  from ODM F inner join vehicules v on f.VEHICULE = v.id  where 1=1 ";

            if (comboBox1.SelectedIndex == -1)
            {

                req =  "SELECT f.[n°ODM] ,f.[DATE] , f.[DESTINATION] , v.[VEHICULE] , f.[KILOMÉTRAGE] , f.[BÉNÉFICIANT] ,f.[MONTANT],f.[QUALITÉ]  from ODM F inner join vehicules v on f.VEHICULE = v.id  where 1 = 1 ";


            }
            if (comboBox1.SelectedIndex == 1)
            {
                if (textBox7.Text != null)
                {
                    req1 = "and  f.[n°ODM] LIKE '%" + textBox7.Text + "%'";


                }
                else { this.Alert("text required", Form_Alert.enmType.Info); }

            }

            if (comboBox1.SelectedIndex == 2)
            {
                if (comboBox2.SelectedIndex != -1)
                {
                    req2 = "and  v.[VEHICULE]  LIKE '%" + comboBox2.Text + "%'";


                }
                else { this.Alert("text required", Form_Alert.enmType.Info); }

            }

            if (datecheck.Checked)
            {
                if (textBox7.Text != null || comboBox2.SelectedIndex != -1)
                {
                    req3 = "and f.[DATE] BETWEEN  '" + date1.Value.ToString("yyyy-MM-dd") + "'  and  '" + date2.Value.ToString("yyyy-MM-dd") + "'";
                   

                }
                else { this.Alert("text required", Form_Alert.enmType.Info); }
            }




            try
            {
                req += req1 + req2 + req3 + "order by date desc";
                if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open(); DataTable dt = new DataTable();
                Program.sql_cmd.CommandText = req;


                Program.db = Program.sql_cmd.ExecuteReader();
                if (Program.db.HasRows)
                {

                    dt.Load(Program.db);



                    dataGridView1.DataSource = null;

                    dataGridView1.DataSource = dt;

                    //textBox1.Text = dataGridView1.
                    dataGridView1.ClearSelection();
                    Montant_sum();




                }
                else
                {

                    this.Alert("Données non trouvées  ", Form_Alert.enmType.Error);
                }
                Program.db.Close();

                Program.sql_con.Close();

            }
            catch (Exception x)
            {

                MessageBox.Show(x.Message);
            }


           






        }
        

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        
        
        private void Showallodm_Load(object sender, EventArgs e)
        {

            date1.CustomFormat = "dd/MM/yyyy";
            date2.CustomFormat = "dd/MM/yyyy";

            ////work

            //data searching
            textBox7.Visible = false;
            comboBox2.Visible = false;


            //date things
            date1.Visible = false;
            a.Visible = false;
            date2.Visible = false;
           


            dataGridView1.DataSource = null;
            LoadData();
            dataGridView1.ClearSelection();
            comboBox1.Text = "Tout";
            //comboBox2.SelectedText = "";
            //comboBox1.SelectedText ="Tout";




            if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();

            Program.sql_cmd.CommandText = string.Format("select  * from vehicules ");
            Program.db = Program.sql_cmd.ExecuteReader();
            DataTable dts = new DataTable();
            dts.Load(Program.db);
            comboBox2.DataSource = dts;
            comboBox2.ValueMember = "id";
            comboBox2.DisplayMember = "VEHICULE";

            Program.sql_con.Close();
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox1.SelectedIndex = -1;

            Montant_sum();


        }

        private void dataGridView1_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow.Selected == false)
            {
                this.Alert("sélectionnez la ligne à mettre à jour", Form_Alert.enmType.Warning);
            }
            else
            {
                if (dataGridView1.SelectedCells.Count > 0)
                {
                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    string numero = Convert.ToString(selectedRow.Cells[0].Value);
                    DateTime date = Convert.ToDateTime(selectedRow.Cells[1].Value.ToString());
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
                    Montant_sum();
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
            try
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
            worksheet = workbook.Sheets[1];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "ordre de mission";
                // storing header part in Excel 
                
                worksheet.Columns.ColumnWidth = 21;
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
                        worksheet.Cells[i + 2, j + 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Lavender);
                    }
            }
                


                worksheet.Cells[dataGridView1.Rows.Count + 3, dataGridView1.Columns.Count - 2].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);
                worksheet.Cells[dataGridView1.Rows.Count + 3, dataGridView1.Columns.Count - 1].Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightSalmon);

                worksheet.Cells[dataGridView1.Rows.Count + 3, dataGridView1.Columns.Count-2 ] = "Total_Montant";
                worksheet.Cells[dataGridView1.Rows.Count+3, dataGridView1.Columns.Count-1] = textBox1.Text;
            // save the application  
            //workbook.SaveAs("c:\\output.xls", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();


            }
            catch (Exception ex)
{
    MessageBox.Show(ex.Message);
}
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





                    if (MessageBox.Show("Voulez-vous vraiment supprimer  ODM N° " + numero + " ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (Program.sql_con.State == ConnectionState.Closed) Program.sql_con.Open();

                        Program.sql_cmd.CommandText = string.Format("delete from ODM where n°ODM ='{0}' ", numero);
                        Program.sql_cmd.ExecuteNonQuery();




                        Program.sql_con.Close();




                        ///////////show data to grid  



                        dataGridView1.DataSource = null;
                        LoadData();
                        Montant_sum();

                        this.Alert("supprimer la facture Succès", Form_Alert.enmType.Info);

                    }
                }
            }
        }

        private void textBox7_KeyDown_2(object sender, KeyEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox1.SelectedIndex == 2)
            {
                textBox7.Visible = false;
                comboBox2.Visible = true;
                comboBox2.SelectedIndex = -1;
            }
            else if (comboBox1.SelectedIndex==-1)
            {
                textBox7.Visible = false;
                comboBox2.Visible = false;
            }

            else if (comboBox1.SelectedIndex == 0)
            {
                textBox7.Visible = false;
                comboBox2.Visible = false;
            }

            else
            {
                textBox7.Visible = true;
                comboBox2.Visible = false;
            }
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
          //  //Init print datagridview
          //  DGVPrinter printer = new DGVPrinter();
          //  printer.Title = "LISTE DES Ordre De Mission ";//Header
          //  //printer.SubTitle = string.Format("Date: {0}", DateTime.Now.Date.ToString("MM/dd/yyyy"));
          //  printer.SubTitle = string.Format("     ");
          //  printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
          //  printer.PageNumbers = true;
          //  printer.PageNumberInHeader = false;
          //  printer.PorportionalColumns = true;
          //  printer.HeaderCellAlignment = StringAlignment.Near;
          ////  printer.Footer = "FoxLearn";//Footer
          //  printer.FooterSpacing = 0;
          //  //Print landscape mode
          //  printer.printDocument.DefaultPageSettings.Landscape = true;
          //  printer.PrintDataGridView(dataGridView1);

        }

        private void Showallodm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void datecheck_CheckedChanged(object sender, EventArgs e)
        {
            if (datecheck.Checked)
            {
                date1.Visible = true;
                a.Visible = true;
                date2.Visible = true;
            }
            else
            {
                date1.Visible = false;
                a.Visible = false;
                date2.Visible = false;

            }
        }
    }
}
