using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
namespace BusinessLayer
{
    public class PHONG
    {
        Entities db;
        public PHONG()
        {
            db = Entities.CreateEntities();
        }
        public tb_Phong getItem(int idphong)
        {
            return db.tb_Phong.FirstOrDefault(x => x.IDPHONG == idphong);
        }
        public List<tb_Phong> getAll()
        {
            return db.tb_Phong.ToList();
        }
        public List<tb_Phong>getByTang(int idTang)
        {
            return db.tb_Phong.Where(x => x.IDTANG == idTang).ToList(); 
        }
        public List<tb_Phong> getList()
        {
            return db.tb_Phong.ToList();
        }
        //Khai báo hàm getItemFull để có thể lấy dữu liệu show ra được tên Phòng và giá Phòng 
        public OBJ_PHONG getItemFull(int id)
        {
            var _p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == id);
            OBJ_PHONG phong = new OBJ_PHONG();
            phong.IDPHONG = _p.IDPHONG;
            phong.TENPHONG = _p.TENPHONG;
            phong.STATUS = bool.Parse(_p.STATUS.ToString());
            phong.DISABLED = _p.DISABLED;
            phong.IDLOAIPHONG = _p.IDLOAIPHONG;
            phong.IDTANG = _p.IDTANG;
            var tang = db.tb_Tang.FirstOrDefault(t => t.IDTANG == _p.IDTANG);
            phong.TENTANG = tang.TENTANG;
            var lp = db.tb_LoaiPhong.FirstOrDefault(l => l.IDLOAIPHONG == _p.IDLOAIPHONG);
            phong.TENLOAIPHONG = lp.TENLOAIPHONG;
            phong.DONGIA = double.Parse(lp.DONGIA.ToString());
            return phong;
        }
        public List<OBJ_PHONG> getListFull()
        {
            var lsPhong = db.tb_Phong.ToList();
            List<OBJ_PHONG> lsPhongOBJ = new List<OBJ_PHONG>();
            OBJ_PHONG phong;
            foreach (var _p in lsPhong)
            {
                phong = new OBJ_PHONG();
                phong.IDPHONG = _p.IDPHONG;
                phong.TENPHONG = _p.TENPHONG;
                phong.STATUS = bool.Parse(_p.STATUS.ToString());
                phong.DISABLED = bool.Parse(_p.DISABLED.ToString());
                phong.IDLOAIPHONG = _p.IDLOAIPHONG;
                phong.IDTANG = _p.IDTANG;
                var tang = db.tb_Tang.FirstOrDefault(t => t.IDTANG == _p.IDTANG);
                phong.TENTANG = tang.TENTANG;
                var lp = db.tb_LoaiPhong.FirstOrDefault(l => l.IDLOAIPHONG == _p.IDLOAIPHONG);
                phong.TENLOAIPHONG = lp.TENLOAIPHONG;
                phong.DONGIA = double.Parse(lp.DONGIA.ToString());

                lsPhongOBJ.Add(phong);
            }
            return lsPhongOBJ;
        }

