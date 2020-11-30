using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace simpleDatabase7
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new acceulle());
        }
        static string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static SQLiteConnection sql_con = new SQLiteConnection(@"Data Source=" + filePath + "/base_Donnée_ODM.db3");

        public static SQLiteCommand sql_cmd;
        public static SQLiteDataReader db;
    }
}
