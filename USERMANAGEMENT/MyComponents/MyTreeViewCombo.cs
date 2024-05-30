using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer;
using BusinessLayer;

namespace USERMANAGEMENT.MyComponents
{// Component giống như Control,kế thừa từ cái có sẵn biến lại thành cái của mình 
    //Khi nó thành cái Component thì sử dụng như Control trong Toolbar
    //Muốn xổ ra danh sách xổ xuống đa cấp như toolbox thì phải biến tấu nó, kế thừa từ Combobox nên ở đây sẽ là TreeView sẽ phân cấp ra cho mình 
    public partial class MyTreeViewCombo : System.Windows.Forms.ComboBox
    {
        ToolStripControlHost treeViewHost;
        public ToolStripDropDown dropDown;
        TreeView treeView;
        public MyTreeViewCombo(int _width, int _height)
        {
            treeView = new TreeView();
            treeView.BorderStyle = BorderStyle.None;
            treeView.Width = _width;
            treeView.Height = _height;
            treeView.Font = new Font("Tahoma", 10, FontStyle.Bold);
            treeViewHost = new ToolStripControlHost(treeView);
            dropDown = new ToolStripDropDown();
            dropDown.Items.Add(treeViewHost);
        }

        public void sizeChanged(int _width, int _height)
        {
            treeView.Width = _width;
            treeView.Height = _height;
        }
        public TreeView TreeView
        {
            get { return treeViewHost.Control as TreeView; }
        }
        public void ShowDropDown()
        {
            if (dropDown != null)
            {
                treeViewHost.Width = DropDownWidth;
                treeViewHost.Height = DropDownHeight;
                dropDown.Show(this, 0, this.Height);
            }
        }

        private const int WM_USER = 0x0400,
                            WM_REFLECT = WM_USER + 0x1C00,
                            WM_COMMAND = 0X0111,
                            CBN_DROPDOWN = 7;
        public static int HIWORD(int n)
        {
            return (n >> 16) & 0xffff;
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (WM_REFLECT + WM_COMMAND))
            {
                if (HIWORD((int)m.WParam) == CBN_DROPDOWN)
                {
                    ShowDropDown();
                    return;
                }    
            }
            base.WndProc(ref m);
        }
        //protected override void Dispose (bool disposing)
        //{
        //    if (disposing)
        //    {
        //        if (dropDown != null)
        //        {
        //            dropDown.Dispose();
        //            dropDown = null;
        //        }    
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
//Phân quyền người dùng | Giao diện MainForm | Sql server | Entity Framework : 1:39:00 
