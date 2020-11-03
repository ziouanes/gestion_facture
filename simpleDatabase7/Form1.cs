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
            //con.Close();
            //con.Open();
            //DataTable dt = new DataTable();
            //adapt = new SQLiteDataAdapter("select * from [Table]", con);
            //adapt.Fill(dt);
            //dataGridView1.DataSource = dt;
            //con.Close();
        }
        //Clear Data  
        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            ID = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

            Program.sql_con.Open();
            SQLiteCommand cmd = new SQLiteCommand("insert into [Table](name,code) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "')", Program.sql_con);

            cmd.Parameters.AddWithValue("@name", textBox1.Text);
            cmd.Parameters.AddWithValue("@code", textBox2.Text);

            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);

            cmd.ExecuteNonQuery();

            Program.sql_con.Close();
            MessageBox.Show("Inserted");
            DisplayData();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Program.sql_con.Open();
            SQLiteCommand cmd = new SQLiteCommand("select * from [facture] ", Program.sql_con);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            Program.sql_con.Close();
        }

       
    }
}
