using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Book_Management
{
    /// <summary>
    /// Interaction logic for TaiKhoan.xaml
    /// </summary>
    public partial class TaiKhoan : Window
    {
        private string connectionString = @"Data Source=DESKTOP-HKC8B7E\HONGPHUOC;Initial Catalog=QLNS;Integrated Security=True";
        public TaiKhoan()
        {
            InitializeComponent();
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT [USERNAME], [PASS_WORD] FROM [QLNS].[dbo].[TAIKHOAN]";
                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string tenDangNhap = reader.GetString(0);
                        dgvTaiKhoan.Items.Add(tenDangNhap);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void btChon_Click(object sender, RoutedEventArgs e)
        {
            if (dgvTaiKhoan.SelectedItem != null)
            {
                string selectedTenDangNhap = dgvTaiKhoan.SelectedItem.ToString();
                txtTenDangNhap.Text = selectedTenDangNhap;
            }
        }

        private void btCapNhat_Click(object sender, RoutedEventArgs e)
        {
            string tenDangNhap = txtTenDangNhap.Text;
            string matkhauHienTai = passwMKHienTai.Password;
            string matkhauMoi = passwMKMoi.Password;
            string nhapLai = passwNhapLai.Password;
            if (matkhauMoi != nhapLai)
            {
                MessageBox.Show("Mặt khẩu mới và nhập lại không khớp!");
                return;
            }
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "UPDATE [QLNS].[dbo].[TAIKHOAN] SET [PASS_WORD] = @MatKhauMoi WHERE [USERNAME] = @TenDangNhap AND [PASS_WORD] = @MatKhauHienTai";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@MatKhauMoi", matkhauMoi);
                    command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    command.Parameters.AddWithValue("@MatKhauHienTai", matkhauHienTai);

                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cập nhật tài khoản thành công!");
                        ClearInputs();
                    }
                    else
                    {
                        MessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void ClearInputs()
        {
            txtTenDangNhap.Text = "";
            passwMKHienTai.Password = "";
            passwMKMoi.Password = "";
            passwNhapLai.Password = "";
        }

        private void txtTenDangNhap_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}