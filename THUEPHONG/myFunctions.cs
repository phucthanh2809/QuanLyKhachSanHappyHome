using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace THUEPHONG
{
    //File chứa các hàm dùng chung cho các trường hợp khi nào cần thì vô lấy ra 
    public class myFunctions
    {
        //Các biến về server,khi phần mềm chạy lên sẽ gán các thông số về server database cho các biến trong đây   
        public static string _macty;
        public static string _madvi;
        public static string _srv;
        public static string _us;
        public static string _pw;
        public static string _db;
        //Khai báo 1 biến SqlConnection
        static SqlConnection con = new SqlConnection();

        //Hàm tạo kết nối
        public static void taoketnoi()
        {
            con.ConnectionString = Program.connoi;
            try
            {
                con.Open(); // Mở kết nối đến CSDL
            }
            catch (Exception)
            {

            }
        }
        //Hàm đóng kết nối  
        public static void dongketnoi()
        {
            con.Close();
        }
        //Lấy dữ liệu ra 
        //Hàm đổ dữ liệu vào Datatable
        public static DataTable laydulieu(string qr)
        {
            taoketnoi();
            DataTable datatbl = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter();
            dap.SelectCommand = new SqlCommand(qr, con);
            dap.Fill(datatbl);
            dongketnoi();
            return datatbl;
        }
        public static DateTime GetFirstDayInMont(int year, int month)
        {
            //Hàm lấy về ngày đầu tháng
            return new DateTime(year, month, 1);
        }
    }
}
