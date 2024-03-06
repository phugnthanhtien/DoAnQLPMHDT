using QuanLiKhachSan.DAO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLiKhachSan
{
    /// <summary>
    /// Interaction logic for UcKhachHang.xaml
    /// </summary>
    public partial class UcKhachHang : UserControl
    {
        CustomerDao customerDao = new CustomerDao();
        PhoneNumberOfCustomerDao phoneDao = new PhoneNumberOfCustomerDao();
        public UcKhachHang()
        {
            InitializeComponent();
        }

        public void LayDanhSach()
        {
            dtgDanhSachKhachHang.ItemsSource = customerDao.LayDanhSach().DefaultView;
            dtgDanhSachSdtCuaKhach.ItemsSource = phoneDao.LayDanhSach().DefaultView;
            cbKhachHangCuaSDT.ItemsSource = customerDao.LayDanhSachTenKhach().AsEnumerable()
                .Select(x => x.Field<int>("customer_id").ToString() + "|" + x.Field<string>("customer_name"))
                .ToList<string>();
        }

        private void btnThemKhachHang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                customerDao.Them(txtTenKhachHang.Text, cbGioiTinh.Text, dtpNgaySinh.SelectedDate, txtIdentifyCard.Text, txtPhoneNumber.Text, txtEmail.Text, txtDiaChi.Text, Boolean.Parse(chbTrangThai.IsChecked.ToString()));
                LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSuaKhachHang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                customerDao.Sua(int.Parse((string)lbMaKhachHang.Content),txtTenKhachHang.Text, cbGioiTinh.Text, dtpNgaySinh.SelectedDate, txtIdentifyCard.Text, txtEmail.Text, txtDiaChi.Text, Boolean.Parse(chbTrangThai.IsChecked.ToString()));
                LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThongTinKhachHang_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)dtgDanhSachKhachHang.SelectedValue;
            try
            {
                lbMaKhachHang.Content = drv["customer_id"].ToString();
                txtTenKhachHang.Text = drv["customer_name"].ToString();
                cbGioiTinh.SelectedValue = drv["gender"].ToString();
                txtEmail.Text = drv["email"].ToString();
                dtpNgaySinh.SelectedDate = DateTime.Parse(drv["birthday"].ToString());
                txtIdentifyCard.Text = drv["identify_card"].ToString();
                txtDiaChi.Text = drv["address"].ToString();
                chbTrangThai.IsChecked = (bool)drv["status"];
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnXoaKhachHang_Click(object sender, RoutedEventArgs e)
        {

            DataRowView drv = (DataRowView)dtgDanhSachKhachHang.SelectedValue;
            try
            {
                customerDao.Xoa((int)drv["customer_id"]);
                LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSuaSdtKhach_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btnThemSdtKhach_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                phoneDao.Them(txtSDT.Text, int.Parse(((string)cbKhachHangCuaSDT.SelectedValue).Split("|")[0]));
                LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnThongTinSdtKhach_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)dtgDanhSachSdtCuaKhach.SelectedValue;
            try
            {
                cbKhachHangCuaSDT.SelectedValue = drv["customer_id"].ToString() + "|" + drv["customer_name"].ToString();
                txtSDT.Text = drv["phone_number"].ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);    
            }

        }

        private void btnXoaSdtKhach_Click(object sender, RoutedEventArgs e)
        {
            DataRowView drv = (DataRowView)dtgDanhSachSdtCuaKhach.SelectedValue;
            try
            {
                phoneDao.Xoa(drv["phone_number"].ToString(), int.Parse(drv["customer_id"].ToString()));
                LayDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = ((TextBox)sender).Text;
            dtgDanhSachKhachHang.ItemsSource = phoneDao.TimKiem(searchText).DefaultView;
            dtgDanhSachSdtCuaKhach.ItemsSource = phoneDao.TimKiem(searchText).DefaultView;
        }

        private void btnTimKiemTheoThongTinKH_Click(object sender, RoutedEventArgs e)
        {
            string infor = txtLocTheoThongTinKH.Text;
            dtgDanhSachKhachHang.ItemsSource = customerDao.TimKiemTheoThongTinKH(infor).DefaultView;
        }

        private void btnTimKiemTheoNgaySinh_Click(object sender, RoutedEventArgs e)
        {
            if (dtpFromDoB.SelectedDate != null && dtpToDoB.SelectedDate != null)
            {
                DateTime fromDate = dtpFromDoB.SelectedDate.Value;
                DateTime toDate = dtpToDoB.SelectedDate.Value;
                dtgDanhSachKhachHang.ItemsSource = customerDao.TimKiemTheoNgaySinh(fromDate, toDate).DefaultView;
            }
        }
    }
}
