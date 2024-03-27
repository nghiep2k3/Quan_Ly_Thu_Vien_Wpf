using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for LinhVuc.xaml
    /// </summary>
    public partial class LinhVuc : Window
    {
        //ObservableCollection<string> linhVucList = new ObservableCollection<string>();
        private string connectionString = @"Data Source=DESKTOP-HKC8B7E\HONGPHUOC;Initial Catalog=QLNS;Integrated Security=True";
        private List<string> linhvucList = new List<string>();
        public LinhVuc()
        {
            InitializeComponent();
            LoadLinhVuc();

            dgvLinhVuc.ItemsSource = linhvucList;
            cbXoaLinhVuc.ItemsSource = linhvucList;
        }
        private void LoadLinhVuc()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT TENLINHVUC FROM LINHVUC";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    linhvucList.Add(reader.GetString(0));
                }
                reader.Close();
            }
            dgvLinhVuc.ItemsSource = linhvucList;
            cbXoaLinhVuc.ItemsSource = linhvucList;
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            string tenLinhVuc = txtThemLinhVuc.Text.Trim();
            if (txtThemLinhVuc.Text == "")
            {
                MessageBox.Show("Vui lòng nhập tên lĩnh vực!", "THÔNG BÁO");
            }
            else if (!string.IsNullOrEmpty(tenLinhVuc))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO LINHVUC (TENLINHVUC) VALUES (@tenLinhVuc)";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@tenLinhvuc", tenLinhVuc);
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    linhvucList.Add(tenLinhVuc);
                    txtThemLinhVuc.Clear();
                    dgvLinhVuc.Items.Refresh();
                    cbXoaLinhVuc.ItemsSource = null;
                    cbXoaLinhVuc.ItemsSource = linhvucList;
                    MessageBox.Show("ĐÃ THÊM!", "THÔNG BÁO");
                }
                catch
                {
                    MessageBox.Show("Lĩnh vực đã tồn tại!", "THÔNG BÁO");
                }
            }
        }
        private void txtThemLinhVuc_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (!string.IsNullOrEmpty(txtThemLinhVuc.Text))
            //{
            //    btThem.IsEnabled = true;
            //}
            //else
            //{
            //   btThem.IsEnabled = false;
            //}
        }

        private void dgvLinhVuc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (dgvLinhVuc.SelectedItems.Count > 0)
            //{
            //    cbXoaLinhVuc.ItemsSource = dgvLinhVuc.SelectedItems.Cast<string>();
            //}
        }

        private void btXoa_Click(object sender, RoutedEventArgs e)
        {
            string tenLinhVuc = cbXoaLinhVuc.SelectedItem as string;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM LINHVUC WHERE TENLINHVUC = @tenLinhVuc";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@tenLinhVuc", tenLinhVuc);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                // Kiểm tra lĩnh vực đã được chọn hay chưa
                if (!string.IsNullOrEmpty(tenLinhVuc))
                {
                    // Xóa lĩnh vực khỏi danh sách và ComboBox
                    linhvucList.Remove(tenLinhVuc);
                    dgvLinhVuc.Items.Refresh();
                    cbXoaLinhVuc.ItemsSource = null;
                    cbXoaLinhVuc.ItemsSource = linhvucList;
                    MessageBox.Show("Đã xóa!", "THÔNG BÁO");
                }
            }
            catch
            {
                MessageBox.Show("Lỗi xóa lĩnh vực!", "THÔNG BÁO");
            }

        }
    }
}