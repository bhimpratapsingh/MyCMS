using ControlPanel.Models;
using ServicesWebsite.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace ServicesWebsite.Models
{
    public class BaseRepository
    {
        public List<HeaderViewModel> GetHeaderByType(int type)
        {
            var query = $@"select * from header where type={type} and makepublic=1 order by sortorder,createdon desc";
            return CommonRepository.Query<HeaderViewModel>(query);
        }

        public List<MainContentViewModel> GetAllMainConentByHeaderId(long headerId)
        {
            var query = $@"select * from MainContent where headerId={headerId} and makepublic=1 order by sortorder";
            return CommonRepository.Query<MainContentViewModel>(query);
        }

        public MainContentViewModel GetMainContenyById(long id)
        {
            MainContentViewModel mainContent = new MainContentViewModel();
            var query = $@"select * from mainContent where id = @id";
            mainContent = CommonRepository.Query<MainContentViewModel>(query, new { @id = id }).FirstOrDefault();
            return mainContent;
        }

        public List<SubContentViewModel> GetAllSubContentByMainContentId(long mainContentId)
        {
            var query = $@"select * from SubContent where MainContentId={mainContentId} and makepublic=1 order by sortorder";
            return CommonRepository.Query<SubContentViewModel>(query);
        }

        public SubContentViewModel GetSubContentById(long id)
        {
            SubContentViewModel subContent = new SubContentViewModel();
            var query = $@"select * from SubContent where id = @id";
            subContent = CommonRepository.Query<SubContentViewModel>(query, new { @id = id }).FirstOrDefault();
            return subContent;
        }

        public List<SubContentListViewModel> GetAllSubContentListBySubContentId(long subContentId)
        {
            var query = $@"select * from SubContentList where SubContentId={subContentId} and makepublic=1 order by sortorder";
            return CommonRepository.Query<SubContentListViewModel>(query);
        }

        public List<HeaderViewModel> GetContentByType(int type)
        {
            List<HeaderViewModel> headerData = GetHeaderByType(type);

            foreach (var headerItem in headerData)
            {
                headerItem.MainContentData = GetAllMainConentByHeaderId(headerItem.Id);

                foreach (var mainContentItem in headerItem.MainContentData)
                {
                    mainContentItem.SubContentData = GetAllSubContentByMainContentId(mainContentItem.Id);

                    foreach (var subContentItem in mainContentItem.SubContentData)
                    {
                        subContentItem.SubContentListData = GetAllSubContentListBySubContentId(subContentItem.Id);
                    }
                }
            }

            return headerData;
        }

        public MainContentViewModel GetContentByMainContentId(long mainContentId)
        {
            MainContentViewModel mainContentData = GetMainContenyById(mainContentId);

            mainContentData.SubContentData = GetAllSubContentByMainContentId(mainContentId);

            foreach (var subContentItem in mainContentData.SubContentData)
            {
                subContentItem.SubContentListData = GetAllSubContentListBySubContentId(subContentItem.Id);
            }

            return mainContentData;
        }

        public SubContentViewModel GetContentBySubContentId(long  subContentId)
        {
            SubContentViewModel subContentData = GetSubContentById(subContentId);

            subContentData.SubContentListData = GetAllSubContentListBySubContentId(subContentId);

            return subContentData;
        }

        public List<ClientLogoViewModel> GetAllClientLogos()
        {
            var query = $@"select * from clientLogo where makepublic=1";
            return CommonRepository.Query<ClientLogoViewModel>(query);
        }
    }
}