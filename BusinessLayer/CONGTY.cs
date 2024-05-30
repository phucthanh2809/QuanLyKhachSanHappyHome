using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using BusinessLayer;

namespace BusinessLayer
{
    public class CONGTY
    {
        Entities db;
        //Khởi tạo contructor công ty
        public CONGTY() 
        {
            db = Entities.CreateEntities();
        }
        //Tại sao là String macty vì khai báo macty là nvchar nên là String 
        //xây dựng phương thức 
        public tb_CongTy getItem(string macty) //xây dựng phương thức 5
        {
            //FirstOrDefaut: chỉ lấy ra 1 bảng duy nhất hoặc là lấy mặc định của nó
            return db.tb_CongTy.FirstOrDefault(x => x.MACTY == macty);
        }
        //Khổi tạo lấy ra 1 danh sách List Congty, getAll là lấy ra hết luôn những cái đánh dấu xóa và những cái không đánh dấu xóa
        public List<tb_CongTy>getAll()
        {
            //lấy ra hết tất cả trả về danh sách Congty
            return db.tb_CongTy.ToList();
        }
        //Phương thức thêm đối tượng là cty
        public void add(tb_CongTy cty)
        {
            try
            {
                db.tb_CongTy.Add(cty);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu. " + ex.Message);
            }
        }
        //update dữ liệu
        public void update(tb_CongTy cty)
        {
            //Khi muốn sửa cái Default này thì phải lấy cái Default đó ra sau đó mới sửa nó 
            tb_CongTy _cty = db.tb_CongTy.FirstOrDefault(x => x.MACTY == cty.MACTY);
            //Sau khi lấy được thì tiến hành sửa nó 
            _cty.TENCTY = cty.TENCTY;
            _cty.DIENTHOAI = cty.DIENTHOAI;
            _cty.FAX = cty.FAX;
            _cty.EMAIL = cty.EMAIL;
            _cty.DIACHI = cty.DIACHI;
            _cty.DISABLED = cty.DISABLED;
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
        public void delete(string macty)
        {
            //tìm và lôi default nó ra sau đó set DISABLED = true 
            tb_CongTy cty = db.tb_CongTy.FirstOrDefault(x => x.MACTY == macty);
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
