using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class SYS_GROUP
    {
        Entities db;
        public SYS_GROUP()
        {
            db = Entities.CreateEntities();
        }   
        public tb_SYS_GROUP1 getGroupByMemBer (int idUser)
        {
            return db.tb_SYS_GROUP1.FirstOrDefault(x => x.MEMBER == idUser);
        }
        public void add(tb_SYS_GROUP1 gr)
        {
            try
            {
                db.tb_SYS_GROUP1.Add(gr);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lí dữ liệu" + ex.Message);
            }
        }
        public void delGroup(int idUser, int idGroup)
        {
            var gr = db.tb_SYS_GROUP1.FirstOrDefault(x => x.MEMBER == idUser && x.GROUP == idGroup);
            if (gr!=null)
            {
                try
                {
                    db.tb_SYS_GROUP1.Remove(gr);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Có lỗi xảy ra trong quá trình xử lí dữ liêju" + ex.Message);
                }
            }    
        }
    }
}
