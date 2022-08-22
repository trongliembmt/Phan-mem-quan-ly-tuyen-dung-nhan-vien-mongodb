using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nosql
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox2.UseSystemPasswordChar = false;
                var checkbox = (CheckBox)sender;
                checkBox1.Text = "Hide password";
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
                checkBox1.Text = "Show password";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(@"Data Source=MSI\SQL;Initial Catalog=QLTTNV;Integrated Security=True");
            try
            {
                conn.Open();
                string tk = textBox1.Text;
                string mk = textBox2.Text;
                string sql = "select * from DANGNHAP where TENDN='" + tk + "' and MK='" + mk + "'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader rd = cmd.ExecuteReader();
                if (rd.Read() == true)
                {
                    MessageBox.Show("Đăng nhập thành công !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Main formMain = new Main();
                    formMain.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Sai mật khẩu hoặc tên đăng nhập. Không thể đăng nhập !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đăng nhập thất bại !!!");
            }
        }

        private void DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn muốn đóng chương trình không?", "Thông báo", MessageBoxButtons.YesNo,
           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Yes)
                Close();
        }
    }
}
