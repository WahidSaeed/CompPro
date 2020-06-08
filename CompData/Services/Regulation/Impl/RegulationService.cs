﻿using CompData.Dao.Regulation;
using CompData.ViewModels.Library;
using CompData.ViewModels.Procedure.Library;
using Microsoft.EntityFrameworkCore.Internal;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompData.Services.Regulation.Impl
{
    public class RegulationService : IRegulationService
    {
        private readonly IRegulationDao regulationDao;
        public RegulationService(IRegulationDao regulationDao) {
            this.regulationDao = regulationDao;
        }

        public List<RegulationFilteredBySource> GetAllRegulationFilteredBySourceID(int sourceId)
        {
            return regulationDao.GetAllRegulationFilteredBySourceID(sourceId);
        }

        public List<GetAllRegulationGroupBySourceViewModel> GetAllRegulationGroupBySource()
        {
            List<GetAllRegulationGroupBySourceViewModel> groupBySourceViewModels = new List<GetAllRegulationGroupBySourceViewModel>();
            List<RegulationGroupBySourceProcedure> groupBySourceProcedures = this.regulationDao.GetAllRegulationGroupBySource();
            var sources = groupBySourceProcedures.Select(e => new { e.SourceId, e.FullName, e.Regcount }).Distinct().ToList();

            foreach (var source in sources)
            {
                GetAllRegulationGroupBySourceViewModel viewModel = new GetAllRegulationGroupBySourceViewModel();
                viewModel.SourceId = source.SourceId;
                viewModel.SourceTitle = source.FullName;
                viewModel.TotalRegulation = source.Regcount;
                viewModel.RegulationsList = groupBySourceProcedures.Where(x => x.SourceId.Equals(source.SourceId)).Select(x => new RegulationListItem { 
                    RegulationId = x.RegId,
                    RegulationTitle = x.RegTitle
                }).ToList();

                groupBySourceViewModels.Add(viewModel);
            }

            return groupBySourceViewModels;
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
                regulationViewModels.SectionItems = sectionItems; 
            }
            
            return regulationViewModels;
        }
    }
}