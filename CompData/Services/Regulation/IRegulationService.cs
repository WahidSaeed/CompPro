using CompData.Models.Library;
using CompData.ViewModels.Library;
using CompData.ViewModels.Procedure.Library;
using CRMData.Configurations.Generics;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.Services.Regulation
{
    public interface IRegulationService
    {
        public List<GetAllRegulationGroupBySourceViewModel> GetAllRegulationGroupBySource(int sourceId);
        public SelectedRegulationViewModel GetSelectedRegulation(int regulationId);
        public List<RegulationFilteredBySource> GetAllRegulationFilteredBySourceID(int sourceId);
        public List<RegulationSource> GetRegulationSourcesByCountryCode(string countryCode);
        public List<RegulationSource> GetSelectedRegulationSourcesByUserId(Guid userId);
        public List<Models.Library.Regulation> GetUpdatedRegulationsBySource(int sourceId);
        public List<int> GetSubscribedRegulationTypeByUserId(Guid userId, int sourceId);
        public Result SubscribeRegulationTypeByUser(Guid userID, int typeId, int sourceId);
        public Result LinkUserByRegulationSource(Guid userID, List<int> SourceIds);
        
    }
}
