using DevExpress.XtraEditors;
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
using DataLayer;

namespace THUEPHONG
{
    public partial class frmSetParam : DevExpress.XtraEditors.XtraForm
    {
        public frmSetParam()
        {
            InitializeComponent();
        }
        CONGTY _congty;
        DONVI _donvi;

        private void frmSetParam_Load(object sender, EventArgs e)
        {
            _congty  = new CONGTY();
            _donvi = new DONVI();
            loadCongty();
            cboCongTy.SelectedValue = "CT01";
            cboCongTy.SelectedIndexChanged += cboCongTy_SelectedIndexChanged;
            loadDonvi();
        }
        private void cboCongTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDonvi();
        }
        void loadCongty()
        {
            cboCongTy.DataSource = _congty.getAll();
            cboCongTy.DisplayMember = "TENCTY";
            cboCongTy.ValueMember = "MACTY";
        }

        void loadDonvi()
        {
            cboDonVi.DataSource = _donvi.getAll(cboCongTy.SelectedValue.ToString());
            cboDonVi.DisplayMember = "TENDVI";
            cboDonVi.ValueMember = "MADVI";
            cboDonVi.SelectedIndex = -1;
        }
        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            string macty = cboCongTy.SelectedValue.ToString();
            //Trim() bỏ các khoảng trắng thừa đi
            //Nếu = "" thì sẽ lấy dấu ~ và ngược lại sẽ lấy cboDonvi - Cấu trúc viết tắt mệnh đề if 
            string madvi = (cboDonVi.Text.Trim() == "") ? "~" : cboDonVi.SelectedValue.ToString();
            SYS_PARAM _sysParam = new SYS_PARAM(macty,madvi);
            _sysParam.SaveFile();
            MessageBox.Show("Xác lập đơn vị sử dụng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}