using ControlPanel.ViewModels;
using System.Linq;

namespace ControlPanel.Models
{
    public class UsersRepository
    {
        public bool VerifyOldPassword(long id, string oldPassword)
        {
            var query = $@"select id from users where id=@id and password=HASHBYTES('SHA2_256', cast(@password as varchar(max)))";
            UserViewModel user = CommonRepository.Query<UserViewModel>(query,new { @id=id,@password=oldPassword}).FirstOrDefault();

            if (user == null)
            {
                return false;
            }
            else
                return true;
        }

        public bool ChangePassword(long id, string password)
        {
            var query = $@"update users set password=HASHBYTES('SHA2_256', cast(@password as varchar(max))) where id=@id";
            var res = CommonRepository.Execute(query, new { @id = id, @password = password });
            return res != 0;
        }

        public UserViewModel Get(long id)
        {
            UserViewModel user = new UserViewModel();
            var query = $@"select id, name, username, email, designation, status from users where id = @id";
            user = CommonRepository.Query<UserViewModel>(query, new { @id = id }).FirstOrDefault();
            return user;
        }
    }
}