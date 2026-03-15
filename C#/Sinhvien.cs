using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace C_
{
    public partial class Sinhvien : Form
    {
        SqlConnection conn = new SqlConnection("Server=.\\SQLEXPRESS;Database=Quanlysinhvien;Trusted_Connection=True;");
        string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=Quanlysinhvien;Integrated Security=True";
        void LoadSinhvien()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            string query = "SELECT * FROM Sinhvien";
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
        void Clear() {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        
            textBox5.Clear();
            Nam.Checked = false; 
            Nữ.Checked = false;
        }
        public Sinhvien()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSinhvien();
            Clear();
           

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        

        private void Nam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["Hoten"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["Malop"].Value.ToString();
            dateTimePicker1.Text = dataGridView1.Rows[e.RowIndex].Cells["Ngaysinh"].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells["Chuyennganh"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["MaSv"].Value.ToString();
            string gt = dataGridView1.Rows[e.RowIndex].Cells["Gioitinh"].Value.ToString();
          

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void them(object sender, EventArgs e)
        {
            string gioiTinh = "";
            if (Nam.Checked)
            {
                gioiTinh = "Nam";
            }
             else if (Nữ.Checked) 
            {
                gioiTinh = "Nữ"; 
            }

            string query = "INSERT INTO Sinhvien (MaSv, Hoten, Tenlop, Ngaysinh, Chuyennganh, Gioitinh) " +
                           "VALUES (@MaSv, @Hoten, @Tenlop, @Ngaysinh, @Chuyennganh, @Gioitinh)";

            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    string maSV = textBox2.Text.Trim();
                    if (maSV.Length == 6)
                    {
                        cmd.Parameters.AddWithValue("@MaSv", textBox2.Text.Trim());
                    }
                    else
                    {
                        
                        MessageBox.Show("Vui lòng nhập Mã sinh viên đúng định dạng 6 ký tự (VD: 123456)!", "Lỗi định dạng", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                     cmd.Parameters.AddWithValue("@Hoten", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@Tenlop", textBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@Ngaysinh", dateTimePicker1.Value.Date);
                    cmd.Parameters.AddWithValue("@Chuyennganh", textBox5.Text.Trim());
                    cmd.Parameters.AddWithValue("@Gioitinh", gioiTinh);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadSinhvien();
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

        private void Nu_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void timkiem(object sender, EventArgs e)
        {
         
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                
                string query = "SELECT * FROM Sinhvien WHERE 1=1";

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;

               
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    query += " AND Hoten LIKE @Hoten";
                    cmd.Parameters.AddWithValue("@Hoten", "%" + textBox1.Text.Trim() + "%");
                    Clear();
                }

                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    query += " AND MaSv LIKE @MaSv";
                    cmd.Parameters.AddWithValue("@MaSv", "%" + textBox2.Text.Trim() + "%");
                    Clear();
                }

                if (!string.IsNullOrWhiteSpace(textBox3.Text))
                {
                    query += " AND Tenlop LIKE @Tenlop";
                    cmd.Parameters.AddWithValue("@Tenlop", "%" + textBox3.Text.Trim() + "%");
                    Clear();
                }


            
                if (!string.IsNullOrWhiteSpace(textBox5.Text))
                {
                    query += " AND Chuyennganh LIKE @Chuyennganh";
                    cmd.Parameters.AddWithValue("@Chuyennganh", "%" + textBox5.Text.Trim() + "%");
                    Clear();
                }

               
                if (Nam.Checked && !Nữ.Checked)
                {
                    query += " AND Gioitinh = @Gioitinh";
                    cmd.Parameters.AddWithValue("@Gioitinh", "Nam");
                    Clear();
                }
                else if (!Nam.Checked && Nữ.Checked)
                {
                    query += " AND Gioitinh = @Gioitinh";
                    cmd.Parameters.AddWithValue("@Gioitinh", "Nữ");
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
                        MessageBox.Show("Không tìm thấy sinh viên nào khớp với thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message);
                
                }
            }
        }

        private void sua(object sender, EventArgs e)
        {
          
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Vui lòng chọn một sinh viên từ danh sách để sửa!", "Thông báo");
                return;
            }

            
            string gioiTinh = "";
            if (Nam.Checked) gioiTinh = "Nam";
            else if (Nữ.Checked) gioiTinh = "Nữ";

            try
            {
               
                string connStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLySinhVien;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

                    string sql = "UPDATE SinhVien SET Hoten = @Hoten, Ngaysinh = @Ngaysinh, Chuyennganh = @Chuyennganh, Gioitinh = @Gioitinh, Tenlop = @Tenlop WHERE MaSv = @MaSv";

                    SqlCommand cmd = new SqlCommand(sql, conn);

                    
                    cmd.Parameters.AddWithValue("@MaSv", textBox2.Text); 
                    cmd.Parameters.AddWithValue("@Hoten", textBox1.Text); 
                    cmd.Parameters.AddWithValue("@Tenlop", textBox3.Text); 
                    cmd.Parameters.AddWithValue("@Ngaysinh", dateTimePicker1.Value.Date); 
                    cmd.Parameters.AddWithValue("@Chuyennganh", textBox5.Text); 
                    cmd.Parameters.AddWithValue("@Gioitinh", gioiTinh); 

                   
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("Sửa thông tin sinh viên thành công!");
                        LoadSinhvien(); 
                        Clear();        
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy sinh viên để sửa!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa: " + ex.Message);
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
                   
                    string maSV = dataGridView1.CurrentRow.Cells["MaSv"].Value.ToString();

                    
                    string query = "DELETE FROM SinhVien WHERE MaSv = @MaSv";

                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();
                            SqlCommand command = new SqlCommand(query, connection);
                            command.Parameters.AddWithValue("@MaSv", maSV);

                            int rows = command.ExecuteNonQuery();
                            if (rows > 0)
                            {
                                MessageBox.Show("Xóa thành công!");
                               
                                LoadSinhvien();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

               
                textBox2.Text = row.Cells["MaSv"].Value?.ToString();
                textBox1.Text = row.Cells["Hoten"].Value?.ToString();
                textBox3.Text = row.Cells["Tenlop"].Value?.ToString();
                dateTimePicker1.Text = row.Cells["Ngaysinh"].Value?.ToString();
                textBox5.Text = row.Cells["Chuyennganh"].Value?.ToString();

                string gt = row.Cells["Gioitinh"].Value?.ToString();
                if (gt == "Nam")
                {
                    Nam.Checked = true; 
                }
                else if (gt == "Nữ")
                {
                    Nữ.Checked = true; 
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
    }
    

      
    
