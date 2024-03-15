using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Book_Management
{
    /// <summary>
    /// Interaction logic for SuaSach.xaml
    /// </summary>
    public partial class SuaSach : Window
    {
        int bChon = 0;

        public SuaSach()
        {
            InitializeComponent();
            generateSuaSach();
        }

        private void generateSuaSach()
        {
            string query = "SELECT MASACH, TENSACH, TENTG, TENLINHVUC, TENLOAISACH, GIAMUA, GIABIA, LANTAIBAN, " +
                "TENNHAXUATBAN, NAMXUATBAN FROM SACH LEFT JOIN TACGIA ON SACH.MATG = TACGIA.MATG";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            dgvSuaSach.ItemsSource = data.DefaultView;
        }

        private void generateTenTacGia()
        {
            string query = "SELECT MATG, TENTG FROM TACGIA";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            cbTenTacGia.ItemsSource = data.DefaultView;
            cbTenTacGia.SelectedValuePath = "MATG";
            cbTenTacGia.DisplayMemberPath = "TENTG";
        }

        private void generateTenLoaiSach()
        {
            string query = "SELECT TENLOAISACH FROM LOAISACH";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            cbTenLoaiSach.ItemsSource = data.DefaultView;
            cbTenLoaiSach.DisplayMemberPath = "TENLOAISACH";
        }

        private void generateTenNXB()
        {
            string query = "SELECT TENNHAXUATBAN FROM NHAXUATBAN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            cbTenNXB.ItemsSource = data.DefaultView;
            cbTenNXB.DisplayMemberPath = "TENNHAXUATBAN";
        }

        private void generateLinhVuc()
        {
            string query = "SELECT TENLINHVUC FROM LINHVUC";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            cbTenLinhVuc.ItemsSource = data.DefaultView;
            cbTenLinhVuc.DisplayMemberPath = "TENLINHVUC";
        }

        private void btChon_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = dgvSuaSach.SelectedItem as DataRowView;
            if (row != null)
            {
                txtMaSach.Text = row["MASACH"].ToString();
                txtTenSach.Text = row["TENSACH"].ToString();
                generateTenTacGia();
                cbTenTacGia.Text = row["TENTG"].ToString();
                generateTenNXB();
                cbTenNXB.Text = row["TENNHAXUATBAN"].ToString();
                generateLinhVuc();
                cbTenLinhVuc.Text = row["TENLINHVUC"].ToString();
                generateTenLoaiSach();
                cbTenLoaiSach.Text = row["TENLOAISACH"].ToString();
                txtGiaMua.Text = row["GIAMUA"].ToString();
                txtGiaBia.Text = row["GIABIA"].ToString();
                UpDownLanTaiBan.Value = row["LANTAIBAN"] != DBNull.Value ? (int)row["LANTAIBAN"] : 0;
                DatePickerNamXB.SelectedDate = row["NAMXUATBAN"] != DBNull.Value ? (DateTime?)row["NAMXUATBAN"] : null;

                bChon = 1;
            }
            else
            {
                MessageBox.Show("CHƯA CHỌN SÁCH!", "THÔNG BÁO");
            }
        }

        private void btLuu_Click(object sender, RoutedEventArgs e)
        {
            if (bChon == 1)
            {
                
                DateTime namxb = DatePickerNamXB.SelectedDate.Value;

                string maTacGia = cbTenTacGia.SelectedValue.ToString();


                string query = "UPDATE SACH " +
                    "SET TENSACH = N'" + txtTenSach.Text + "', MATG ='"+ maTacGia +"' , " +
                    "TENLINHVUC = N'" + cbTenLinhVuc.Text.ToString() + "', TENLOAISACH = N'" + cbTenLoaiSach.Text.ToString() + "', GIAMUA = " +
                    txtGiaMua.Text + ", GIABIA = " + txtGiaBia.Text + ", LANTAIBAN = " + UpDownLanTaiBan.Value +
                    ", TENNHAXUATBAN = N'" + cbTenNXB.Text.ToString() + "', NAMXUATBAN = '" + namxb.ToString("yyyy-MM-dd") + "' WHERE MASACH = '" + txtMaSach.Text + "'";

                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                MessageBox.Show("ĐÃ CẬP NHẬP!", "THÔNG BÁO");
                txtMaSach.Text = "";
                txtTenSach.Text = "";
                txtGiaMua.Text = "";
                txtGiaBia.Text = "";
                UpDownLanTaiBan.Value = 0;
                DatePickerNamXB.SelectedDate = DateTime.Now;
                generateSuaSach();
                bChon = 0;
            }
            else
            {
                MessageBox.Show("CHƯA CHỌN SÁCH!", "THÔNG BÁO");
            }
        }

        
    }
}
