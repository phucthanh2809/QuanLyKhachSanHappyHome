
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
using CrystalDecisions.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace THUEPHONG
{
    public partial class frmDatPhongDon : DevExpress.XtraEditors.XtraForm
    {
        public frmDatPhongDon()
        {
            InitializeComponent();
            ////Đầu tiên khi Form Đặt phòng chạy lên sẽ tạo ra 1 DataTable chứa danh sách các phòng trống 
            //DataTable tb = myFunctions.laydulieu();
            //// và gán cho gcPhong cái Datasource của DataTable
            //gcSanPham.DataSource = tb;
            ////Tiếp tục lấy DataSource của gcPhong đó gán cho DataSource của gcDatPhong 
            //gcSanPham.DataSource = tb.Clone();
        }
        public bool _them;
        //Vì sẽ lấy idPhong từ frmMain qua nên khai báo biến là idPhong
        public int _idPhong;
        int _idDP=0;
        DATPHONG _datphong;
        DATPHONG_CHITIET _datphongct;
        DATPHONG_SANPHAM _datphongsp;
        OBJ_PHONG _phongHienTai;
        PHONG _phong;
        SANPHAM _sanpham;
        KHACHHANG _khachhang;
        SYS_PARAM _param;
        List<OBJ_DPSP> lstDPSP;
        double _tongtien = 0;
        string _macty;
        string _madvi;
        frmMain objMain = (frmMain)Application.OpenForms["frmMain"];

        void _enabled(bool t)
        {
            txtThanhTien.Enabled = !t;
            txtTienPhong.Enabled = !t;
            txtTienSPDV.Enabled = !t;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (searchKH.EditValue == null || searchKH.EditValue.ToString() == "")
            {
                MessageBox.Show("Vui lòng chọn Khách Hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            saveData();

            _tongtien = double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + _phong.getItemFull(_idPhong).DONGIA * (dtNgayTra.Value.Day - dtNgayDat.Value.Day);
            var dp = _datphong.getItem(_idDP);
            dp.SOTIEN = _tongtien;
            _datphong.update(dp);
            objMain.gControl.Gallery.Groups.Clear();
            objMain.showRoom();

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if(!_them)
            {
                saveData();
                _tongtien = double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + _phong.getItemFull(_idPhong).DONGIA * (dtNgayTra.Value.Day - dtNgayDat.Value.Day);
                var dp = _datphong.getItem(_idDP);
                dp.SOTIEN = _tongtien;
                _datphong.update(dp);
                _datphong.updateStatus(_idDP);
                _phong.updateStatus(_idPhong, false);
                XuatReport("PHIEU_DATPHONGKS_DON", "Chi tiết đặt phòng");
                cboTrangThai.SelectedValue = true;
                objMain.gControl.Gallery.Groups.Clear();
                objMain.showRoom();
            }    
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmDatPhongDon_Load(object sender, EventArgs e)
        {
            _datphong = new DATPHONG();
            _datphongsp = new DATPHONG_SANPHAM();
            _datphongct = new DATPHONG_CHITIET();
            _phong = new PHONG();
            _sanpham = new SANPHAM();
            lstDPSP = new List<OBJ_DPSP>();
            //Hiển thị tên Phòng và giá tiền phòng 
            _phongHienTai = _phong.getItemFull(_idPhong);
            lblPhong.Text = _phongHienTai.TENPHONG + " - Đơn giá: " + _phongHienTai.DONGIA.ToString("N0") + " VNĐ";

            dtNgayDat.Value = DateTime.Now;
            dtNgayTra.Value = DateTime.Now.AddDays(1);

            cboTrangThai.DataSource = TRANGTHAI.getList();
            cboTrangThai.ValueMember = "_value";
            cboTrangThai.DisplayMember = "_display";
            spSoNguoi.Text = "1";
            _macty = myFunctions._macty;
            _madvi = myFunctions._madvi;
            loadKH();
            loadSP();
            var dpct = _datphongct.getIDDPByPhong(_idPhong);
            if (!_them && dpct !=null)
            {
                _idDP = dpct.IDDP;
                //từ id đặt phòng lấy ra được chi tiết của đặt phòng đó 
                var dp = _datphong.getItem(_idDP);
                searchKH.EditValue = dp.IDKH;
                dtNgayDat.Value = dp.NGAYDATPHONG.Value;
                if (dp.NGAYDATPHONG.Value.ToShortDateString() == DateTime.Now.ToShortDateString())
                    dtNgayTra.Value = dp.NGAYDATPHONG.Value.AddDays(1);
                else
                    dtNgayTra.Value = DateTime.Now;

                cboTrangThai.SelectedValue = dp.STATUS;
                spSoNguoi.Text = dp.SONGUOIO.ToString();
                txtGhiChu.Text = dp.GHICHU.ToString();
                //lblPhong.Text = _phongHienTai.TENPHONG + " -Đơn giá: " + _phongHienTai.DONGIA.ToString("N0");
                //txtThanhTien.Text = dp.SOTIEN.Value.ToString("N0");
            }
            loadSPDV();
            loadDPSP();

            txtTienPhong.Text = _phongHienTai.DONGIA.ToString();
            txtTienSPDV.Text = gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString();
            txtThanhTien.Text = (double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + _phongHienTai.DONGIA * (dtNgayTra.Value.Day - dtNgayDat.Value.Day)).ToString("N0");

        }
        void loadSPDV()
        {
            gcSPDV.DataSource = _datphongsp.getAllByDatPhong(_idDP);
            //cộng dồng SP-DV thay đổi vào mà không bị không bị mất dữ liệu SP-DV trước đó  
            lstDPSP = _datphongsp.getAllByDatPhong(_idDP);
        }
        void loadSP()
        {
            gcSanPham.DataSource = _sanpham.getAll();
            gvSanPham.OptionsBehavior.Editable = false;
        }
        public void loadKH()
        {
            _khachhang = new KHACHHANG();
            searchKH.Properties.DataSource = _khachhang.getAll();
            searchKH.Properties.ValueMember = "IDKH";
            searchKH.Properties.DisplayMember = "HOTEN";
        }

        public void setKH(int idKH)
        {
            searchKH.EditValue = idKH; 
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            frm.kh_dp = "datphongdon";
            frm.ShowDialog();
        }

        private void gvSanPham_DoubleClick(object sender, EventArgs e)
        {
            if (_idPhong == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(bool.Parse(cboTrangThai.SelectedValue.ToString()) ==true)
            {
                MessageBox.Show("Phiếu đã hoàn tất không được chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }    
            if (gvSanPham.GetFocusedRowCellValue("IDSP") != null)
            {
                //Khi mà click vô lưới sẽ tạo ra 1 đối tượng là sản phẩm (sp)
                OBJ_DPSP sp = new OBJ_DPSP();
                //IDSP là int nên sẽ ép kiểu về int 
                sp.IDSP = int.Parse(gvSanPham.GetFocusedRowCellValue("IDSP").ToString());
                sp.TENSP = gvSanPham.GetFocusedRowCellValue("TENSP").ToString();
                sp.IDPHONG = _idPhong;
                sp.TENPHONG = _phongHienTai.TENPHONG;
                sp.DONGIA = float.Parse(gvSanPham.GetFocusedRowCellValue("DONGIA").ToString());
                sp.SOLUONG = 1;
                sp.THANHTIEN = sp.DONGIA * sp.SOLUONG;
                //Lần đầu chưa tồn tại thì add hẳn vào 
                foreach (var item in lstDPSP)
                {
                    if (item.IDSP == sp.IDSP && item.IDPHONG == sp.IDPHONG)
                    {
                        //Gán cái list cho lưới 
                        item.SOLUONG = item.SOLUONG + 1;
                        item.THANHTIEN = item.SOLUONG * item.DONGIA;
                        //Nếu đã có rồi thì chỉ tăng số lượng không tạo thêm tránh trừng lập 2 cái gioosnh nhau
                        loadDPSP();
                        return;
                    }
                }
                //Sau đó sẽ add vào 
                lstDPSP.Add(sp);
            }
            loadDPSP();
            txtTienPhong.Text = _phongHienTai.DONGIA.ToString();
            txtTienSPDV.Text = gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString();
            txtThanhTien.Text = (double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + _phongHienTai.DONGIA*(dtNgayTra.Value.Day-dtNgayDat.Value.Day)).ToString("N0");
        }
        void loadDPSP()
        {
            List<OBJ_DPSP> lsDP = new List<OBJ_DPSP>();
            foreach (var item in lstDPSP)
            {
                lsDP.Add(item);
            }
            gcSPDV.DataSource = lsDP;
        }

        private void gvSPDV_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "SOLUONG")
            {
                int sl = int.Parse(e.Value.ToString());
                if (sl != 0)
                {
                    //Lấy đơn giá ra
                    double gia = double.Parse(gvSPDV.GetRowCellValue(gvSPDV.FocusedRowHandle, "DONGIA").ToString());
                    gvSPDV.SetRowCellValue(gvSPDV.FocusedRowHandle, "THANHTIEN", sl * gia);
                }
                else
                {
                    gvSPDV.SetRowCellValue(gvSPDV.FocusedRowHandle, "THANHTIEN", 0);
                }
            }
            //Tính tổng tiền rồi show dữ liệu ra ở ô textThanhTien
            gvSPDV.UpdateTotalSummary();
            txtTienPhong.Text = _phongHienTai.DONGIA.ToString();
            txtTienSPDV.Text = gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString();
            txtThanhTien.Text = (double.Parse(gvSPDV.Columns["THANHTIEN"].SummaryItem.SummaryValue.ToString()) + _phongHienTai.DONGIA * (dtNgayTra.Value.Day - dtNgayDat.Value.Day)).ToString("N0");
        }

        private void gvSPDV_HiddenEditor(object sender, EventArgs e)
        {
            gvSPDV.UpdateCurrentRow();
        }

        void saveData()
        {
            if (_them)
            {
                //Lưu xong datphong thi mới tới dpct và dpsp nên chưa gọi new bởi vì sẽ dùng 1 vòng lặp để lưu tất cả trên lưới dữ liệu
                tb_DatPhong dp = new tb_DatPhong();
                tb_DatPhong_CT dpct;
                tb_DatPhong_SanPham dpsp;
                dp.NGAYDATPHONG = dtNgayDat.Value;
                dp.NGAYTRAPHONG = dtNgayTra.Value;
                dp.SONGUOIO = int.Parse(spSoNguoi.EditValue.ToString());
                dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                dp.THEODOAN = false;
                dp.IDKH = int.Parse(searchKH.EditValue.ToString());
                dp.SOTIEN = double.Parse(txtThanhTien.Text);
                dp.GHICHU = txtGhiChu.Text;
                dp.DISABLED = false;
                dp.IDUSER = 1;
                dp.MACTY = _macty;
                dp.MADVI = _madvi;
                dp.CREATED_DATE = DateTime.Now;
                var _dp = _datphong.add(dp);
                _idDP = _dp.IDDP;

                dpct = new tb_DatPhong_CT();
                dpct.IDDP = _dp.IDDP;
                dpct.IDPHONG = _idPhong;
                dpct.SONGAYO = dtNgayTra.Value.Day - dtNgayDat.Value.Day;
                dpct.DONGIA = int.Parse(_phongHienTai.DONGIA.ToString());
                dpct.THANHTIEN = dpct.SONGAYO * dpct.DONGIA;
                dpct.NGAY = DateTime.Now;
                var _dpct = _datphongct.add(dpct);
                _phong.updateStatus(dpct.IDPHONG, true);
                    //Khi không sử dụng SPDV thì khỏi lưu
                    if (gvSPDV.RowCount > 0)
                    {
                        //Nếu sử dụng SPDV thì mới lưu
                        for (int j = 0; j < gvSPDV.RowCount; j++)
                        {
                            if (dpct.IDPHONG == int.Parse(gvSPDV.GetRowCellValue(j, "IDPHONG").ToString()))
                            {
                                dpsp = new tb_DatPhong_SanPham();
                                dpsp.IDDP = _dp.IDDP;
                                dpsp.IDDPCT = _dpct.IDDPCT;
                                dpsp.IDPHONG = int.Parse(gvSPDV.GetRowCellValue(j, "IDPHONG").ToString());
                                dpsp.IDSP = int.Parse(gvSPDV.GetRowCellValue(j, "IDSP").ToString());
                                dpsp.SOLUONG = int.Parse(gvSPDV.GetRowCellValue(j, "SOLUONG").ToString());
                                dpsp.DONGIA = int.Parse(gvSPDV.GetRowCellValue(j, "DONGIA").ToString());
                                dpsp.THANHTIEN = dpsp.SOLUONG * dpsp.DONGIA;
                                _datphongsp.add(dpsp);
                            }
                        }
                    } 
            }
            else
            {
                //update bảng Đặt phòng 
                tb_DatPhong dp = _datphong.getItem(_idDP);
                tb_DatPhong_CT dpct;
                tb_DatPhong_SanPham dpsp;
                dp.NGAYDATPHONG = dtNgayDat.Value;
                dp.NGAYTRAPHONG = dtNgayTra.Value;
                dp.SONGUOIO = int.Parse(spSoNguoi.EditValue.ToString());
                dp.STATUS = bool.Parse(cboTrangThai.SelectedValue.ToString());
                dp.IDKH = int.Parse(searchKH.EditValue.ToString());
                dp.SOTIEN = double.Parse(txtThanhTien.Text);
                dp.GHICHU = txtGhiChu.Text;
                dp.UPDATE_BY = 1;
                dp.IDUSER = 1;
                dp.UPDATE_DATE = DateTime.Now;
                var _dp = _datphong.update(dp);
                _idDP = _dp.IDDP;
                _datphongct.deleteAll(_dp.IDDP);
                _datphongsp.deleteAll(_dp.IDDP);

                //Sau khi xóa hết cái cũ sẽ tạo lại cái mới
                dpct = new tb_DatPhong_CT();
                dpct.IDDP = _dp.IDDP;
                dpct.IDPHONG = _idPhong;
                dpct.SONGAYO = dtNgayTra.Value.Day - dtNgayDat.Value.Day;
                dpct.DONGIA = int.Parse(_phongHienTai.DONGIA.ToString());
                dpct.THANHTIEN = dpct.SONGAYO * dpct.DONGIA;
                dpct.NGAY = DateTime.Now;
                var _dpct = _datphongct.add(dpct);
                _phong.updateStatus(dpct.IDPHONG, true); 
                //Khi không sử dụng SPDV thì khỏi lưu
                if (gvSPDV.RowCount > 0)
                {
                    //Nếu sử dụng SPDV thì mới lưu
                    for (int j = 0; j < gvSPDV.RowCount; j++)
                    {
                        if (dpct.IDPHONG == int.Parse(gvSPDV.GetRowCellValue(j, "IDPHONG").ToString()))
                        {
                            dpsp = new tb_DatPhong_SanPham();
                            dpsp.IDDP = _dp.IDDP;
                            dpsp.IDDPCT = _dpct.IDDPCT;
                            dpsp.IDPHONG = int.Parse(gvSPDV.GetRowCellValue(j, "IDPHONG").ToString());
                            dpsp.IDSP = int.Parse(gvSPDV.GetRowCellValue(j, "IDSP").ToString());
                            dpsp.SOLUONG = int.Parse(gvSPDV.GetRowCellValue(j, "SOLUONG").ToString());
                            dpsp.DONGIA = int.Parse(gvSPDV.GetRowCellValue(j, "DONGIA").ToString());
                            dpsp.THANHTIEN = dpsp.SOLUONG * dpsp.DONGIA;
                            _datphongsp.add(dpsp);
                        }
                    }
                }
            }
        }

        private void XuatReport(string _reportName, string _tieude)
        {
            if (_idDP != 0)
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
                    doc.SetParameterValue("@IDDP", _idDP.ToString());

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
        private void gvSanPham_CustomDrawRowIndicator_1(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (!gvSanPham.IsGroupRow(e.RowHandle)) // Nếu không phải là Group
            {
                if (e.Info.IsRowIndicator) //Nếu là dòng Indicator
                {
                    if (e.RowHandle < 0)
                    {
                        e.Info.ImageIndex = 0;
                        e.Info.DisplayText = string.Empty;
                    }
                    else
                    {
                        e.Info.ImageIndex = -1; //Không hiển thị hình
                        e.Info.DisplayText = (e.RowHandle + 1).ToString(); //Số thứ tự tăng dần lên 
                    }
                    SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font); // Lấy kích thước của vùng hiển thị Text
                    Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                    BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvSanPham); }));// Tăng kích thước Text vượt quá 
                }
            }
            else
            {
                e.Info.ImageIndex = -1;
                e.Info.DisplayText = string.Format("[{0}]", (e.RowHandle * -1)); // Nhân -1 để đánh lại stt tăng dần 
                SizeF _Size = e.Graphics.MeasureString(e.Info.DisplayText, e.Appearance.Font);
                Int32 _Width = Convert.ToInt32(_Size.Width) + 20;
                BeginInvoke(new MethodInvoker(delegate { cal(_Width, gvSanPham); }));
            }
        }
        bool cal(Int32 _Width, GridView _View)
        {
            _View.IndicatorWidth = _View.IndicatorWidth < _Width ? _Width : _View.IndicatorWidth;
            return true;
        }

        private void gvSanPham_CustomDrawGroupRow(object sender, DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventArgs e)
        {
            //ép sender về kiểu gridview
            GridView view = sender as GridView;
            GridGroupRowInfo info = e.Info as GridGroupRowInfo;
            string caption = info.Column.Caption;
            if (info.Column.Caption == string.Empty)
                //Nếu tiêu đề không có gì thì mình gán cái tiêu đề bằng cái tiêu đề (caption) cột 
                caption = info.Column.ToString();
            //GetChildRowCount Đếm số dòng trong Group đó xem còn bao nhiêu phòng trống
            info.GroupText = string.Format("{0}: {1} ({2} phòng trống)", caption, info.GroupValueText, view.GetChildRowCount(e.RowHandle));
        }
    }
}