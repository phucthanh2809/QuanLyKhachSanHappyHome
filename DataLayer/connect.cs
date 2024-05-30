using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

//File kết nối để đọc và truyền các thông tin CSDL vào các Entity 
namespace DataLayer
{
    [Serializable]
    public class connect
    {
        public string servername;

        public string Servername
        {
            get { return servername; }
            set { servername = value; }
        }

        public string username;

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string passwd;

        public string Passwd
        {
            get { return passwd; }
            set { passwd = value; }
        }

        public string database;

        public string Database
        {
            get { return database; }
            set { database = value; }
        }

        public connect(string _servername, string _username, string _passwd, string _database)
        {
            this.servername = _servername;
            this.username = _username;
            this.passwd = _passwd;
            this.database = _database;
        }

        public void SaveFile()
        {
            //Using các thư viện cần thiết
            if (File.Exists("connectdb1.dba"))
                File.Delete("connectdb1.dba");
            FileStream fs = File.Open("connectdb1.dba", FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, this);
            fs.Close();
        }
    }
}
