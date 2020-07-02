﻿using CompData.Dao.Regulation;
using CompData.Models.Library;
using CompData.ViewModels.Library;
using CompData.ViewModels.Procedure.Library;
using CRMData.Configurations.Generics;
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
        public RegulationService(IRegulationDao regulationDao) {
            this.regulationDao = regulationDao;
        }

        public List<RegulationFilteredBySource> GetAllRegulationFilteredBySourceID(int sourceId, int? typeId = null)
        {
            return regulationDao.GetAllRegulationFilteredBySourceID(sourceId, typeId);
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
                viewModel.RegulationsList = groupBySourceProcedures.Where(x => x.TypeId.Equals(source.TypeId)).Select(x => new RegulationListItem { 
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

        public SelectedRegulationViewModel GetSelectedRegulation(int regulationId)
        {
            SelectedRegulationViewModel regulationViewModels = new SelectedRegulationViewModel();
            if (regulationId != 0)
            {
                List<SelectedRegulationProcedure> selectedRegulationDetails = regulationDao.GetSelectedRegulation(regulationId);
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
                List<SectionItem> sectionItems = new List<SectionItem>();

                var viewModel = selectedRegRequirementDetails.FirstOrDefault();
                requirementViewModels.CommentID = viewModel.CommentID;
                requirementViewModels.Requirement = viewModel.Requirement;
            }

            return requirementViewModels;
        }

        public List<RegulationSource> GetSelectedRegulationSourcesByUserId(Guid userId)
        {
            return this.regulationDao.GetSelectedRegulationSourcesByUserId(userId);
        }

        public List<int> GetSubscribedRegulationTypeByUserId(Guid userId, int sourceId)
        {
            return this.regulationDao.GetSubscribedRegulationTypeByUserId(userId,sourceId);
        }

        public List<Models.Library.Regulation> GetUpdatedRegulationsBySource(int sourceId)
        {
            var updatedRegulations = this.regulationDao.GetUpdatedRegulationsBySource(sourceId);
            return updatedRegulations;
        }

        public Result LinkUserByRegulationSource(Guid userID, List<int> SourceIds)
        {
            return this.regulationDao.LinkUserByRegulationSource(userID, SourceIds);
        }

        public async Task<Result> SaveRegulation(SaveRegulationViewModel viewModel)
        {
            return await this.regulationDao.SaveRegulation(viewModel);
        }

        public Result SubscribeRegulationTypeByUser(Guid userID, int typeId, int sourceId)
        {
            return this.regulationDao.SubscribeRegulationTypeByUser(userID, typeId, sourceId);
        }
    }
}
