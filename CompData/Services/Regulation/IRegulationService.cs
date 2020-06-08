using CompData.ViewModels.Library;
using CompData.ViewModels.Procedure.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.Services.Regulation
{
    public interface IRegulationService
    {
        public List<GetAllRegulationGroupBySourceViewModel> GetAllRegulationGroupBySource();
        public SelectedRegulationViewModel GetSelectedRegulation(int regulationId);
        public List<RegulationFilteredBySource> GetAllRegulationFilteredBySourceID(int sourceId);
    }
}
