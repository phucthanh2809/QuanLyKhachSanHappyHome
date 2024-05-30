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
    public partial class frm_Phong_ThietBi : DevExpress.XtraEditors.XtraForm
    {
        public frm_Phong_ThietBi()
        {
            InitializeComponent();
        }
        public frm_Phong_ThietBi(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }
        tb_SYS_USER _user;
        int _right;

        PHONG_THIETBI _phongtb;
        PHONG _phong;
        THIETBI _thietbi;
        bool _them;
        int _idPhong;
        int _idTB;
        private void frm_Phong_ThietBi_Load(object sender, EventArgs e)
        {
            _phongtb = new PHONG_THIETBI();
            _phong = new PHONG();
            _thietbi = new THIETBI();
            loadData();
            loadThietBi();
            loadPhong();
            showHideControl(true);
            _enabled(false);
            cboPhong.Enabled = false;
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
            //cboPhong.Enabled = t;
            cboThietBi.Enabled = t;
            spSoLuong.Enabled = t;
        }
        void _reset()
        {
            spSoLuong.EditValue = 0;
        }
        void loadData()
        {
            gcDanhSach.DataSource = _phongtb.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        void loadThietBi()
        {
            cboThietBi.DataSource = _thietbi.getAll();
            cboThietBi.DisplayMember = "TENTHIETBI";
            cboThietBi.ValueMember = "IDTB";
        }
        void loadPhong()
        {
            cboPhong.DataSource = _phong.getAll();
            cboPhong.DisplayMember = "TENPHONG";
            cboPhong.ValueMember = "IDPHONG";
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
            _enabled(true);
            _reset();
            cboPhong.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _them = false;
            _enabled(true);
            showHideControl(false);
            cboPhong.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_right == 1)
            {
                MessageBox.Show("Không có quyền thao tác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có chắc chắn xóa không?","Thông báo",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                _phongtb.delete(_idTB, _idPhong);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_Phong_ThietBi1 tb = new tb_Phong_ThietBi1();
                tb.IDPHONG = int.Parse(cboPhong.SelectedValue.ToString());
                tb.SOLUONG = int.Parse(spSoLuong.EditValue.ToString());
                tb.IDTB = int.Parse(cboThietBi.SelectedValue.ToString());
                _phongtb.add(tb);
            }
            else
            {
                tb_Phong_ThietBi1 tb = _phongtb.getItem(_idPhong , _idTB);
                tb.SOLUONG = int.Parse(spSoLuong.EditValue.ToString());
                tb.IDTB = int.Parse(cboThietBi.SelectedValue.ToString());
                _phongtb.update(tb);

            }
            _them = false;
            loadData();
            _enabled(false);
            showHideControl(true);
            cboPhong.Enabled = false;
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
            cboPhong.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _idPhong = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDPHONG").ToString());
                _idTB = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDTB").ToString());
                spSoLuong.Text = gvDanhSach.GetFocusedRowCellValue("SOLUONG").ToString();
                cboPhong.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDPHONG");
                cboThietBi.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDTB");
            }
        }
    }
}