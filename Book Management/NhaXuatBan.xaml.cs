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
    /// Interaction logic for NhaXuatBan.xaml
    /// </summary>
    public partial class NhaXuatBan : Window
    {

        private List<string> NXBList = new List<string>();

        public NhaXuatBan()
        {
            InitializeComponent();
            InitializeNhaXuatBan();
            dgvNhaXuatBan.ItemsSource = NXBList;
            cbXoaNXB.ItemsSource = NXBList;
        }

        // Khởi tạo danh sách các nhà xuất bản
        void InitializeNhaXuatBan()
        {
            string query = "SELECT TENNHAXUATBAN FROM NHAXUATBAN";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                NXBList.Add(row["TENNHAXUATBAN"].ToString());
            }
            this.dgvNhaXuatBan.ItemsSource = NXBList;
        }

        // Xử lý sự kiện click button "Thêm NXB"
        private void btThemNXB_Click(object sender, RoutedEventArgs e)
        {

            if (txtThemNXB.Text == "")
            {
                MessageBox.Show("CHƯA NHẬP TÊN NHÀ XUẤT BẢN!", "THÔNG BÁO");
            }
            else
            {
                try
                {
                    string query = "INSERT INTO NHAXUATBAN VALUES(N'" + txtThemNXB.Text + "')";
                    DataTable data = DataProvider.Instance.ExecuteQuery(query);

                    // Sau khi thêm thành công, cập nhật lại danh sách nhà xuất bản
                    NXBList.Add(txtThemNXB.Text);
                    txtThemNXB.Clear();
                    dgvNhaXuatBan.Items.Refresh();
                    cbXoaNXB.ItemsSource = null;
                    cbXoaNXB.ItemsSource = NXBList;
                    MessageBox.Show("ĐÃ THÊM!", "THÔNG BÁO");
                }
                catch
                {
                    MessageBox.Show("ĐÃ TỒN TẠI!", "THÔNG BÁO");
                }
            }

        }

        private void btXoaNXB_Click(object sender, RoutedEventArgs e)
        {
            string tenLinhVuc = cbXoaNXB.SelectedItem as string;
            if (cbXoaNXB.SelectedItem == null)
            {
                MessageBox.Show("CHƯA CHỌN NHÀ XUẤT BẢN ĐỂ XÓA!", "THÔNG BÁO");
            }
            else
            {
                try
                {
                    string tenNXB = cbXoaNXB.SelectedItem.ToString();
                    string query = "DELETE FROM NHAXUATBAN WHERE TENNHAXUATBAN = '" + tenNXB + "'";
                    DataTable data = DataProvider.Instance.ExecuteQuery(query);

                    NXBList.Remove(tenNXB);
                    dgvNhaXuatBan.Items.Refresh();
                    cbXoaNXB.ItemsSource = null;
                    cbXoaNXB.ItemsSource = NXBList;
                    MessageBox.Show("ĐÃ XÓA!", "THÔNG BÁO");
                }
                catch
                {
                    MessageBox.Show("CÓ LỖI XẢY RA!", "THÔNG BÁO");
                }
            }
        }

    }
}
