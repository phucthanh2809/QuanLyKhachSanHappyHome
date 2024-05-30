using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class KHACHHANG
    {
        Entities db;
        public KHACHHANG()
        {
            db = Entities.CreateEntities();
        }

        //FirstOrDefaut: chỉ lấy ra 1 bảng duy nhất hoặc là lấy mặc định của nó
        public tb_KhachHang getItem(int idkh)
        {
            return db.tb_KhachHang.FirstOrDefault(x => x.IDKH == idkh);
        }
        //Khổi tạo lấy ra 1 danh sách List Khách hàng, getAll là lấy ra hết luôn những cái đánh dấu xóa và những cái không đánh dấu xóa
        public List<tb_KhachHang> getAll()
        {
            //lấy ra hết tất cả trả về danh sách Khach hang
            return db.tb_KhachHang.ToList();
        }
        public void add(tb_KhachHang kh) //đưa vòa 1 đối tượng khách hàng 
        {
            try
            {
                db.tb_KhachHang.Add(kh);
                //Khi lưu xong sẽ bấm thay đổi 
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //Nếu có lỗi xảy ra thì sẽ hiện lên thông báo
                throw new Exception("Có lỗi xày ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void update(tb_KhachHang khachhang)
        {
            tb_KhachHang _khachhang = db.tb_KhachHang.FirstOrDefault(x => x.IDKH == khachhang.IDKH);
            //Sau khi lấy được bắt đầu tiến hành sửa nó 
            _khachhang.HOTEN = khachhang.HOTEN;
            _khachhang.CCCD = khachhang.CCCD;
            _khachhang.DIENTHOAI = khachhang.DIENTHOAI;
            _khachhang.EMAIL = khachhang.EMAIL;
            _khachhang.DIACHI = khachhang.DIACHI;
            _khachhang.DISABLED = khachhang.DISABLED;
            _khachhang.GIOITINH = khachhang.GIOITINH;
            _khachhang.IDKH = khachhang.IDKH;
            _khachhang.CREATED_DATE = khachhang.CREATED_DATE;
            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lí dữ liệu" + ex.Message);   
            }
        }
        public void delete(int makhachhang)
        {
            tb_KhachHang kh = db.tb_KhachHang.FirstOrDefault(x => x.IDKH == makhachhang);
            kh.DISABLED = true;
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

