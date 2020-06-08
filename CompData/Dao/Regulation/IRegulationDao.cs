using CompData.ViewModels.Library;
using CompData.ViewModels.Procedure.Library;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.Dao.Regulation
{
    public interface IRegulationDao
    {
        public List<RegulationGroupBySourceProcedure> GetAllRegulationGroupBySource();
        public List<SelectedRegulationProcedure> GetSelectedRegulation(int regulationId);
        public List<RegulationFilteredBySource> GetAllRegulationFilteredBySourceID(int sourceId);
    }
}
