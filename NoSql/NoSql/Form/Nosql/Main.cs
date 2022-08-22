using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nosql
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 nv = new Form1();
            nv.ShowDialog();
        }

        private void côngTyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            CongTy ct = new CongTy();
            ct.ShowDialog();
        }

        private void hồSơỨngTuyểnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            HoSo hs = new HoSo();
            hs.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            DangNhap dn = new DangNhap();
            dn.ShowDialog();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn muốn đóng chương trình không?", "Thông báo", MessageBoxButtons.YesNo,
            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Yes)
                Close();
        }

        private void nhânViênToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TimKiemNhanVien tk = new TimKiemNhanVien();
            tk.ShowDialog();
        }

        private void côngTyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TimKiemCongTy tk = new TimKiemCongTy();
            tk.ShowDialog();
        }

        private void hồSơỨngTuyểnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Hide();
            TimKiemNguoiUngTuyen tk = new TimKiemNguoiUngTuyen();
            tk.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
