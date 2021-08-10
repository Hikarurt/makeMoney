using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using Microsoft.Office.Interop.Access.Dao;
using System.Data.SqlClient;

namespace zj
{
    class AccessHelper
    {
        private OleDbConnection conn = null;
        public static string m_strCurrentPath = Application.StartupPath + "\\";
        static string connProvider = System.Configuration.ConfigurationSettings.AppSettings["ConnProvider"];
        //static string connSource = System.Configuration.ConfigurationSettings.AppSettings["ConnSource"];
        private string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data source =" + m_strCurrentPath + "database\\zj.mdb;Jet OLEDB:Database Password=*#886402#;";
        private string connString_receive = "Provider=Microsoft.ACE.OLEDB.12.0;Data source =" + m_strCurrentPath + "receive\\decrypt.mdb;Jet OLEDB:Database Password=*#886402#;";

        private string connString_RT = "Provider=Microsoft.ACE.OLEDB.12.0;Data source =" + m_strCurrentPath + "database\\rtsj.mdb;Jet OLEDB:Database Password=*#886402#;";

        public AccessHelper()//此为构造函数
        {
            conn = new OleDbConnection(connString);
        }

        /// 返回一个可用的连接
        public OleDbConnection getcon()
        {
            conn.ConnectionString = connString;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conn;
        }

        public OleDbConnection getcon_rt()
        {
            conn.ConnectionString = connString_RT;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conn;
        }
        public OleDbConnection getcon_receive()
        {
            conn.ConnectionString = connString_receive;
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return conn;
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void CloseDB()
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }


        /// <summary>
        /// TCLCData批量插入（ACCESS）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool ExPiliang(List<TCLCData> list)
        {
            bool result = false;
            DBEngine dbEngine = new DBEngine();
            Database db = dbEngine.OpenDatabase(m_strCurrentPath + "database\\zj.mdb", true, false, "MS Access;PWD=*#886402#");
            Recordset rs = db.OpenRecordset("t_lctc");
            try
            {
                Field[] myFields = new Field[18];
                myFields[0] = rs.Fields["ID"];
                myFields[1] = rs.Fields["dwdm"];
                myFields[2] = rs.Fields["bmbs"];
                myFields[3] = rs.Fields["lb"];
                myFields[4] = rs.Fields["pm"];
                myFields[5] = rs.Fields["ly"];
                myFields[6] = rs.Fields["hqsj"];
                myFields[7] = rs.Fields["sl"];
                myFields[8] = rs.Fields["jldw"];
                myFields[9] = rs.Fields["dj"];
                myFields[10] = rs.Fields["zz"];
                myFields[11] = rs.Fields["kysl"];
                myFields[12] = rs.Fields["kbxjz"];
                myFields[13] = rs.Fields["czfs"];
                myFields[14] = rs.Fields["bz"];
                myFields[15] = rs.Fields["wpbs"];
                myFields[16] = rs.Fields["djlx"];
                myFields[17] = rs.Fields["dwbm"];

                foreach (TCLCData item in list)
                {
                    rs.AddNew();
                    myFields[1].Value = item.dwdm;
                    myFields[2].Value = item.bmbs;
                    myFields[3].Value = item.lb;
                    myFields[4].Value = item.pm;
                    myFields[5].Value = item.ly;
                    myFields[6].Value = item.hqsj;
                    myFields[7].Value = item.sl;
                    myFields[8].Value = item.jldw;
                    myFields[9].Value = item.dj;
                    myFields[10].Value = item.zz;
                    myFields[11].Value = item.kysl;
                    myFields[12].Value = item.kbxjz;
                    myFields[13].Value = item.czfs;
                    myFields[14].Value = item.bz;
                    myFields[15].Value = item.wpbs;
                    myFields[16].Value = item.djlx;
                    rs.Update();
                }
                result = true;
            }
            catch 
            {
                result = false;
            }
            finally
            {
                rs.Close();
                db.Close();
            }

            return result;
        }



