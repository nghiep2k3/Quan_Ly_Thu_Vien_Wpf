using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ThemHoaDon.xaml
    /// </summary>
    public partial class ThemHoaDon : Window
    {
        private string MaHoaDonHT;
        private string maSachHT;
        private string tenSachHT;
        private int soSachHT;

        public ThemHoaDon()
        {
            InitializeComponent();
            txtMaHoaDon.Text = GetNextHoaDonNumber();
            InitializeChonSach();
        }
        private void InitializeChonSach()
        {
            string query = "SELECT s.MASACH, s.TENSACH, k.SOLUONG FROM SACH s INNER JOIN KHO k ON s.MASACH = k.MASACH";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Clear existing items and columns
            //lvHoaDon.Items.Clear();

            // Add columns
            GridView gridView = new GridView();
            lvChonSach.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Mã Sách",
                DisplayMemberBinding = new Binding("MASACH"),
                Width = 80
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Tên Sách",
                DisplayMemberBinding = new Binding("TENSACH"),
                Width = 180
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Số Lượng",
                DisplayMemberBinding = new Binding("SOLUONG"),

                Width = 60
            });

            // Bind data to ListView
            lvChonSach.ItemsSource = data.DefaultView;
        }
        private string GetNextHoaDonNumber()
        {
            string query = "SELECT MAX(MAHOADON) FROM HOADON";
            object result = DataProvider.Instance.ExecuteScalar(query);

            if (result != DBNull.Value)
            {
                string maxNumber = result.ToString();
                int lastNumber = int.Parse(maxNumber.Substring(2)); // tim ra so tan cung va bo di 2 tu trong string
                int nextNumber = lastNumber + 1;
                return "HD" + nextNumber.ToString("D5"); // voi dinh dang 5 chu so
            }
            else
            {
                return "HD00001"; // bat dau voi hd00001
            }
        }
        private void lvChiTiet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            string maSach = maSachHT;
            // Get the DONGIA from KHO
            string khoQuery = $"SELECT GIABIA FROM SACH WHERE MASACH = '{maSach}'";
            double dongia = Convert.ToDouble(DataProvider.Instance.ExecuteScalar(khoQuery));

            // lay so sach trong kho de so sanh
            string sachKho = $"SELECT SOLUONG FROM KHO WHERE MASACH = '{maSach}'";
            int sachTrongKho = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(sachKho));
            soSachHT = (int)UpDownSoLuong.Value;
            if (sachTrongKho < soSachHT)
            {
                MessageBox.Show("Sách trong kho đã hết!");
                return;
            }
            double thanhtien = soSachHT * dongia;
            // Create a new object that contains the values for each column

            var newItem = new
            {
                MASACH = maSach,
                TENSACH = tenSachHT,
                SOLUONG = soSachHT,
                DONGIA = dongia,
                THANHTIEN = thanhtien
            };

            // Add the new item to the ListView's ItemsSource collection
            lvChiTiet.Items.Add(newItem);
            //Them thong bao
            MessageBox.Show("Thêm sách thành công!");

            lvChiTiet.SelectedIndex = 0;
        }

        private void btThemHoaDon_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve values from UI controls
            string tenKH = txtTenKH.Text.Trim();
            double tongTien = 0;
            foreach (dynamic item in lvChiTiet.Items)
            {
                tongTien += item.THANHTIEN;
            }

            // Insert into HOADON table
            string maHoaDon = GetNextHoaDonNumber();
            string insertHoaDonQuery = $"INSERT INTO HOADON(MAHOADON, TENKHACHHANG, NGAYLAP, TONGTIEN) " +
                $"VALUES('{maHoaDon}', N'{tenKH}', GETDATE(), {tongTien})";
            int affectedRows = DataProvider.Instance.ExecuteNonQuery(insertHoaDonQuery);
            if (affectedRows == 0)
            {
                MessageBox.Show("Thêm hóa đơn thất bại!");
                return;
            }

            // Insert into CHITIETHOADON table
            foreach (dynamic item in lvChiTiet.Items)
            {
                string maSach = item.MASACH;
                int soLuong = item.SOLUONG;
                double giaTien = item.DONGIA;
                double thanhTien = item.THANHTIEN;
                string insertChiTietQuery = $"INSERT INTO CHITIETHOADON(MAHOADON, MASACH, SOLUONG, GIATIEN, THANHTIEN) " +
                    $"VALUES('{maHoaDon}', '{maSach}', {soLuong}, {giaTien}, {thanhTien})";
                string updateSoLuongSach = $"UPDATE KHO SET SOLUONG = SOLUONG - {soLuong} WHERE MASACH = '{maSach}'";
                DataProvider.Instance.ExecuteNonQuery(updateSoLuongSach);
                DataProvider.Instance.ExecuteNonQuery(insertChiTietQuery);
            }

            MessageBox.Show("Thêm hóa đơn thành công!");
            this.Close();
        }

        private void lvChonSach_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvChonSach.SelectedItems.Count > 0)
            {
                // Get the selected row
                DataRowView row = (DataRowView)lvChonSach.SelectedItems[0];
                // Get the values of the selected row
                maSachHT = row["MASACH"].ToString();
                tenSachHT = row["TENSACH"].ToString();
            }
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btXoa_Click(object sender, RoutedEventArgs e)
        {
            int index = lvChiTiet.SelectedIndex;
            lvChiTiet.Items.RemoveAt(index);

            //lvChiTiet.Items.Clear();

            // Refresh the ListView
            //lvChiTiet.Items.Refresh();
        }

        private void txtMaHoaDon_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
