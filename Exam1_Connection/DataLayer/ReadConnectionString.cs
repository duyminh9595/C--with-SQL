using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ReadConnectionString
    {
        string serverName, databaseName, uid = string.Empty, pwd = string.Empty;
        bool winNT = false;
        string connectionString;

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        public bool WinNT
        {
            get { return winNT; }
            set { winNT = value; }
        }

        public string Pwd
        {
            get { return pwd; }
            set { pwd = value; }
        }

        public string Uid
        {
            get { return uid; }
            set { uid = value; }
        }

        public string DatabaseName
        {
            get { return databaseName; }
            set { databaseName = value; }
        }

        public string ServerName
        {
            get { return serverName; }
            set { serverName = value; }
        }

        public string DocChuoiKetNoiTuFile(string path)
        {
            using(FileStream fs=new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.Read))
            {
                using(StreamReader sw=new StreamReader(fs))
                {
                    string line=string.Empty;
                    while((line=sw.ReadLine())!=null)
                    {
                        if(line.IndexOf("=")>0)
                        {
                            switch (line.Substring(0,line.IndexOf(("=").Trim().ToLower())))
                            {
                                case "server":
                                    serverName=line.Substring(line.IndexOf("=")+1);
                                    break;
                                case "database":
                                    databaseName=line.Substring(line.IndexOf("=")+1);
                                    break;
                                case "uid":
                                    uid = line.Substring(line.IndexOf("=") + 1);
                                    break;
                                case "pwd":
                                    pwd = line.Substring(line.IndexOf("=") + 1);
                                    break;
                                case "winnt":
                                    winNT=Convert.ToBoolean(line.Substring(line.IndexOf("=")+1));
                                    break;
                            }
                        }
                    }
                }
            }
            return GhepChuoiKetNoi();
        }
        private string GhepChuoiKetNoi()
        {
            if(winNT)
            {
                connectionString=string.Format("server={0};database={1};integrated security=true",serverName,databaseName);
            }
            else
                connectionString = string.Format("server={0};database={1};uid={2};pwd={3}", serverName, databaseName,uid,pwd);
            return connectionString;
        }
        public void GhiChuoiKetNoiVaoFile(string path)
        {
            using(FileStream fs =new FileStream(path,FileMode.OpenOrCreate,FileAccess.Write,FileShare.Write))
            {
                using (StreamWriter sw=new StreamWriter(fs))
                {
                    sw.WriteLine(string.Format("server={0}", serverName));
                    sw.WriteLine(string.Format("database={0}", databaseName));
                    sw.WriteLine(string.Format("uid={0}", uid));
                    sw.WriteLine(string.Format("pwd={0}", pwd));
                    sw.WriteLine(string.Format("winnt={0}", winNT.ToString()));
                }
            }
        }
    }
}
