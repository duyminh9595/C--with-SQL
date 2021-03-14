using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml;

namespace DataLayer.ConnectionStringManage
{
    public class ReadConnectionStringByAppConfig:IReadConnectString
    {

        public System.Data.SqlClient.SqlConnectionStringBuilder ReadConnectionString(string path)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(path);
            string _connectionString = string.Empty;
            foreach(XmlDocument xElement in XmlDoc.DocumentElement)
            {
                if(xElement.Name=="connectionStrings")
                {
                    _connectionString = xElement.FirstChild.Attributes[1].Value;
                }
            }
            connectionStringBuilder.ConnectionString = _connectionString;
            return connectionStringBuilder;
        }

        public void WriteConnectionString(string path, System.Data.SqlClient.SqlConnectionStringBuilder connectionString)
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(path);
            foreach(XmlElement xElement in XmlDoc.DocumentElement)
            {
                if(xElement.Name=="connectionStrings")
                {
                    xElement.FirstChild.Attributes[1].Value = connectionString.ToString();
                }
            }
            XmlDoc.Save(path);
        }
    }
}