        /// <summary>
        /// TCLCData批量插入（ACCESS）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool ExPiliang_rt(List<RTSJDR.WP_LCTCdata> list)
        {
            bool result = false;
            DBEngine dbEngine = new DBEngine();
            Database db = dbEngine.OpenDatabase(m_strCurrentPath + "database\\zj.mdb", true, false, "MS Access;PWD=*#886402#");
            Recordset rs = db.OpenRecordset("t_lctc");
            try
            {
                Field[] myFields = new Field[21];
                myFields[0] = rs.Fields["ID"];
                myFields[1] = rs.Fields["dwdm"];
                myFields[2] = rs.Fields["bmbs"];
                myFields[3] = rs.Fields["lb"];
                myFields[4] = rs.Fields["pm"];
                myFields[5] = rs.Fields["ly"];
                myFields[6] = rs.Fields["hqsj"];
                myFields[7] = rs.Fields["sl"];
                myFields[8] = rs.Fields["jldw"];
                myFields[9] = rs.Fields["dj"];
                myFields[10] = rs.Fields["zz"];
                myFields[11] = rs.Fields["kysl"];
                myFields[12] = rs.Fields["kbxjz"];
                myFields[13] = rs.Fields["czfs"];
                myFields[14] = rs.Fields["bz"];
                myFields[15] = rs.Fields["wpbs"];
                myFields[16] = rs.Fields["djlx"];
                myFields[17] = rs.Fields["dwbm"];
                myFields[18] = rs.Fields["jjqrsl"];
                myFields[19] = rs.Fields["jjqrpm"];
                myFields[20] = rs.Fields["jjbz"];

                foreach (RTSJDR.WP_LCTCdata item in list)
                {
                    rs.AddNew();
                    myFields[1].Value = item.dwdm;
                    myFields[2].Value = item.bmbs;
                    myFields[3].Value = item.lb;
                    myFields[4].Value = item.pm;
                    myFields[5].Value = item.ly;
                    myFields[6].Value = item.hqsj;
                    myFields[7].Value = item.sl;
                    myFields[8].Value = item.jldw;
                    myFields[9].Value = item.dj;
                    myFields[10].Value = item.zz;
                    myFields[11].Value = item.kysl;
                    myFields[12].Value = item.kbxjz;
                    myFields[13].Value = item.czfs;
                    myFields[14].Value = item.bz;
                    myFields[15].Value = item.wpbs;
                    myFields[16].Value = item.djlx;
                    myFields[17].Value = item.dwbm;
                    myFields[18].Value = item.jjqrsl;
                    myFields[19].Value = item.jjqrpm;
                    myFields[20].Value = item.jjbz;
                    rs.Update();
                }
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                rs.Close();
                db.Close();
            }

            return result;
        }

        /// <summary>
        /// TCLCData批量插入（ACCESS）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool ExPiliang_rt_lctc(List<LCTC> list)
        {
            bool result = false;
            DBEngine dbEngine = new DBEngine();
            Database db = dbEngine.OpenDatabase(m_strCurrentPath + "database\\rtsj.mdb", true, false, "MS Access;PWD=*#886402#");
            Recordset rs = db.OpenRecordset("t_lctc");
            try
            {
                Field[] myFields = new Field[19];
                myFields[0] = rs.Fields["ID"];
                myFields[1] = rs.Fields["wpbs"];
                myFields[2] = rs.Fields["dwdm"];
                myFields[3] = rs.Fields["lb"];
                myFields[4] = rs.Fields["pm"];
                myFields[5] = rs.Fields["ly"];
                myFields[6] = rs.Fields["hqsj"];
                myFields[7] = rs.Fields["sl"];
                myFields[8] = rs.Fields["jldw"];
                myFields[9] = rs.Fields["dj"];
                myFields[10] = rs.Fields["zz"];
                myFields[11] = rs.Fields["kysl"];
                myFields[12] = rs.Fields["kbxjz"];
                myFields[13] = rs.Fields["czfs"];
                myFields[14] = rs.Fields["bz"];
                myFields[15] = rs.Fields["djlx"];
                myFields[16] = rs.Fields["jjqrsl"];
                myFields[17] = rs.Fields["jjqrpm"];
                myFields[18] = rs.Fields["jjbz"];

                foreach (LCTC item in list)
                {
                    rs.AddNew();
                    myFields[1].Value =EncodeBase64(item.wpbs.ToString());
                    myFields[2].Value =EncodeBase64(item.dwdm.ToString());
                    myFields[3].Value =EncodeBase64(item.lb.ToString());
                    myFields[4].Value =EncodeBase64(item.pm.ToString());
                    myFields[5].Value =EncodeBase64(item.ly.ToString());
                    myFields[6].Value =EncodeBase64(item.hqsj.ToString());
                    myFields[7].Value =EncodeBase64(item.sl.ToString());
                    myFields[8].Value =EncodeBase64(item.jldw.ToString());
                    myFields[9].Value = EncodeBase64(item.dj.ToString());
                    myFields[10].Value =EncodeBase64(item.zz.ToString());
                    myFields[11].Value =EncodeBase64(item.kysl.ToString());
                    myFields[12].Value =EncodeBase64(item.kbxjz.ToString());
                    myFields[13].Value =EncodeBase64(item.czfs.ToString());
                    myFields[14].Value =EncodeBase64(item.bz.ToString());
                    myFields[15].Value =EncodeBase64(item.djlx.ToString());
                    myFields[16].Value =EncodeBase64(item.jjqrsl.ToString());
                    myFields[17].Value =EncodeBase64(item.jjqrsl.ToString());
                    myFields[18].Value = EncodeBase64(item.jjbz.ToString());
                    rs.Update();
                }
                result = true;
            }
            catch
            {
                result = false;
            }
            finally
            {
                rs.Close();
                db.Close();
            }

            return result;
        }


