using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;

namespace DataLayer.ConnectionStringManage
{
    public class ReadConnetionStringByText:IReadConnectString
    {

        public SqlConnectionStringBuilder ReadConnectionString(string path)
        {
            SqlConnectionStringBuilder connectionString = new SqlConnectionStringBuilder();
            if(File.Exists(path))
            {
                using(FileStream fs=new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.Read))
                {
                    using(StreamReader sw=new StreamReader(fs))
                    {
                        string line = string.Empty;
                        while((line=sw.ReadLine())!=null)
                        {
                            if(line.IndexOf("=")>0)
                            {
                                switch (line.Substring(0,line.IndexOf("=")).Trim().ToLower())
                                {
                                    case "server":
                                        connectionString.DataSource = line.Substring(line.IndexOf("=") + 1);
                                        break;
                                    case "database":
                                        connectionString.InitialCatalog = line.Substring(line.IndexOf("=") + 1);
                                        break;
                                    case "uid":
                                        connectionString.UserID = line.Substring(line.IndexOf("=") + 1);
                                        break;
                                    case "pwd":
                                        connectionString.Password = line.Substring(line.IndexOf("=") + 1);
                                        break;
                                    case "winnt":
                                        connectionString.IntegratedSecurity = Convert.ToBoolean(line.Substring(line.IndexOf("=") + 1));
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }
            return connectionString;
        }

        public void WriteConnectionString(string path, SqlConnectionStringBuilder connectionString)
        {
            using(FileStream fs=new FileStream(path,FileMode.OpenOrCreate,FileAccess.Write,FileShare.Write))
            {
                using(StreamWriter sw=new StreamWriter(fs))
                {
                    sw.WriteLine("server={0}", connectionString.DataSource);
                    sw.WriteLine("database={0}", connectionString.InitialCatalog);
                    sw.WriteLine("uid={0}", connectionString.UserID);
                    sw.WriteLine("pwd={0}", connectionString.Password);
                    sw.WriteLine("winnt={0}", connectionString.IntegratedSecurity);
                }
            }
        }
    }
}
