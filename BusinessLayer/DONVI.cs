using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class DONVI
    {
        Entities db;
        public DONVI()
        {
            db = Entities.CreateEntities();
        }
        //Phương thức lấy dữ liệu từ table Donvi ra 
        public tb_DonVi getItem(string madvi)
        {
            return db.tb_DonVi.FirstOrDefault(x => x.MADVI == madvi);
        }
        public List<tb_DonVi> getAll()
        {
            return db.tb_DonVi.ToList();
        }
        public List<tb_DonVi> getAll(string macty)
        {
            return db.tb_DonVi.Where(x => x.MACTY == macty).ToList();
        } 
        public void add(tb_DonVi dvi)
        {
            try
            {
                db.tb_DonVi.Add(dvi);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " +ex.Message);
            }
        }
        public void update(tb_DonVi dvi)
        {
            tb_DonVi _dvi = db.tb_DonVi.FirstOrDefault(x => x.MADVI == dvi.MADVI);
            _dvi.MACTY = dvi.MACTY;
            _dvi.TENDVI = dvi.TENDVI;
            _dvi.DIENTHOAI = dvi.DIENTHOAI;
            _dvi.FAX = dvi.FAX;
            _dvi.EMAIL = dvi.EMAIL;
            _dvi.DIACHI = dvi.DIACHI;
            _dvi.DISABLED = dvi.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }
        public void delete(string madvi)
        {
            tb_DonVi _dvi = db.tb_DonVi.FirstOrDefault(x => x.MADVI == madvi);
            _dvi.DISABLED = true;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }
    }
}
