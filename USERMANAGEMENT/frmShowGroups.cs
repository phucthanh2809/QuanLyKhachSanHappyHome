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

namespace USERMANAGEMENT
{
    public partial class frmShowGroups : DevExpress.XtraEditors.XtraForm
    {
        public frmShowGroups()
        {
            InitializeComponent();
        }
        public string _macty;
        public string _madvi;
        public int _idUser;
        SYS_GROUP _sysGroup;
        VIEW_USER_IN_GROUP _vUserInGroup;

        frmUser objUser = (frmUser)Application.OpenForms["frmUser"];

        private void frmShowGroups_Load(object sender, EventArgs e)
        {  
            _sysGroup = new SYS_GROUP();
            _vUserInGroup = new VIEW_USER_IN_GROUP();
            loadGroup();
        }
        void loadGroup()
        {
            gcNhom.DataSource = _vUserInGroup.getGroupByDonVi(_macty, _madvi);
            gvNhom.OptionsBehavior.Editable = false;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_vUserInGroup.checkGroupByUser(_idUser, int.Parse(gvNhom.GetFocusedRowCellValue("IDUSER").ToString())))
            {
                MessageBox.Show("Người dùng đã tồn tại trong nhóm. Vui lòng chọn nhóm khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            tb_SYS_GROUP1 gr = new tb_SYS_GROUP1();
            gr.GROUP = int.Parse(gvNhom.GetFocusedRowCellValue("IDUSER").ToString());
            gr.MEMBER = _idUser;
            _sysGroup.add(gr);
            objUser.loadGroupByUser(_idUser);
            this.Close();
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}