using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class LOAIPHONG
    {
        Entities db;
        public LOAIPHONG()
        {
            db = Entities.CreateEntities();
        }

        //FirstOrDefaut: chỉ lấy ra 1 bảng duy nhất hoặc là lấy mặc định của nó
        public tb_LoaiPhong getItem(int idlp)
        {
            return db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == idlp);
        }
        //Khổi tạo lấy ra 1 danh sách List Khách hàng, getAll là lấy ra hết luôn những cái đánh dấu xóa và những cái không đánh dấu xóa
        public List<tb_LoaiPhong> getAll()
        {
            //lấy ra hết tất cả trả về danh sách Khach hang
            return db.tb_LoaiPhong.ToList();
        }
        public void add(tb_LoaiPhong lp) //đưa vòa 1 đối tượng khách hàng 
        {
            try
            {
                db.tb_LoaiPhong.Add(lp);
                //Khi lưu xong sẽ bấm thay đổi 
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //Nếu có lỗi xảy ra thì sẽ hiện lên thông báo
                throw new Exception("Có lỗi xày ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void update(tb_LoaiPhong loaiphong)
        {
            tb_LoaiPhong _loaiphong = db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == loaiphong.IDLOAIPHONG);
            _loaiphong.IDLOAIPHONG = loaiphong.IDLOAIPHONG;
            _loaiphong.SOGIUONG = loaiphong.SOGIUONG;
            _loaiphong.SONGUOI = loaiphong.SONGUOI;
            _loaiphong.TENLOAIPHONG = loaiphong.TENLOAIPHONG;
            _loaiphong.DONGIA = loaiphong.DONGIA;
            _loaiphong.DISABLED = loaiphong.DISABLED;
            try
            {
                // Nếu exception (ngoại lệ) không lỗi thì sẽ lưu lại
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //Nếu exception(ngoại lệ) có lỗi thì sẽ không thóat ch/trình mà sẽ hiển thị thông báo lỗi lên 
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }

        }
        public void delete(int maloaiphong)
        {
            tb_LoaiPhong lp = db.tb_LoaiPhong.FirstOrDefault(x => x.IDLOAIPHONG == maloaiphong);
            lp.DISABLED = true;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình tiến hành dữ liệu" + ex.Message);
            }
        }
    }
}


