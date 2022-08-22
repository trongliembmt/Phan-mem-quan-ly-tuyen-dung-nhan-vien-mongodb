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
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
namespace Nosql
{
    public partial class Form1 : Form
    {
        MongoDatabase db;
        public Form1()
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
            dataGridView1.DataSource = readToTable("QL", "NV");
        }
        public DataTable readToTable(string databaseName, string collectionName)
        {
            string[] attribute = new string[] { "Manv", "Tennv", "Ngaysinh", "Diachi", "Sdt" };
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
        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var collection = db.GetCollection<BsonDocument>("NV");

            BsonDocument document = new BsonDocument()
                    .Add("Manv", textBox1.Text)
                    .Add("Tennv", textBox2.Text)
                    .Add("Ngaysinh", dateTimePicker1.Text)
                    .Add("Diachi", textBox3.Text)
                    .Add("Sdt", maskedTextBox1.Text);
            collection.Insert(document);
            DataTable dataTable = (DataTable)dataGridView1.DataSource;
            DataRow newrow = dataTable.NewRow();
            newrow[0] = textBox1.Text;
            newrow[1] = textBox2.Text;
            newrow[2] = dateTimePicker1.Text;
            newrow[3] = textBox3.Text;
            newrow[4] = maskedTextBox1.Text;
            dataTable.Rows.Add(newrow);
            dataTable.AcceptChanges();
            MessageBox.Show("Thêm Thành Công !!!");
            loaddata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Enabled = true;
            label3.Enabled = true;
            label5.Enabled = true;
            label6.Enabled = true;
            label7.Enabled = true;
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            textBox3.Enabled = true;
            dateTimePicker1.Enabled = true;
            maskedTextBox1.Enabled = true;
            button4.Enabled = true;
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now;
            maskedTextBox1.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var collection = db.GetCollection<BsonDocument>("NV");
                var query = new QueryDocument("Manv", textBox1.Text);
                collection.Remove(query);
                DataTable dataTable = (DataTable)dataGridView1.DataSource;
                int index = dataGridView1.SelectedRows[0].Index;
                dataTable.Rows.RemoveAt(index);
                MessageBox.Show("Xóa Thành Công !!!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var collection = db.GetCollection<BsonDocument>("NV");
                var query = new QueryDocument("Manv", textBox1.Text);
                collection.Remove(query);
                BsonDocument nhanvien = new BsonDocument()
                        .Add(getID(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                        .Add("Manv", textBox1.Text)
                        .Add("Tennv", textBox2.Text)
                        .Add("Ngaysinh", dateTimePicker1.Text)
                        .Add("Diachi", textBox3.Text)
                        .Add("Sdt", maskedTextBox1.Text);

                collection.Save(nhanvien);
                int index = dataGridView1.SelectedRows[0].Index;
                DataTable dataTable = (DataTable)dataGridView1.DataSource;
                dataTable.Rows[index][0] = textBox1.Text;
                dataTable.Rows[index][1] = textBox2.Text;
                dataTable.Rows[index][2] = dateTimePicker1.Text;
                dataTable.Rows[index][3] = textBox3.Text;
                dataTable.Rows[index][4] = maskedTextBox1.Text;
                MessageBox.Show("Cập Nhật Thành Công !!!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                dateTimePicker1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                maskedTextBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
            dataGridView1.ReadOnly = true;
        }
        public BsonElement getID(string manv)
        {
            var collection = db.GetCollection<BsonDocument>("NV");
            var query = new QueryDocument("Manv", manv);
            foreach (BsonDocument item in collection.Find(query))
            {
                return item.GetElement("_id");
            }
            return null;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
