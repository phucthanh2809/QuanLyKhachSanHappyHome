using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class OBJ_DATPHONG
    {
        //public int ID { set; get; }
        //public string TENPHONG { set; get; }
        //public double DONGIA { set; get; }
        //public bool STATUS { set; get; }
        //public int IDTANG { set; get; }
        //public int IDLOAIPHONG { set; get; }
        //public bool DISABLED { set; get; }
        //public string TENTANG { set; get; }
        //public string TENLOAIPHONG { set; get; }

        public int IDDP { set; get; }
        public int IDKH { set; get; }
        public string HOTEN { set; get; }
        public DateTime? NGAYDATPHONG { set; get; }
        public DateTime? NGAYTRAPHONG { set; get; }
        public double? SOTIEN { set; get; }
        public int? SONGUOIO { set; get; }
        public int IDUSER { set; get; } 
        public string MACTY { set; get; }
        public string MADVI { set; get; }
        public bool? STATUS { set; get; }
        public bool? THEODOAN { set; get; }
        public bool? DISABLED { set; get; }
        public string GHICHU { set; get; }
    }
}
