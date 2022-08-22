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
    public partial class TimKiemNguoiUngTuyen : Form
    {
        MongoDatabase db;
        public TimKiemNguoiUngTuyen()
        {
            InitializeComponent();
            var connectionString = "mongodb://localhost:27017/admin";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            db = server.GetDatabase("QL");
        }
        public void loaddata()
        {
            dataGridView1.DataSource = readToTable("QL", "NVUngTuyen");
        }
        public DataTable readToTable(string databaseName, string collectionName)
        {
            string[] attribute = new string[] { "MaUT", "Hoten", "NgaySinh", "DiaChi", "Gmail", "SDT", "CongTy", "TrinhDo" };
            DataTable datatable = new DataTable();
            //Create datatable
            for (int i = 0; i < attribute.Length; i++)
            {
                datatable.Columns.Add(attribute[i]);
            }
            if (textBox1.Text == "")
            {
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
            }
            else
            {
                var collection = db.GetCollection<BsonDocument>(collectionName);
                var query = new QueryDocument("MaUT", textBox1.Text);
                foreach (BsonDocument item in collection.FindAll())
                {
                    DataRow newrow = datatable.NewRow();
                    for (int j = 0; j < attribute.Length; j++)
                    {
                        newrow[j] = item.GetElement(attribute[j]).Value.ToString();
                    }
                    datatable.Rows.Add(newrow);
                }
            }
            return datatable;
        }
        public DataTable TimKiem(string databaseName, string collectionName)
        {
            string[] attribute = new string[] { "MaUT", "Hoten", "NgaySinh", "DiaChi", "Gmail", "SDT", "CongTy", "TrinhDo" };
            DataTable datatable = new DataTable();
            //Create datatable
            for (int i = 0; i < attribute.Length; i++)
            {
                datatable.Columns.Add(attribute[i]);
            }
            if (textBox1.Text == "")
            {
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
            }
            else
            {
                var collection = db.GetCollection<BsonDocument>(collectionName);
                var query = new QueryDocument("MaUT", textBox1.Text);
                foreach (BsonDocument item in collection.Find(query))
                {
                    DataRow newrow = datatable.NewRow();
                    for (int j = 0; j < attribute.Length; j++)
                    {
                        newrow[j] = item.GetElement(attribute[j]).Value.ToString();
                    }
                    datatable.Rows.Add(newrow);
                }
            }
            return datatable;
        }
        private void TimKiemNguoiUngTuyen_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = TimKiem("QL", "NVUngTuyen");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loaddata();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
