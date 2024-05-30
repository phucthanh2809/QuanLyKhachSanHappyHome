using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataLayer;
using BusinessLayer;
using USERMANAGEMENT.MyComponents;

namespace USERMANAGEMENT
{
    public partial class frmMainUser : DevExpress.XtraEditors.XtraForm
    {
        public frmMainUser()
        {
            InitializeComponent();
        }

        public frmMainUser(tb_SYS_USER user, int right)
        {
            InitializeComponent();
            //truyền đối tượng user vào
            this._user = user;
            this._right = right;
            //Hiển thị tiêu đề trên form main 
            this.Text = "PHẦN MỀM QUẢN LÝ KHÁCH SẠN - NGƯỜI ĐANG SỬ DỤNG: " + _user.FULLNAME;
        }

        int _right;
        tb_SYS_USER _user;
        MyTreeViewCombo _treeView;
        CONGTY _congty;
        DONVI _donvi;
        SYS_USER _sysUser;
        bool _isRoot;
        string _macty;
        string _madvi;

        private void frmMain_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            _donvi = new DONVI();
            _sysUser = new SYS_USER();
            _isRoot = true;
            loadTreeView();
            //Đầu tiên khi load Form lên chưa biết là cái nào nên sẽ cho là CT01
            loadUser("CT01","~");
        }
        public void loadUser(string macty, string madvi)
        {
            _sysUser = new SYS_USER();
            gcUser.DataSource = _sysUser.getUserByDVI(macty,madvi);
            gvUser.OptionsBehavior.Editable = false;
        }

        void loadTreeView()
        {
            //độ rộng treeview = panel Nhóm, độ cao 300
            _treeView = new MyTreeViewCombo(pnNhom.Width, 400);
            //font cho treeview
            _treeView.Font = new Font("Tahoma", 10, FontStyle.Bold);
            //Cho treeview 1 cái list ra
            var lstCTY = _congty.getAll();
            foreach (var item in lstCTY)
            {
                //Node cha là công ty, node con là đơn vị. Còn các node cha là công ty chi nhánh và con là khách sạn 
                TreeNode parentNode = new TreeNode();
                parentNode.Text = item.MACTY + " - " + item.TENCTY;
                parentNode.Tag = item.MACTY;
                parentNode.Name = item.MACTY;
                _treeView.TreeView.Nodes.Add(parentNode);

                //Cấp 2:Tạo ra mã đơn vị
                foreach (var dv in _donvi.getAll(item.MACTY)) 
                {
                    TreeNode childdNode = new TreeNode();
                    childdNode.Text = dv.MADVI + " - " + dv.TENDVI;
                    childdNode.Tag = dv.MACTY +"."+ dv.MADVI;
                    childdNode.Name = dv.MACTY + "." + dv.MADVI;
                    //Truy cập Node cha và add Node con vào 
                    _treeView.TreeView.Nodes[parentNode.Name].Nodes.Add(childdNode);
                }
            }
            //Khi add xong hết vào rồi thì expand ra 
            _treeView.TreeView.ExpandAll();
            pnNhom.Controls.Add(_treeView);
            _treeView.Width = pnNhom.Width;
            _treeView.Height = pnNhom.Height;
            _treeView.TreeView.AfterSelect += TreeView_AfterSelect;
            _treeView.TreeView.Click += TreeView_Click;

            _treeView.KeyPress += _treeView_KeyPress;
            _treeView.TextChanged += _treeView_TextChanged;
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _treeView.Text = _treeView.TreeView.SelectedNode.Text;
            if (_treeView.TreeView.SelectedNode.Parent==null)
            {
                _isRoot = true;
                _macty = _treeView.TreeView.SelectedNode.Tag.ToString();
                //~ : tượng trưng cho người không thuộc đơn vị nào mà chỉ thuộc chi nhánh công ty 
                _madvi = "~";
            }    
            else
            {
                _isRoot = false;
                ///*  vd: substring     input: str = "geeksforgeeks"
                //    str.substring(5);
                //*/        output: forgeeks
                //VD CT01.CTKSHP
                _macty = _treeView.TreeView.SelectedNode.Name.Substring(0,4);
                _madvi = _treeView.TreeView.SelectedNode.Name.Substring(5); 
            }
            //Sau khi chọn xong thì mới loadUser 
            loadUser(_macty, _madvi);
            _treeView.dropDown.Close();
        }
        private void _treeView_TextChanged(object Sender, EventArgs e)
        {
            _isRoot = true;
            loadUser(_macty, _madvi);
        }
        private void _treeView_KeyPress(object Sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != (char)Keys.Back && e.KeyChar != (char)Keys.Delete && e.KeyChar != (char)Keys.Enter)
                e.Handled = true;
        }
        private void TreeView_Click(object sender, EventArgs e)
        {
            _treeView.dropDown.Focus();
            _treeView.SelectAll();
        }

        private void btnGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_treeView.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Đơn vị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmGroup frm = new frmGroup();
            frm._them = true;
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();
        } 

        private void btnUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_treeView.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Đơn vị", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmUser frm = new frmUser();
            frm._them = true;
            frm._macty = _macty;
            frm._madvi = _madvi;
            //frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
            frm.ShowDialog();
        }

        private void btnCapNhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvUser.RowCount > 0 && gvUser.GetFocusedRowCellValue("ISGROUP").Equals(true))
            {
                frmGroup frm = new frmGroup();
                frm._them = false;
                frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
                frm.ShowDialog();
            }
            else
            {
                frmUser frm = new frmUser();
                frm._them = false;
                frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
                frm.ShowDialog();
            }    
        }

        private void btnChucNang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPhanQuyenChucNang frm = new frmPhanQuyenChucNang();
            frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();
        }

        private void btnBaoCao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPhanQuyenBaoCao frm = new frmPhanQuyenBaoCao();
            frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();
        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        //private void gvUser_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        //{
        //    if (e.Column.Name == "ISGROUP" && bool.Parse(e.CellValue.ToString()) == true)
        //    {
        //        Image img = Properties.Resources.Team_16x16;
        //        e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
        //        e.Handled = true; 
        //    }
        //    if (e.Column.Name == "ISGROUP" && bool.Parse(e.CellValue.ToString()) == false)
        //    {
        //        Image img = Properties.Resources.Customer_16x16_;
        //        e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
        //        e.Handled = true;
        //    }
        //    if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
        //    {
        //        Image img = Properties.Resources._1398919_close_cross_incorrect_invalid_x_icon;
        //        e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
        //        e.Handled = true;
        //    }
        //}

        private void gvUser_DoubleClick(object sender, EventArgs e)
        {
            if (gvUser.RowCount > 0 && gvUser.GetFocusedRowCellValue("ISGROUP").Equals(true) )
            {
                frmGroup frm = new frmGroup();
                frm._them = false;
                frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
                frm.ShowDialog();
            }
            else
            {
                frmUser frm = new frmUser();
                frm._them = false;
                frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
                frm.ShowDialog();
            }    
        }

        private void gvUser_CustomDrawCell_1(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "ISGROUP" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources.Team_16x162;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
            if (e.Column.Name == "ISGROUP" && bool.Parse(e.CellValue.ToString()) == false)
            {
                Image img = Properties.Resources.Customer_16x16_2;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
            if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources._1398919_close_cross_incorrect_invalid_x_icon;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;
            }
        }
    }
}