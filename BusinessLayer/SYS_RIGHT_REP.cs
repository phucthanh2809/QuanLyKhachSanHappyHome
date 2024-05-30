using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class SYS_RIGHT_REP
    {
        Entities db;
        public SYS_RIGHT_REP()
        {
            db = Entities.CreateEntities();
        }
        public List<tb_SYS_RIGHT_REP> getListByUser(int idUser)
        {
            //sẽ lấy ra quyền liên quan đến group và user 
            SYS_GROUP sGroup = new SYS_GROUP();
            //từ iduser truyền vào sẽ lấy ra group
            var group = sGroup.getGroupByMemBer(idUser);
            //Nếu id lấy ra không nằm trong group nào thì sẽ lấy trong cái bảng userRight này ra cái user được phân quyền là true thôi - Nghĩa là nếu không phải nhóm nào thì chỉ lấy quyền user thôi 
            if (group==null)
            {
                return db.tb_SYS_RIGHT_REP.Where(x => x.IDUSER == idUser && x.USER_RIGHT == true).ToList();
            }    
            //Nếu là nhóm thì phải có quyền của nhóm và quyền của user
            else
            {
                //lấy quyền group 
                List<tb_SYS_RIGHT_REP> lstByGroup = db.tb_SYS_RIGHT_REP.Where(x => x.IDUSER == group.GROUP && x.USER_RIGHT == true).ToList();
                //và lấy quyền iduser 
                List<tb_SYS_RIGHT_REP> lstByUser = db.tb_SYS_RIGHT_REP.Where(x => x.IDUSER == idUser && x.USER_RIGHT == true).ToList();
                //gộp group và user lại : concat (hợp lại)
                List<tb_SYS_RIGHT_REP> lstAll = lstByUser.Concat(lstByGroup).ToList();
                //tổng hợp lại trả về cả quyền của nhóm và user
                return lstAll;
            }
        }
        public void update(int idUser, int rep_code, bool right)
        {
            tb_SYS_RIGHT_REP sRigh = db.tb_SYS_RIGHT_REP.FirstOrDefault(x => x.IDUSER == idUser && x.REP_CODE == rep_code);
            try
            {
                sRigh.USER_RIGHT = right;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Lỗi: " +  ex.Message);
            }
        }
    }
}
