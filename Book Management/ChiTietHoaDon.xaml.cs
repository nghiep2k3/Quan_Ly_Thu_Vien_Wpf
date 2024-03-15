using System;
using System.Collections;
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
    /// Interaction logic for ChiTietHoaDon.xaml
    /// </summary>
    public partial class ChiTietHoaDon : Window
    {
        private string _maHoaDonChiTiet;
        private string maSachHT = " ";
        private string tenSachHT;
        private int soSachThem = 0;
        private double tienConThieuKhiThemSach;
        private int soSachHT = 0;
        // so lan them la khi user nhan them nhieu lan
        private int soLanThem = 1;
        public ChiTietHoaDon(string maHoaDonChiTiet)
        {
            _maHoaDonChiTiet = maHoaDonChiTiet;
            InitializeComponent();
            InitializeChonSach();
            InitializeLvChiTiet();
            btThemSach.IsEnabled = false;
            btXoa.Visibility = Visibility.Collapsed;
            btHoanTat.Visibility = Visibility.Collapsed;
            btThem.Visibility = Visibility.Collapsed;
            btHuyThem.Visibility = Visibility.Collapsed;
            txtMaDon.Text = maHoaDonChiTiet;
            string query1 = $"SELECT TENKHACHHANG FROM HOADON where MAHOADON = '{maHoaDonChiTiet}' ";
            txtTenKhachHang.Text = Convert.ToString(DataProvider.Instance.ExecuteScalar(query1));
            string query2 = $"SELECT NGAYLAP FROM HOADON where MAHOADON = '{maHoaDonChiTiet}' ";
            txtThoiGian.Text = Convert.ToString(DataProvider.Instance.ExecuteScalar(query2));
        }
        private void InitializeLvChiTiet()
        {
            // Get the details of the selected invoice
            string query = "SELECT c.MASACH, s.TENSACH, c.SOLUONG, c.GIATIEN, c.THANHTIEN FROM CHITIETHOADON c INNER JOIN SACH s ON c.MASACH = s.MASACH WHERE c.MAHOADON = '" + _maHoaDonChiTiet + "'";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Clear existing items and columns
            //lvChiTiet.Items.Clear();
            // Add columns
            GridView gridView = new GridView();
            lvChiTiet.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Mã Sách",
                DisplayMemberBinding = new Binding("MASACH"),
                Width = 90
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Tên Sách",
                DisplayMemberBinding = new Binding("TENSACH"),
                Width = 150
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Số Lượng",
                DisplayMemberBinding = new Binding("SOLUONG"),
                Width = 80
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Đơn Giá",
                DisplayMemberBinding = new Binding("GIATIEN"),
                Width = 80
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Thành Tiền",
                DisplayMemberBinding = new Binding("THANHTIEN"),
                Width = 80
            });

            // Bind data to ListView
            lvChiTiet.ItemsSource = data.DefaultView;
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
                Width = 90
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Tên Sách",
                DisplayMemberBinding = new Binding("TENSACH"),
                Width = 150
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Số Lượng",
                DisplayMemberBinding = new Binding("SOLUONG"),

                Width = 80
            });

            // Bind data to ListView
            lvChonSach.ItemsSource = data.DefaultView;
        }

        private void lvChiTiet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btXoa.Visibility = Visibility.Visible;
            // Get the selected row as a DataRowView
            DataRowView selectedRow = (DataRowView)lvChiTiet.SelectedItem;

            if (selectedRow != null)
            {
                // Get the value of the "MASACH" field in the selected row
                maSachHT = selectedRow["MASACH"].ToString();
            }
        }

        private void btBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btCapNhat_Click(object sender, RoutedEventArgs e)
        {
            btCapNhat.Visibility = Visibility.Collapsed;
            btBack.Visibility = Visibility.Collapsed;
            btHoanTat.Visibility = Visibility.Visible;
            btThemSach.IsEnabled = true;
        }

        private void btXoa_Click(object sender, RoutedEventArgs e)
        {
            //lay thanh tien cua sach chuan bi xoa
            string thanhTien = $"SELECT THANHTIEN from CHITIETHOADON where MASACH = '{maSachHT}' and MAHOADON = '{_maHoaDonChiTiet}'";
            double thanhtien = Convert.ToDouble(DataProvider.Instance.ExecuteScalar(thanhTien));
            Console.WriteLine(thanhtien);

            // tru gia thanh tien so sach xoa
            string updateTong = $"UPDATE HOADON SET TONGTIEN = TONGTIEN - {thanhtien} where MAHOADON = '{_maHoaDonChiTiet}' ";
            DataProvider.Instance.ExecuteScalar(updateTong);

            //xoa sach trong hoa don chi tiet
            string query = $"DELETE from CHITIETHOADON where MASACH = '{maSachHT}' and MAHOADON = '{_maHoaDonChiTiet}' ";
            DataProvider.Instance.ExecuteScalar(query);

            MessageBox.Show("Đã xóa thành công! Vui lòng Refresh lại để cập nhật thông tin!");
        }

        private void btThemSach_Click(object sender, RoutedEventArgs e)
        {
            btThem.Visibility = Visibility.Visible;
            btHuyThem.Visibility = Visibility.Visible;
            btXoa.Visibility = Visibility.Collapsed;
        }

        private void btThem_Click(object sender, RoutedEventArgs e)
        {
            string maSach = maSachHT;

            // Get the DONGIA from KHO
            soSachThem = (int)UpDownSoLuong.Value;
            // lay so sach trong kho de so sanh
            string sachKho = $"SELECT SOLUONG FROM KHO WHERE MASACH = '{maSach}'";
            int sachTrongKho = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(sachKho));
            if (sachTrongKho < soSachThem)
            {
                MessageBox.Show("Sách trong kho đã hết!");
                return;
            }


            string sachTrongChiTiet = $"SELECT SOLUONG FROM CHITIETHOADON WHERE MASACH = '{maSach}' and MAHOADON ='{_maHoaDonChiTiet}'";
            int soSachTrongChiTiet = Convert.ToInt32(DataProvider.Instance.ExecuteScalar(sachTrongChiTiet));


            // lay don gia cua sach duoc them
            string khoQuery = $"SELECT GIABIA FROM SACH WHERE MASACH = '{maSach}'";
            double dongia = Convert.ToDouble(DataProvider.Instance.ExecuteScalar(khoQuery));
            // so sach hien tai sau khi cong trong database va so sach duoc them
            soSachHT = soSachTrongChiTiet + soSachThem;
            Console.WriteLine(soSachHT);
            if (soSachTrongChiTiet == 0) // them sach moi chua co trong hoa don
            {
                Console.WriteLine("chwua có sách này trong hóa đơn");
                //them vao chi tiet hoa don
                double thanhtien1 = soSachHT * dongia;
                string themSach1 = $"Insert into CHITIETHOADON (MAHOADON,MASACH,SOLUONG,GIATIEN,THANHTIEN) VALUES('{_maHoaDonChiTiet}','{maSach}','{soSachHT}','{dongia}','{thanhtien1}')";

                DataTable data = DataProvider.Instance.ExecuteQuery(themSach1);

                string updateSoLuongSach1 = $"UPDATE KHO SET SOLUONG = SOLUONG - {soSachHT} WHERE MASACH = '{maSach}'";
                DataProvider.Instance.ExecuteScalar(updateSoLuongSach1);

                string updateTong1 = $"UPDATE HOADON SET TONGTIEN = TONGTIEN + {thanhtien1}";
                DataProvider.Instance.ExecuteScalar(updateTong1);
                

                // thay doi tong hoa don khi them sach vao
                tienConThieuKhiThemSach = thanhtien1 - soSachThem * dongia;
                Console.WriteLine(thanhtien1);
                Console.WriteLine(tienConThieuKhiThemSach);
                string updateTong = $"UPDATE HOADON SET TONGTIEN = TONGTIEN + {tienConThieuKhiThemSach}";
                DataProvider.Instance.ExecuteScalar(updateTong);
                tienConThieuKhiThemSach = 0.0;
                thanhtien1 = 0.0;
                soLanThem++;
                Console.WriteLine(soLanThem);
                // thong bao them thanh cong
                MessageBox.Show("Thêm sách thành công! Vui lòng Refresh lại để cập nhật thông tin!");

            }
            else
            {
                Console.WriteLine("Có sách này trong hóa đơn");

                // tinh thanh tien cua so sach hien tai ( sau khi them )
                double thanhtien = soSachHT * dongia;

                // thay doi so luong sach trong chitiethoadon va trong kho

                string themSach = $"UPDATE CHITIETHOADON SET SOLUONG = '{soSachHT}', THANHTIEN = '{thanhtien}' WHERE MAHOADON = '{_maHoaDonChiTiet}' AND MASACH = '{maSach}'";
                DataProvider.Instance.ExecuteScalar(themSach);

                string updateSoLuongSach = $"UPDATE KHO SET SOLUONG = SOLUONG - {soSachThem} WHERE MASACH = '{maSach}'";
                DataProvider.Instance.ExecuteScalar(updateSoLuongSach);

                // thay doi tong hoa don khi them sach vao
                tienConThieuKhiThemSach = thanhtien - soSachThem * dongia;
                Console.WriteLine(thanhtien);
                Console.WriteLine(tienConThieuKhiThemSach);
                string updateTong = $"UPDATE HOADON SET TONGTIEN = TONGTIEN + {tienConThieuKhiThemSach}";
                DataProvider.Instance.ExecuteScalar(updateTong);
                tienConThieuKhiThemSach = 0.0;
                thanhtien = 0.0;
                soLanThem++;
                Console.WriteLine(soLanThem);
                // thong bao them thanh cong
                MessageBox.Show("Thêm sách thành công! Vui lòng Refresh lại để cập nhật thông tin!");
            }

        }

        private void btHuyThem_Click(object sender, RoutedEventArgs e)
        {
            btHuyThem.Visibility = Visibility.Collapsed;
            btThem.Visibility = Visibility.Collapsed;
            btThemSach.Visibility = Visibility.Visible;

        }

        private void lvChonSach_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get the selected row
            DataRowView row = (DataRowView)lvChonSach.SelectedItems[0];
            // Get the values of the selected row
            maSachHT = row["MASACH"].ToString();
            tenSachHT = row["TENSACH"].ToString();
            //soSachHT = (int)UpDownSoLuong.Value;
            //Console.WriteLine(soSachHT);
        }

        private void btHoanTat_Click(object sender, RoutedEventArgs e)
        {
            btCapNhat.Visibility = Visibility.Visible;
            btBack.Visibility = Visibility.Visible;
            btHoanTat.Visibility = Visibility.Collapsed;
            btThemSach.IsEnabled = false;
            btThem.Visibility = Visibility.Collapsed;
            btHuyThem.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ChiTietHoaDon b = new ChiTietHoaDon(_maHoaDonChiTiet);
            b.Show();
        }
    }
}