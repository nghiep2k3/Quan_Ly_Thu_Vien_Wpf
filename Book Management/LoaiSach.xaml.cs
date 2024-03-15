using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for LoaiSach.xaml
    /// </summary>
    public partial class LoaiSach : Window
    {
        ObservableCollection<string> loaiSachList = new ObservableCollection<string>();
        private string connectionString = @"Data Source=LAPTOP-2KMTSAL1;Initial Catalog=QLNS;Integrated Security=True";
        private List<string> loaisachList = new List<string>();
        public LoaiSach()
        {
            InitializeComponent();
            LoadLoaiSach();

            // Hiển thị danh sách loại sách lên ListView
            dgvLoaiSach.ItemsSource = loaiSachList;

            // Hiển thị danh sách loại sách lên ComboBox
            cbXoaLoaiSach.ItemsSource = loaiSachList;
        }
        private void LoadLoaiSach()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TENLOAISACH FROM LOAISACH";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    loaiSachList.Add(reader.GetString(0));
                }
                reader.Close();
            }
            dgvLoaiSach.ItemsSource = loaiSachList;
        }

        private void btThemLoaiSach_Click(object sender, RoutedEventArgs e)
        {
            string tenLoaiSach = txtThemLoaiSach.Text.Trim();

            if (txtThemLoaiSach.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên loại sách!", "THÔNG BÁO");
            }
            // Kiểm tra tên loại sách có trống hay không
            else if (!string.IsNullOrEmpty(tenLoaiSach))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO LOAISACH (TENLOAISACH) VALUES (@tenLoaiSach)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@tenLoaiSach", tenLoaiSach);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }

                    // Kiểm tra loại sách đã tồn tại chưa
                    if (!loaiSachList.Contains(tenLoaiSach))
                    {
                        // Thêm loại sách vào danh sách và hiển thị lên ListView
                        loaiSachList.Add(tenLoaiSach);
                        txtThemLoaiSach.Clear();
                        MessageBox.Show("Đã thêm!", "THÔNG BÁO");
                    }
                }
                catch
                {
                    MessageBox.Show("Loại sách đã tồn tại!", "THÔNG BÁO");
                }
            }
            }

        private void btXoaLoaiSach_Click(object sender, RoutedEventArgs e)
        {
            string tenLoaiSach = cbXoaLoaiSach.SelectedItem as string;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM LOAISACH WHERE TENLOAISACH = @tenLoaiSach";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@tenLoaiSach", tenLoaiSach);
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                // Kiểm tra loại sách đã được chọn hay chưa
                if (!string.IsNullOrEmpty(tenLoaiSach))
                {
                    // Xóa loại sách khỏi danh sách và ComboBox
                    loaiSachList.Remove(tenLoaiSach);
                    cbXoaLoaiSach.SelectedItem = null;
                    MessageBox.Show("Đã xóa!", "THÔNG BÁO");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi xóa loại sách!", "THÔNG BÁO");
            }
        }
    }
}