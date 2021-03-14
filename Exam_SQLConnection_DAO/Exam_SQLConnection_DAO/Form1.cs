using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.DatabaseManager;
using DataLayer.ConnectionStringManage;
using System.Data.SqlClient;

namespace Exam_SQLConnection_DAO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Database data;
        string err = string.Empty;
        string[] arrayPath = new string[]
        {
            Application.StartupPath+@"\Connect.ini",
            Application.StartupPath+@"\App.config","",Application.StartupPath+@"\Binary.bin"
        };
        private void Form1_Load(object sender, EventArgs e)
        {
            data = new Database(arrayPath, FileConnectType.REGEDIT);
            
                if(data.KiemTraKetNoi(ref err))
                {
                    MessageBox.Show("Thanh cong");
                }
                else
                    MessageBox.Show("Khong thanh cong");
            this.Text=data.connectionStringBuilder.ToString();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder cn = new SqlConnectionStringBuilder();
            cn.DataSource = @"DESKTOP-971TEAH";
            cn.InitialCatalog = "a";
            cn.IntegratedSecurity = true;
            cn.UserID = "sa";
            cn.Password = "123456";
            data.WriteConnectionString(arrayPath, cn);
        }
    }
}
