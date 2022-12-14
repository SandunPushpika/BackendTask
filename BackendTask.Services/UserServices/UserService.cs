using BackendTask.DataAccess;
using BackendTask.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Services.UserServices {
    public class UserService : IUserService {

        private readonly IDbContext _context;

        public UserService(IDbContext context) {
            _context = context;
        }
        public async Task<UserModel> AddNewUser(UserModel user) {

            var check = await GetUserByUsername(user.Username);

            if (check == null) {

                user.Password = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password));

                return await _context.AddObject<UserModel>(
                    "insert into users (Username,Name,Age,Role,Password) values (@Username,@Name,@Age,@Role,@Password)"
                    , user);
            }

            return null;
            
        }

        public async Task DeleteUser(string username) {

            await _context.DeleteUser("delete from users where Username = @username", username);
        }

        public Task<IEnumerable<UserModel>> GetAllUsers() {
            return _context.GetAllObjects<UserModel> ("select * from users");
        }

        public async Task<UserModel> GetUserByUsername(string username) {

            var user = await _context.GetObjectByUsername<UserModel>(
                "select * from users where username = @username", username);
            
            return user;
        }

        public async Task UpdateUser(UserModel model) {

            model.Password = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(model.Password));

            string query = "update users set Name = @Name, Age = @Age, Role = @Role, Password = @Password where Username = @Username";
            await _context.UpdateObject<UserModel>(query, model);

        }
    }
}
