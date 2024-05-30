using BusinessLayer;
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

namespace THUEPHONG.MyControls
{
    public partial class frmShowDonVi : DevExpress.XtraEditors.XtraForm
    {
        public frmShowDonVi()
        {
            InitializeComponent();
        }
        public frmShowDonVi(TextBox txtDonVi)
        {
            InitializeComponent();
            this._txtDonvi = txtDonVi;
        }
        TextBox _txtDonvi;
        DONVI _donvi;
        CONGTY _congty;
        private void frmShowDonVi_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            _donvi = new DONVI();
            loadCongty();
            loadDonVi();
             //khi phát sinh sự kiện mà chọn cái chi nhánh khác thi dữ liệu lưới gvDanhSach sẽ thay đổi
            cboCongTy.SelectedValueChanged += CboCongTy_SelectedValueChanged;
            //khi form này load lên thì mặc định mã cty sẽ = mã cty hiện tại
            cboCongTy.SelectedValue = myFunctions._macty;
        }

        private void CboCongTy_SelectedValueChanged(object sender, EventArgs e)
        {
            //Khi mà combobox công ty có sự thay đổi về giá trị thì cái lưới GvDanhSach sẽ load lại dữ liệu
            loadDonVi();
        }
        void loadCongty()
        {
            cboCongTy.DataSource = _congty.getAll();
            cboCongTy.DisplayMember = "TENCTY";
            cboCongTy.ValueMember = "MACTY";
        }
        void loadDonVi()
        {
            
            gcDanhSach.DataSource = _donvi.getAll(cboCongTy.SelectedValue.ToString());
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        private void btnThucHien_Click(object sender, EventArgs e)
        {

        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}