        /// <summary>
        /// TCLCData批量插入（ACCESS_rt）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool ExPiliang_rt_dwxx(List<DWXX> list)
        {
            bool result = false;
            DBEngine dbEngine = new DBEngine();
            Database db = dbEngine.OpenDatabase(m_strCurrentPath + "database\\rtsj.mdb", true, false, "MS Access;PWD=*#886402#");
            Recordset rs = db.OpenRecordset("t_dwxx");
            try
            {
                Field[] myFields = new Field[6];
                myFields[0] = rs.Fields["ID"];
                myFields[1] = rs.Fields["dwdm"];
                myFields[2] = rs.Fields["yjdd"];
                myFields[3] = rs.Fields["yjsheng"];
                myFields[4] = rs.Fields["yjshi"];
                myFields[5] = rs.Fields["yjjsd"];

                foreach (DWXX item in list)
                {
                    rs.AddNew();
                    myFields[1].Value = EncodeBase64(item.dwdm.ToString());
                    myFields[2].Value = EncodeBase64(item.yjdd.ToString());
                    myFields[3].Value = EncodeBase64(item.yjsheng.ToString());
                    myFields[4].Value = EncodeBase64(item.yjshi.ToString());
                    myFields[5].Value = EncodeBase64(item.yjjsd.ToString());
                    rs.Update();
                }
                result = true;
            }
            catch 
            {
                result = false;
            }
            finally
            {
                rs.Close();
                db.Close();
            }

            return result;
        }

        /// <summary>
        /// Base64位加密
        /// </summary>
        /// <param name="code">需要加密的数据</param>
        /// <returns></returns>
        public static string EncodeBase64(string code)
        {
            string encode = "";
            byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }


        /// <summary>
        /// Base64为解码
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public  string DecodeBase64(string a)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(a);
            try
            {
                decode = Encoding.GetEncoding("UTF-8").GetString(bytes);
            }
            catch
            {
                decode = "MTE=";
            }
            return decode;
        }


        /// <summary>
        /// 执行增加、删除、修改操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parm">sql语句参数数组</param>
        /// <returns></returns>
        public int ExcueteCommand(string sql, params OleDbParameter[] parm)
        {
            //SqlCommand cmd = getcon().CreateCommand();
            OleDbCommand omd = getcon().CreateCommand();
            //cmd.CommandType = CommandType.Text;
            omd.CommandType = CommandType.Text;
            omd.CommandText = sql;
            int result = -1;
            if (parm != null)
            {
                //cmd.Parameters.AddRange(parm);
                omd.Parameters.AddRange(parm);
            }
            try
            {
                //result = cmd.ExecuteNonQuery();
                result = omd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //cmd.Dispose();
                omd.Dispose();
                CloseDB();
            }
            return result;

        }



        /// <summary>
        /// 执行增加、删除、修改操作
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parm">sql语句参数数组</param>
        /// <returns></returns>
        public int ExcueteCommand_rt(string sql, params OleDbParameter[] parm)
        {
            //SqlCommand cmd = getcon().CreateCommand();
            OleDbCommand omd = getcon_rt().CreateCommand();
            //cmd.CommandType = CommandType.Text;
            omd.CommandType = CommandType.Text;
            omd.CommandText = sql;
            int result = -1;
            if (parm != null)
            {
                //cmd.Parameters.AddRange(parm);
                omd.Parameters.AddRange(parm);
            }
            try
            {
                //result = cmd.ExecuteNonQuery();
                result = omd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //cmd.Dispose();
                omd.Dispose();
                CloseDB();
            }
            return result;

        }
        public int ExcueteCommand_receive(string sql, params OleDbParameter[] parm)
        {
            OleDbCommand omd = getcon_receive().CreateCommand();
            omd.CommandType = CommandType.Text;
            omd.CommandText = sql;
            int result = -1;
            if (parm != null)
            {
                omd.Parameters.AddRange(parm);
            }
            try
            {
                result = omd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                omd.Dispose();
                CloseDB();
            }
            return result;
        }

