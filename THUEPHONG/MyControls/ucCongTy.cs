using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;

namespace THUEPHONG.MyControls
{
    public partial class ucCongTy : UserControl
    {
        public ucCongTy()
        {
            InitializeComponent();
        }

        private void ucCongTy_Load(object sender, EventArgs e)
        {
            CONGTY _congty = new CONGTY();
            cboCongTy.DataSource = _congty.getAll();
            cboCongTy.DisplayMember = "TENCTY";
            cboCongTy.ValueMember = "MACTY";
            //khi form này load lên thì mặc định mã cty sẽ = mã cty hiện tại
            cboCongTy.SelectedValue = myFunctions._macty;
        }
    }
}
