using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class QuanLiKhachSan : Form
    {
        SqlConnection con = new SqlConnection();
        public QuanLiKhachSan()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=QLKhachSan;Integrated Security=True";
            con.ConnectionString = connectionString;
            con.Open();
            string sql = "Select * From tblPhongKS";
            
            SqlDataAdapter apd = new SqlDataAdapter(sql, con);
            DataTable tableQuanLyKhachSan = new DataTable();
            apd.Fill(tableQuanLyKhachSan);
            dataGridView1.DataSource = tableQuanLyKhachSan;
            loaddatagr();
        }
        private void loaddatagr()
        {
            string sql = "Select * From tblPhongKS";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter apd = new SqlDataAdapter(sql, con);
            DataTable tableQuanLyKhachSan = new DataTable();
            apd.Fill(tableQuanLyKhachSan);  
            dataGridView1.DataSource = tableQuanLyKhachSan;
        }

   

        private void button1_Click(object sender, EventArgs e)
        {
            txtMaPhong.Enabled = true;
            txtTenPhong.Text = "";
            txtDongia.Text = "";
            txtMaPhong.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtMaPhong.ReadOnly = true;
            string sql = "update tblPhongKS set TenPhong = '" + txtTenPhong.Text.ToString() + "',"
              + "DonGia = '" + txtDongia.Text.ToString() + "'where MaPhong = '" + txtMaPhong.Text + "'";
            MessageBox.Show(sql);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loaddatagr();
        }

        private void bnt3Xoa_Click(object sender, EventArgs e)
        {
            string sql = "Delete From tblPhongKS Where MaPhong; = '" + txtMaPhong.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            loaddatagr();
        }

        private void bntLuu_Click(object sender, EventArgs e)
        {
            if (txtMaPhong.Text == "")
            {
                MessageBox.Show("Bạn Cần nhập mã phòng");
                txtMaPhong.Focus();
                return;
            }
            if (txtTenPhong.Text == "")
            {
                MessageBox.Show("Bạn cần nhập tên phòng");
                txtTenPhong.Focus();
            }
            else
            {
                string sql = "insert into QuanLyKS values('" + txtMaPhong.Text + "', '" + txtTenPhong.Text + "'";
                if (txtDongia.Text != "")
                    sql = sql + " , " + txtDongia.Text.Trim();
                sql = sql + " ) ";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                loaddatagr();
            }
        }

        private void bntHuy_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtDongia.Text = "";
            bntHuy.Enabled = false;
            button1.Enabled = true;
            loaddatagr();
        }

        private void bntThoat_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();
        }

        private void txtDongia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) ||
               (Convert.ToInt32(e.KeyChar) == 8|| (Convert.ToInt32(e.KeyChar) == 13)))
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

   

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPhong.Text = dataGridView1.CurrentRow.Cells["MaPhong"].Value.ToString();
            txtTenPhong.Text = dataGridView1.CurrentRow.Cells["TenPhong"].Value.ToString();
            txtDongia.Text = dataGridView1.CurrentRow.Cells["DonGia"].Value.ToString();
        }
    }
}
