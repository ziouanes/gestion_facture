using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Globalization;

namespace simpleDatabase7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //static string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //public static SQLiteConnection con = new SQLiteConnection(@"Data Source=" + filePath + "/sqliteDB.db3");

        int ID = 0;
        //public static SQLiteConnection con = new SQLiteConnection("Data Source=C:\\Users\\android-00\\Desktop\\simpleDatabase8 SQLite - Copy\\simpleDatabase7\\bin\\Debug\\sqliteDB.db3");

       // public static SQLiteConnection con = new SQLiteConnection("Data Source= C:\\Users\\MyUser\\simpleDatabase8 SQLite\\simpleDatabase7\\bin\\Debug\\sqliteDB.db3");
      

        private void DisplayData()
        {
            Program.sql_con.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * from [Table] order by date1 desc ", Program.sql_con);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;


















            Program.sql_con.Close();
        }
        //Clear Data  
        private void ClearData()
        {
            //textBox1.Text = "";
            //textBox2.Text = "";
            //ID = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            //textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            //textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            Program.sql_con.Open();
            SQLiteCommand cmd = new SQLiteCommand("insert into [Table](name,date1) VALUES ('" + textBox1.Text + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "')", Program.sql_con);

            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            cmd.Parameters.AddWithValue("@code", dateTimePicker1.Value.ToString("yyyy-MM-dd"));

            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);

            cmd.ExecuteNonQuery();

            Program.sql_con.Close();
            MessageBox.Show("Inserted");
            DisplayData();


        }


        private void Form1_Load(object sender, EventArgs e)
        {
            // CultureInfo culture = new CultureInfo("fr-ca");
            DisplayData();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Black, 6), this.DisplayRectangle);
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {


                int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                string numero = Convert.ToString(selectedRow.Cells[1].Value);
                
                DateTime date = Convert.ToDateTime(selectedRow.Cells[2].Value.ToString());
            textBox1.Text = numero;
            dateTimePicker1.Value = date;
            }



        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
