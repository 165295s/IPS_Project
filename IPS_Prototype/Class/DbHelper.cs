using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


namespace IPS_Prototype.Class
{
    public class DbHelper
    {
        // Internal members
        protected string _connString = null;
        //    protected SqlConnection _conn = null;
        protected SqlConnection _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["IPS"].ConnectionString);
        protected SqlTransaction _trans = null;
        protected bool _disposed = false;

        /// <summary>
        /// Constructs a SqlCommand with the given parameters. This method is normally called
        /// from the other methods and not called directly. But here it is if you need access
        /// to it.
        /// </summary>
        /// <param name="qry">SQL query or stored procedure name</param>
        /// <param name="type">Type of SQL command</param>
        /// <param name="args">Query arguments. Arguments should be in pairs where one is the
        /// name of the parameter and the second is the value. The very last argument can
        /// optionally be a SqlParameter object for specifying a custom argument type</param>
        /// <returns></returns>
        public SqlCommand CreateCommand(string qry, CommandType type, params object[] args)
        {
            SqlCommand cmd = new SqlCommand(qry, _conn);

            // Associate with current transaction, if any
            if (_trans != null)
                cmd.Transaction = _trans;

            // Set command type
            cmd.CommandType = type;

            // Construct SQL parameters
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is string && i < (args.Length - 1))
                {
                    SqlParameter parm = new SqlParameter();
                    parm.ParameterName = (string)args[i];
                    parm.Value = args[++i];
                    cmd.Parameters.Add(parm);
                }
                else if (args[i] is SqlParameter)
                {
                    cmd.Parameters.Add((SqlParameter)args[i]);
                }
                else throw new ArgumentException("Invalid number or type of arguments supplied");
            }
            return cmd;

        }

        /// <summary>
        /// Executes a query that returns no results
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>The number of rows affected</returns>
        public int ExecNonQuery(string qry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, args))
            {
                int rowaffected = 0;
                try
                {
                    _conn.Open();
                    rowaffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteErrorLog(ex.ToString());
                }
                _conn.Close();
                return rowaffected;

            }
        }

        /// <summary>
        /// Executes a query that returns a single value
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Value of first column and first row of the results</returns>
        public object ExecScalar(string qry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, args))
            {
                object theObj = null;
                try
                {
                    _conn.Open();
                    theObj = cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    ErrorLog.WriteErrorLog(ex.ToString());
                }
                _conn.Close();
                return theObj;

            }
        }

        /// <summary>
        /// Executes a query and returns the results as a SqlDataReader
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a SqlDataReader</returns>
        public DataTable ExecDataReader(string qry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, args))
            {
                DataTable dt = new DataTable();
                try
                {
                    _conn.Open();
                    SqlDataReader rdr;
                    rdr = cmd.ExecuteReader();
                    dt.Load(rdr);
                }
                catch (Exception ex)
                {

                    ErrorLog.WriteErrorLog(ex.ToString());
                }
                _conn.Close();
                return dt;
            }
        }
        /// <summary>
        /// Executes a query and returns the results as a DataSet
        /// </summary>
        /// <param name="qry">Query text</param>
        /// <param name="args">Any number of parameter name/value pairs and/or SQLParameter arguments</param>
        /// <returns>Results as a DataTable</returns>
        public DataTable ExecDataSet(string qry, params object[] args)
        {
            using (SqlCommand cmd = CreateCommand(qry, CommandType.Text, args))
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                try
                {
                    SqlDataAdapter adaptor = new SqlDataAdapter(cmd);

                    adaptor.Fill(ds, "dbtable");
                    dt = ds.Tables["dbtable"];
                }
                catch (Exception ex)
                {

                    ErrorLog.WriteErrorLog(ex.ToString());
                }
                _conn.Close();
                return dt;
            }
        }
        public int ExecTrans(List<SqlCommand> tranCommands)
        {
            //  SqlCommand cmd = _conn.CreateCommand();
            SqlTransaction transaction;
            _conn.Open();
            // Start a local transaction.
            transaction = _conn.BeginTransaction();

            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            //cmd.Connection = _conn;
            //cmd.Transaction = transaction;
            string qryitem = string.Empty;
            try
            {
                foreach (SqlCommand sqlitem in tranCommands)
                {
                    sqlitem.Connection = _conn;
                    sqlitem.Transaction = transaction;
                    qryitem = sqlitem.CommandText;
                    sqlitem.ExecuteNonQuery();

                }
                transaction.Commit();
                _conn.Close();
                return tranCommands.Count;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                ErrorLog.WriteErrorLog(ex.ToString());
                _conn.Close();
                return 0;
            }

        }

       


    }
}