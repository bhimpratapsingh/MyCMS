using ControlPanel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlPanel.Models
{
    public class ClientLogoRepository
    {
        public List<ClientLogoViewModel> GetAll()
        {
            var query = $@"select * from clientLogo";
            return CommonRepository.Query<ClientLogoViewModel>(query);
        }

        public bool Add(ClientLogoViewModel clientLogo)
        {
            var query = $@"insert into clientLogo values (@Name, @image, @sortOrder, @makePublic)";
            var param = new
            {
                @Name = clientLogo.Name,
                @image = clientLogo.Image,
                @sortOrder = clientLogo.SortOrder,
                @makePublic = clientLogo.MakePublic
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }

        public ClientLogoViewModel Get(long id)
        {
            ClientLogoViewModel clientLogo = new ClientLogoViewModel();
            var query = $@"select * from clientLogo where id = @id";
            clientLogo = CommonRepository.Query<ClientLogoViewModel>(query, new { @id = id }).FirstOrDefault();
            return clientLogo;
        }

        public bool Edit(ClientLogoViewModel clientLogo)
        {
            var query = $@"update clientLogo set  
                            name = @name,image=@image ,sortOrder= @sortOrder, makePublic= @makePublic
                            where id = @id";
            var param = new
            {
                @id = clientLogo.Id,
                @name = clientLogo.Name,
                @image = clientLogo.Image,
                @sortOrder = clientLogo.SortOrder,
                @makePublic = clientLogo.MakePublic
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }

        public bool Delete(long id)
        {
            var query = $@"delete from clientLogo
                            where id = @id";
            var param = new
            {
                @id = id
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }
    }
}