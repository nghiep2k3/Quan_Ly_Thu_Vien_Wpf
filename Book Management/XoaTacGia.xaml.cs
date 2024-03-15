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
    /// Interaction logic for XoaTacGia.xaml
    /// </summary>
    public partial class XoaTacGia : Window
    {
        public XoaTacGia()
        {
            InitializeComponent();
            InitializeXoaTacGia();
        }
        void InitializeXoaTacGia()
        {
            string query = "SELECT MATG, TENTG, NAMSINH, NAMMAT, QUEQUAN FROM TACGIA";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            this.dgvXoaTacGia.ItemsSource = data.DefaultView;


        }
        private void btThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btXoaTacGia_Click(object sender, RoutedEventArgs e)
        {

            var selectedItems = dgvXoaTacGia.SelectedItems.Cast<DataRowView>().ToList(); // tạo danh sách tạm thời
            int count0 = selectedItems.Count;

            if (count0 > 0)
            {
                MessageBoxResult result = MessageBox.Show("BẠN CÓ CHẮC CHẮN MUỐN XÓA?", "THÔNG BÁO", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        foreach (var item in selectedItems)
                        {
                            string Ma = item["MATG"].ToString();
                            string query = "Delete From TACGIA Where MATG = '" + Ma + "'";
                            DataTable data = DataProvider.Instance.ExecuteQuery(query);
                        }
                        dgvXoaTacGia.SelectedItems.Clear(); // xóa các phần tử đã chọn khỏi danh sách SelectedItems
                        foreach (var item in selectedItems)
                        {
                            item.Delete(); // xóa các phần tử khỏi DataTable và cập nhật giao diện
                        }
                        MessageBox.Show("ĐÃ XÓA!", "THÔNG BÁO");
                    }
                    catch
                    {
                        MessageBox.Show("KHÔNG THỂ XÓA!", "THÔNG BÁO");
                    }
                }
            }
            else
            {
                MessageBox.Show("KHÔNG CÓ GÌ ĐỂ XÓA!", "THÔNG BÁO");
            }


        }

        private void dgvXoaTacGia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
