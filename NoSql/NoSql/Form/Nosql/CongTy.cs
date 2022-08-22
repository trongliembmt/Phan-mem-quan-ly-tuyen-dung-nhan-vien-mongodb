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
    public partial class CongTy : Form
    {
        MongoDatabase db;
        public CongTy()
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
            dataGridView1.DataSource = readToTable("QL", "CongTy");
        }
        public DataTable readToTable(string databaseName, string collectionName)
        {
            string[] attribute = new string[] { "MaCty", "TenCty", "Diachi", "Sdt","CongViec" };
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
        private void CongTy_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var collection = db.GetCollection<BsonDocument>("CongTy");

            BsonDocument document = new BsonDocument()
                    .Add("MaCty", textBox1.Text)
                    .Add("TenCty", textBox2.Text)
                    .Add("Diachi", textBox3.Text)
                    .Add("Sdt", maskedTextBox1.Text)
                    .Add("CongViec", textBox4.Text);
            collection.Insert(document);
            DataTable dataTable = (DataTable)dataGridView1.DataSource;
            DataRow newrow = dataTable.NewRow();
            newrow[0] = textBox1.Text;
            newrow[1] = textBox2.Text;
            newrow[2] = textBox3.Text;
            newrow[3] = maskedTextBox1.Text;
            newrow[4] = textBox4.Text;
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
                textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                maskedTextBox1.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            }
            dataGridView1.ReadOnly = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            maskedTextBox1.Clear();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var collection = db.GetCollection<BsonDocument>("CongTy");
                var query = new QueryDocument("MaCty", textBox1.Text);
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
                var collection = db.GetCollection<BsonDocument>("CongTy");
                var query = new QueryDocument("MaCty", textBox1.Text);
                collection.Remove(query);
                BsonDocument nhanvien = new BsonDocument()
                        .Add(getID(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                        .Add("MaCty", textBox1.Text)
                        .Add("TenCty", textBox2.Text)
                        .Add("Diachi", textBox3.Text)
                        .Add("Sdt", maskedTextBox1.Text)
                        .Add("CongViec", textBox4.Text);

                collection.Save(nhanvien);
                int index = dataGridView1.SelectedRows[0].Index;
                DataTable dataTable = (DataTable)dataGridView1.DataSource;
                dataTable.Rows[index][0] = textBox1.Text;
                dataTable.Rows[index][1] = textBox2.Text;
                dataTable.Rows[index][2] = textBox3.Text;
                dataTable.Rows[index][3] = maskedTextBox1.Text;
                dataTable.Rows[index][4] = textBox4.Text;
                MessageBox.Show("Cập Nhật Thành Công !!!");
            }
        }
        public BsonElement getID(string manv)
        {
            var collection = db.GetCollection<BsonDocument>("CongTy");
            var query = new QueryDocument("MaCty", manv);
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
