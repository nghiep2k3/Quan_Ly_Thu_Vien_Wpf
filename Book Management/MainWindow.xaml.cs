using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Book_Management;

namespace Book_Management
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        int count = 1;
        private void btnDangNhap_Click(object sender, RoutedEventArgs e)
        {

            string username = txtTaiKhoan.Text;
            string password = passwMatKhau.Password;

            if (Login(username, password))
            {
                ViewTongQuanLyNhaSach f = new ViewTongQuanLyNhaSach(username);
                this.Hide();
                f.ShowDialog();
                this.Show();
            }
            else
            {
                count++;
                if (count <= 3)
                    MessageBox.Show("SAI TÊN ĐĂNG NHẬP HOẶC MẬT KHẨU!", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    MessageBoxResult result = MessageBox.Show("BẠN ĐÃ NHẬP SAI 3 LẦN LIÊN TIẾP. THOÁT CHƯƠNG TRÌNH ?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        Application.Current.Shutdown();
                    }
                    count = 1;
                }
            }
            txtTaiKhoan.Text = "";
            passwMatKhau.Password = "";
        }

        bool Login(string username, string password)
        {
            return Account.Instance.Login(username, password);
        }


        private void btThoat_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát khỏi chương trình không?",
             "Xác nhận thoát chương trình",
         MessageBoxButton.YesNo,
         MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }

        }
    }
}

