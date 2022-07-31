using BackendTask.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Services.UserServices {
    public interface IUserService {
        public Task<UserModel> AddNewUser(UserModel model);
        public Task<IEnumerable<UserModel>> GetAllUsers();
        public Task<UserModel> GetUserByUsername(string username);
        public Task UpdateUser(UserModel model);
        public Task DeleteUser(string username);
    }
}
