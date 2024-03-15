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
    /// Interaction logic for ThongKe.xaml
    /// </summary>

    public partial class ThongKe : Window
    {
        //private List<string> ThongKeList = new List<string>();
        public ThongKe()
        {
            InitializeComponent();
            //dgvThongKe.ItemsSource = ThongKeList;
          

        }
        void InitializeThongKe()
        {
            DateTime tungay = DatePickerTuNgay.SelectedDate.Value;
            DateTime denngay = DatePickerDenNgay.SelectedDate.Value;
            string query = "SELECT MAHOADON, TENKHACHHANG, NGAYLAP, TONGTIEN FROM HOADON WHERE NGAYLAP BETWEEN @tungay AND @denngay";
            DataTable data = DataProvider.Instance.ExecuteQuery(query, new object[] { tungay, denngay });
            // dgvThongKe.ItemsSource = ThongKeList;
            GridView gridView = new GridView();
            dgvThongKe.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Mã Hoá Đơn",
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
           
            this.dgvThongKe.ItemsSource = data.DefaultView;

            double Sum = 0;
            foreach (DataRow row in data.Rows)
            {
                double amount = Convert.ToDouble(row["TONGTIEN"]);
                Sum += amount;
            }

            // Display total amount
            txtSum.Text = Sum.ToString();
        }

        private void btThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btTimKiem_Click(object sender, RoutedEventArgs e)
        {
            InitializeThongKe();    
        }

        private void dgvThongKe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}