        public List<OBJ_PHONG> getPhongTrongFull()
        {
            var lsPhong = db.tb_Phong.Where(x => x.STATUS == false).ToList();
            List<OBJ_PHONG> lsPhongOBJ = new List<OBJ_PHONG>();
            OBJ_PHONG phong;
            foreach (var _p in lsPhong)
            {
                phong = new OBJ_PHONG();
                phong.IDPHONG = _p.IDPHONG;
                phong.TENPHONG = _p.TENPHONG;
                phong.STATUS = bool.Parse(_p.STATUS.ToString());
                phong.DISABLED = _p.DISABLED;
                phong.IDLOAIPHONG = _p.IDLOAIPHONG;
                phong.IDTANG = _p.IDTANG;
                var tang = db.tb_Tang.FirstOrDefault(t => t.IDTANG == _p.IDTANG);
                phong.TENTANG = tang.TENTANG;
                var lp = db.tb_LoaiPhong.FirstOrDefault(l => l.IDLOAIPHONG == _p.IDLOAIPHONG);
                phong.TENLOAIPHONG = lp.TENLOAIPHONG;
                phong.DONGIA = double.Parse(lp.DONGIA.ToString());

                lsPhongOBJ.Add(phong);
            }
            return lsPhongOBJ;
        }    
            public List<tb_Phong> getListByTang(int _idTang)
        {
            return db.tb_Phong.Where(t => t.IDTANG == _idTang).ToList();
        }
        public List<tb_Phong> getPhongTrong(int idTang)
        {
            return db.tb_Phong.Where(x => x.STATUS == false && x.DISABLED == false && x.IDTANG == idTang).ToList();
        }
        public void add(tb_Phong _phong)
        {
            tb_Phong p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == _phong.IDPHONG);
            p.STATUS = _phong.STATUS;
            p.IDLOAIPHONG = _phong.IDLOAIPHONG;
            p.IDTANG = _phong.IDTANG;
            p.TENPHONG = _phong.TENPHONG;
            p.DISABLED = _phong.DISABLED;
            db.tb_Phong.Add(p);
            db.SaveChanges();
        }

        public void update(tb_Phong _phong)
        {
            tb_Phong p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == _phong.IDPHONG);
            p.STATUS = _phong.STATUS;
            p.IDLOAIPHONG = _phong.IDLOAIPHONG;
            p.IDTANG = _phong.IDTANG;
            p.TENPHONG = _phong.TENPHONG;
            p.DISABLED = _phong.DISABLED;
            db.SaveChanges();
        }

        public void updateStatus(int id, bool status)
        {
            tb_Phong p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == id);
            p.STATUS = status;
            db.SaveChanges();
        }

        public void delete(int id)
        {
            tb_Phong p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == id);
            try
            {
                db.tb_Phong.Remove(p);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xả ra trong quá trình xử lý dữ liệu. " + ex.Message); ;
            }

        }
        //gọi hàm checkEmpty để kiểm tra tình trạng phòng có trống không hay đã đặt
        public bool checkEmpty(int idPhong)
        {
            var p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == idPhong);
            if (p.STATUS == true)
                return true;
            else
                return false;
        }
        public List<OBJ_PHONG> getList(int idPhong)
        {
            List<tb_Phong> lstPhong = db.tb_Phong.Where(x => x.IDPHONG == idPhong).ToList();
            List<OBJ_PHONG> lstOBJPhong = new List<OBJ_PHONG>();
            OBJ_PHONG _phong;
            foreach (var item in lstPhong)
            {
                _phong = new OBJ_PHONG();
                _phong.IDPHONG = item.IDPHONG;
                _phong.IDLOAIPHONG = item.IDLOAIPHONG;
                _phong.IDTANG = item.IDTANG;
                _phong.STATUS = item.STATUS;
                tb_Phong _p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == item.IDPHONG);
                if (_p != null)
                {
                    _phong.TENPHONG = _p.TENPHONG;
                }
                tb_Tang _t = db.tb_Tang.FirstOrDefault(x => x.IDTANG == item.IDTANG);
                if (_t != null)
                {
                    _phong.TENTANG = _t.TENTANG;
                }
                tb_LoaiPhong _lp = db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == item.IDLOAIPHONG);
                if (_p != null)
                {
                    _phong.TENLOAIPHONG = _lp.TENLOAIPHONG;
                }

                lstOBJPhong.Add(_phong);
            }
            return lstOBJPhong;
        }
        public List<OBJ_PHONG> getAllOBJPhong()
        {
            List<tb_Phong> lstPhong = db.tb_Phong.ToList();
            List<tb_LoaiPhong> lstLP = db.tb_LoaiPhong.ToList();
            List<tb_Tang> lstTang = db.tb_Tang.ToList();
            List<OBJ_PHONG> lstOBJPhong = new List<OBJ_PHONG>();
            OBJ_PHONG _phong;
            foreach (var item in lstPhong)
            {
                _phong = new OBJ_PHONG();
                _phong.IDPHONG = item.IDPHONG;
                _phong.IDLOAIPHONG = item.IDLOAIPHONG;
                _phong.IDTANG = item.IDTANG;
                _phong.STATUS = item.STATUS;
                tb_Phong _p = db.tb_Phong.FirstOrDefault(x => x.IDPHONG == item.IDPHONG);
                if (_p != null)
                {
                    _phong.TENPHONG = _p.TENPHONG;
                }
                tb_Tang _t = db.tb_Tang.FirstOrDefault(x => x.IDTANG == item.IDTANG);
                if (_t != null)
                {
                    _phong.TENTANG = _t.TENTANG;
                }
                tb_LoaiPhong _lp = db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == item.IDLOAIPHONG);
                if (_lp != null)
                {
                    _phong.TENLOAIPHONG = _lp.TENLOAIPHONG;
                }

                lstOBJPhong.Add(_phong);
            }
            return lstOBJPhong;
        }
    }
}
