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

namespace C_
{
    public partial class FormLopHoc : Form
    {
        SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=Quanlysinhvien;Trusted_Connection=True;");
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Quanlysinhvien;Integrated Security=True";
        public FormLopHoc()
        {
            InitializeComponent();
            dataGridView1.CellClick += dataGridView1_CellClick;
        }
        void LoadLopHoc()
        {
           
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "SELECT * FROM Lophoc";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            
            conn.Close();
        }
        void Clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }


        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {
        }

        private void timkiem(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Lophoc WHERE 1=1";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

              
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    query += " AND Tenlop LIKE @Tenlop";
                    cmd.Parameters.AddWithValue("@Tenlop", "%" + textBox1.Text.Trim() + "%");
                    Clear();
                }

              
                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    query += " AND Malop LIKE @Malop";
                    cmd.Parameters.AddWithValue("@Malop", "%" + textBox2.Text.Trim() + "%");
                    Clear();
                }

               
                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    query += " AND Khoa LIKE @Khoa";
                    cmd.Parameters.AddWithValue("@Khoa", "%" + textBox3.Text.Trim() + "%");
                    Clear();
                }


              
                if (!string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    query += " AND Chuyennganh LIKE @Chuyennganh";
                    cmd.Parameters.AddWithValue("@Chuyennganh", "%" + textBox5.Text.Trim() + "%");
                    Clear();
                }

                if (!string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    query += " AND Soluongsinhvien LIKE @Soluongsinhvien";
                    cmd.Parameters.AddWithValue("@Soluongsinhvien", "%" + textBox4.Text.Trim() + "%");
                    Clear();
                }

               
                cmd.CommandText = query;

               
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);

                 
                    dataGridView1.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy Lớp nào khớp với thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);

                }
            }
        }

        private void xoa(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
             
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    string malop = dataGridView1.CurrentRow.Cells["Malop"].Value.ToString();

                    string query = "DELETE FROM Lophoc WHERE Malop = @Malop";

                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@Malop", malop);

                            int rows = command.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                MessageBox.Show("Xóa thành công!");
                              
                                LoadLopHoc();
                                Clear();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một dòng để xóa!");
            }
        }

        private void them(object sender, EventArgs e)
        {
            string query = "INSERT INTO Lophoc (Tenlop, Malop, Khoa, Chuyennganh, Soluongsinhvien) " +
                          "VALUES (@Tenlop, @Malop, @Khoa, @Chuyennganh, @Soluongsinhvien)";

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    string malop = textBox2.Text.Trim();
                    if (malop.Length == 7)
                    {
                        cmd.Parameters.AddWithValue("@Malop", textBox2.Text.Trim());
                    }
                    else
                    {
                       
                        MessageBox.Show("Vui lòng nhập Mã sinh viên đúng định dạng 7 ký tự (VD: 1234567)!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cmd.Parameters.AddWithValue("@Tenlop", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Chuyennganh", textBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@Khoa", textBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Soluongsinhvien", textBox4.Text.Trim());


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm Lớp thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadLopHoc();
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        private void sua(object sender, EventArgs e)
        {
           
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Vui lòng chọn một Lớp từ danh sách để sửa!", "Thông báo");
                return;
            }

            try
            {
                string connStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                   
                    string sql = "UPDATE Lophoc SET Tenlop = @Tenlop, Chuyennganh = @Chuyennganh, Khoa = @Khoa, Soluongsinhvien = @Soluongsinhvien WHERE Malop = @Malop";

                    SqlCommand cmd = new SqlCommand(sql, conn);

            
                    cmd.Parameters.AddWithValue("@Malop", textBox2.Text); 
                    cmd.Parameters.AddWithValue("@Tenlop", textBox1.Text); 
                    cmd.Parameters.AddWithValue("@Khoa", textBox3.Text);
                    cmd.Parameters.AddWithValue("@Soluongsinhvien", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Chuyennganh", textBox5.Text); 
                  

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Sửa thông tin sinh viên thành công!");

                        LoadLopHoc(); 
                        Clear();      
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy Lớp để sửa!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
            }
        }

        private void FormLopHoc_Load(object sender, EventArgs e)
        {
            LoadLopHoc();
            Clear();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textBox2.Text = row.Cells["Malop"].Value?.ToString();
                textBox1.Text = row.Cells["Tenlop"].Value?.ToString();
                textBox3.Text = row.Cells["Khoa"].Value?.ToString();
                textBox4.Text = row.Cells["Soluongsinhvien"].Value?.ToString();
                textBox5.Text = row.Cells["Chuyennganh"].Value?.ToString();
            }
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Tenlop"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Khoa"].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["Chuyennganh"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Malop"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["Soluongsinhvien"].Value.ToString();
        }
        }
}
