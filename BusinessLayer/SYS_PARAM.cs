using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    [Serializable]
    public class SYS_PARAM
    {
        //Entities db;
        //public SYS_PARAM()
        //{
        //    db = Entities.CreateEntities();
        //}
        //public tb_Param GetParam()
        //{
        //    //Chỉ lấy 1 cái không đưa được dưa thêm cái thứ 2 vì sẽ lấy đại cái nào đó mà mình không biết được 
        //    return db.tb_Param.FirstOrDefault();
        //}

        string _macty;
        public string macty
        {
            get { return _macty; }
            set { _macty = value; }
        }
        string _madvi;
        public string madvi
        {
            get { return _madvi; }
            set { _madvi = value; }
        }
        //Khi gọi tới class Param này thì sẽ tự khởi tạo cho mình luôn
        public SYS_PARAM (string macty, string madvi)
        {
            this._macty = macty;
            this._madvi = madvi;
        }
        public void SaveFile()
        {
            if (File.Exists("sysparam.ini"))
                File.Delete("sysparam.ini");
            FileStream fs = File.Open("sysparam.ini", FileMode.OpenOrCreate, FileAccess.Write);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, this);
            fs.Close();
        }
    }
}
