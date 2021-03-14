using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataLayer
{
    public class Database
    {
        private SqlConnection cnn;
        private string connectString = string.Empty;
        ReadConnectionString readcconnect;
        public string ConnectString
        {
            get { return connectString; }
            set { connectString = value; }
        }
        public Database(string path)
        {
            readcconnect = new ReadConnectionString();
            cnn = new SqlConnection();
            cnn.ConnectionString = readcconnect.DocChuoiKetNoiTuFile(path);
            connectString = cnn.ConnectionString;
        }
        public bool KiemTraKetNoi(ref string err)
        {
            try
            {
                cnn.Open();
                return true;
            }
            catch(Exception ex)
            {
                err = ex.Message;
                return false;
            }
            finally
            {
                cnn.Close();
            }
        }
    }
}
