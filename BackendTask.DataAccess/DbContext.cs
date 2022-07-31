using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;

namespace BackendTask.DataAccess {
    public class DbContext : IDbContext{

        private NpgsqlConnection con;

        public DbContext(IConfiguration config) {
            
            con = new NpgsqlConnection(config.GetConnectionString("postgresConnection"));
            
        }

        public async Task<TObject> AddObject<TObject>(string query,TObject obj) {
            await con.ExecuteAsync(query,obj);
            return obj;
        }

        public async Task<T> GetObjectByUsername<T>(string query,string username) {
            var res = await con.QueryAsync<T>(query, new { username });
            
            if (res.ToList().Count == 0) {
                return default(T);
            }
            return res.ToList<T>().FirstOrDefault();
        }

        public async Task UpdateObject<TObject>(string query,TObject obj) {
            await con.ExecuteAsync(query,obj);
        }

        public async Task DeleteUser(string query, string username) {
            await con.ExecuteAsync(query,new { username });
        }

        public async Task<IEnumerable<TObject>> GetAllObjects<TObject>(string query) {
            return await con.QueryAsync<TObject> (query);
        }
    }
}
