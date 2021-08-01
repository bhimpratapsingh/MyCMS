using ControlPanel.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ControlPanel.Models
{
    public class SubContentListRepository
    {
        public bool Add(SubContentListViewModel subContentListData)
        {
            var query = $@"insert into SubContentList values ( @subContentId,@content, @sortOrder ,@makePublic,@fileName ,GETDATE(), null)";
            var param = new
            {
                @subContentId=subContentListData.SubContentId,
                @content = subContentListData.Content,
                @sortOrder = subContentListData.SortOrder,
                @makePublic = subContentListData.MakePublic,
                @fileName = subContentListData.FileName
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }

        public List<SubContentListViewModel> GetAllSubContentListBySubContentId(long subContentId)
        {
            var query = $@"select * from SubContentList where SubContentId={subContentId} order by sortorder";
            return CommonRepository.Query<SubContentListViewModel>(query);
        }

        public SubContentListViewModel Get(long id)
        {
            SubContentListViewModel subContent = new SubContentListViewModel();
            var query = $@"select * from SubContentList where id = @id";
            subContent = CommonRepository.Query<SubContentListViewModel>(query, new { @id = id }).FirstOrDefault();
            return subContent;
        }

        public bool Edit(SubContentListViewModel subContentListData)
        {
            var query = $@"update SubContentList set  
                            content = @content,sortOrder= @sortOrder, makePublic= @makePublic,
                            fileName=@fileName ,modifiedon = getdate()
                            where id = @id";
            var param = new
            {
                @id = subContentListData.Id,
                @content = subContentListData.Content,
                @sortOrder = subContentListData.SortOrder,
                @makePublic = subContentListData.MakePublic,
                @fileName = subContentListData.FileName
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }

        public bool Delete(long id)
        {
            var query = $@"delete from subContentList
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