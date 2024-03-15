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
    /// Interaction logic for XoaSach.xaml
    /// </summary>
    public partial class XoaSach : Window
    {
        public XoaSach()
        {
            InitializeComponent();
            InitializeXoaSach();
        }
        void InitializeXoaSach()
        {
            string query = "Select MASACH, TENSACH, TENTG From SACH LEFT JOIN TACGIA ON SACH.MATG = TACGIA.MATG";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Clear existing items and columns
            lvSach.Items.Clear();

            // Add columns
            GridView gridView = new GridView();
            lvSach.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "MÃ SÁCH",
                DisplayMemberBinding = new Binding("MASACH"),
                Width = 100
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "TÊN SÁCH",
                DisplayMemberBinding = new Binding("TENSACH"),
                Width = 230
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "TÊN TÁC GIẢ",
                DisplayMemberBinding = new Binding("TENTG"),
                Width = 150
            });

            // Bind data to ListView
            lvSach.ItemsSource = data.DefaultView;
        }
        private void btThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btXoa_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = lvSach.SelectedItems.Cast<DataRowView>().ToList(); // tạo danh sách tạm thời
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
                            string Ma = item["MASACH"].ToString();
                            string XoaKho = "Delete From KHO Where MASACH = '" + Ma + "'";
                            string query = "Delete From SACH Where MASACH = '" + Ma + "'";
                            
                            DataTable data1 = DataProvider.Instance.ExecuteQuery(XoaKho);
                            DataTable data = DataProvider.Instance.ExecuteQuery(query);
                        }
                        lvSach.SelectedItems.Clear(); // xóa các phần tử đã chọn khỏi danh sách SelectedItems
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
    }
}
