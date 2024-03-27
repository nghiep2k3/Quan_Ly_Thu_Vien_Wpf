using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for ThemTacGia.xaml
    /// </summary>
    public partial class ThemTacGia : Window
    {
        public ThemTacGia()
        {
            InitializeComponent();
            txtMaTacGia.Text = MaTacGia();
        }



        private string MaTacGia()
        {
            // Random rd = new Random();
            // txtMaTacGia.Text = ("TG0" + rd.Next(99, 1000));

            string query = "SELECT MAX(MATG) FROM TACGIA";
            object result = DataProvider.Instance.ExecuteScalar(query);

            if (result != DBNull.Value)
            {
                string maxNumber = result.ToString();
                int lastNumber = int.Parse(maxNumber.Substring(2)); // tim ra so tan cung va bo di 2 tu trong string
                int nextNumber = lastNumber + 1;
                return "TG" + nextNumber.ToString("D3"); // voi dinh dang 5 chu so
            }
            else
            {
                return "TG001"; // bat dau voi hd001
            }
        }
        private void btThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            DateTime ngaymat = DateTime.MinValue;
            DateTime ngaysinh = DateTime.MinValue;
            if (txtTenTacGia.Text == "")
            {
                MessageBox.Show("VUI LÒNG ĐIỀN TÊN TÁC GIẢ!", "THÔNG BÁO");
            }

            if (chkNgaySinh.IsChecked == true)
            {
                DatePickerNgaySinh.SelectedDate = DateTime.MinValue;
            }
            else
            {
                ngaysinh = DatePickerNgaySinh.SelectedDate.Value;
            }
            if (chkNgayMat.IsChecked == true)
            {
                DatePickerNgayMat.SelectedDate = DateTime.MinValue;
            }
            else
            {
                ngaymat = DatePickerNgayMat.SelectedDate.Value;
            }
            string query;
            if (chkNgaySinh.IsChecked == true && !chkNgayMat.IsChecked == true)
            {
                query = "Insert into TACGIA VALUES ('" + txtMaTacGia.Text + "', N'" + txtTenTacGia.Text + "',NULL, '" + ngaymat.ToString("yyyy-MM-dd") + "', N'" + txtQueQuan.Text + "')";
            }
            else if (!chkNgaySinh.IsChecked == true && chkNgayMat.IsChecked == true)
            {
                query = "Insert into TACGIA VALUES ('" + txtMaTacGia.Text + "', N'" + txtTenTacGia.Text + "', '" +
                        ngaysinh.ToString("yyyy-MM-dd") + "', NULL, N'" + txtQueQuan.Text + "')";
            }
            else if (chkNgaySinh.IsChecked == true && chkNgayMat.IsChecked == true)
            {
                query = "Insert into TACGIA VALUES ('" + txtMaTacGia.Text + "', N'" + txtTenTacGia.Text + "', NULL, NULL, N'" + txtQueQuan.Text + "')";
            }
            else
            {
                query = "Insert into TACGIA VALUES ('" + txtMaTacGia.Text + "', N'" + txtTenTacGia.Text + "', '" +
                        ngaysinh.ToString("yyyy-MM-dd") + "', '" + ngaymat.ToString("yyyy-MM-dd") + "', N'" + txtQueQuan.Text + "')";
            }
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            MessageBox.Show("ĐÃ THÊM!", "THÔNG BÁO");
            txtMaTacGia.Text = MaTacGia();
            txtTenTacGia.Text = "";
            txtQueQuan.Text = "";
            chkNgaySinh.IsChecked = false;
            chkNgayMat.IsChecked = false;
            DatePickerNgaySinh.Text = DateTime.Now.ToString();
            DatePickerNgayMat.Text = DateTime.Now.ToString();

        }

        private void chkQueQuan_Checked(object sender, RoutedEventArgs e)
        {
            /*  chkQueQuan.Content = "(Không có thông tin)"; */
            txtQueQuan.Text = "(Không có thông tin)";
            txtQueQuan.IsReadOnly = true;
        }

        private void chkQueQuan_Unchecked(object sender, RoutedEventArgs e)
        {
            /*chkQueQuan.Content = "CHƯA RÕ"; */
            txtQueQuan.Text = "";
            txtQueQuan.IsReadOnly = false;
        }

        private void chkQueQuan_Checked_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
