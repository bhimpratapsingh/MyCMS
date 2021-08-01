using ControlPanel.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ControlPanel.Models
{
    public class SubContentRepository
    {
        public bool Add(SubContentViewModel subContentData)
        {
            var query = $@"insert into SubContent values (@mainContentId, @content, @image, @sortOrder ,@makePublic, @addPage,@pageTitle,@fileName ,GETDATE(), null)";
            var param = new
            {
                @mainContentId=subContentData.MainContentId,
                @content = subContentData.Content,
                @image = subContentData.Image,
                @sortOrder = subContentData.SortOrder,
                @makePublic = subContentData.MakePublic,
                @addPage = subContentData.AddPage,
                @pageTitle = subContentData.PageTitle,
                @fileName = subContentData.FileName
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }

        public List<SubContentViewModel> GetAllSubContentByMainContentId(long mainContentId)
        {
            var query = $@"select * from SubContent where MainContentId={mainContentId} order by sortorder";
            return CommonRepository.Query<SubContentViewModel>(query);
        }

        public SubContentViewModel Get(long id)
        {
            SubContentViewModel subContent = new SubContentViewModel();
            var query = $@"select * from SubContent where id = @id";
            subContent = CommonRepository.Query<SubContentViewModel>(query, new { @id = id }).FirstOrDefault();
            return subContent;
        }

        public bool Edit(SubContentViewModel subContentData)
        {
            var query = $@"update SubContent set  
                            content = @content,image=@image ,sortOrder= @sortOrder, makePublic= @makePublic,
                            addPage=@addPage,pageTitle=@pageTitle,fileName=@fileName ,modifiedon = getdate()
                            where id = @id";
            var param = new
            {
                @id = subContentData.Id,
                @content = subContentData.Content,
                @image = subContentData.Image,
                @sortOrder = subContentData.SortOrder,
                @makePublic = subContentData.MakePublic,
                @addPage = subContentData.AddPage,
                @pageTitle = subContentData.PageTitle,
                @fileName = subContentData.FileName
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }

        public bool Delete(long id)
        {
            var query = $@"delete from subContent
                            where id = @id";
            var param = new
            {
                @id = id
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }

        internal void DeleteSubContentListData(long subContentId)
        {
            var query = $@"delete from subContentList where subContentId=@id";

            var param = new
            {
                @id = subContentId
            };
            var res = CommonRepository.Execute(query, param);
        }
    }
}