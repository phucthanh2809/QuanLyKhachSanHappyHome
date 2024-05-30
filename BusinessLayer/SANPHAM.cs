using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class SANPHAM
    {
        Entities db;

        public SANPHAM()
        {
            db = Entities.CreateEntities();
        }
        public tb_SanPham getItem(int idsp) //xây dựng phương thức 
        
        {
            //FirstOrDefaut: chỉ lấy ra 1 bảng duy nhất hoặc là lấy mặc định của nó
            return db.tb_SanPham.FirstOrDefault(x => x.IDSP == idsp);
        }
        public List<tb_SanPham> getAll()
        {
            return db.tb_SanPham.ToList();
        }
        //Khổi tạo lấy ra 1 danh sách List Congty, getAll là lấy ra hết luôn những cái đánh dấu xóa và những cái không đánh dấu xóa
        //Phương thức thêm đối tượng là spdv
        public void add(tb_SanPham sp) //đưa vào 1 đối tượng công ty
        {
            //try catch này nên dùng để có cơ chế quay lại khi ứng dụng thêm không được 
            try
            {
                db.tb_SanPham.Add(sp);
                //Thêm vào thì phải lưu nó lại 
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                //Nếu có lỗi xảy ra thì thông báo 
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu." + ex.Message);
            }
        }
        //update dữ liệu
        public void update(tb_SanPham sanpham)
        {
            tb_SanPham _sanpham = db.tb_SanPham.FirstOrDefault(x => x.IDSP == sanpham.IDSP);
            _sanpham.IDSP = sanpham.IDSP;
            _sanpham.TENSP = sanpham.TENSP;
            _sanpham.DONGIA = sanpham.DONGIA;
            _sanpham.DISABLED = sanpham.DISABLED;
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
        //delete dữ liệu 
        public void delete(int masanpham)
        {
            //tìm và lôi default nó ra sau đó set DISABLED = true 
            tb_SanPham sp = db.tb_SanPham.FirstOrDefault(x => x.IDSP == masanpham);
            //Nêu dữ liệu mới thì có thể remove được, còn nếu cũ đã phát sinh các chứng từ thì remove không được mà chỉ có thể nên đánh dấu nó là không dùng nữa để sau này có chứng từ liên quan còn lôi nó ra được 
            sp.DISABLED = true;
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
    }

}
