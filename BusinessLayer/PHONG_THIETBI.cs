using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class PHONG_THIETBI
    {
        Entities db;

        public PHONG_THIETBI()
        {
            db = Entities.CreateEntities();
        }
        public tb_Phong_ThietBi1 getItem(int idphong, int idTB) //xây dựng phương thức 
        {
            //FirstOrDefaut: chỉ lấy ra 1 bảng duy nhất hoặc là lấy mặc định của nó
            return db.tb_Phong_ThietBi1.FirstOrDefault(x => x.IDPHONG == idphong && x.IDTB == idTB);
        }

        //Khổi tạo lấy ra 1 danh sách , getAll là lấy ra hết luôn những cái đánh dấu xóa và những cái không đánh dấu xóa
        public List<OBJ_PHONGTHIETBI> getList(int idPhong)
        {
            List<tb_Phong_ThietBi1> lstPTB = db.tb_Phong_ThietBi1.Where(x => x.IDPHONG == idPhong).ToList();
            List<OBJ_PHONGTHIETBI> lstPhongTB = new List<OBJ_PHONGTHIETBI>();
            OBJ_PHONGTHIETBI _ptb;
            foreach (var item in lstPTB)
            {
                _ptb = new OBJ_PHONGTHIETBI();
                _ptb.IDPHONG = item.IDPHONG;
                _ptb.IDTB = item.IDTB;
                _ptb.SOLUONG = item.SOLUONG;
                tb_Phong _p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == item.IDPHONG);
                if (_p != null)
                {
                    _ptb.TENPHONG = _p.TENPHONG;
                }
                tb_ThietBi _t = db.tb_ThietBi.FirstOrDefault(x => x.IDTB == item.IDTB);
                if (_t != null)
                {
                    _ptb.TENTHIETBI = _t.TENTHIETBI;
                }
                lstPhongTB.Add(_ptb);
            }
            return lstPhongTB;
        }
        public List<OBJ_PHONGTHIETBI> getAll()
        {
            //List<tb_Phong> lstPhong = db.tb_Phong.ToList();
            //List<tb_ThietBi> lstTB = db.tb_ThietBi.ToList();
            List<tb_Phong_ThietBi1> lstPTB = db.tb_Phong_ThietBi1.ToList();
            List<OBJ_PHONGTHIETBI> lstPhongTB = new List<OBJ_PHONGTHIETBI>();
            OBJ_PHONGTHIETBI _ptb;
            foreach (var item in lstPTB)
            {
                _ptb = new OBJ_PHONGTHIETBI();
                _ptb.IDPHONG = item.IDPHONG;
                _ptb.IDTB = item.IDTB;
                _ptb.SOLUONG = item.SOLUONG;
                tb_Phong _p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == item.IDPHONG);
                if (_p != null)
                {
                    _ptb.TENPHONG = _p.TENPHONG;
                }
                tb_ThietBi _t = db.tb_ThietBi.FirstOrDefault(x => x.IDTB == item.IDTB);
                if (_t != null)
                {
                    _ptb.TENTHIETBI = _t.TENTHIETBI;
                }
                lstPhongTB.Add(_ptb);
            }
            return lstPhongTB;
        }

        public void add(tb_Phong_ThietBi1 ptb)
        {
            try
            {
                db.tb_Phong_ThietBi1.Add(ptb);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Có lỗi xảy ra trong quá trình xử lí dữ liệu" + ex.Message);
            }
        }
        public void update (tb_Phong_ThietBi1 _ptb)
        {
            tb_Phong_ThietBi1 ptb = db.tb_Phong_ThietBi1.FirstOrDefault(x => x.IDPHONG == _ptb.IDPHONG && x.IDTB == _ptb.IDTB);
            
            //fix 
            ptb.IDTB = _ptb.IDTB;
            ptb.SOLUONG = _ptb.SOLUONG;
            db.SaveChanges();
        }

        public void delete(int idTB, int idPhong)
        {
            tb_Phong_ThietBi1 tb = db.tb_Phong_ThietBi1.FirstOrDefault(x => x.IDPHONG == idPhong && x.IDTB == idTB);
            try
            {
                db.tb_Phong_ThietBi1.Remove(tb);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xả ra trong quá trình xử lý dữ liệu. " + ex.Message); ;
            }

        }
    }
}