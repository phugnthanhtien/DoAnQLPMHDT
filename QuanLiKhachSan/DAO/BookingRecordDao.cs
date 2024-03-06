using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiKhachSan.DAO
{
    class BookingRecordDao
    {
        public DataTable layDanhSach()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = DbConnection.conn;
            try
            {
                conn.Open();
                String sql = "Select* from View_Booking_Record";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            return dt;
        }
        public DataTable LayDsHoSoDaCheckin()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = DbConnection.conn;
            try
            {
                conn.Open();
                String sql = "Select* from View_Confirmed_Booking_Record";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            return dt;
        }
        public DataTable LayDsHoSoChuaCheckin()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = DbConnection.conn;
            try
            {
                conn.Open();
                String sql = "Select* from View_Pending_Booking_Record";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            return dt;
        }
        public DataTable LayDsHoSoDaBiHuy()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = DbConnection.conn;
            try
            {
                conn.Open();
                String sql = "Select* from View_Canceled_Booking_Record";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            return dt;
        }
        public DataTable LayDsHoSoDaDatCoc()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = DbConnection.conn;
            try
            {
                conn.Open();
                String sql = "Select* from View_Deposit_Booking_Record";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            return dt;
        }
        public DataTable LayDsHoSoChuaDatCoc()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = DbConnection.conn;
            try
            {
                conn.Open();
                String sql = "Select* from View_No_Deposit_Booking_Record";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            return dt;
        }
        public void Delete(int bookingRecordId)
        {
            SqlConnection conn = DbConnection.conn;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_deleteBookingRecord";
            cmd.Parameters.Add("@booking_record_id", SqlDbType.Int).Value = bookingRecordId;

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xóa thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public void Update(int bookingRecordId, DateTime? expectedCheckinDate, DateTime? expectedCheckoutDate,
            DateTime? actualCheckinDate, DateTime? actualCheckoutDate, float deposit, float surcharge,
            string note, string status, int representativeId, int roomId)
        {
            SqlConnection conn = DbConnection.conn;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_updateBookingRecord";
            cmd.Parameters.AddWithValue("@booking_record_id", bookingRecordId);
            cmd.Parameters.AddWithValue("@expected_checkin_date", (object)expectedCheckinDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@expected_checkout_date", (object)expectedCheckoutDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@actual_checkin_date", (object)actualCheckinDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@actual_checkout_date", (object)actualCheckoutDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@deposit", deposit);
            cmd.Parameters.AddWithValue("@surcharge", surcharge);
            cmd.Parameters.AddWithValue("@note", note);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@representative_id", representativeId);
            cmd.Parameters.AddWithValue("@room_id", roomId);
            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Sửa thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
        public void Insert(DateTime? expectedCheckinDate, DateTime? expectedCheckoutDate,
            DateTime? actualCheckinDate, DateTime? actualCheckoutDate, float deposit, float surcharge,
            string note, string status, int representativeId, int roomId)
        {
            SqlConnection conn = DbConnection.conn;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_insertBookingRecord";
            cmd.Parameters.AddWithValue("@expected_checkin_date", (object)expectedCheckinDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@expected_checkout_date", (object)expectedCheckoutDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@actual_checkin_date", (object)actualCheckinDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@actual_checkout_date", (object)actualCheckoutDate ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@deposit", deposit);
            cmd.Parameters.AddWithValue("@surcharge", surcharge);
            cmd.Parameters.AddWithValue("@note", note);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@representative_id", representativeId);
            cmd.Parameters.AddWithValue("@room_id", roomId);

            try
            {
                conn.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Thêm thành công");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }
        public DataTable TimKiemHopDongTheoTenKhachHang(string customerName)
        {
            if (customerName == null || customerName == "")
            {
                return LayDsHoSoDaDatCoc();
            }
            else
            {
                string sql = "SELECT * FROM func_getBookingRecordByCustomerName(@customer_name)";
                DataTable dt = new DataTable();
                SqlConnection conn = DbConnection.conn;
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@customer_name", customerName);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { conn.Close(); }
                return dt;
            }
        }
        public DataTable TimKiemHopDongTheoTenPhong(string roomName)
        {
            if (roomName == null || roomName == "")
            {
                return LayDsHoSoDaDatCoc();
            }
            else
            {
                string sql = "SELECT * FROM func_getBookingRecordByRoomName(@room_name)";
                DataTable dt = new DataTable();
                SqlConnection conn = DbConnection.conn;
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@room_name", roomName);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally { conn.Close(); }
                return dt;
            }
        }
    }
}