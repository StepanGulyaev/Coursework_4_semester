using Npgsql;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Coursework_client.DB
    {
    public sealed class User
        {
        private static readonly string connection_string = "Server=localhost;Port=5432;User ID={0};Database=airport_db;Password={1};";
        private static NpgsqlConnection _connection = default!;

        public User(string login, string password)
            {
            _connection = new NpgsqlConnection(string.Format(connection_string, login, password));
            _connection.OpenAsync();
            }

        ~User() => _connection.Dispose();

        public static async Task<bool> checkConnection(string login, string password)
            {
            var c_string = string.Format(connection_string, login, password);
            bool result = false;

            await using (var conn = new NpgsqlConnection(c_string))
                {
                try
                    {
                    await conn.OpenAsync();
                    result = conn.State == ConnectionState.Open ? true : false;
                    }
                catch (Exception e) { Console.Error.WriteLine(e); }
                finally { conn.Close(); }
                }

            return result;
            }

        public DataSet Query(string queryStr)
            {
            using var cmd = new NpgsqlCommand(queryStr, _connection);
            cmd.Connection = _connection;

            var adapter = new NpgsqlDataAdapter(cmd);

            var ds = new DataSet();
            adapter.Fill(ds);
            return ds;
            }
        }
    }
 
