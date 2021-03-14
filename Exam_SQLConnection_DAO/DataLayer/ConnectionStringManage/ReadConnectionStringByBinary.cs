using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace DataLayer.ConnectionStringManage
{
    class ReadConnectionStringByBinary:IReadConnectString
    {
        BinaryWriter bw;
        BinaryReader br;
        public System.Data.SqlClient.SqlConnectionStringBuilder ReadConnectionString(string path)
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            br = new BinaryReader(new FileStream(path, FileMode.Open));
            connectionString.DataSource = br.ReadString();
            connectionString.InitialCatalog = br.ReadString();
            connectionString.UserID = br.ReadString();
            connectionString.Password = br.ReadString();
            connectionString.IntegratedSecurity = br.ReadBoolean();
            br.Close();
            return connectionString;
        }

        public void WriteConnectionString(string path, System.Data.SqlClient.SqlConnectionStringBuilder connectionString)
        {
            bw = new BinaryWriter(new FileStream(path, FileMode.OpenOrCreate));
            bw.Write(connectionString.DataSource);
            bw.Write(connectionString.InitialCatalog);
            bw.Write(connectionString.UserID);
            bw.Write(connectionString.Password);
            bw.Write(connectionString.IntegratedSecurity);
            bw.Close();

        }
    }
}
