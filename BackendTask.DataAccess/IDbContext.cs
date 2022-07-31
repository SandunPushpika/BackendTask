using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.DataAccess {
    public interface IDbContext {
        public Task<TObject> AddObject<TObject>(string query, TObject obj);
        public Task<T> GetObjectByUsername<T>(string query, string username);
        public Task<IEnumerable<TObject>> GetAllObjects<TObject>(string query);
        public Task UpdateObject<TObject>(string query, TObject obj);
        public Task DeleteUser(string query, string username);

    }
}
