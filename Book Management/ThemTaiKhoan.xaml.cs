using System;
using System.Collections.Generic;
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
    /// Interaction logic for ThemTaiKhoan.xaml
    /// </summary>
    public partial class ThemTaiKhoan : Window
    {
        private string connectionString = @"Data Source=VIET;Initial Catalog=QLNS;User ID=sa;Password=123456";
        public ThemTaiKhoan()
        {
            InitializeComponent();
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            string query = "INSERT INTO TAIKHOAN (USERNAME, PASS_WORD) VALUES (@TenDangNhap, @MatKhau)";

            string tenDangNhap = txtTenDangNhap.Text;
            string matKhau = passwMatKhau.Password;
            string nhapLai = passwNhapLai.Password;

            if (matKhau != nhapLai)
            {
                MessageBox.Show("Mật khẩu và nhập lại mật khẩu không khớp.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    command.Parameters.AddWithValue("@MatKhau", matKhau);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thêm tài khoản thành công.");
                        txtTenDangNhap.Clear();
                        passwMatKhau.Clear();
                        passwNhapLai.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Thêm tài khoản thất bại.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
    }
}
