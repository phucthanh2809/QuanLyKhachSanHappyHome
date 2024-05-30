using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class SYS_REPORT
    {
        Entities db;
        public SYS_REPORT()
        {
            db = Entities.CreateEntities();
        } 
        public tb_SYS_REPORT getItem(int rep_code)
        {
            return db.tb_SYS_REPORT.FirstOrDefault(x => x.REP_CODE == rep_code);
        }
        public List<tb_SYS_REPORT> getList()
        {
            return db.tb_SYS_REPORT.ToList();
        }
        //chỉ lấy những báo cáo được phân thôi
        //truyền vô 1 cái list mà cái user này được phân quyền : 
        public List<tb_SYS_REPORT> getlistByRight(List<tb_SYS_RIGHT_REP> lst)
        {
            //truyền vô 1 cái list được phân quyền khi mà user login vào thì sẽ lấy ra được quyền được phân sau đó truyền nguyên list đó vào hàm getlistByRight 
            List<int> rep = lst.Select(ls => ls.REP_CODE).ToList();
            //chứa: contain
            //cái hàm getlistByRight sẽ lấy ra những cái rep_code có trong list đó để hiển thị lên form Báo cáo
            return db.tb_SYS_REPORT.Where(x => rep.Contains(x.REP_CODE)).OrderBy(x => x.REP_CODE).ToList(); 
        }
    }
}
