using CompData.Configurations.Constants.Enums;
using CompData.Models.Library;
using CompData.ViewModels;
using CompData.ViewModels.Library;
using CompData.ViewModels.Procedure.Library;
using CRMData.Configurations.Generics;
using CRMData.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompData.Dao.Regulation
{
    public interface IRegulationDao
    {
        public List<RegulationGroupBySourceProcedure> GetAllRegulationGroupBySource(int sourceId);
        public List<SelectedRegulationProcedure> GetSelectedRegulation(int regulationId, string searchTerm = null, List<string> detailTag = null);
        public JQueryDtaTableOutput<List<RegulationFilteredBySource>> GetAllRegulationFilteredBySourceID(SourceGrid sourceGrid, Guid userId);
        public List<RegulationSource> GetRegulationSourcesByCountryCode(string countryCode);
        public List<RegulationSource> GetSelectedRegulationSourcesByUserId(Guid userId);
        public List<CompData.Models.Library.Regulation> GetUpdatedRegulationsBySource(int sourceId);
        public Task<List<Models.Library.Regulation>> GetMostViewedRegulationsByUserSource(Guid userId);
        public Task<Result> GetSuggestedRegulationsByUserSource(Guid userId, string searchTerm);
        public List<int> GetSubscribedRegulationTypeByUserId(Guid userId, int sourceId);
        public Result SubscribeRegulationTypeByUser(Guid userID, int typeId, int sourceId);
        public Result SubscribeRegulationByUser(Guid userID, int regId);
        public Result LinkUserByRegulationSource(Guid userID, List<int> SourceIds);
        public Task<Result> SaveRegulation(SaveRegulationViewModel viewModel);
        public Task<Result> SaveRegulationDetail(SaveRegulationDetailViewModel viewModel);
        public Task<Result> GetTagsGroup(string tagGroupId);
        public Task<Result> SetTagsGroup(List<string> tag, string tagGroupId, int regId, int secId, int descId);
        public Task<Result> GetAllTagFilters(int? sourceId, int? typeId, TagType tagType);
        public Task<Result> GetAllTagFiltersByRegId(int regId, TagType tagType);
        public Task<Result> GetAllRegulations(AjaxDropDown ajaxDropDown);
        public List<SelectedRegulationRequirement> GetSelectedRegRequirement(int regulationId);
        public List<SelectedRegulationProcedure> GetSelectedRegSummary(int regulationId);
    }
}
