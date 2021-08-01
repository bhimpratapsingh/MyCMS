using ControlPanel.ViewModels;
using System.Linq;

namespace ControlPanel.Models
{
    public class AuthenticateRepository
    {
        public int Authenticate(string username, string password)
        {
            int result = 0;

            var query = $@"Exec sp_AuthenticateUser @Username, @Password";
            var param = new
            {
                @Username = username,
                @Password = password
            };
            result = CommonRepository.Query<int>(query, param).FirstOrDefault();

            return result;
        }

        public UserViewModel GetUser(string username)
        {
            UserViewModel user = new UserViewModel();

            var query = $@"select id, name, username, email, designation, status from users where username = @Username";
            var param = new
            {
                @Username = username
            };
            user = CommonRepository.Query<UserViewModel>(query, param).FirstOrDefault();

            return user;
        }
    }
}