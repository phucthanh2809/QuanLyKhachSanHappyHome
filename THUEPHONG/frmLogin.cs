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
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace THUEPHONG
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        SYS_PARAM _sysParam;
        SYS_USER _sysUser;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Nếu tên đăng nhập trống
           if (txtUsername.Text.Trim()=="")
            {
                MessageBox.Show("Tên đăng nhập không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           //Nếu tên đăng nhập không tồn tại trong CSDL
            bool us = _sysUser.checkUserExist(_sysParam.macty, _sysParam.madvi,txtUsername.Text.Trim());
           if (!us)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
           //so sánh mật khẩu khi người dùng nhập vào với database xem có đúng không
            string pass = Encryptor.Encrypt(txtPassWord.Text, "qwert@123!poiuy",true);
            tb_SYS_USER user = _sysUser.getItem(txtUsername.Text.Trim(), _sysParam.macty, _sysParam.madvi);
            if (user.PASSWD.Equals(pass))
            {
                //Nếu mà pass = nhau thì truyền thẳng cái thông tin user vào để lấy thông tin user 
                frmMain frm = new frmMain(user);
                frm.ShowDialog();
                this.Hide();
            }    
            else
            {
                MessageBox.Show("Mật khẩu đăng nhập không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }    
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            _sysUser = new SYS_USER();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open("sysparam.ini", FileMode.Open, FileAccess.Read);
            _sysParam = (SYS_PARAM)bf.Deserialize(fs);
            fs.Close();
            myFunctions._macty = _sysParam.macty;
            myFunctions._madvi = _sysParam.madvi;
        }
    }
}