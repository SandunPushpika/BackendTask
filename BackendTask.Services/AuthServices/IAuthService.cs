using BackendTask.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendTask.Services.AuthServices {
    public interface IAuthService {
        public Task<UserModel> Authenticate(UserLogin login);
        public string GenerateAccessToken(UserModel model);
        public string GenerateRefreshToken(UserModel model);
        public Task<string> RegenerateAccessToken(string refresh_token);
    }
}
