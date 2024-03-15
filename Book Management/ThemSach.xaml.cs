using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for ThemSach.xaml
    /// </summary>
    public partial class ThemSach : Window
    {
        public ThemSach()
        {
            InitializeComponent();
            txtMaSach.Text = MaSach();
            generateTenTacGia();
            generateLinhVuc();
            generateTenLoaiSach();
            generateTenNXB();
        }
        private string MaSach()
        {
            string query = "SELECT MAX(MASACH) FROM SACH";
            object result = DataProvider.Instance.ExecuteScalar(query);

            if (result != DBNull.Value)
            {
                string maxNumber = result.ToString();
                int lastNumber = int.Parse(maxNumber.Substring(2)); // tim ra so tan cung va bo di 2 tu trong string
                int nextNumber = lastNumber + 1;
                return "SA" + nextNumber.ToString("D5"); // voi dinh dang 5 chu so
            }
            else
            {
                return "SA00001"; // bat dau voi hd00001
            }
        }
        private void generateLinhVuc()
        {
            string query = "Select TENLINHVUC from LINHVUC";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            cbTenLinhVuc.ItemsSource = data.DefaultView;
            cbTenLinhVuc.DisplayMemberPath = "TENLINHVUC";
        }
        private void generateTenTacGia()
        {
            string query = "Select MATG, TENTG from TACGIA";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            cbTenTacGia.ItemsSource = data.DefaultView;
            cbTenTacGia.SelectedValuePath = "MATG";
            cbTenTacGia.DisplayMemberPath = "TENTG";
        }
        private void generateTenLoaiSach()
        {
            string query = "Select TENLOAISACH from LOAISACH";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            cbTenLoaiSach.ItemsSource = data.DefaultView;
            cbTenLoaiSach.DisplayMemberPath = "TENLOAISACH";
        }
        private void generateTenNXB()
        {
            string query = "Select TENNHAXUATBAN from NHAXUATBAN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            cbTenNXB.ItemsSource = data.DefaultView;
            cbTenNXB.DisplayMemberPath = "TENNHAXUATBAN";
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            if (txtTenSach.Text == "" || cbTenTacGia.Text == "" || cbTenLinhVuc.Text == "" || cbTenLoaiSach.Text == "" || cbTenNXB.Text == "" || txtGiaMua.Text == "" || txtGiaBia.Text == "")
            {
                MessageBox.Show("VUI LÒNG ĐIỀN ĐỦ THÔNG TIN!", "THÔNG BÁO");
            }
            else
            {
                DateTime namxb = DatePickerNamXuatBan.SelectedDate.Value;

                string query = "Insert into SACH values(" +
                    "'" + txtMaSach.Text + "',N'" + txtTenSach.Text + "'," +
                        "(Select MATG From TACGIA Where TENTG = N'" + cbTenTacGia.Text.ToString() + "'),N'" + cbTenLinhVuc.Text + "',N'" + cbTenLoaiSach.Text + "','" + txtGiaMua.Text + "'," +
                        "'" + txtGiaMua.Text + "','" + UpDownLanTaiBan.Value + "',N'" + cbTenNXB.Text.ToString() + "'," +
                        "'" + namxb.ToString("yyyy-MM-dd") + "')";
                DataTable data = DataProvider.Instance.ExecuteQuery(query);
                string query1 = "Insert Into KHO Values('" + txtMaSach.Text + "', " + 1 + ")";
                DataTable data1 = DataProvider.Instance.ExecuteQuery(query1);
                MessageBox.Show("ĐÃ THÊM!", "THÔNG BÁO");
                txtMaSach.Text = MaSach();
                txtTenSach.Text = "";
                generateTenTacGia();
                generateLinhVuc();
                generateTenLoaiSach();
                generateTenNXB();
                txtGiaMua.Text = "";
                txtGiaBia.Text = "";
                UpDownLanTaiBan.Value = 0;
                DatePickerNamXuatBan.Text = DateTime.Now.ToString();
            }


        }
    }
}
