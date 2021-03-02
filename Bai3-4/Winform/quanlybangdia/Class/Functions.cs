using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Quanlybangdia.Class
{
    class Functions
    {
        public static SqlConnection conn;
        public static void Connect()
        {
            conn = new SqlConnection("Data Source=DESKTOP-71BIMAM;Initial Catalog=quanlybangdia;Integrated Security=True");
            conn.Open();
            if (conn.State == ConnectionState.Open)
                MessageBox.Show("Kết nối thành công");

            else MessageBox.Show("Kết nối không thành công");
        }

        public static void DisConnect()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        public static DataTable GetDataToTable(string sql)
        {
            SqlDataAdapter MyData = new SqlDataAdapter();

            MyData.SelectCommand = new SqlCommand();
            MyData.SelectCommand.Connection = Functions.conn;
            MyData.SelectCommand.CommandText = sql;

            DataTable table = new DataTable();
            MyData.Fill(table);
            return table;
        }

        public static bool CheckKey(string sql)
        {

            SqlDataAdapter MyData = new SqlDataAdapter(sql, conn);
            DataTable table = new DataTable();
            MyData.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }

        public static void RunSQL(string sql)
        {
            SqlCommand cmd;
            cmd = new SqlCommand();

            cmd.Connection = conn;

            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }

        public static void RunSqlDel(string sql)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = Functions.conn;
            cmd.CommandText = sql;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
            cmd.Dispose();
            cmd = null;
        }

    }
}