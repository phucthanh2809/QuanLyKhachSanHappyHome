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
    public partial class frmPhong : DevExpress.XtraEditors.XtraForm
    {
        public frmPhong()
        {
            InitializeComponent();
        }
        public frmPhong(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }
        tb_SYS_USER _user;
        int _right;
        //Khao báo biến _congty
        PHONG _phong;
        LOAIPHONG _loaiphong;
        TANG _tang;
        //Khai báo biến them
        bool _them;
        int _idloaiphong;
        int _idTang;
        int _idphong;

        private void frmPhong_Load(object sender, EventArgs e)
        {
            _phong = new PHONG();
            _tang = new TANG();
            _loaiphong = new LOAIPHONG();
            loadData();
            loadLoaiPhong();
            loadTang();
            showHideControl(true);
            _enabled(false);
        }
        //Set chức năng nút Lưu và nút Bỏ Qua khi nào cần sửa thì mới hiện, không thì sẽ ẩn đi 
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
            cboLoaiPhong.Enabled = t;
            cboTang.Enabled = t;
            chkStatus.Enabled = t;
        }
        //reset các control khi muốn thêm
        void _reset()
        {
            txtTen.Text = "";
            cboLoaiPhong.SelectedValue = "";
            cboTang.SelectedValue = "";
            chkStatus.Checked = false;
        }
        void loadData()
        {
            gcDanhSach.DataSource = _phong.getAllOBJPhong();
            //Set để không được chỉnh sửa trên lưới gvDanhSach
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        void loadLoaiPhong()
        {
            cboLoaiPhong.DataSource = _loaiphong.getAll();
            cboLoaiPhong.DisplayMember = "TENLOAIPHONG";
            cboLoaiPhong.ValueMember = "IDLOAIPHONG";
        }
        void loadTang()
        {
            cboTang.DataSource = _tang.getAll();
            cboTang.DisplayMember = "TENTANG";
            cboTang.ValueMember = "IDTANG";
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
                _phong.delete(_idphong);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_Phong phong = new tb_Phong();
                phong.STATUS = chkStatus.Checked;
                phong.IDLOAIPHONG = int.Parse(cboLoaiPhong.SelectedValue.ToString());
                phong.IDTANG = int.Parse(cboTang.SelectedValue.ToString());
                phong.TENPHONG = txtTen.Text;
                _phong.add(phong);
            }
            else
            {
                tb_Phong phong = _phong.getItem(_idphong);
                phong.STATUS = chkStatus.Checked;
                phong.IDLOAIPHONG = int.Parse(cboLoaiPhong.SelectedValue.ToString());
                phong.IDTANG = int.Parse(cboTang.SelectedValue.ToString());
                phong.TENPHONG = txtTen.Text;
                _phong.update(phong);
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

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            _idphong = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDPHONG").ToString());
            _idloaiphong = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDLOAIPHONG").ToString());
            _idTang = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDTANG").ToString());
            txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENPHONG").ToString();
            chkStatus.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("STATUS").ToString());
            //chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            cboTang.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDTANG");
            cboLoaiPhong.SelectedValue = gvDanhSach.GetFocusedRowCellValue("IDLOAIPHONG");
        }
        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //Kiểm tra xem thứ nhất cột có bằng DISABLED không, thứ hai là = true không,//Nếu bằng true thì mới tạo ra cái hình ảnh x đỏ vào
            if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
            {

                Image img = Properties.Resources._1398919_close_cross_incorrect_invalid_x_icon;
                //Khi thêm hình vào rồi thì mới bắt đầu kiểm tra xem đúng đối tượng chưa rồi mới đưa vào hình vào 
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
        }
    }
}