        /// <summary>
        /// 执行查询操作填充到dataset中
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parm">SQL语句参数数组</param>
        /// <returns></returns>
        public DataSet getds(string sql, params OleDbParameter[] parm)
        {
            //SqlDataAdapter sda = new SqlDataAdapter(sql, this.Connectionstring);
            OleDbDataAdapter oda = new OleDbDataAdapter(sql, connProvider);
            DataSet ds = new DataSet();
            if (parm != null)
            {
                //sda.SelectCommand.Parameters.AddRange(parm);
                oda.SelectCommand.Parameters.AddRange(parm);
            }
            try
            {
                //sda.Fill(ds);
                oda.Fill(ds);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //sda.Dispose();
                oda.Dispose();
            }
            return ds;
        }

        public DataSet getDataSet(String sql)
        {
            DataSet ds = new DataSet();
            OleDbConnection testConnection = getcon();
            OleDbDataAdapter oda = new OleDbDataAdapter(sql, testConnection);
            try
            {
                oda.Fill(ds);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                testConnection.Close();
                testConnection.Dispose();
                oda.Dispose();
            }
            return ds;
        }

        public DataSet getDataSet_rt(String sql)
        {
            DataSet ds = new DataSet();
            OleDbConnection testConnection = getcon_rt();
            OleDbDataAdapter oda = new OleDbDataAdapter(sql, testConnection);
            try
            {
                oda.Fill(ds);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                testConnection.Close();
                testConnection.Dispose();
                oda.Dispose();
            }
            return ds;
        }

        public DataSet getDataSet_receive(String sql)
        {
            DataSet ds = new DataSet();
            OleDbConnection testConnection = getcon_receive();
            OleDbDataAdapter oda = new OleDbDataAdapter(sql, testConnection);
            try
            {
                oda.Fill(ds);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                testConnection.Close();
                testConnection.Dispose();
                oda.Dispose();
            }
            return ds;
        }

        public OleDbDataReader GetReader(string sql, params OleDbParameter[] parm)
        {

            OleDbCommand omd = new OleDbCommand(sql, getcon());

            omd.Parameters.AddRange(parm);
            //SqlDataReader reader = cmd.ExecuteReader();
            OleDbDataReader reader = omd.ExecuteReader();
            return reader;

        }

        public OleDbDataReader GetReader(string safeSql)
        {
            OleDbCommand omd = new OleDbCommand(safeSql, getcon());
            OleDbDataReader reader = omd.ExecuteReader();
            return reader;
        }

        //执行查询语句
        public OleDbDataReader ExecuteReader(string sql)
        {
            OleDbDataReader reader = null; //查询结果
            try
            {

                OleDbCommand somd = new OleDbCommand(sql, getcon());
                reader = somd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conn.Close();
            return reader;
        }
        //执行带参查询语句
        public OleDbDataReader ExecuteReader(string sql, params OleDbParameter[] sp)
        {
            OleDbDataReader reader = null; //查询结果
            try
            {
                //Connect.Open();
                OleDbCommand odc = new OleDbCommand(sql, getcon());

                odc.Parameters.AddRange(sp);
                reader = odc.ExecuteReader();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            return reader;
        }

        /// <summary>
        /// 根据路径和sheet名称读取excel数据
        /// </summary>
        /// <param name="strExcelFileName">文件路径</param>
        /// <param name="strSheetName">sheet名称</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string strExcelFileName, string strSheetName)
        {
            //源的定义
            string strConn = "Provider=Microsoft.Ace.OLEDB.12.0;" + "Data Source=" + strExcelFileName + ";" + "Extended Properties='Excel 12.0;HDR=YES;IMEX=1';";


            //Sql语句
            //string strExcel = string.Format("select * from [{0}$]", strSheetName); 这是一种方法
            string strExcel = "select * from   [" + strSheetName + "$]";

            //定义存放的数据表
            DataSet ds = new DataSet();

            //连接数据源
            OleDbConnection conn = new OleDbConnection(strConn);

            conn.Open();

            //适配到数据源
            OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, strConn);
            try
            {
                adapter.Fill(ds, strSheetName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return ds.Tables[strSheetName];

        }
    }
}
