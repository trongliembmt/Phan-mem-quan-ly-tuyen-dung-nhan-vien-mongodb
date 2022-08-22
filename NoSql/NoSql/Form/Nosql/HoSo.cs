using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
namespace Nosql
{
    public partial class HoSo : Form
    {
        MongoDatabase db;
        public HoSo()
        {
            InitializeComponent();
            var connectionString = "mongodb://localhost:27017/admin";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            db = server.GetDatabase("QL");
            loaddata();
        }
        public void loaddata()
        {
            dataGridView1.DataSource = readToTable("QL", "NVUngTuyen");
        }
        public DataTable readToTable(string databaseName, string collectionName)
        {
            string[] attribute = new string[] { "MaUT", "Hoten", "NgaySinh", "DiaChi", "Gmail", "SDT", "TrinhDo", "CongTy" };
            DataTable datatable = new DataTable();
            //Create datatable
            for (int i = 0; i < attribute.Length; i++)
            {
                datatable.Columns.Add(attribute[i]);
            }
            var collection = db.GetCollection<BsonDocument>(collectionName);
            foreach (BsonDocument item in collection.FindAll())
            {
                DataRow newrow = datatable.NewRow();
                for (int j = 0; j < attribute.Length; j++)
                {
                    newrow[j] = item.GetElement(attribute[j]).Value.ToString();
                }
                datatable.Rows.Add(newrow);
            }
            return datatable;
        }
        private void HoSo_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            maskedTextBox1.Clear();
            textBox1.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var collection = db.GetCollection<BsonDocument>("NVUngTuyen");

            BsonDocument document = new BsonDocument()
                    .Add("MaUT", textBox1.Text)
                    .Add("Hoten", textBox2.Text)
                    .Add("NgaySinh", dateTimePicker1.Text)
                    .Add("DiaChi", textBox3.Text)
                    .Add("Gmail", textBox4.Text)
                    .Add("SDT", maskedTextBox1.Text)
                    .Add("TrinhDo", textBox6.Text)
                    .Add("CongTy", textBox5.Text);        
            collection.Insert(document);
            DataTable dataTable = (DataTable)dataGridView1.DataSource;
            DataRow newrow = dataTable.NewRow();
            newrow[0] = textBox1.Text;
            newrow[1] = textBox2.Text;
            newrow[2] = dateTimePicker1.Text;
            newrow[3] = textBox3.Text;
            newrow[4] = textBox4.Text;
            newrow[5] = maskedTextBox1.Text;
            newrow[6] = textBox6.Text;
            newrow[7] = textBox5.Text;
            dataTable.Rows.Add(newrow);
            dataTable.AcceptChanges();
            MessageBox.Show("Thêm Thành Công !!!");
            loaddata();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                maskedTextBox1.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                textBox5.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            }
            dataGridView1.ReadOnly = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var collection = db.GetCollection<BsonDocument>("NVUngTuyen");
                var query = new QueryDocument("MaUT", textBox1.Text);
                collection.Remove(query);
                DataTable dataTable = (DataTable)dataGridView1.DataSource;
                int index = dataGridView1.SelectedRows[0].Index;
                dataTable.Rows.RemoveAt(index);
                MessageBox.Show("Xóa Thành Công !!!");
            }
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            maskedTextBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var collection = db.GetCollection<BsonDocument>("NVUngTuyen");
                var query = new QueryDocument("MaUT", textBox1.Text);
                collection.Remove(query);
                BsonDocument nhanvien = new BsonDocument()
                        .Add(getID(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                        .Add("MaUT", textBox1.Text)
                        .Add("Hoten", textBox2.Text)
                        .Add("NgaySinh", dateTimePicker1.Text)
                        .Add("DiaChi", textBox3.Text)
                        .Add("Gmail", textBox4.Text)
                        .Add("SDT", maskedTextBox1.Text)
                        .Add("TrinhDo", textBox6.Text)
                        .Add("CongTy", textBox5.Text);              

                collection.Save(nhanvien);
                int index = dataGridView1.SelectedRows[0].Index;
                DataTable dataTable = (DataTable)dataGridView1.DataSource;
                dataTable.Rows[index][0] = textBox1.Text;
                dataTable.Rows[index][1] = textBox2.Text;
                dataTable.Rows[index][2] = dateTimePicker1.Text;
                dataTable.Rows[index][3] = textBox3.Text;
                dataTable.Rows[index][4] = textBox4.Text;
                dataTable.Rows[index][5] = maskedTextBox1.Text;
                dataTable.Rows[index][6] = textBox6.Text;
                dataTable.Rows[index][7] = textBox5.Text;
                MessageBox.Show("Cập Nhật Thành Công !!!");
            }
        }
        public BsonElement getID(string manv)
        {
            var collection = db.GetCollection<BsonDocument>("NVUngTuyen");
            var query = new QueryDocument("MaUT", manv);
            foreach (BsonDocument item in collection.Find(query))
            {
                return item.GetElement("_id");
            }
            return null;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.ShowDialog();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
