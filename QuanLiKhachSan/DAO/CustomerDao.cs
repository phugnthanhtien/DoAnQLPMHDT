﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanLiKhachSan.DAO
{
    class CustomerDao
    {
        public DataTable LayDanhSach()
        {
            DataTable dt = new DataTable();
            string sql = "select* from View_Customer";
            SqlConnection conn = DbConnection.conn;
            try
            {
                conn.Open();
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
        public DataTable LayDanhSachTenKhach()
        {
            DataTable dt = new DataTable();
            string sql = "select* from View_Customer_Name";
            SqlConnection conn = DbConnection.conn;
            try
            {
                conn.Open();
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
        public void Them(string customerName, string gender, DateTime? birthday, string identifyCard, string phoneNumber, string email, string address, bool status)
        {
            SqlConnection conn = DbConnection.conn;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_add_customer";
            cmd.Parameters.Add("@customer_name", SqlDbType.NVarChar).Value = customerName;
            cmd.Parameters.Add("@gender", SqlDbType.NVarChar).Value = gender;
            cmd.Parameters.Add("@birthday", SqlDbType.Date).Value = birthday;
            cmd.Parameters.Add("@identify_card", SqlDbType.VarChar).Value = identifyCard;
            cmd.Parameters.Add("@phone_number", SqlDbType.VarChar).Value = phoneNumber;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
            cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = address;
            cmd.Parameters.Add("@status", SqlDbType.Bit).Value = status;

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
        public void Sua(int customerId, string customerName, string gender, DateTime? birthday, string identifyCard, string email, string address, bool status)
        {
            SqlConnection conn = DbConnection.conn;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_update_customer";
            cmd.Parameters.Add("@customer_id", SqlDbType.Int).Value = customerId;
            cmd.Parameters.Add("@customer_name", SqlDbType.NVarChar).Value = customerName;
            cmd.Parameters.Add("@gender", SqlDbType.NVarChar).Value = gender;
            cmd.Parameters.Add("@birthday", SqlDbType.Date).Value = birthday;
            cmd.Parameters.Add("@identify_card", SqlDbType.VarChar).Value = identifyCard;
            cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
            cmd.Parameters.Add("@address", SqlDbType.NVarChar).Value = address;
            cmd.Parameters.Add("@status", SqlDbType.Bit).Value = status;

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
        public void Xoa(int customerId)
        {
            SqlConnection conn = DbConnection.conn;
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_delete_customer";
            cmd.Parameters.Add("@customer_id", SqlDbType.Int).Value = customerId;

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
        public DataTable TimKiemTheoThongTinKH(string infor)
        {
            if (infor == null || infor == "")
            {
                return LayDanhSach();
            }
            else
            {
                string sql = "SELECT * FROM func_search_customer(@string)";
                DataTable dt = new DataTable();
                SqlConnection conn = DbConnection.conn;
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@string", infor);
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
        public DataTable TimKiemTheoNgaySinh(DateTime fromDate, DateTime toDate)
        {
            if (fromDate == null || toDate == null)
            {
                return LayDanhSach();
            }
            else
            {
                string sql = "SELECT * FROM func_search_customer_by_dob(@fromdate, @todate)";
                DataTable dt = new DataTable();
                SqlConnection conn = DbConnection.conn;
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(new SqlParameter("@fromdate", fromDate));
                    cmd.Parameters.Add(new SqlParameter("@todate", toDate));
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
