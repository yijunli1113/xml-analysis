
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
        
        

        public List<OpenData> select(SqlConnection conn)
        {
            conn.Open();
            var result = new List<OpenData>();

            var command = new SqlCommand("", conn);

            //command.CommandText = string.Format(@"Select Id,資料集名稱,資料集提供機關聯絡人,資料集描述,授權方式 From STable");
            command.CommandText = string.Format(@"Select * From STable");
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var item = new OpenData();
                item.Id = reader.GetInt32(0);
                item.資料集名稱 = reader.GetString(1);
                item.資料集提供機關聯絡人= reader.GetString(2);
                item.資料集描述 = reader.GetString(3);
                item.授權方式 = reader.GetString(4);
                result.Add(item);
            }

            conn.Close();

            return result;
        }
    }
}
