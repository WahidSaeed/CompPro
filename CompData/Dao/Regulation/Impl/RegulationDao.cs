using CompData.Models.Library;
using CompData.ViewModels.Library;
using CompData.ViewModels.Procedure.Library;
using CRMData.Configurations.Constants.Enums;
using CRMData.Configurations.Generics;
using CRMData.Data;
using CRMData.Models.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CompData.Dao.Regulation.Impl
{
    public class RegulationDao : IRegulationDao
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IHttpContextAccessor httpContext;

        public RegulationDao(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContext) 
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.httpContext = httpContext;
        }

        public List<RegulationFilteredBySource> GetAllRegulationFilteredBySourceID(int sourceId)
        {
            List<RegulationFilteredBySource> sourceProcedure = this.dbContext.Set<RegulationFilteredBySource>().FromSqlRaw($"EXEC Library.GetAllRegulationFilteredBySourceID {sourceId}").ToList();
            return sourceProcedure;
        }

        public List<RegulationGroupBySourceProcedure> GetAllRegulationGroupBySource(int sourceId)
        {
            List<RegulationGroupBySourceProcedure> sourceProcedure = this.dbContext.Set<RegulationGroupBySourceProcedure>().FromSqlRaw($"EXEC Library.GetAllRegulationGroupBySource {sourceId}").ToList();
            return sourceProcedure;
        }

        public List<RegulationSource> GetRegulationSourcesByCountryCode(string countryCode)
        {
            List<RegulationSource> regulationSources = this.dbContext.RegulationSources.Where(x => x.CountryId.Equals(countryCode)).ToList();
            return regulationSources;
        }

        public List<SelectedRegulationProcedure> GetSelectedRegulation(int regulationId)
        {
            List<SelectedRegulationProcedure> regulationProcedures = this.dbContext.Set<SelectedRegulationProcedure>().FromSqlRaw($"EXEC Library.GetSelectedRegulation {regulationId} ").ToList();
            
            var regulation = this.dbContext.Regulations.Where(x => x.RegId.Equals(regulationId)).FirstOrDefault();
            regulation.Views += 1;
            this.dbContext.Entry<CompData.Models.Library.Regulation>(regulation).State = EntityState.Modified;
            this.dbContext.SaveChanges();

            return regulationProcedures;
        }

        public List<RegulationSource> GetSelectedRegulationSourcesByUserId(Guid userId)
        {
            List<RegulationSource> regulationSources = this.dbContext.LinkedUserRegulationSources.Where(x => x.UserId.Equals(userId)).Select(x => x.RegulationSource).ToList();
            return regulationSources;
        }

        public List<int> GetSubscribedRegulationTypeByUserId(Guid userId, int sourceId)
        {
            List<int> regulationTypes = this.dbContext.LinkUserRegTypeSubscriptions.Where(x => x.UserId.Equals(userId) && x.RegSourceId.Equals(sourceId)).Select(x => x.RegTypeId).ToList();
            return regulationTypes;
        }

        public Result SubscribeRegulationTypeByUser(Guid userID, int typeId, int sourceId)
        {
            try
            {
                LinkUserRegTypeSubscription linkUser = this.dbContext.LinkUserRegTypeSubscriptions.Where(x => x.UserId.Equals(userID) && x.RegTypeId.Equals(typeId) && x.RegSourceId.Equals(sourceId)).FirstOrDefault();
                string message = string.Empty;
                if (linkUser == null)
                {
                    LinkUserRegTypeSubscription subscription = new LinkUserRegTypeSubscription
                    {
                        UserId = userID,
                        RegTypeId = typeId,
                        RegSourceId = sourceId
                    };

                    this.dbContext.Entry<LinkUserRegTypeSubscription>(subscription).State = EntityState.Added;
                    int result = this.dbContext.SaveChanges();
                    if (result == 0) throw new Exception("Something went wrong please try again");
                    message = "Regulation type has been subscribed.";
                }
                else
                {
                    this.dbContext.Entry<LinkUserRegTypeSubscription>(linkUser).State = EntityState.Deleted;
                    int result = this.dbContext.SaveChanges();
                    if (result == 0) throw new Exception("Something went wrong please try again");
                    message = "Regulation type has been unsubscribed.";
                }

                return new Result
                {
                    Status = ResultStatus.Success,
                    Message = message
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    Message = ex.Message,
                    Status = ResultStatus.Error
                };
            }
        }

        public List<Models.Library.Regulation> GetUpdatedRegulationsBySource(int sourceId)
        {
            var updatedRegulations = this.dbContext.Regulations.Where(x => x.SourceID.Equals(sourceId)).Take(10).OrderByDescending(x => x.UpdatedBy).ToList();
            return updatedRegulations;
        }

        public Result LinkUserByRegulationSource(Guid userID, List<int> SourceIds)
        {
            try
            {
                this.dbContext.LinkedUserRegulationSources.AddRange(SourceIds.Select(sourceId => new LinkedUserRegulationSource
                {
                    UserId = userID,
                    SourceId = sourceId
                }));
                int result = this.dbContext.SaveChanges();
                if (result == 0) throw new Exception("Something went wrong please try again");

                return new Result
                {
                    Status = ResultStatus.Success,
                };
            }
            catch (Exception ex)
            {

                return new Result {
                    Message = ex.Message,
                    Status = ResultStatus.Error
                };
            }
        }

        public async Task<Result> SaveRegulation(SaveRegulationViewModel viewModel) 
        {
            try
            {
                string message = string.Empty;
                var httpUser = httpContext.HttpContext.User;
                var user = await userManager.GetUserAsync(httpUser);
                Models.Library.Regulation regulation = new Models.Library.Regulation
                {
                    RegulationTitle = viewModel.Title,
                    ReferenceNumber = viewModel.ReferenceNumber,
                    IssueDate = viewModel.IssueDate,
                    EffectiveDate = viewModel.EffectiveDate,
                    SourceID = viewModel.SourceID,
                    RegTypeID = viewModel.TypeID,
                    CreatedAt = DateTime.Now,
                    CreatedBy = user.Id
                };
                this.dbContext.Entry<Models.Library.Regulation>(regulation).State = EntityState.Added;
                int result = this.dbContext.SaveChanges();
                if (result == 0) throw new Exception("Something went wrong please try again");
                message = "New Regulation has been saved.";

                return new Result
                {
                    Status = ResultStatus.Success,
                    Message = message
                };
            }
            catch (Exception ex)
            {

                return new Result
                {
                    Message = ex.Message,
                    Status = ResultStatus.Error
                };
            }
        }



    }
}
