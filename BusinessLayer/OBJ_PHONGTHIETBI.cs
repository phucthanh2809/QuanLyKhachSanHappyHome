using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;

namespace BusinessLayer
{
    public class OBJ_PHONGTHIETBI
    {
        public int IDPHONG { set; get; }
        public int IDTB { set; get; }
        public int? SOLUONG { set; get; }
        public string TENPHONG { set; get; }
         public string TENTHIETBI { set; get; }
    }
}
