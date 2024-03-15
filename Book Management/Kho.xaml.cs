using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
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
    /// Interaction logic for Kho.xaml
    /// </summary>
    public partial class Kho : Window
    {

        public Kho()
        {
            InitializeComponent();
            InitializeKho();
           

        }
        void InitializeKho()
        {
            string query = "Select SACH.MASACH, TENSACH, SOLUONG From KHO LEFT JOIN SACH ON KHO.MASACH = SACH.MASACH";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            // Clear existing items and columns
            lvKho.Items.Clear();

            // Add columns
            GridView gridView = new GridView();
            lvKho.View = gridView;

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "MÃ SÁCH",
                DisplayMemberBinding = new Binding("MASACH"),
                Width = 150
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "TÊN SÁCH",
                DisplayMemberBinding = new Binding("TENSACH"),
                Width = 250
            });

            gridView.Columns.Add(new GridViewColumn
            {
                Header = "SỐ LƯỢNG CÒN LẠI",
                DisplayMemberBinding = new Binding("SOLUONG"),
                Width = 150
            });

            // Bind data to ListView
            lvKho.ItemsSource = data.DefaultView;
        }

        private void lvKho_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView row = (DataRowView)lvKho.SelectedItems[0];
            string maSach = row["MASACH"].ToString();
            txtMaSach.Text = maSach;
            string tenSach = row["TENSACH"].ToString();
            txtTenSach.Text = tenSach;
            int soLuong = int.Parse(row["SOLUONG"].ToString());
            txtSoLuong.Text = soLuong.ToString();
        }

        private void btLuu_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected row
            DataRowView row = (DataRowView)lvKho.SelectedItems[0];

            // Get the values of the selected row
            string maSach = row["MASACH"].ToString();
            string tenSach = row["TENSACH"].ToString();

            // Get the new value of SOLUONG from the txtSoLuong TextBox
            int newSoLuong = int.Parse(txtSoLuong.Text);

            // Update the SOLUONG column in the database
            string query = $"UPDATE KHO SET SOLUONG = {newSoLuong} WHERE MASACH = '{maSach}'";
            int result = DataProvider.Instance.ExecuteNonQuery(query);

            if (result > 0)
            {
                // Update the ListView item
                row["SOLUONG"] = newSoLuong;
                MessageBox.Show("Cập nhật thành công!");
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }

        private void txtMaSach_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = txtSearch.Text.Trim();

            // Build the SQL query with the search keyword
            string query = $"SELECT SACH.MASACH, TENSACH, SOLUONG FROM KHO " +
                $"LEFT JOIN SACH ON KHO.MASACH = SACH.MASACH " +
                $"WHERE SACH.MASACH LIKE '%{keyword}%' OR TENSACH LIKE '%{keyword}%'";

            // Execute the query and update the listview
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            lvKho.ItemsSource = data.DefaultView;
        }
    }
}
