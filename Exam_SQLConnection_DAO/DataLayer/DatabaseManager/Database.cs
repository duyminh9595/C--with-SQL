using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataLayer.ConnectionStringManage;

namespace DataLayer.DatabaseManager
{
    public class Database
    {
        SqlConnection cnn;
        ReadConnectionStringFactory readConnect;
        public SqlConnectionStringBuilder connectionStringBuilder;
        public FileConnectType fileType;

        public Database(string[]path,FileConnectType fileType)
        {
            connectionStringBuilder=new SqlConnectionStringBuilder();
            readConnect=new ReadConnectionStringFactory();
            readConnect.CreateReadConnectionString(fileType);
            connectionStringBuilder=readConnect.ReadConnectionString.ReadConnectionString(path[(int)fileType]);
            this.fileType = fileType;
            cnn = new SqlConnection();
            cnn.ConnectionString = connectionStringBuilder.ToString();
        }
        public void WriteConnectionString(string[]path,SqlConnectionStringBuilder connectionString)
        {
            readConnect.ReadConnectionString.WriteConnectionString(path[(int)fileType], connectionString);
        }
        public bool KiemTraKetNoi(ref string err)
        {
            try
            {
                cnn.Open();
                return true;
            }
            catch (Exception ex)
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
