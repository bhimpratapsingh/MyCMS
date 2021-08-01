using ControlPanel.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ControlPanel.Models
{
    public class MainContentRepository
    {
        public bool Add(MainContentViewModel mainContentData)
        {
            var query = $@"insert into MainContent values (@headerId, @content, @image, @sortOrder ,@makePublic, @addPage,@pageTitle,@fileName ,GETDATE(), null)";
            var param = new
            {
                @headerId = mainContentData.HeaderId,
                @content = mainContentData.Content,
                @image = mainContentData.Image,
                @sortOrder = mainContentData.SortOrder,
                @makePublic = mainContentData.MakePublic,
                @addPage = mainContentData.AddPage,
                @pageTitle = mainContentData.PageTitle,
                @fileName = mainContentData.FileName
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }

        public List<MainContentViewModel> GetAllMainConentByHeaderId(long headerId)
        {
            var query = $@"select * from MainContent where headerId={headerId} order by sortorder";
            return CommonRepository.Query<MainContentViewModel>(query);
        }

        public MainContentViewModel Get(long id)
        {
            MainContentViewModel mainContent = new MainContentViewModel();
            var query = $@"select * from mainContent where id = @id";
            mainContent = CommonRepository.Query<MainContentViewModel>(query, new { @id = id }).FirstOrDefault();
            return mainContent;
        }

        public bool Edit(MainContentViewModel mainContentData)
        {
            var query = $@"update mainContent set  
                            content = @content,image=@image ,sortOrder= @sortOrder, makePublic= @makePublic,
                            addPage=@addPage,pageTitle=@pageTitle,fileName=@fileName ,modifiedon = getdate()
                            where id = @id";
            var param = new
            {
                @id = mainContentData.Id,
                @content = mainContentData.Content,
                @image = mainContentData.Image,
                @sortOrder = mainContentData.SortOrder,
                @makePublic = mainContentData.MakePublic,
                @addPage = mainContentData.AddPage,
                @pageTitle = mainContentData.PageTitle,
                @fileName = mainContentData.FileName
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }

        public bool Delete(long id)
        {
            var query = $@"delete from mainContent
                            where id = @id";
            var param = new
            {
                @id = id
            };
            var res = CommonRepository.Execute(query, param);
            return res != 0;
        }

        internal void DeleteSubContentAndSubContentListData(long mainContentDataId)
        {
            SubContentRepository subContentRepository = new SubContentRepository();
            List<SubContentViewModel> subContentData = subContentRepository.GetAllSubContentByMainContentId(mainContentDataId);

            List<string> subContentIds = subContentData.Select(x => x.Id.ToString()).ToList();

            string.Join(",", subContentIds.ToArray());

            var query = $@"delete from subContentList where subContentId in (@id)";

            var param = new
            {
                @id = subContentIds
            };
            var res = CommonRepository.Execute(query, param);


            query = $@"delete from subContent where mainContentId=@id";

            var param2 = new
            {
                @id = mainContentDataId
            };
            res = CommonRepository.Execute(query, param2);
        }
    }
}