using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class SYS_FUNC
    {
        Entities db; //Tạo ra biến entities

        public SYS_FUNC()
        {
            db = Entities.CreateEntities();
        }
        //Lấy ra các menu là Group (Menu cha)
        public List<tb_SYS_FUNC>getParent()
        {
            return db.tb_SYS_FUNC.Where(x => x.ISGROUP == true && x.MENU == true).OrderBy(s => s.SORT).ToList();
        }
        public List<tb_SYS_FUNC>getChild(string parent)
        {
            return db.tb_SYS_FUNC.Where(x => x.ISGROUP == false && x.MENU == true && x.PARENT == parent).OrderBy(s => s.SORT).ToList();
        }
    }
}

