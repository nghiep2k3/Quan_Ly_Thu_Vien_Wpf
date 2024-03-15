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
    /// Interaction logic for SuaTacGia.xaml
    /// </summary>
    public partial class SuaTacGia : Window
    {
        int bChonTG = 0;
        //List<string> TacGia;
        public SuaTacGia()
        {
            InitializeComponent();
            InitializeSuaTacGia();
        }
        void InitializeSuaTacGia()
        {
            string query = "SELECT MATG, TENTG, NAMSINH, NAMMAT, QUEQUAN FROM TACGIA";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            this.dgvTacGia.ItemsSource = data.DefaultView;

        }
        /*
        public class TacGia
        {
            public string MaTacGia { get; set; }
            public string TenTacGia { get; set; }
            public string QueQuan { get; set; }
            public DateTime NgaySinh { get; set; }
            public DateTime NgayMat { get; set; }
            public bool IsSelected { get; set; }
        }
        */
        private void btChon_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = dgvTacGia.SelectedItem as DataRowView;
            if (row != null)
            {
                txtMaTacGia.Text = row["MATG"].ToString();
                txtTenTacGia.Text = row["TENTG"].ToString();
                txtQueQuan.Text = row["QUEQUAN"].ToString();
                DatePickerNgaySinh.SelectedDate = row["NAMSINH"] != DBNull.Value ? (DateTime?)row["NAMSINH"] : null;
                DatePickerNgayMat.SelectedDate = row["NAMMAT"] != DBNull.Value ? (DateTime?)row["NAMMAT"] : null;
                chkNgaySinh.IsChecked = DatePickerNgaySinh.SelectedDate == null;
                chkNgayMat.IsChecked = DatePickerNgayMat.SelectedDate == null;
                bChonTG = 1;
            }
            else
            {
                MessageBox.Show("CHƯA CHỌN TÁC GIẢ!", "THÔNG BÁO");
            }

        }

        private void btLuu_Click(object sender, RoutedEventArgs e)
        {
            if (bChonTG == 1)
            {
                if (string.IsNullOrEmpty(txtTenTacGia.Text.Trim()))
                {
                    MessageBox.Show("Vui lòng nhập tên tác giả", "THÔNG BÁO");
                    return;
                }

                DateTime ngaySinh = chkNgaySinh.IsChecked == true ? DateTime.MinValue : DatePickerNgaySinh.SelectedDate.Value;
                DateTime ngayMat = chkNgayMat.IsChecked == true ? DateTime.MaxValue : DatePickerNgayMat.SelectedDate.Value;

                string query = $"UPDATE TACGIA SET TENTG = N'{txtTenTacGia.Text.Trim()}', NAMSINH = '{ngaySinh.ToString("yyyy-MM-dd")}', NAMMAT = '{ngayMat.ToString("yyyy-MM-dd")}', QUEQUAN = N'{txtQueQuan.Text.Trim()}' WHERE MATG = '{txtMaTacGia.Text.Trim()}'";
                int result = DataProvider.Instance.ExecuteNonQuery(query);
                if (result > 0)
                {
                    MessageBox.Show("Cập nhật tác giả thành công", "THÔNG BÁO");
                    InitializeSuaTacGia();
                    txtMaTacGia.Text = "";
                    txtTenTacGia.Text = "";
                    txtQueQuan.Text = "";
                    chkQueQuan.IsChecked = false;
                    chkNgaySinh.IsChecked = false;
                    chkNgayMat.IsChecked = false;
                    DatePickerNgaySinh.SelectedDate = null;
                    DatePickerNgayMat.SelectedDate = null;
                    bChonTG = 0;
                }
                else
                {
                    MessageBox.Show("Cập nhật tác giả thất bại", "THÔNG BÁO");
                }
            }
            else
            {
                MessageBox.Show("Chưa chọn tác giả để cập nhật", "THÔNG BÁO");
            }

        }

        private void dgvTacGia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void chkQueQuan_Checked(object sender, RoutedEventArgs e)
        {
            chkQueQuan.Content = "(Không có thông tin)";
            txtQueQuan.Text = "(Không có thông tin)";
            txtQueQuan.IsReadOnly = true;
        }

        private void chkQueQuan_Unchecked(object sender, RoutedEventArgs e)
        {
            chkQueQuan.Content = "CHƯA RÕ";
            txtQueQuan.Text = "";
            txtQueQuan.IsReadOnly = false;
        }

        private void chkNgaySinh_Checked(object sender, RoutedEventArgs e)
        {
            if (chkNgaySinh.IsChecked == true)
            {
                DatePickerNgaySinh.SelectedDate = DateTime.MinValue;
            }
            else
            {
                DatePickerNgaySinh.SelectedDate = DateTime.Now;
            }
        }

        private void chkNgayMat_Checked(object sender, RoutedEventArgs e)
        {
            if (chkNgayMat.IsChecked == true)
            {
                DatePickerNgayMat.SelectedDate = DateTime.MaxValue;
            }
            else
            {
                DatePickerNgayMat.SelectedDate = DateTime.Now;
            }
        }

        private void chkNgaySinh_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chkNgaySinh.IsChecked == true)
            {
                DatePickerNgaySinh.SelectedDate = DateTime.MinValue;
            }
            else
            {
                DatePickerNgaySinh.SelectedDate = DateTime.Now;
            }
        }

        private void chkNgayMat_Unchecked(object sender, RoutedEventArgs e)
        {
            if (chkNgayMat.IsChecked == true)
            {
                DatePickerNgayMat.SelectedDate = DateTime.MaxValue;
            }
            else
            {
                DatePickerNgayMat.SelectedDate = DateTime.Now;
            }
        }
    }
}