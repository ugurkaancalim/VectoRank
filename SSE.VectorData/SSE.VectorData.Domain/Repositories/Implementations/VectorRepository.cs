using SSE.VectorData.Domain.Repositories.Interfaces;
using SSE.VectorData.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSE.VectorData.Domain.Extensions;
using System.Xml.Serialization;
using System.Diagnostics;
using Npgsql;

namespace SSE.VectorData.Domain.Repositories.Implementations
{
    public class VectorRepository : IVectorRepository
    {
        private readonly VDDbContext _dbContext;
        public VectorRepository(VDDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public double[]? GetVector(string word)
        {
            if (word == null || word.Length == 0)
                return null;
            var reader = _dbContext.Get("select \"Vector\" from " + CreateTableName(word) + " where \"Word\"=@Word", new NpgsqlParameter[] {
                new NpgsqlParameter("@Word",word)
            });
            double[]? result = null;
            if (reader.Read())
                result = reader.GetString(0).ToVector();
            reader.Close();
            return result;
        }
        public IEnumerable<double[]> GetVectors(List<string> words)
        {
            words = words.OrderBy(x => x).ToList();
            var query = "";
            char start = ' ';
            var sqlParameters = new NpgsqlParameter[words.Count];
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i] == null || words[i].Length == 0)
                    continue;
                if (start != words[i][0])
                {
                    start = words[i][0];
                    if (query != "") query += ")) union ";
                    query += "(select \"Vector\" from " + CreateTableName(words[i]) + " where \"Word\" In (@Word_" + i + "";
                    sqlParameters[i] = (new NpgsqlParameter("@Word_" + i, words[i]));
                }
                else
                {
                    query += ",@Word_" + i;
                    sqlParameters[i] = (new NpgsqlParameter("@Word_" + i, words[i]));
                }
            }
            query += "))";
            var reader = _dbContext.Get(query, sqlParameters.ToArray());
            var result = new List<double[]>();
            while (reader.Read())
            {
                result.Add(reader.GetString(0).ToVector());
            }
            reader.Close();
            return result;
        }
        private string CreateTableName(string word)
        {

            return "VectorsFor" + ((int)word[0]).ToString();
        }


        private List<string> GetTableNames()
        {
            var reader = _dbContext.Get(@"SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE = 'BASE TABLE'");
            var result = new List<string>();
            while (reader.Read())
                result.Add(reader.GetString(0));
            reader.Close();
            return result;
        }
        private long getTotal(string filePath)
        {
            long counter = -1;
            using (var fileStream = File.OpenRead(filePath))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string w;
                    while ((w = streamReader.ReadLine()) != null) { counter++; }
                }
            }
            return counter;
        }
        public void InsertBaseVectors(string filePath)
        {
            var createTable = @"CREATE TABLE {tableName} (
    ""Word"" VARCHAR(400) NULL,
    ""Vector"" VARCHAR(4000) NULL
);

CREATE INDEX {tableName}_Index ON {tableName}(""Word"");
";

            var tableNames = GetTableNames();
            var addedTables = new List<char>();
            var transaction = _dbContext.CreateTransaction();
            bool firstLine = true;
            long numOfRequests = 0;
            var total = getTotal(filePath);
            using (var fileStream = File.OpenRead(filePath))
            {
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string w;
                    while ((w = streamReader.ReadLine()) != null)
                    {
                        numOfRequests++;
                        if (firstLine || w.Trim().Length == 0) { firstLine = false; continue; }
                        var word = w.Substring(0, w.IndexOf(' '));
                        var vector = w.Substring(word.Length + 1, w.Length - (word.Length + 1));
                        if (addedTables.FirstOrDefault(x => x == word[0]) == '\0')
                        {
                            var tableName = CreateTableName(word);
                            _dbContext.Set(createTable.Replace("{tableName}", tableName));
                            addedTables.Add(word[0]);
                        }
                        var tblName = CreateTableName(word);
                        transaction.Command.CommandText = "insert into " + tblName + "(\"Word\",\"Vector\") values(@Word,@Vector)";
                        transaction.Command.Parameters.AddRange(new NpgsqlParameter[] {
                            new NpgsqlParameter("@Word",word),
                            new NpgsqlParameter("@Vector",vector)
                        });
                        if (word.Length > 400)
                        {
                            transaction.Command.Parameters.Clear();
                            continue;
                        }
                        transaction.Command.ExecuteNonQuery();
                        transaction.Command.Parameters.Clear();
                        if (numOfRequests % 3000 == 0)
                        {
                            Debug.WriteLine(numOfRequests + " / " + total);
                            _dbContext.CommitTransaction(transaction.Id);
                            transaction = _dbContext.CreateTransaction();
                        }
                    }
                }
            }
            _dbContext.CommitTransaction(transaction.Id);
        }
    }
}
