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
    public partial class frmKhachHang : DevExpress.XtraEditors.XtraForm
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }
        public frmKhachHang(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }
        tb_SYS_USER _user;
        int _right;
        //Khai báo khi nhấn đúp chọn KH ở frmKhachHang nhưng nhận dữ liệu ở frmDatPhong
        //Khi form này chạy lên sẽ tạo cho mình cái biến đối tượng là objDP cái form DatPhong và từ cái form KH này có thể truy cập được đến bất cứ đên cái biến hay cái hàm nào trên cái form đặt phòng từ form KH
        frmDatPhong objDP = (frmDatPhong)Application.OpenForms["frmDatPhong"];
        frmDatPhongDon objDPDon = (frmDatPhongDon)Application.OpenForms["frmDatPhongDon"];

        KHACHHANG _khachhang;
        bool _them;
        int _makh;
        public string kh_dp;

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            _khachhang = new KHACHHANG();
            loadData();
            showHideControl(true); //Chỉ hiển thị chức năng Lưu và Bỏ qua khi chỉnh sửa, còn không sẽ ẩn đi 
            _enabled(false); //Khi nhập liệu thì text sáng lên còn không sẽ mờ đi
           
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
            txtDienThoai.Enabled = t;
            chkGioiTinh.Enabled = t;
            txtEmail.Enabled = t;
            txtDiaChi.Enabled = t;
            chkDisabled.Enabled = t;
            txtCCCD.Enabled = t;
        }
        //reset các control khi muốn thêm
        void _reset()
        {
            txtTen.Text = "";
            txtDienThoai.Text = "";
            chkGioiTinh.Checked = false;
            txtEmail.Text = "";
            txtCCCD.Text = "";
            txtDiaChi.Text = "";
            chkDisabled.Checked = false;
        }
        void loadData()
        {
            gcDanhSach.DataSource = _khachhang.getAll();
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
                _khachhang.delete(_makh);
            }
            loadData();
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {

            if (_them)
            {
                tb_KhachHang kh = new tb_KhachHang();
                kh.CCCD = txtCCCD.Text;
                kh.HOTEN = txtTen.Text;
                kh.DIACHI = txtDiaChi.Text;
                kh.DIENTHOAI = txtDienThoai.Text;
                kh.GIOITINH = chkGioiTinh.Checked;
                kh.EMAIL = txtEmail.Text;
                kh.DISABLED = chkDisabled.Checked;
                _khachhang.add(kh);
            }
            else
            {
                tb_KhachHang kh = _khachhang.getItem(_makh);
                kh.CCCD = txtCCCD.Text;
                kh.HOTEN = txtTen.Text;
                kh.DIACHI = txtDiaChi.Text;
                kh.DIENTHOAI = txtDienThoai.Text;
                kh.GIOITINH = chkGioiTinh.Checked;
                kh.EMAIL = txtEmail.Text;
                kh.DISABLED = chkDisabled.Checked;
                _khachhang.update(kh);
            }
            _them = false; //Để đảm bảo cho chắc chắn cái them này lần sau có nhấn sửa hay thêm thì vẫn quay về như cũ
            //Gọi hàm để cập nhật lại
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

       
        //Thay đổi dấu tích thành hình dấu x màu đỏ ngay ô DISABLED cho đẹp mắt
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
         //Khi double click thì sẽ truyền cái id khách hàng từ form khách hàng về form đặt phòng để lấy được khách hàng 
        private void gvDanhSach_DoubleClick(object sender, EventArgs e)
        {
             if(gvDanhSach.GetFocusedRowCellValue("IDKH")!=null)
            {
                if (kh_dp == "datphongdon")
                {
                    objDPDon.loadKH();
                    objDPDon.setKH(int.Parse(gvDanhSach.GetFocusedRowCellValue("IDKH").ToString()));
                }
                else
                {
                    //khi thêm khách hàng vào form khách hàng thì dữ liệu của text Khách hàng của form Đặt phòng sẽ tự động cập nhật có tên liền
                    objDP.loadKH();
                    objDP.setKhachHang(int.Parse(gvDanhSach.GetFocusedRowCellValue("IDKH").ToString()));
                }
                this.Close();
            }    
        }

        private void gvDanhSach_Click_1(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                //thì khi đó bắt sự kiện của nó
                _makh = int.Parse(gvDanhSach.GetFocusedRowCellValue("IDKH").ToString());
                chkGioiTinh.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("GIOITINH").ToString());
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("HOTEN").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("DIENTHOAI").ToString();
                txtCCCD.Text = gvDanhSach.GetFocusedRowCellValue("CCCD").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("EMAIL").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DIACHI").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
        }
    }
}