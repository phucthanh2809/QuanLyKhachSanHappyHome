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
    public partial class frmTang : DevExpress.XtraEditors.XtraForm
    {
        public frmTang()
        {
            InitializeComponent();
        }
        public frmTang(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }
        tb_SYS_USER _user;
        int _right;
        TANG _tang;
        bool _them;
        int _idTang;
        private void frmTang_Load(object sender, EventArgs e)
        {
            _tang = new TANG();
            loadTang();
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
            txtTen.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtTen.Text = "";
            chkDisabled.Checked = false;
        }
        void loadTang()
        {
            gcDanhSach.DataSource = _tang.getAll();
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
            _enabled(true);
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
            _enabled(true);
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
                _tang.delete(_idTang);
            }
            loadTang();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_Tang tang = new tb_Tang();
                tang.TENTANG = txtTen.Text;
                tang.DISABLED = chkDisabled.Checked;
                _tang.add(tang);
            }
            else
            {
                tb_Tang tang = _tang.getItem(_idTang);
                tang.TENTANG = txtTen.Text;
                tang.DISABLED = chkDisabled.Checked;
                _tang.update(tang);
            }
            _them = false;
            loadTang();
            showHideControl(true);
            _enabled(false);

        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            //Trường hợp nếu như lưới có dữ liệu (>0 sẽ là có dữ liệu)
            if (gvDanhSach.RowCount > 0)
            {
                //thì khi đó bắt sự kiện của nó 
                _idTang = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDTANG").ToString());
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENTANG").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}