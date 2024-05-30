using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class TRANGTHAI
    {
        //Khai báo biến 
        public bool _value { set; get; }
        public string _display { set; get; }
        //Khai báo khởi tạo
        public TRANGTHAI()
        {

        }
        //Khai báo khởi tạo giá trị 
        public TRANGTHAI(bool _val,string _dis)
        {
            this._value = _val;
            this._display = _dis;
        }
        //Hàm lấy trạng thái ra
        public static List<TRANGTHAI> getList()
        {
            List<TRANGTHAI> lst = new List<TRANGTHAI>();
            TRANGTHAI[] collect = new TRANGTHAI[2]
            {
                new TRANGTHAI(false, "Chưa hoàn tất"),
                new TRANGTHAI(true, "Đã hoàn tất")
            };
            //Phương thức AddRange () thêm tất cả các phần tử từ bộ sưu tập được chỉ định vào ArrayList (List<TRANGTHAI>)
            lst.AddRange(collect);
            return lst;
        }
    }
}
