using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SSE.VectorData.Infrastructure.Data
{
    public class VDDbContextSQL
    {
        private readonly string _connectionString;
        Dictionary<string, (SqlConnection conn, SqlTransaction transaction)> transactionList;
        public VDDbContextSQL(string connString)
        {
            _connectionString = connString;
            transactionList = new Dictionary<string, (SqlConnection conn, SqlTransaction transaction)>();
        }

        public SqlDataReader Get(string query, SqlParameter[] parameters = null)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = query;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return reader;
        }
        public int Set(string query, SqlParameter[] parameters = null)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = query;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            var res = cmd.ExecuteNonQuery();
            conn.Close();
            return res;
        }
        public object GetOne(string query, SqlParameter[] parameters = null)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = query;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            var res  = cmd.ExecuteScalar();
            conn.Close();
            return res;
        }


        public (SqlCommand Command, string Id) CreateTransaction()
        {
            var transactionId = Guid.NewGuid().ToString("N");
            var sqlConn = new SqlConnection(_connectionString);
            sqlConn.Open();
            SqlTransaction transaction = sqlConn.BeginTransaction();
            SqlCommand sqlCmd = sqlConn.CreateCommand();
            sqlCmd.Transaction = transaction;
            transactionList.Add(transactionId, (sqlConn, transaction));
            return (sqlCmd, transactionId);
        }
        public bool CommitTransaction(string transactionId)
        {
            if (transactionList.TryGetValue(transactionId, out var conn))
            {
                conn.transaction.Commit();
                conn.conn.Close();
                transactionList.Remove(transactionId);
                return true;
            }
            else
                return false;
        }
        public bool RollbackTransaction(string transactionId)
        {
            if (transactionList.TryGetValue(transactionId, out var conn))
            {
                conn.transaction.Rollback();
                conn.conn.Close();
                return true;
            }
            else
                return false;
        }
    }
}
