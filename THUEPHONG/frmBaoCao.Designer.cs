
namespace THUEPHONG
{
    partial class frmBaoCao
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBaoCao));
            this.splBaoCao = new DevExpress.XtraEditors.SplitContainerControl();
            this.lstDanhSach = new DevExpress.XtraEditors.ImageListBoxControl();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            this.btnThucHien = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splBaoCao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splBaoCao.Panel1)).BeginInit();
            this.splBaoCao.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splBaoCao.Panel2)).BeginInit();
            this.splBaoCao.Panel2.SuspendLayout();
            this.splBaoCao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstDanhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // splBaoCao
            // 
            this.splBaoCao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splBaoCao.Location = new System.Drawing.Point(0, 0);
            this.splBaoCao.Name = "splBaoCao";
            // 
            // splBaoCao.Panel1
            // 
            this.splBaoCao.Panel1.Controls.Add(this.lstDanhSach);
            this.splBaoCao.Panel1.Text = "Panel1";
            // 
            // splBaoCao.Panel2
            // 
            this.splBaoCao.Panel2.Controls.Add(this.btnDong);
            this.splBaoCao.Panel2.Controls.Add(this.btnThucHien);
            this.splBaoCao.Panel2.Text = "Panel2";
            this.splBaoCao.Size = new System.Drawing.Size(1033, 707);
            this.splBaoCao.SplitterPosition = 409;
            this.splBaoCao.TabIndex = 0;
            // 
            // lstDanhSach
            // 
            this.lstDanhSach.Appearance.Font = new System.Drawing.Font("Tahoma", 11F);
            this.lstDanhSach.Appearance.Options.UseFont = true;
            this.lstDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstDanhSach.Location = new System.Drawing.Point(0, 0);
            this.lstDanhSach.Name = "lstDanhSach";
            this.lstDanhSach.Size = new System.Drawing.Size(409, 707);
            this.lstDanhSach.TabIndex = 0;
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDong.ImageOptions.Image")));
            this.btnDong.Location = new System.Drawing.Point(277, 589);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(126, 49);
            this.btnDong.TabIndex = 1;
            this.btnDong.Text = "Đóng";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // btnThucHien
            // 
            this.btnThucHien.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.btnThucHien.Appearance.Options.UseFont = true;
            this.btnThucHien.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnThucHien.ImageOptions.Image")));
            this.btnThucHien.Location = new System.Drawing.Point(106, 589);
            this.btnThucHien.Name = "btnThucHien";
            this.btnThucHien.Size = new System.Drawing.Size(126, 49);
            this.btnThucHien.TabIndex = 0;
            this.btnThucHien.Text = "Thực hiện";
            this.btnThucHien.Click += new System.EventHandler(this.btnThucHien_Click);
            // 
            // frmBaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 707);
            this.Controls.Add(this.splBaoCao);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBaoCao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo";
            this.Load += new System.EventHandler(this.frmBaoCao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splBaoCao.Panel1)).EndInit();
            this.splBaoCao.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splBaoCao.Panel2)).EndInit();
            this.splBaoCao.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splBaoCao)).EndInit();
            this.splBaoCao.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstDanhSach)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splBaoCao;
        private DevExpress.XtraEditors.ImageListBoxControl lstDanhSach;
        private DevExpress.XtraEditors.SimpleButton btnThucHien;
        private DevExpress.XtraEditors.SimpleButton btnDong;
    }
}