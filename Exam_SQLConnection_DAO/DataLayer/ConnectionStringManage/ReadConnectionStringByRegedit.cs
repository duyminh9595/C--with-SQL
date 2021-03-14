using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Win32;
namespace DataLayer.ConnectionStringManage
{
    public class ReadConnectionStringByRegedit:IReadConnectString
    {
        public System.Data.SqlClient.SqlConnectionStringBuilder ReadConnectionString(string path)
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ReadRegedit",true);
            connectionString.DataSource = key.GetValue("server").ToString();
            connectionString.InitialCatalog = key.GetValue("database").ToString();
            connectionString.UserID = key.GetValue("uid").ToString();
            connectionString.Password = key.GetValue("pwd").ToString();
            connectionString.IntegratedSecurity = Convert.ToBoolean(key.GetValue("winnt").ToString());
            key.Close();
            return connectionString;
        }

        public void WriteConnectionString(string path, System.Data.SqlClient.SqlConnectionStringBuilder connectionString)
        {
            //RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\ReadRegedit"); 
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\ReadRegedit",true);
            key.SetValue("server", connectionString.DataSource);
            key.SetValue("database", connectionString.InitialCatalog);
            key.SetValue("uid", connectionString.UserID);
            key.SetValue("pwd", connectionString.Password);
            key.SetValue("winnt", connectionString.IntegratedSecurity);
            key.Close();
        }
    }
}
