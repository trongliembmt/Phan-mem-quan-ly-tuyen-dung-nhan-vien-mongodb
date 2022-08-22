using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
namespace Nosql
{
    public partial class Form1 : Form
    {
        MongoClientSettings settings = new MongoClientSettings();
        public void Loaddata()
        {
            settings.Server = new MongoServerAddress("localhost", 27017);
            MongoClient client = new MongoClient();
            var db = client.GetDatabase("QUANLITHONGTINNHANVIEN");
            var collection = db.GetCollection<NV>("NhanVien");
            var query = collection.AsQueryable<NV>().ToList();
            dataGridView1.DataSource = query;
            dataGridView1.ReadOnly = true;
        }
        public Form1()
        {
            InitializeComponent();
            Loaddata();
            label1.Enabled = label2.Enabled = label3.Enabled = label4.Enabled = label5.Enabled = label6.Enabled = false;
            textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = false;
            maskedTextBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
        }
        //public void dataBindings(DataTable pTable)
        //{
        //    textBox1.DataBindings.Clear();
        //    textBox2.DataBindings.Clear();
        //    textBox1.DataBindings.Add("Text", pTable, "Manv", true, DataSourceUpdateMode.Never);
        //    textBox2.DataBindings.Add("Text", pTable, "Tennv", true, DataSourceUpdateMode.Never);
        //    dateTimePicker1.DataBindings.Add("Text", pTable, "Ngaysinh", true, DataSourceUpdateMode.Never);
        //    textBox3.DataBindings.Add("Text", pTable, "Diachi", true, DataSourceUpdateMode.Never);
        //    maskedTextBox1.DataBindings.Add("Text", pTable, "Sdt", true, DataSourceUpdateMode.Never);
  

        //}
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Bạn muốn đóng chương trình không?", "Thông báo", MessageBoxButtons.YesNo,
           MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Yes)
                Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Enabled = label2.Enabled = label3.Enabled = label4.Enabled = label5.Enabled = label6.Enabled = true;
            textBox1.Enabled = textBox2.Enabled = textBox3.Enabled = true;
            maskedTextBox1.Enabled = true;
            dateTimePicker1.Enabled = true;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now;
            maskedTextBox1.Clear();
            textBox1.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            settings.Server = new MongoServerAddress("localhost", 27017);
            MongoClient client = new MongoClient();
            var db = client.GetDatabase("QUANLITHONGTINNHANVIEN");
            var collection = db.GetCollection<NV>("NhanVien");
            NV nv = new NV();
            nv.Manv = textBox1.Text;
            nv.Tennv = textBox2.Text;
            nv.Ngaysinh = dateTimePicker1.Text.Trim();
            nv.Diachi = textBox3.Text;
            nv.Sdt = maskedTextBox1.Text.Trim();
            collection.InsertOne(nv);
            MessageBox.Show("Lưu thành công!");
            Loaddata();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
    public class NV
    {
        public Object id { get; set; }
        public string Manv { get; set; }
        public string Tennv { get; set; }
        public string Ngaysinh { get; set; }
        public string Diachi { get; set; }
        public string Sdt { get; set; }
    }
}
