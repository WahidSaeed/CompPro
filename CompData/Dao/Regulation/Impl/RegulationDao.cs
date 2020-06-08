using CompData.ViewModels.Library;
using CompData.ViewModels.Procedure.Library;
using CRMData.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompData.Dao.Regulation.Impl
{
    public class RegulationDao : IRegulationDao
    {
        private readonly ApplicationDbContext dbContext;
        public RegulationDao(ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
        }

        public List<RegulationFilteredBySource> GetAllRegulationFilteredBySourceID(int sourceId)
        {
            List<RegulationFilteredBySource> sourceProcedure = this.dbContext.Set<RegulationFilteredBySource>().FromSqlRaw($"EXEC Library.GetAllRegulationFilteredBySourceID {sourceId}").ToList();
            return sourceProcedure;
        }

        public List<RegulationGroupBySourceProcedure> GetAllRegulationGroupBySource()
        {
            List<RegulationGroupBySourceProcedure> sourceProcedure = this.dbContext.Set<RegulationGroupBySourceProcedure>().FromSqlRaw($"EXEC Library.GetAllRegulationGroupBySource").ToList();
            return sourceProcedure;
        }

        public List<SelectedRegulationProcedure> GetSelectedRegulation(int regulationId)
        {
            List<SelectedRegulationProcedure> regulationProcedures = this.dbContext.Set<SelectedRegulationProcedure>().FromSqlRaw($"EXEC Library.GetSelectedRegulation {regulationId} ").ToList();
            return regulationProcedures;
        }
    }
}
