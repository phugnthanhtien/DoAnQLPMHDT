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
    class RoomDao
    {
        public DataTable LayDanhSach()
        {
            DataTable dt = new DataTable();
            SqlConnection conn = DbConnection.conn;
            try
            {
                conn.Open();
                string sql = "select* from View_Room";
                SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
                adapter.Fill(dt);
            }
            catch(Exception ex) 
            { 
                MessageBox.Show(ex.Message); 
            }
            finally { conn.Close(); }
            return dt;
        }
        /*
        public void Them(string tenPhong, int sucChua, string trangThai, string moTa, string anh, int loaiPhong)
        {
            SqlConnection conn = DbConnection.conn;
            string sql = $"insert into Room(room_name, room_capacity, room_status, room_description, room_image, room_type_id) " +
                $"values('{tenPhong}', {sucChua},'{trangThai}',N'{moTa}',{anh},{loaiPhong})";
            MessageBox.Show("OK");
            try
            {
                conn.Open();
                SqlCommand cm = new SqlCommand(sql, conn);
                if (cm.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("thanh cong");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message );
            }
            finally
            {
                conn.Close();
            }
        }
        */
        public DataTable LayDanhSachTenPhong()
        {
            DataTable dt = new DataTable() ;
            string sql = "select* from View_Room_Name";
            SqlConnection conn = DbConnection.conn;
            try
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql,conn);
                adapter.Fill(dt);
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message );
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        public void Them(string roomName, int roomCapacity, string roomStatus, string roomDescription,
            byte[] roomImage, int roomTypeId)
        {
            SqlConnection conn = DbConnection.conn;
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "proc_add_room";
                cmd.Parameters.Add("@room_name", SqlDbType.NVarChar).Value = roomName;
                cmd.Parameters.Add("@room_capacity", SqlDbType.Int).Value = roomCapacity;
                cmd.Parameters.Add("@room_status", SqlDbType.NVarChar).Value = roomStatus;
                cmd.Parameters.Add("@room_description", SqlDbType.NVarChar).Value = roomDescription;
                cmd.Parameters.Add("@room_image", SqlDbType.VarBinary).Value = roomImage;
                cmd.Parameters.Add("@room_type_id", SqlDbType.Int).Value = roomTypeId;
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
        public void Sua(int roomId, string roomName, int roomCapacity, string roomStatus, string roomDescription,
            byte[] roomImage, int roomTypeId)
        {
            SqlConnection conn = DbConnection.conn;
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "proc_update_room";
                cmd.Parameters.Add("@room_id", SqlDbType.Int).Value = roomId;
                cmd.Parameters.Add("@room_name", SqlDbType.NVarChar).Value = roomName;
                cmd.Parameters.Add("@room_capacity", SqlDbType.Int).Value = roomCapacity;
                cmd.Parameters.Add("@room_status", SqlDbType.NVarChar).Value = roomStatus;
                cmd.Parameters.Add("@room_description", SqlDbType.NVarChar).Value = roomDescription;
                cmd.Parameters.Add("@room_image", SqlDbType.VarBinary).Value = roomImage;
                cmd.Parameters.Add("@room_type_id", SqlDbType.Int).Value = roomTypeId;
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
        public void Xoa(int roomId)
        {
            SqlConnection conn = DbConnection.conn;
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "proc_delete_room";
                cmd.Parameters.Add("@room_id", SqlDbType.Int).Value = roomId;
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
    }
}
