using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.VectorData.Infrastructure.Data
{
    public class VDDbContext
    {
        private readonly string _connectionString;
        private Dictionary<string, (NpgsqlConnection conn, NpgsqlTransaction transaction)> transactionList;

        public VDDbContext(string connString)
        {
            _connectionString = connString;
            transactionList = new Dictionary<string, (NpgsqlConnection conn, NpgsqlTransaction transaction)>();
        }

        public NpgsqlDataReader Get(string query, NpgsqlParameter[] parameters = null)
        {
            NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            NpgsqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = query;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            return reader;
        }

        public int Set(string query, NpgsqlParameter[] parameters = null)
        {
            NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            NpgsqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = query;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            var res = cmd.ExecuteNonQuery();
            conn.Close();
            return res;
        }

        public object GetOne(string query, NpgsqlParameter[] parameters = null)
        {
            NpgsqlConnection conn = new NpgsqlConnection(_connectionString);
            conn.Open();
            NpgsqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = query;
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            var res = cmd.ExecuteScalar();
            conn.Close();
            return res;
        }

        public (NpgsqlCommand Command, string Id) CreateTransaction()
        {
            var transactionId = Guid.NewGuid().ToString("N");
            var npgsqlConn = new NpgsqlConnection(_connectionString);
            npgsqlConn.Open();
            NpgsqlTransaction transaction = npgsqlConn.BeginTransaction();
            NpgsqlCommand npgsqlCmd = npgsqlConn.CreateCommand();
            npgsqlCmd.Transaction = transaction;
            transactionList.Add(transactionId, (npgsqlConn, transaction));
            return (npgsqlCmd, transactionId);
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
