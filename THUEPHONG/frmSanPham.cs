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
using DataLayer;
using BusinessLayer;

namespace THUEPHONG
{
    public partial class frmSP_DV : DevExpress.XtraEditors.XtraForm
    {
        public frmSP_DV()
        {
            InitializeComponent();
        }
        public frmSP_DV(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }
        tb_SYS_USER _user;
        int _right;
        SANPHAM _sanpham;
        bool _them;
        int _idsp;

        private void frmSP_DV_Load(object sender, EventArgs e)
        {
            _sanpham = new SANPHAM();
            loadData();
            showHideControl(true);
            _enabled(false);
        }
        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }
        //Khi nhập liệu thì text sáng lên còn không sẽ mờ đi 
        void _enabled(bool t)
        {
            txtTen.Enabled = t;
            spDonGia.Enabled = t;
            chkDisabled.Enabled = t;
        }
        //reset các control khi muốn thêm
        void _reset()
        {
            txtTen.Text = "";
            spDonGia.EditValue = 0;
            chkDisabled.Checked = false;
        }
        void loadData()
        {
            gcDanhSach.DataSource = _sanpham.getAll();
            //Set để không được chỉnh sửa trên lưới gvDanhSach
            gvDanhSach.OptionsBehavior.Editable = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _them = true;
            showHideControl(false);
            _enabled(true); //Khi nhập liệu thì text sáng lên còn không sẽ mờ đi 
            _reset();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _them = false;
            _enabled(true); //Khi nhập liệu thì text sáng lên còn không sẽ mờ đi 
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                _sanpham.delete(_idsp);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_SanPham sp = new tb_SanPham();
                sp.TENSP = txtTen.Text;
                sp.DONGIA = double.Parse(spDonGia.EditValue.ToString());
                sp.DISABLED = chkDisabled.Checked;
                _sanpham.add(sp);
            }
           else
            {
                tb_SanPham sp = _sanpham.getItem(_idsp);
                sp.TENSP = txtTen.Text;
                sp.DONGIA = double.Parse(spDonGia.EditValue.ToString());
                sp.DISABLED = chkDisabled.Checked;
                _sanpham.update(sp);
            }    
            _them = false; 
            loadData();
            _enabled(false); //Lưu xong thì phải mờ đi chỗ nhập text 
            showHideControl(true);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idsp = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDSP").ToString());
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENSP").ToString();
                spDonGia.EditValue= gvDanhSach.GetFocusedRowCellValue("DONGIA").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
        }
    }
}