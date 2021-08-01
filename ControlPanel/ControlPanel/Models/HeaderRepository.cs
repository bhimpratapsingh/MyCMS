using ControlPanel.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ControlPanel.Models
{
    public class HeaderRepository
    {
        public string GetCategoryType(int type)
        {
            if (Category.Services == (Category)type)
            {
                return Constants.CATEGORY_SERVICE;
            }
            else if (Category.Products == (Category)type)
            {
                return Constants.CATEGORY_PRODUCT;
            }
            else if (Category.Careers == (Category)type)
            {
                return Constants.CATEGORY_CAREER;
            }
            return null;
        }

        public List<HeaderViewModel> GetAllHeaders()
        {
            var query = $@"select * from header";
            return CommonRepository.Query<HeaderViewModel>(query);
        }

        public List<CategoryViewModel> GetAllCategory()
        {
            var query = $@"select * from category";
            return CommonRepository.Query<CategoryViewModel>(query);
        }

        internal bool SaveBackColor(List<HeaderViewModel> headers)
        {
            foreach (var item in headers)
            {
                var query = $@"update header set backcolor=@backColor where id={item.Id}";
                var res = CommonRepository.Execute(query, new { @backColor = item.BackColor });

                if (res == 0)
                    return false;
            }
            return true;
        }

        public bool Add(HeaderViewModel headerData)
        {
            var query = $@"insert into header values (@header, @image, @sortOrder, @makePublic,  GETDATE(), null,@type,'#e10e0e')";
            var param = new
            {
                @header = headerData.Header,
                @image = headerData.Image,
                @sortOrder = headerData.SortOrder,
                @makePublic = headerData.MakePublic,
                @type = headerData.Type
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }

        public List<HeaderViewModel> GetByType(int type)
        {
            var query = $@"select * from header where type={type} order by sortorder,createdon desc,modifiedon desc";
            return CommonRepository.Query<HeaderViewModel>(query);
        }

        public HeaderViewModel Get(long id)
        {
            HeaderViewModel header = new HeaderViewModel();
            var query = $@"select * from header where id = @id";
            header = CommonRepository.Query<HeaderViewModel>(query, new { @id = id }).FirstOrDefault();
            return header;
        }

        public bool Edit(HeaderViewModel headerData)
        {
            var query = $@"update header set  
                            header = @header,image=@image ,sortOrder= @sortOrder, makePublic= @makePublic,
                            modifiedon = getdate()
                            where id = @id";
            var param = new
            {
                @id = headerData.Id,
                @header = headerData.Header,
                @image = headerData.Image,
                @sortOrder = headerData.SortOrder,
                @makePublic = headerData.MakePublic
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }

        public bool Delete(long id)
        {
            var query = $@"delete from header
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