using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace OpenData.repository
{
    class Repositorys
    {
        public SqlConnection Connection()
        {
            //建立連線
            string strConn = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\軟體工程\OpenData\xml-analysis\OpenData\OpenData\App_Data\Database1.mdf;Integrated Security=True";
            SqlConnection myConn = new SqlConnection(strConn);
            return myConn;
        }

        public void Insert_Data_SQL(SqlConnection conn, OpenData item)
        {
            conn.Open();

            string sql_Insert = "  INSERT INTO STable(資料集名稱, 資料集提供機關聯絡人, 資料集描述 , 授權方式) VALUES ( N'" + item.資料集名稱 + "',N'" + item.資料集提供機關聯絡人 + "',N'" + item.資料集描述 + "',N'" + item.授權方式 + "')";

            SqlCommand mySqlCmd = new SqlCommand(sql_Insert, conn);

            mySqlCmd.ExecuteNonQuery();

            conn.Close();
        }

    }
}
