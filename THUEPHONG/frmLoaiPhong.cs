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
    public partial class frmLoaiPhong : DevExpress.XtraEditors.XtraForm
    {
        public frmLoaiPhong()
        {
            InitializeComponent();
        }
        public frmLoaiPhong(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }
        tb_SYS_USER _user;
        int _right;
        LOAIPHONG _loaiphong;
        bool _them;
        int _idlp;
        private void frmLoaiPhong_Load(object sender, EventArgs e)
        {
            _loaiphong = new LOAIPHONG();
            loadLP();
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
        void _enabled(bool t)
        {
            txtTenLoaiPhong.Enabled = t;
            spSoNguoi.Enabled = t;
            spSoGiuong.Enabled = t;
            spDonGia.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtTenLoaiPhong.Text = "";
            spDonGia.EditValue = 0;
            spSoGiuong.EditValue = 0;
            spSoNguoi.EditValue = 0;
        }

        void loadLP()
        {
            gcDanhSach.DataSource = _loaiphong.getAll();
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
                _loaiphong.delete(_idlp);
            }
            loadLP();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if(_them)
            {
                tb_LoaiPhong lp = new tb_LoaiPhong();
                lp.TENLOAIPHONG = txtTenLoaiPhong.Text;
                lp.SONGUOI = int.Parse(spSoNguoi.EditValue.ToString());
                lp.SOGIUONG = int.Parse(spSoGiuong.EditValue.ToString());
                lp.DONGIA = double.Parse(spDonGia.EditValue.ToString());
                lp.DISABLED = chkDisabled.Checked;
                _loaiphong.add(lp);
            }
            else
            {
                tb_LoaiPhong lp = _loaiphong.getItem(_idlp);
                lp.TENLOAIPHONG = txtTenLoaiPhong.Text;
                lp.SONGUOI = int.Parse(spSoNguoi.EditValue.ToString());
                lp.SOGIUONG = int.Parse(spSoGiuong.EditValue.ToString());
                lp.DONGIA =  double.Parse(spDonGia.EditValue.ToString());
                lp.DISABLED = chkDisabled.Checked;
                _loaiphong.update(lp);
            }
            _them = false;
            loadLP();
            _enabled(false);
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
            if (gvDanhSach.RowCount>0)
            {
                _idlp = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDLOAIPHONG").ToString());
                txtTenLoaiPhong.Text = gvDanhSach.GetFocusedRowCellValue("TENLOAIPHONG").ToString();
                spDonGia.EditValue = gvDanhSach.GetFocusedRowCellValue("DONGIA").ToString();
                spSoGiuong.EditValue = gvDanhSach.GetFocusedRowCellValue("SOGIUONG").ToString();
                spSoNguoi.EditValue = gvDanhSach.GetFocusedRowCellValue("SONGUOI").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }    
        }
    }
}