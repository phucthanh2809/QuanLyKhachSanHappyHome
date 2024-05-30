using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class DATPHONG_CHITIET
    {
        Entities db;
       
        public DATPHONG_CHITIET()
        {
            db = Entities.CreateEntities();
        }
        public tb_DatPhong_CT getItem(int _idDPCT)
        {
            //FirstOrDefaut: chỉ lấy ra 1 bảng duy nhất hoặc là lấy mặc định của nó
            return db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDPCT == _idDPCT);
        }
        public tb_DatPhong_CT getItem(int idDP, int idPhong)
        {
            //FirstOrDefaut: chỉ lấy ra 1 bảng duy nhất hoặc là lấy mặc định của nó
            return db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDP == idDP && x.IDPHONG == idPhong);
        }
        public List<tb_DatPhong_CT> getAllByDatPhong(int _idDP)
       {
           return db.tb_DatPhong_CT.Where(x => x.IDDP == _idDP).ToList();
       }
        public tb_DatPhong_CT getIDDPByPhong(int idPhong)
        {
            //OrderByDescending: sắp xếp giảm dần
            //Đầu tiền sắp xếp giảm dần theo ngày sau đó lấy 1 cái mới nhất của id Phòng ra 
            return db.tb_DatPhong_CT.OrderByDescending(x => x.NGAY).FirstOrDefault(x => x.IDPHONG == idPhong);
        }
        public tb_DatPhong_CT add(tb_DatPhong_CT _dpct)
        {
            try
            {
                db.tb_DatPhong_CT.Add(_dpct);
                db.SaveChanges();
                return _dpct;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message); ;
            }
        }
        public void update(tb_DatPhong_CT _dpct)
        {
            tb_DatPhong_CT dpct = db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDPCT == _dpct.IDDPCT);
            dpct.IDDP = _dpct.IDDP;
            dpct.IDPHONG = _dpct.IDPHONG;
            dpct.IDDPCT = _dpct.IDDPCT;
            dpct.NGAY = _dpct.NGAY;
            dpct.DONGIA = _dpct.DONGIA;
            dpct.SONGAYO = _dpct.SONGAYO;
            dpct.THANHTIEN = _dpct.THANHTIEN;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                 throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu nè!" + ex.Message);
            }
        }
        public void delete(int _idDP, int _idPhong)
        {
            tb_DatPhong_CT dpct = db.tb_DatPhong_CT.FirstOrDefault(x => x.IDDP == _idDP && x.IDPHONG == _idPhong);
            try
            {
                db.tb_DatPhong_CT.Remove(dpct);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("Hey u! Có lỗi xảy ra trong quá trình xử lý dữ liệu nè!"+ex.Message);
            }
        }

        public void deleteAll(int idDP)
        {
            List<tb_DatPhong_CT> lstDPCT = db.tb_DatPhong_CT.Where(x => x.IDDP == idDP).ToList();
            try 
            {
                db.tb_DatPhong_CT.RemoveRange(lstDPCT);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Hey u! Có lỗi xảy ra trong quá trình xử lí dữ liệu nè" + ex.Message);
            }
        }
    }
}
