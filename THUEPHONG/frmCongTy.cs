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
using DataLayer;
using THUEPHONG;
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace THUEPHONG
{
    public partial class frmCongTy : DevExpress.XtraEditors.XtraForm
    {
        public frmCongTy()
        {
            InitializeComponent();
        }
        public frmCongTy(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            this._user = user;
            this._right = right;
        }
        tb_SYS_USER _user;
        int _right;
        CONGTY _congty;
        bool _them;
        string _macty;
        private void frmCongTy_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            loadData();
            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
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
            txtDienThoai.Enabled = t;
            txtFax.Enabled = t;
            txtEmail.Enabled = t;
            txtDiaChi.Enabled = t;
            chkDisabled.Enabled = t;
        }
        //reset các control khi muốn thêm
        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDienThoai.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            chkDisabled.Checked = false;
        }
        void loadData()
        {
            gcDanhSach.DataSource = _congty.getAll();
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
            txtMa.Enabled = true;
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
            txtMa.Enabled = false;
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
                _congty.delete(_macty);
            }
            loadData();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_CongTy cty = new tb_CongTy();
                cty.MACTY = txtMa.Text;
                cty.TENCTY = txtTen.Text;
                cty.DIACHI = txtDiaChi.Text;
                cty.DIENTHOAI = txtDienThoai.Text;
                cty.FAX = txtDienThoai.Text;
                cty.EMAIL = txtEmail.Text;
                cty.DISABLED = chkDisabled.Checked;
                _congty.add(cty);
            }    
            else
            {
                tb_CongTy cty = _congty.getItem(_macty);
                cty.TENCTY = txtTen.Text;
                cty.DIACHI = txtDiaChi.Text;
                cty.DIENTHOAI = txtDienThoai.Text;
                cty.FAX = txtFax.Text;
                cty.EMAIL = txtEmail.Text;
                cty.DISABLED = chkDisabled.Checked;
                _congty.update(cty);
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
            txtMa.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            //Trường hợp nếu như lưới có dữ liệu (>0 sẽ là có dữ liệu)
            if (gvDanhSach.RowCount > 0)
            {
                //thì khi đó bắt sự kiện của nó 
                _macty = gvDanhSach.GetFocusedRowCellValue("MACTY").ToString();
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("MACTY").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENCTY").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("DIENTHOAI").ToString();
                txtFax.Text = gvDanhSach.GetFocusedRowCellValue("FAX").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("EMAIL").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DIACHI").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            XuatReport("DM_CONGTY", "Danh mục công ty");
        }
        private void XuatReport(string _reportName, string _tieude)
        {
            if (_macty != null)
            {
                //tạo ra 1 form mới
                Form frm = new Form();
                //tạo ra 1 ReportViewer mới
                CrystalReportViewer Crv = new CrystalReportViewer();
                //gán vô, nút nào cho hiển thị thì cho, không thì ẩn đi
                Crv.ShowGroupTreeButton = false;
                Crv.ShowParameterPanelButton = false;
                //gán vô, nút nào cho hiển thị thì cho, không thì ẩn đi
                Crv.ToolPanelView = ToolPanelViewType.None;
                //lấy các thông tin của ReportViewer, lấy chuỗi kết nối để log in vô database để lấy dữ liệu vào
                TableLogOnInfo Thongtin;
                ReportDocument doc = new ReportDocument();
                //chỉ ra đường dẫn để add cái report vô
                doc.Load(System.Windows.Forms.Application.StartupPath + "\\Reports\\" + _reportName + @".rpt");
                //lấy dữ liệu để truyền vào
                Thongtin = doc.Database.Tables[0].LogOnInfo;
                Thongtin.ConnectionInfo.ServerName = myFunctions._srv;
                Thongtin.ConnectionInfo.DatabaseName = myFunctions._db;
                Thongtin.ConnectionInfo.UserID = myFunctions._us;
                Thongtin.ConnectionInfo.Password = myFunctions._pw;
                //Apply vô để lấy thông tin ra
                doc.Database.Tables[0].ApplyLogOnInfo(Thongtin);
                try
                {
                    //lọc dữ liệu để chỉ lấy thông tin cần thiết theo ý mình cần
                    doc.SetParameterValue("macty", _macty.ToString());

                    Crv.Dock = DockStyle.Fill;
                    //Set cái reportsource cho nó từ cái report nãy add vào từ chỉ ra đường dẫn
                    Crv.ReportSource = doc;
                    //Add vào form
                    frm.Controls.Add(Crv);
                    //Refresh report lại
                    Crv.Refresh();
                    //Sau đó show lên
                    frm.Text = _tieude;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi :" + ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}