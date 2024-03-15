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
    /// Interaction logic for ViewTongQuanLyNhaSach.xaml
    /// </summary>
    public partial class ViewTongQuanLyNhaSach : Window
    {
        //private string username;
        public ViewTongQuanLyNhaSach(string username)
        {
            InitializeComponent();
            Initialize_SelectedIndexChanged();
            //username = "uit";
            txtTenNguoiDung.Text =username;
        }

        private void MenuCapNhatThongTin_Click(object sender, RoutedEventArgs e)
        {
            //Khi ấn đăng nhập thì hiện ra giao diện TaiKhoan
            TaiKhoan f = new TaiKhoan();
            this.Hide();
            //Khi thao tác trên dialog xong thì mới chạy lệnh show ở dưới
            f.ShowDialog();
            this.Show();
        }

        private void MenuDangXuat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuThemTaiKhoan_Click(object sender, RoutedEventArgs e)
        {
            ThemTaiKhoan f = new ThemTaiKhoan();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void MenuThemSach_Click(object sender, RoutedEventArgs e)
        {
            ThemSach f = new ThemSach();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void MenuSuaSach_Click(object sender, RoutedEventArgs e)
        {
            SuaSach f = new SuaSach();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void MenuXoaSach_Click(object sender, RoutedEventArgs e)
        {
            XoaSach f = new XoaSach();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void MenuLinhVuc_Click(object sender, RoutedEventArgs e)
        {
            LinhVuc f = new LinhVuc();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void MenuLoaiSach_Click(object sender, RoutedEventArgs e)
        {
            LoaiSach f = new LoaiSach();
            this.Hide();
            f.ShowDialog();
            this.Show();

        }

        private void MenuThemTacGia_Click(object sender, RoutedEventArgs e)
        {
            ThemTacGia f = new ThemTacGia();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void MenuSuaTacGia_Click(object sender, RoutedEventArgs e)
        {
            SuaTacGia f = new SuaTacGia();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void MenuXoaTacGia_Click(object sender, RoutedEventArgs e)
        {
            XoaTacGia f = new XoaTacGia();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void MenuHoaDon_Click(object sender, RoutedEventArgs e)
        {
            HoaDon f = new HoaDon();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void MenuThongKe_Click(object sender, RoutedEventArgs e)
        {
            ThongKe f = new ThongKe();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void MenuNhaXuatBan_Click(object sender, RoutedEventArgs e)
        {
            NhaXuatBan f = new NhaXuatBan();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void MenuKho_Click(object sender, RoutedEventArgs e)
        {
            Kho f = new Kho();
            this.Hide();
            f.ShowDialog();
            this.Show();
        }

        private void fViewTong_Load()
        {
            DateTime tg = DateTime.Now;
            lblTime.Content = tg.ToString("dddd, dd/MM/yyyy");
        }
        private void ViewTong_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        void Initialize_SelectedIndexChanged()
        {
            fViewTong_Load();
            string query = "Select TENSACH, TENTG, TENLINHVUC, TENLOAISACH, " +
                "GIABIA, LANTAIBAN, TENNHAXUATBAN, NAMXUATBAN From SACH LEFT JOIN TACGIA " +
                "ON SACH.MATG = TACGIA.MATG";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            this.lstSach.ItemsSource = data.DefaultView;
            lstSach.Items.Refresh();

            string timenow = DateTime.Now.ToString("yyyy-MM-dd");
            Console.WriteLine(timenow);
            string query2 = $"SELECT SUM(TONGTIEN) AS [DOANHTHU] FROM HOADON WHERE NGAYLAP BETWEEN '{timenow} 00:00:00' AND '{timenow} 23:59:59'";
            DataTable data2 = DataProvider.Instance.ExecuteQuery(query2);
            if (data2.Rows.Count > 0 && data2.Rows[0][0] != DBNull.Value)
            {
                txtDoanhThu.Text = data2.Rows[0][0].ToString() + " VND";
            }
            else
            {
                txtDoanhThu.Text = "0 VND";
            }

            string query3 = $"SELECT COUNT(MAHOADON) FROM HOADON WHERE NGAYLAP BETWEEN '{timenow} 00:00:00' AND '{timenow} 23:59:59'";
            DataTable data3 = DataProvider.Instance.ExecuteQuery(query3);
            if (data3.Rows.Count > 0 && data3.Rows[0][0] != DBNull.Value)
            {
                txtSLKhach.Text = data3.Rows[0][0].ToString() + " người";
            }
            else
            {
                txtSLKhach.Text = "0 người";
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Initialize_SelectedIndexChanged();


        }

        private void txtSLKhach_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
