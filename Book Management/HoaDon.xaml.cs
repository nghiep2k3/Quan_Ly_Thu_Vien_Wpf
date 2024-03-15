using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for HoaDon.xaml
    /// </summary>
    public partial class HoaDon : Window
    {
        public HoaDon()
        {

            InitializeComponent();
            InitializeHoaDon();
            btChiTiet.IsEnabled = false;
            btXoa.IsEnabled = false;
        }
        private void InitializeHoaDon()
        {
            string query = "Select MAHOADON,TENKHACHHANG,NGAYLAP,TONGTIEN FROM HOADON";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Clear existing items and columns
            //lvHoaDon.Items.Clear();

            // Add columns
            GridView gridView = new GridView();
            lvHoaDon.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Mã Đơn",
                DisplayMemberBinding = new Binding("MAHOADON"),
                Width = 100
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Tên Khách hàng",
                DisplayMemberBinding = new Binding("TENKHACHHANG"),
                Width = 100
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Ngày lập",
                DisplayMemberBinding = new Binding("NGAYLAP"),
                Width = 150
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Tổng tiền",
                DisplayMemberBinding = new Binding("TONGTIEN"),
                Width = 100
            });

            // Bind data to ListView
            lvHoaDon.ItemsSource = data.DefaultView;
        }
        private void lvHoaDon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvHoaDon.SelectedItems.Count > 0)
            {
                btChiTiet.IsEnabled = true;
                btXoa.IsEnabled = true;
                // Get the selected row
                DataRowView row = (DataRowView)lvHoaDon.SelectedItems[0];

            }
        }

        private void btXoa_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected row
            DataRowView row = (DataRowView)lvHoaDon.SelectedItems[0];

            // Get the values of the selected row
            string maHoaDon = row["MAHOADON"].ToString();
            Console.WriteLine(maHoaDon);
            // Ask for user confirmation
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string selectQuery = $"SELECT MASACH, SUM(SOLUONG) AS TONG_SOLUONG FROM CHITIETHOADON WHERE MAHOADON = '{maHoaDon}' GROUP BY MASACH;";
                DataProvider.Instance.ExecuteScalar(selectQuery);

                string updateQuery = $"UPDATE KHO " +
                    $"SET SOLUONG = SOLUONG + TONG_SOLUONG " +
                    $"FROM KHO " +
                    $"INNER JOIN " +
                    $"( " +
                    $"SELECT MASACH, SUM(SOLUONG) AS TONG_SOLUONG " +
                    $"FROM CHITIETHOADON WHERE MAHOADON = '{maHoaDon}' " +
                    $"GROUP BY MASACH " +
                    $") AS CTHD ON KHO.MASACH = CTHD.MASACH WHERE KHO.MASACH = CTHD.MASACH;";
                int isUpdated = DataProvider.Instance.ExecuteNonQuery(updateQuery);
                // Delete the related rows in the CHITIETHOADON table
                string deleteChiTietQuery = $"DELETE FROM CHITIETHOADON WHERE MAHOADON = '{maHoaDon}'";
                int chiTietResult = DataProvider.Instance.ExecuteNonQuery(deleteChiTietQuery);

                // Delete the row in the HOADON table
                string deleteHoaDonQuery = $"DELETE FROM HOADON WHERE MAHOADON = '{maHoaDon}'";
                int hoaDonResult = DataProvider.Instance.ExecuteNonQuery(deleteHoaDonQuery);
                if (hoaDonResult > 0 && chiTietResult > 0 && isUpdated > 0)
                {

                    // Notify the user that the row has been deleted
                    MessageBox.Show("Row deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    InitializeHoaDon();
                }
                else
                {
                    // Notify the user that the row could not be deleted
                    MessageBox.Show("Could not delete the selected row. Please try again!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            btXoa.IsEnabled = false;
            btChiTiet.IsEnabled = false;

        }


        private void btChiTiet_Click(object sender, RoutedEventArgs e)
        {
            string maHoaDon = ((DataRowView)lvHoaDon.SelectedItem)["MAHOADON"].ToString();

            // Open the ChiTietHoaDon window and pass the maHoaDon value
            ChiTietHoaDon chiTietHoaDonWindow = new ChiTietHoaDon(maHoaDon);
            chiTietHoaDonWindow.ShowDialog();
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            ThemHoaDon add = new ThemHoaDon();
            add.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitializeHoaDon();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text;
            // Query the database to get the data that matches the search text
            string query = $"SELECT MAHOADON, TENKHACHHANG, NGAYLAP, TONGTIEN FROM HOADON WHERE MAHOADON LIKE '%{searchText}%' OR TENKHACHHANG LIKE '%{searchText}%'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Bind the filtered data to the ListView
            lvHoaDon.ItemsSource = data.DefaultView;

        }
    }
}
