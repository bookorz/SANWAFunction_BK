using MySql.Data.MySqlClient;
using SANWA.Utility.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using log4net;

namespace SANWA.Utility
{
    public class DBUtil
    {

        public enum QueryContainer
        {
            DBController,
            DBEquipmentModel
        }
        static ILog logger = LogManager.GetLogger(typeof(DBUtil));
        MySqlConnection Connection_;

        private void open_Conn()
        {

            string connectionStr = SystemConfig.Get().DBConnectionString;
            Connection_ = new MySqlConnection(connectionStr);
            Connection_.Open();
            //MessageBox.Show("Connect OK!");
        }

        private void close_Conn()
        {
            if (Connection_ != null)
            {
                Connection_.Close();
                //MessageBox.Show("Connect Close!");
            }
        }

        /// <summary>
        /// 取得 MySqlDataReader 
        /// while (data.Read())
        ///    {
        ///     //以欄位名稱取得資料並列出
        ///      Console.WriteLine("id={0} , name={1}", data["list_id"], data["list_name"]);
        ///     //以欄位順序取得資料並列出
        ///        Console.WriteLine("id={0} , name={1}", data[0], data[1]);
        ///    }
        ///data.Close();
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">參數</param>
        /// <returns></returns>
        public DataTableReader GetDataReader(string sql, Dictionary<string, object> parameters)
        {
            DataTableReader reader = null;
            try
            {
                //sql = "SELECT * FROM list_item";
                open_Conn();
                MySqlCommand command = new MySqlCommand(sql, Connection_);
                // set parameters
                foreach (KeyValuePair<string, object> param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                //get query result
                MySqlDataReader rs = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(rs);
                reader =  dt.CreateDataReader();
                close_Conn();
            }
            catch(Exception e)
            {
                logger.Error(e.StackTrace);
            }
            return reader;
        }

        /// <summary>
        /// 取得 DataAdapter , 可做為dataGridView 的 source
        /// DataTable dataTable = new DataTable();
        /// adapter.Fill(dataTable);
        /// dataGridViewMariaDB.DataSource = dataTable;
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public MySqlDataAdapter GetDataAdapter(string sql)
        {
            //sql = "SELECT * FROM list_item";
            open_Conn();
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, Connection_);
            close_Conn();
            return adapter;
        }

        /// <summary>
        /// 執行非 Query 類 SQL
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">參數</param>
        /// <returns>影響筆數</returns>
        public int ExecuteNonQuery(string sql, Dictionary<string, object> parameters)
        {
            //sql = string.Format("UPDATE list_item SET modify_timestamp = NOW()");
            string sqlInfo = sql+" : ";
            open_Conn();
            MySqlCommand command = new MySqlCommand(sql, Connection_);
            // set parameters
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                    sqlInfo += param.Key + " - " + param.Value;
                }
            }
            //logger.Debug("ExecuteNonQuery  "+ sqlInfo);
            int affectLines = command.ExecuteNonQuery();
            close_Conn();
            return affectLines;
        }

        /// <summary>
        /// 取得 MySqlDataTable
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">參數</param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql, Dictionary<string, object> parameters)
        {
            DataTable dt = new DataTable();
            try
            {
                //sql = "SELECT * FROM list_item";
                open_Conn();
                MySqlCommand command = new MySqlCommand(sql, Connection_);

                // set parameters
                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> param in parameters)
                    {
                        command.Parameters.AddWithValue(param.Key, param.Value);
                    }
                }

                //get query result
                MySqlDataReader rs = command.ExecuteReader();
                dt.Load(rs);
                close_Conn();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            return dt;
        }

        public IEnumerable<SANWA.Utility.CDBContainer> GetDataList(QueryContainer Container, string sql, Dictionary<string, object> parameters)
        {
            IEnumerable<SANWA.Utility.CDBContainer> dBContainers = null;

            try
            {
                open_Conn();

                switch (Container)
                {
                    case QueryContainer.DBController:
                        dBContainers = Connection_.Query<SANWA.Utility.DBController>(sql, parameters);
                        break;

                    case QueryContainer.DBEquipmentModel:
                        dBContainers = Connection_.Query<SANWA.Utility.DBEquipmentModel>(sql, parameters);
                        break;
                }

                close_Conn();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return dBContainers;
        }
    }
}
