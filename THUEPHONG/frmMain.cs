using BusinessLayer;
using DataLayer;
using USERMANAGEMENT;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using DevExpress.XtraNavBar;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Forms;


namespace THUEPHONG
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        //Phương thức khởi tạo
        public frmMain()
        {
            InitializeComponent();
        }
        public frmMain(tb_SYS_USER user)
        {
            InitializeComponent();
            //truyền đối tượng user vào
            this._user = user;
            //Hiển thị tiêu đề trên form main 
            this.Text = "PHẦN MỀM QUẢN LÝ KHÁCH SẠN - NGƯỜI ĐANG SỬ DỤNG: " + _user.FULLNAME;
        }
        //Khai báo biến 
        tb_SYS_USER _user;
        TANG _tang;
        PHONG _phong;
        SYS_FUNC _func;
        SYS_GROUP _sysGroup;
        SYS_RIGHT _sysRight;
        GalleryItem item = null;

        private void frmMain_Load(object sender, EventArgs e)
        { //Khởi tạo 
            _tang = new TANG();
            _phong = new PHONG();
            _func = new SYS_FUNC();
            _sysGroup = new SYS_GROUP();
            _sysRight = new SYS_RIGHT();
            leftMenu();
            showRoom();
        }
        //Tạo danh mục menu bên trái trong navContron
        void leftMenu()
        {
            int i = 0; //khai báo để stt trong tool imagelist tăng lên để chuyển sang hình khác tương ứng với chức năng  
            var _lsParent = _func.getParent(); 
            foreach (var _pr in _lsParent)
            {
                NavBarGroup navGroup = new NavBarGroup(_pr.DESCRIPTION);
                navGroup.Tag = _pr.FUNC_CODE; //FunC-CODE - sau này bắt nó phân quyền và hiển thị form chức năng do đó gắn Tag và Name 
                navGroup.Name = _pr.FUNC_CODE;
                navGroup.ImageOptions.LargeImageIndex = i; //show icon trong chức năng imagetoolist-Windowsform ra bên cạnh các chữ 
                i++; // số tăng lên --> số hình khác --> đổi hình khác 
                navMain.Groups.Add(navGroup);

                var _lsChild = _func.getChild(_pr.FUNC_CODE);
                foreach (var _ch in _lsChild)
                {
                    NavBarItem navItem = new NavBarItem(_ch.DESCRIPTION);
                    navItem.Tag = _ch.FUNC_CODE;
                    navItem.Name = _ch.FUNC_CODE;
                    navItem.ImageOptions.SmallImageIndex = 0;
                    navGroup.ItemLinks.Add(navItem);
                }
                navMain.Groups[navGroup.Name].Expanded = true; //show mở rộng danh mục không cần phải bấm click vào mới show ra
            }
        }
        public void showRoom()
        {
            //Khởi tạo để khi chọn phòng để đặt thì ngoài danh sashc hiển thị phòng trốn sẽ cập nhật phòng đã đặt
            _tang = new TANG();
            _phong = new PHONG();
            //Hết 1 cái 1 tầng, hết 1 tầng add vào 
            var lsTang = _tang.getAll();
            gControl.Gallery.ItemImageLayout = (DevExpress.Utils.Drawing.ImageLayoutMode)ImageLayoutMode.OriginalSize; // Thêm hình ngôi nhà cho tầng từ ImageList3
            gControl.Gallery.ImageSize = new Size(64, 64); // Thiết lập size cho tất cả hình Size 64px
            gControl.Gallery.ShowItemText = true; //Hiển thị 
            gControl.Gallery.ShowGroupCaption = true;
            foreach (var t in lsTang)
            {
                var galleryItem = new GalleryItemGroup(); 
                galleryItem.Caption = t.TENTANG;
                galleryItem.CaptionAlignment = GalleryItemGroupCaptionAlignment.Stretch;
                List<tb_Phong> lsPhong = _phong.getByTang(t.IDTANG); //Khởi tạo phòng
                foreach (var p in lsPhong)
                {
//Hết 1 phòng thì lấy phòng đó add vào cái tâng (lsphong) và hết cái tầng thì add từng từng phòng vào Gallery, sau đó add Gallery vào từng tầng 
                    var gc_item = new GalleryItem();
                    gc_item.Caption = p.TENPHONG;
                    gc_item.Value = p.IDPHONG;
                    if (p.STATUS == true) //Nếu trạng thái đã đặt  thì
                        gc_item.ImageOptions.Image = imageList3.Images[1]; // Khi phòng đã đặt hiện hình ngôi nhà đỏ stt ảnh 1
                    else
                        gc_item.ImageOptions.Image = imageList3.Images[0]; // Khi phòng còn trống hiện hình ngôi nhà xanh stt ảnh 0

                    galleryItem.Items.Add(gc_item);
                }
                gControl.Gallery.Groups.Add(galleryItem);
            }
            
        }
        private void navMain_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            string func_code = e.Link.Item.Tag.ToString();

            //Lấy ra group và quyền của id user
            var _group = _sysGroup.getGroupByMemBer(_user.IDUSER);
            var _uRight = _sysRight.getRight(_user.IDUSER, func_code);

            if(_group != null)
            {
                var _groupRight = _sysRight.getRight(_group.GROUP,func_code);
                //Kiểm tra quyền (nếu quyền user nhỏ hơn quyền group thì gắn quyền user bawnfgg quyền group)
                if (_uRight.USER_RIGHT < _groupRight.USER_RIGHT)
                    _uRight.USER_RIGHT = _groupRight.USER_RIGHT; 
            }    

            if(_uRight.USER_RIGHT==0)
            {
                MessageBox.Show("Không có quyền thao tác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                switch (func_code) //hiện bảng theo từng chức năng theo chọn
                {
                    case "DATPHONG":
                        {
                            frmDatPhong frm = new frmDatPhong();
                            frm.ShowDialog();
                            break;
                        }
                    case "KHACHHANG":
                        {
                            frmKhachHang frm = new frmKhachHang(_user, _uRight.USER_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "TANG":
                        {
                            frmTang frm = new frmTang(_user, _uRight.USER_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "LOAIPHONG":
                        {
                            frmLoaiPhong frm = new frmLoaiPhong(_user, _uRight.USER_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "PHONG":
                        {
                            //làm chưa xong 
                            frmPhong frm = new frmPhong(_user, _uRight.USER_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "SANPHAM":
                        {
                            frmSP_DV frm = new frmSP_DV(_user, _uRight.USER_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "PHONG_TB":
                        {
                            //chưa làm xong
                            frm_Phong_ThietBi frm = new frm_Phong_ThietBi(_user, _uRight.USER_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "THIETBI":
                        {
                            frmThietBi frm = new frmThietBi(_user, _uRight.USER_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "CONGTY":
                        {
                            frmCongTy frm = new frmCongTy(_user, _uRight.USER_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "DONVI":
                        {
                            frmDonVi frm = new frmDonVi(_user, _uRight.USER_RIGHT.Value);
                            frm.ShowDialog();
                            break;
                        }
                    case "NGUOIDUNG":
                        {
                            frmMainUser frm = new frmMainUser();
                            frm.ShowDialog();
                            break;
                        }
                    case "DOIMATKHAU":
                        {
                            frmUser frm = new frmUser();
                            frm.ShowDialog();
                            break;
                        }
                } 
            }      
        }

        private void btnHeThong_Click(object sender, EventArgs e)
        {
           
        }

        private void btnBaocao_Click(object sender, EventArgs e)
        {
            frmBaoCao frm = new frmBaoCao(_user);
            frm.ShowDialog();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void popupMenu1_Popup(object sender, EventArgs e)
        {
            Point point = gControl.PointToClient(Control.MousePosition);
            RibbonHitInfo hitInfo = gControl.CalcHitInfo(point);
            if (hitInfo.InGalleryItem || hitInfo.HitTest == RibbonHitTest.GalleryImage)
                item = hitInfo.GalleryItem;
        }

        private void btnDatPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Kiểm tra xem phòng có trống chưa, nếu đã đặt rồi thì sẽ hiện ra thông báo đã đặt
            if(_phong.checkEmpty(int.Parse(item.Value.ToString())))
            {
                MessageBox.Show("Phòng đã được đặt. Vui lòng chọn phòng khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }    
            /*Khởi tạo chạy lên formDatPhong*/
            frmDatPhongDon frm = new frmDatPhongDon();
            frm._idPhong = int.Parse(item.Value.ToString());
            frm._them = true;
            frm.ShowDialog();
        }


        private void btnChuyenPhong_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_phong.checkEmpty(int.Parse(item.Value.ToString())))
            {
                MessageBox.Show("Phòng chưa được đặt nên không được phép chuyển . Vui lòng chọn phòng khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            /*Khởi tạo chạy lên formDatPhong*/
            frmChuyenPhong frm = new frmChuyenPhong();
            frm._idPhong = int.Parse(item.Value.ToString());

            frm.ShowDialog();
        }

        private void btnSPDV_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Nếu phòng chưa đặt
            if (!_phong.checkEmpty(int.Parse(item.Value.ToString())))
            {
                MessageBox.Show("Phòng chưa được đặt nên không cập nhật Sản phẩm - Dịch vụ được . Vui lòng chọn phòng khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            /*Khởi tạo chạy lên formDatPhong*/
            frmDatPhongDon frm = new frmDatPhongDon();
            frm._idPhong = int.Parse(item.Value.ToString());
            frm._them = false;
            frm.ShowDialog();
        }

        private void btnThanhToan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!_phong.checkEmpty(int.Parse(item.Value.ToString())))
            {
                MessageBox.Show("Phòng chưa được đặt nên không thể thanh toán được . Vui lòng chọn phòng khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            /*Khởi tạo chạy lên formDatPhong*/
            frmDatPhongDon frm = new frmDatPhongDon();
            frm._idPhong = int.Parse(item.Value.ToString());
            frm._them = false;
            frm.ShowDialog();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}