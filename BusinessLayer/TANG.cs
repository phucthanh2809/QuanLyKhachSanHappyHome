using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    //Đóng vai trò trung chuyển giữa tầng Giao diện và tầng Datalayer
    public class TANG
    {
        //Khai báo biến entities tên là db 
        Entities db;
        //constructor để khởi tạo nó lên 
        public TANG()
        {
            //Khởi tạo Entities lên thì mới có thể thao tác được với nó 
            db = Entities.CreateEntities();
        }
        //Phương thức để lấy dữ liệu hiên thị lên
        public List<tb_Tang> getAll()
        {
            return db.tb_Tang.ToList();
        }
        public tb_Tang getItem(int idTang)
        {
            return db.tb_Tang.FirstOrDefault(x => x.IDTANG == idTang);
        }
        public void add(tb_Tang tang)
        {
            try
            {
                db.tb_Tang.Add(tang);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Có lỗi xảy ra trong quá trình xử lý dữ liệu" + ex.Message);
            }
        }
        public void update(tb_Tang tang)
        {
            tb_Tang _tang = db.tb_Tang.FirstOrDefault(x => x.IDTANG == tang.IDTANG);
            _tang.IDTANG = tang.IDTANG;
            _tang.TENTANG = tang.TENTANG;
            _tang.DISABLED = tang.DISABLED;
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new Exception("Có lỗi xảy ra trong quá trình xử lí dữ liệu" + ex.Message);
            }
        }

        public void delete(int idTang)
        {
            //tìm và lôi default nó ra sau đó set DISABLED = true 
            tb_Tang tang = db.tb_Tang.FirstOrDefault(x => x.IDTANG == idTang);
            //Nêu dữ liệu mới thì có thể remove được, còn nếu cũ đã phát sinh các chứng từ thì remove không được mà chỉ có thể nên đánh dấu nó là không dùng nữa để sau này có chứng từ liên quan còn lôi nó ra được 
            tang.DISABLED = true;
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
