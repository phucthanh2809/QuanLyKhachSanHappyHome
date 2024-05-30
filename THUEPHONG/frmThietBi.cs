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
    public partial class frmThietBi : DevExpress.XtraEditors.XtraForm
    {
        public frmThietBi()
        {
            InitializeComponent();
        }
        public frmThietBi(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }
        tb_SYS_USER _user;
        int _right;
        THIETBI _thietbi;
        bool _them;
        int _idTB;
        private void frmThietBi_Load(object sender, EventArgs e)
        {
            _thietbi = new THIETBI();
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
        void _enabled(bool t)
        {
            txtThietBi.Enabled = t;
            spDonGia.Enabled = t;
            chkDisabled.Checked = t;
        }
        void _reset()
        {
            txtThietBi.Text = "";
            spDonGia.EditValue = 0;
            chkDisabled.Checked = false;
        }

        void loadData()
        {
            gcDanhSach.DataSource = _thietbi.getAll();
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
                _thietbi.delete(_idTB);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_ThietBi tb = new tb_ThietBi();
                tb.TENTHIETBI = txtThietBi.Text;
                tb.DONGIA = double.Parse(spDonGia.EditValue.ToString());
                tb.DISABLED = chkDisabled.Checked;
                _thietbi.add(tb);
            }
            else
            {
                tb_ThietBi tb = _thietbi.getItem(_idTB);
                tb.TENTHIETBI = txtThietBi.Text;
                tb.DONGIA = double.Parse(spDonGia.EditValue.ToString());
                tb.DISABLED = chkDisabled.Checked;
                _thietbi.update(tb);
            }
            _them = false; 
            loadData();
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

        private void gvDanhSach_Click_1(object sender, EventArgs e)
        {
            _idTB=int.Parse(gvDanhSach.GetFocusedRowCellValue("IDTB").ToString());
            txtThietBi.Text = gvDanhSach.GetFocusedRowCellValue("TENTHIETBI").ToString();
            spDonGia.EditValue = gvDanhSach.GetFocusedRowCellValue("DONGIA").ToString();
            chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
        }
    }
}