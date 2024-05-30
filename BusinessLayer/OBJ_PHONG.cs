using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class OBJ_PHONG
    {
        public string TENLOAIPHONG { set; get; }
        public string TENPHONG { set; get; }
        public string TENTANG { set; get; }
        public double DONGIA { set; get; }
        public int IDPHONG { get; set; }
        public bool ?STATUS { set; get; }
        public bool ?DISABLED { set; get; }

        public int IDTANG { get; set; }
        public int IDLOAIPHONG { get; set; }


    }
}
