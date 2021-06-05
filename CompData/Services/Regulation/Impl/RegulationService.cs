using CompData.Configurations.Constants.Enums;
using CompData.Dao.Regulation;
using CompData.Models.Library;
using CompData.ViewModels;
using CompData.ViewModels.Library;
using CompData.ViewModels.Procedure.Library;
using CRMData.Configurations.Generics;
using CRMData.ViewModels.BaseViewModel;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Internal;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompData.Services.Regulation.Impl
{
    public class RegulationService : IRegulationService
    {
        private readonly IRegulationDao regulationDao;
        public RegulationService(IRegulationDao regulationDao)
        {
            this.regulationDao = regulationDao;
        }

        public JQueryDtaTableOutput<List<RegulationFilteredBySource>> GetAllRegulationFilteredBySourceID(SourceGrid sourceGrid, Guid userId)
        {
            return regulationDao.GetAllRegulationFilteredBySourceID(sourceGrid, userId);
        }

        public List<GetAllRegulationGroupBySourceViewModel> GetAllRegulationGroupBySource(int sourceId)
        {
            List<GetAllRegulationGroupBySourceViewModel> groupBySourceViewModels = new List<GetAllRegulationGroupBySourceViewModel>();
            List<RegulationGroupBySourceProcedure> groupBySourceProcedures = this.regulationDao.GetAllRegulationGroupBySource(sourceId);
            var sources = groupBySourceProcedures.Select(e => new { e.TypeId, e.TypeName, e.Regcount }).Distinct().ToList();

            foreach (var source in sources)
            {
                GetAllRegulationGroupBySourceViewModel viewModel = new GetAllRegulationGroupBySourceViewModel();
                viewModel.TypeId = source.TypeId;
                viewModel.TypeTitle = source.TypeName;
                viewModel.TotalRegulation = source.Regcount;
                viewModel.RegulationsList = groupBySourceProcedures.Where(x => x.TypeId.Equals(source.TypeId)).Select(x => new RegulationListItem
                {
                    RegulationId = x.RegId,
                    RegulationTitle = x.RegTitle,
                    Views = x.Views
                }).ToList();

                groupBySourceViewModels.Add(viewModel);
            }

            return groupBySourceViewModels;
        }
        public List<RegulationSource> GetRegulationSourcesByCountryCode(string countryCode)
        {
            return this.regulationDao.GetRegulationSourcesByCountryCode(countryCode);
        }

        public List<SelectedRegulationProcedure> GetSelectedRegulation(int regulationId, string searchTerm, List<string> detailTag, string version = null)
        {
            List<SelectedRegulationProcedure> selectedRegulationDetails = new List<SelectedRegulationProcedure>();
            if (regulationId != 0)
            {
                Int64 versionId = 0;
                Int64.TryParse(version, out versionId);
                selectedRegulationDetails = regulationDao.GetSelectedRegulation(regulationId, searchTerm, detailTag, versionId);
            }

            return selectedRegulationDetails;
        }

        public SelectedRegulationViewModel GetSelectedRegSummary(int regulationId)
        {
            SelectedRegulationViewModel regulationViewModels = new SelectedRegulationViewModel();
            if (regulationId != 0)
            {
                List<SelectedRegulationProcedure> selectedRegulationDetails = regulationDao.GetSelectedRegSummary(regulationId);
                List<SectionItem> sectionItems = new List<SectionItem>();
                foreach (var x in selectedRegulationDetails)
                {
                    SectionItem sectionItem = new SectionItem();
                    sectionItem.SectionId = x.SectionId;
                    sectionItem.SectionTitle = x.SectionTitle;
                    sectionItem.Description = x.RegDescription;
                    sectionItem.ParentId = x.ParentId;
                    sectionItem.Sequence = x.Sequence;

                    sectionItems.Add(sectionItem);
                }

                var viewModel = selectedRegulationDetails.FirstOrDefault();
                regulationViewModels.RegId = viewModel.RegId;
                regulationViewModels.RegTitle = viewModel.RegTitle;
                regulationViewModels.SourceId = viewModel.SourceId;
                regulationViewModels.SourceTitle = viewModel.FullName;
                regulationViewModels.RegTypeId = viewModel.RegTypeId;
                regulationViewModels.RegTypeName = viewModel.RegTypeName;
                regulationViewModels.Summary = viewModel.Summary;
                regulationViewModels.SectionItems = sectionItems;
            }

            return regulationViewModels;
        }

        public SelectedRegulationRequirementViewModel GetSelectedRegRequirement(int regulationId)
        {
            SelectedRegulationRequirementViewModel requirementViewModels = new SelectedRegulationRequirementViewModel();
            if (regulationId != 0)
            {
                List<SelectedRegulationRequirement> selectedRegRequirementDetails = regulationDao.GetSelectedRegRequirement(regulationId);
                List<SectionItemRequirement> sectionItems = new List<SectionItemRequirement>();

                foreach (var x in selectedRegRequirementDetails)
                {
                    SectionItemRequirement sectionItem = new SectionItemRequirement();
                    sectionItem.CommentID = x.CommentID;
                    sectionItem.Requirement = x.Requirement;

                    sectionItems.Add(sectionItem);
                }

                if (sectionItems.Count > 0)
                {
                    var viewModel = selectedRegRequirementDetails.FirstOrDefault();
                    requirementViewModels.RegID = viewModel.RegID;
                    requirementViewModels.SectionItemRequirement = sectionItems; 
                }
            }

            return requirementViewModels;
        }

        public List<RegulationSource> GetSelectedRegulationSourcesByUserId(Guid userId)
        {
            return this.regulationDao.GetSelectedRegulationSourcesByUserId(userId);
        }

        public List<int> GetSubscribedRegulationTypeByUserId(Guid userId, int sourceId)
        {
            return this.regulationDao.GetSubscribedRegulationTypeByUserId(userId, sourceId);
        }

        public List<int> GetSubscribedRegulationByUserId(Guid userId, int regId)
        {
            return this.regulationDao.GetSubscribedRegulationByUserId(userId, regId);
        }

        public List<Models.Library.Regulation> GetUpdatedRegulationsBySource(int sourceId)
        {
            var updatedRegulations = this.regulationDao.GetUpdatedRegulationsBySource(sourceId);
            return updatedRegulations;
        }

        public async Task<List<Models.Library.Regulation>> GetMostViewedRegulationsByUserSource(Guid userId)
        {
            var mostViewedRegulations = await this.regulationDao.GetMostViewedRegulationsByUserSource(userId);
            return mostViewedRegulations;
        }

        public async Task<Result> GetSuggestedRegulationsByUserSource(Guid userId, string searchTerm) 
        {
            var suggestedRegulations = await this.regulationDao.GetSuggestedRegulationsByUserSource(userId, searchTerm);
            return suggestedRegulations;
        }

        public Result LinkUserByRegulationSource(Guid userID, List<int> SourceIds)
        {
            return this.regulationDao.LinkUserByRegulationSource(userID, SourceIds);
        }

        public async Task<Result> SaveRegulation(SaveRegulationViewModel viewModel)
        {
            return await this.regulationDao.SaveRegulation(viewModel);
        }

        public async Task<Result> SaveRegulationDetail(SaveRegulationDetailViewModel viewModel)
        {
            return await this.regulationDao.SaveRegulationDetail(viewModel);
        }

        public Result GetAllTagsGroup(string tagGroupId)
        {
            return this.regulationDao.GetAllTagsGroup(tagGroupId);
        }

        public async Task<Result> SetTagsGroup(List<string> tags, string tagGroupId, int regId, int secId, int descId)
        {
            return await this.regulationDao.SetTagsGroup(tags, tagGroupId, regId, secId, descId);
        }

        public async Task<Result> GetRegulationIdByCustomURL(string customURL) 
        {
            try
            {
                int regId = 0;
                int.TryParse(customURL, out regId);
                if (regId == 0)
                {
                    var response = await regulationDao.GetRegulationIdByCustomURL(customURL);
                    if (response.Status == CRMData.Configurations.Constants.Enums.ResultStatus.Success)
                    {
                        return new Result
                        {
                            Status = CRMData.Configurations.Constants.Enums.ResultStatus.Success,
                            Data = response.Data
                        };
                    }
                }

                return new Result
                {
                    Status = CRMData.Configurations.Constants.Enums.ResultStatus.Success,
                    Data = regId
                };
            }
            catch (Exception ex)
            {
                return new Result { 
                    Message = ex.Message,
                    Status = CRMData.Configurations.Constants.Enums.ResultStatus.Error
                };
            }
        }

        public async Task<Result> GetRelatedRegulation(int regId = 0)
        {
            return await regulationDao.GetRelatedRegulation(regId);
        }

        public async Task<Result> GetRelatedRegulationForEdit(int regId = 0)
        {
            return await regulationDao.GetRelatedRegulationForEdit(regId);
        }

        public async Task<Result> SetLinkedRelatedRegulation(int regId, int relatedRegId)
        {
            return await this.regulationDao.SetLinkedRelatedRegulation(regId, relatedRegId);
        }

        public async Task<Result> GetAllTagFilters(int? sourceId, int? typeId, TagType tagType)
        {
            return await regulationDao.GetAllTagFilters(sourceId, typeId, tagType);
        }

        public async Task<Result> GetAllTagFiltersByRegId(int regId, TagType tagType) 
        {
            return await regulationDao.GetAllTagFiltersByRegId(regId, tagType);
        }

        public Result SubscribeRegulationTypeByUser(Guid userID, int typeId, int sourceId)
        {
            return this.regulationDao.SubscribeRegulationTypeByUser(userID, typeId, sourceId);
        }

        public Result SubscribeRegulationByUser(Guid userID, int regId)
        {
            return this.regulationDao.SubscribeRegulationByUser(userID, regId);
        }

        public async Task<Result> GetAllRegulations(AjaxDropDown ajaxDropDown)
        {
            return await this.regulationDao.GetAllRegulations(ajaxDropDown);
        }
        public async Task<Result> UpdateMetaDetails(UpdateMetaDataRegulationViewModel viewModel)
        {
            return await this.regulationDao.UpdateMetaDetails(viewModel);
        }

        public async Task<ResultSingleObject<RegulationSource>> GetRegulationSourceById(int regSourceId)
        {
            return await regulationDao.GetRegulationSourceById(regSourceId);
        }

        public async Task<ResultSingleObject<RegulationType>> GetRegulationTypeById(int regTypeId)
        {
            return await regulationDao.GetRegulationTypeById(regTypeId);
        }

        public async Task<Result<RegulationVersionViewModel>> GetRegulationVersions(int regId) 
        {
            return await regulationDao.GetRegulationVersions(regId);
        }
    }
}
