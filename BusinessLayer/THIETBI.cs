using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
namespace BusinessLayer
{
    public class THIETBI
    {
        Entities db;
        public THIETBI()
        {
            db = Entities.CreateEntities();
        }
        //Khổi tạo lấy ra 1 danh sách List Congty, getAll là lấy ra hết luôn những cái đánh dấu xóa và những cái không đánh dấu xóa
        public tb_ThietBi getItem(int idtb) //xây dựng phương thức 5
        {
            //FirstOrDefaut: chỉ lấy ra 1 bảng duy nhất hoặc là lấy mặc định của nó
            return db.tb_ThietBi.FirstOrDefault(x => x.IDTB == idtb);
        }
        //Khổi tạo lấy ra 1 danh sách List Congty, getAll là lấy ra hết luôn những cái đánh dấu xóa và những cái không đánh dấu xóa
        public List<tb_ThietBi> getAll()
        {
            //lấy ra hết tất cả trả về danh sách Congty
            return db.tb_ThietBi.ToList();
        }
        //Phương thức thêm đối tượng là cty
        public void add(tb_ThietBi cty) //đưa vào 1 đối tượng công ty
        {
            //try catch này nên dùng để có cơ chế quay lại khi ứng dụng thêm không được 
            try
            {
                db.tb_ThietBi.Add(cty);
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
        public void update(tb_ThietBi thietbi)
        {
            //Khi muốn sửa cái Default này thì phải lấy cái Default đó ra sau đó mới sửa nó 
            tb_ThietBi _thietbi = db.tb_ThietBi.FirstOrDefault(x => x.IDTB == thietbi.IDTB);
            //Sau khi lấy được thì tiến hành sửa nó 
            _thietbi.IDTB = thietbi.IDTB;
            _thietbi.DONGIA = thietbi.DONGIA;
            _thietbi.TENTHIETBI = thietbi.TENTHIETBI;
            _thietbi.DISABLED = thietbi.DISABLED;
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
        public void delete(int idtb)
        {
            //tìm và lôi default nó ra sau đó set DISABLED = true 
            tb_ThietBi cty = db.tb_ThietBi.FirstOrDefault(x => x.IDTB == idtb);
            //Nêu dữ liệu mới thì có thể remove được, còn nếu cũ đã phát sinh các chứng từ thì remove không được mà chỉ có thể nên đánh dấu nó là không dùng nữa để sau này có chứng từ liên quan còn lôi nó ra được 
            cty.DISABLED = true;
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

