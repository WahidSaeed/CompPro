using CompData.Configurations.Constants.Enums;
using CompData.Models.Library;
using CompData.ViewModels;
using CompData.ViewModels.Library;
using CompData.ViewModels.Procedure.Library;
using CRMData.Configurations.Constants.Enums;
using CRMData.Configurations.Generics;
using CRMData.Data;
using CRMData.Models.Identity;
using CRMData.ViewModels.BaseViewModel;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.En;
using Lucene.Net.Analysis.Snowball;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Math.EC.Rfc7748;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Metadata;
using System.Security.Cryptography;
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

        public JQueryDtaTableOutput<List<RegulationFilteredBySource>> GetAllRegulationFilteredBySourceID(SourceGrid sourceGrid, Guid userId)
        {
            try
            {

                List<int> luceneRegIds = new List<int>();
                if (!string.IsNullOrEmpty(sourceGrid.SearchTerm))
                {
                    var directory = FSDirectory.Open("c:\\temp\\directory");
                    using (Analyzer analyzer = new EnglishAnalyzer(LuceneVersion.LUCENE_48))
                    {
                        using (var reader = DirectoryReader.Open(directory))
                        {
                            var searcher = new IndexSearcher(reader);
                            var queryParser = new QueryParser(LuceneVersion.LUCENE_48, "title", analyzer)
                            {
                                AllowLeadingWildcard = true,
                                AutoGeneratePhraseQueries = true,
                                FuzzyMinSim = 3f
                            };
                            var query = queryParser.Parse(sourceGrid.SearchTerm);
                            var collector = TopScoreDocCollector.Create(10000, true);
                            searcher.Search(query, collector);

                            var matches = collector.GetTopDocs().ScoreDocs;
                            foreach (var item in matches)
                            {
                                var id = item.Doc;
                                var doc = searcher.Doc(id);

                                string regId = doc.GetField("regId").GetStringValue();
                                luceneRegIds.Add(int.Parse(regId));
                            }
                        }
                    }
                }


                List<RegulationFilteredBySource> sourceProcedure = new List<RegulationFilteredBySource>();
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@SourceId", sourceGrid.SourceId.ToString()));
                parameters.Add(new SqlParameter("@TypeId", sourceGrid.TypeId.ToString()));
                parameters.Add(new SqlParameter("@UserId", userId));

                System.Data.DataTable tbDetailTags = sourceGrid.DetailTags.Where(x => !string.IsNullOrEmpty(x))
                    .ToList<string>()
                    .ToDataTable();
                var parameterSearchFilters = new SqlParameter("@DetailTags", SqlDbType.Structured);
                parameterSearchFilters.Value = tbDetailTags;
                parameterSearchFilters.TypeName = "[dbo].[StringVector]";
                parameters.Add(parameterSearchFilters);

                System.Data.DataTable tbBussinessLineTags = sourceGrid.BussinessLineTags.Where(x => !string.IsNullOrEmpty(x))
                    .ToList<string>()
                    .ToDataTable();
                var parameterSearchFilters_1 = new SqlParameter("@BussinessLineTags", SqlDbType.Structured);
                parameterSearchFilters_1.Value = tbBussinessLineTags;
                parameterSearchFilters_1.TypeName = "[dbo].[StringVector]";
                parameters.Add(parameterSearchFilters_1);

                System.Data.DataTable tbYears = sourceGrid.Years
                    .ToDataTable();
                var parameterSearchFilters_2 = new SqlParameter("@Years", SqlDbType.Structured);
                parameterSearchFilters_2.Value = tbYears;
                parameterSearchFilters_2.TypeName = "[dbo].[IntVector]";
                parameters.Add(parameterSearchFilters_2);

                System.Data.DataTable tbluceneRegIds = luceneRegIds
                    .ToDataTable();
                var parameterSearchFilters_3 = new SqlParameter("@RegIds", SqlDbType.Structured);
                parameterSearchFilters_3.Value = tbluceneRegIds;
                parameterSearchFilters_3.TypeName = "[dbo].[IntVector]";
                parameters.Add(parameterSearchFilters_3);

                parameters.Add(new SqlParameter("@PageSize", sourceGrid.length));

                parameters.Add(new SqlParameter("@SortColumn", "[A].[IssueDate]"));
                parameters.Add(new SqlParameter("@SortDirection", "DESC"));

                var PageNo = (sourceGrid.start / sourceGrid.length);
                parameters.Add(new SqlParameter("@PageNo", PageNo));
                var _TotalRecords = new SqlParameter("@TotalRecords", SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };
                parameters.Add(_TotalRecords);

                sourceProcedure = this.dbContext.Set<RegulationFilteredBySource>().FromSqlRaw($"EXEC [Library].[GetAllRegulationFilteredBySourceID] @SourceId, @TypeId, @UserId, @DetailTags, @BussinessLineTags, @Years, @RegIds, @PageSize, @PageNo, @SortColumn, @SortDirection, @TotalRecords OUT", parameters.ToArray()).ToList();

                int records = 0;
                int.TryParse(_TotalRecords.Value.ToString(), out records);

                return new JQueryDtaTableOutput<List<RegulationFilteredBySource>>()
                {
                    data = sourceProcedure,
                    draw = sourceGrid.draw,
                    recordsFiltered = records,
                    recordsTotal = records
                };
            }
            catch (Exception ex)
            {
                return new JQueryDtaTableOutput<List<RegulationFilteredBySource>>()
                {
                    error = ex.Message
                };
            }

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

        public List<SelectedRegulationProcedure> GetSelectedRegulation(int regulationId, string searchTerm = null, List<string> detailTags = null)
        {
            List<SelectedRegulationProcedure> regulationProcedures = new List<SelectedRegulationProcedure>();
            try
            {

                if (detailTags == null)
                {
                    detailTags = new List<string>();
                }
                List<int> luceneRegDetailIds = new List<int>();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var directory = FSDirectory.Open("c:\\temp\\directory");
                    using (Analyzer analyzer = new EnglishAnalyzer(LuceneVersion.LUCENE_48))
                    {
                        using (var reader = DirectoryReader.Open(directory))
                        {
                            var searcher = new IndexSearcher(reader);
                            var queryParser = new QueryParser(LuceneVersion.LUCENE_48, "desc", analyzer)
                            {
                                AllowLeadingWildcard = true,
                                AutoGeneratePhraseQueries = true,
                                FuzzyMinSim = 3f
                            };
                            var query = queryParser.Parse(searchTerm);
                            var collector = TopScoreDocCollector.Create(10000, true);
                            searcher.Search(query, collector);

                            var matches = collector.GetTopDocs().ScoreDocs;
                            foreach (var item in matches)
                            {
                                var id = item.Doc;
                                var doc = searcher.Doc(id);

                                string regId = doc.GetField("regDetailId").GetStringValue();
                                luceneRegDetailIds.Add(int.Parse(regId));
                            }
                        }
                    }
                }

                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@RegID", regulationId.ToString()));
                System.Data.DataTable tbDetailTags = detailTags.Where(x => !string.IsNullOrEmpty(x))
                    .ToList<string>()
                    .ToDataTable();
                var parameterSearchFilters = new SqlParameter("@DetailTags", SqlDbType.Structured);
                parameterSearchFilters.Value = tbDetailTags;
                parameterSearchFilters.TypeName = "[dbo].[StringVector]";
                parameters.Add(parameterSearchFilters);

                System.Data.DataTable tbluceneRegIds = luceneRegDetailIds
                        .ToDataTable();
                var parameterSearchFilters_3 = new SqlParameter("@RegDetailIds", SqlDbType.Structured);
                parameterSearchFilters_3.Value = tbluceneRegIds;
                parameterSearchFilters_3.TypeName = "[dbo].[IntVector]";
                parameters.Add(parameterSearchFilters_3);

                regulationProcedures = this.dbContext.Set<SelectedRegulationProcedure>().FromSqlRaw($"EXEC Library.GetSelectedRegulation @RegID, @DetailTags, @RegDetailIds ", parameters.ToArray()).ToList();

                var regulation = this.dbContext.Regulations.Where(x => x.RegId.Equals(regulationId)).FirstOrDefault();
                regulation.Views += 1;
                this.dbContext.Entry<CompData.Models.Library.Regulation>(regulation).State = EntityState.Modified;
                this.dbContext.SaveChanges();

            }
            catch (Exception)
            {

            }
            return regulationProcedures;
        }

        public List<SelectedRegulationProcedure> GetSelectedRegSummary(int regulationId)
        {
            List<SelectedRegulationProcedure> regulationProcedures = this.dbContext.Set<SelectedRegulationProcedure>().FromSqlRaw($"EXEC Library.GetSelectedRegulation {regulationId} ").ToList();

            var regulation = this.dbContext.Regulations.Where(x => x.RegId.Equals(regulationId)).FirstOrDefault();
            regulation.Views += 1;
            this.dbContext.Entry<CompData.Models.Library.Regulation>(regulation).State = EntityState.Modified;
            this.dbContext.SaveChanges();

            return regulationProcedures;
        }

        public List<SelectedRegulationRequirement> GetSelectedRegRequirement(int regulationId)
        {
            List<SelectedRegulationRequirement> requirementProcedures = this.dbContext.Set<SelectedRegulationRequirement>().FromSqlRaw($"EXEC Library.GetAllRegRequirement {regulationId} ").ToList();

            var regulation = this.dbContext.Regulations.Where(x => x.RegId.Equals(regulationId)).FirstOrDefault();
            regulation.Views += 1;
            this.dbContext.Entry<CompData.Models.Library.Regulation>(regulation).State = EntityState.Modified;
            this.dbContext.SaveChanges();

            return requirementProcedures;
        }

        public List<RegulationSource> GetSelectedRegulationSourcesByUserId(Guid userId)
        {
            List<RegulationSource> regulationSources = this.dbContext.LinkedUserRegulationSources.Where(x => x.UserId.Equals(userId)).OrderByDescending(x => x.IsDefault).Select(x => x.RegulationSource).ToList();
            return regulationSources;
        }

        public List<int> GetSubscribedRegulationTypeByUserId(Guid userId, int sourceId)
        {
            List<int> regulationTypes = this.dbContext.LinkUserRegTypeSubscriptions.Where(x => x.UserId.Equals(userId) && x.RegSourceId.Equals(sourceId)).Select(x => x.RegTypeId).ToList();
            return regulationTypes;
        }

        public List<int> GetSubscribedRegulationByUserId(Guid userId, int RegId)
        {
            List<int> regulationTypes = this.dbContext.LinkUserRegulationSubscriptions.Where(x => x.UserId.Equals(userId) && x.RegId.Equals(RegId)).Select(x => x.RegId).ToList();
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

        public Result SubscribeRegulationByUser(Guid userID, int regId)
        {
            try
            {
                LinkUserRegulationSubscription linkUser = this.dbContext.LinkUserRegulationSubscriptions.Where(x => x.UserId.Equals(userID) && x.RegId.Equals(regId)).FirstOrDefault();
                string message = string.Empty;
                if (linkUser == null)
                {
                    LinkUserRegulationSubscription subscription = new LinkUserRegulationSubscription
                    {
                        UserId = userID,
                        RegId = regId
                    };

                    this.dbContext.Entry<LinkUserRegulationSubscription>(subscription).State = EntityState.Added;
                    int result = this.dbContext.SaveChanges();
                    if (result == 0) throw new Exception("Something went wrong please try again");
                    message = "Regulation has been subscribed.";
                }
                else
                {
                    this.dbContext.Entry<LinkUserRegulationSubscription>(linkUser).State = EntityState.Deleted;
                    int result = this.dbContext.SaveChanges();
                    if (result == 0) throw new Exception("Something went wrong please try again");
                    message = "Regulation has been unsubscribed.";
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

        public async Task<List<Models.Library.Regulation>> GetMostViewedRegulationsByUserSource(Guid userId)
        {
            try
            {
                List<Models.Library.Regulation> mostViewedRegulations = await (from r in dbContext.Regulations
                                                                               join ur in dbContext.LinkedUserRegulationSources on r.SourceID equals ur.SourceId
                                                                               where ur.UserId.Equals(userId)
                                                                               orderby r.Views descending
                                                                               select r).Take(5).ToListAsync();
                return mostViewedRegulations;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<Result> GetSuggestedRegulationsByUserSource(Guid userId, string searchTerm, string column = "title", List<string> tags = null)
        {
            try
            {
                List<int> luceneRegIds = new List<int>();
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var directory = FSDirectory.Open("c:\\temp\\directory");
                    using (Analyzer analyzer = new EnglishAnalyzer(LuceneVersion.LUCENE_48))
                    {
                        using (var reader = DirectoryReader.Open(directory))
                        {
                            var searcher = new IndexSearcher(reader);
                            var queryParser = new QueryParser(LuceneVersion.LUCENE_48, column, analyzer)
                            {
                                AllowLeadingWildcard = true,
                                AutoGeneratePhraseQueries = true,
                                FuzzyMinSim = 3f
                            };
                            var query = queryParser.Parse(searchTerm);
                            var collector = TopScoreDocCollector.Create(10000, true);
                            searcher.Search(query, collector);

                            var matches = collector.GetTopDocs().ScoreDocs;
                            foreach (var item in matches)
                            {
                                var id = item.Doc;
                                var doc = searcher.Doc(id);

                                string regId = doc.GetField("regId").GetStringValue();
                                luceneRegIds.Add(int.Parse(regId));
                            }
                        }
                    }
                }

                List<Models.Library.Regulation> suggestedRegulations = new List<Models.Library.Regulation>();

                if (tags == null)
                {
                    suggestedRegulations = await (from r in dbContext.Regulations
                                                  join ur in dbContext.LinkedUserRegulationSources on r.SourceID equals ur.SourceId
                                                  where ur.UserId.Equals(userId) && luceneRegIds.Contains(r.RegId)
                                                  orderby r.Views descending
                                                  select r).Take(5).ToListAsync(); 
                }
                else
                {
                    suggestedRegulations = await (from r in dbContext.Regulations
                                                  join ur in dbContext.LinkedUserRegulationSources on r.SourceID equals ur.SourceId
                                                  join rTag in dbContext.TagMaps on r.RegId equals rTag.RegId
                                                  where ur.UserId.Equals(userId) && luceneRegIds.Contains(r.RegId) && tags.Contains(rTag.Tag)
                                                  orderby r.Views descending
                                                  select r).Take(5).ToListAsync();
                }

                return new Result
                {
                    Status = ResultStatus.Success,
                    Data = suggestedRegulations
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    Status = ResultStatus.Error,
                    Message = ex.Message
                };
            }
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

                return new Result
                {
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
                this.dbContext.Regulations.Add(regulation);
                int result = this.dbContext.SaveChanges();
                if (result == 0) throw new Exception("Something went wrong please try again");
                message = "New Regulation has been saved.";

                return new Result
                {
                    Data = regulation.RegId,
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

        public async Task<Result> SaveRegulationDetail(SaveRegulationDetailViewModel viewModel)
        {
            try
            {
                string message = string.Empty;
                var httpUser = httpContext.HttpContext.User;
                var user = await userManager.GetUserAsync(httpUser);

                List<RegulationSection> existingRegulationSections = await dbContext.RegulationSections.Where(x => x.RegulationId.Equals(viewModel.RegId)).Include(x => x.RegulationDetails).ToListAsync();

                SetUpdateRegulationSectionsModel(viewModel.Sections, viewModel.RegId, ref existingRegulationSections);
                this.dbContext.RegulationSections.UpdateRange(existingRegulationSections);

                int result = this.dbContext.SaveChanges();
                if (result == 0) throw new Exception("Something went wrong please try again");
                message = "New Regulation Detail has been saved.";

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

        public async Task<Result> GetTagsGroup(string tagGroupId)
        {
            try
            {
                string message = string.Empty;

                var tagMaps = await dbContext.TagMaps.Where(x => x.TagGroupKey.Equals(tagGroupId)).ToListAsync();

                return new Result
                {
                    Status = ResultStatus.Success,
                    Message = message,
                    Data = tagMaps
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

        public async Task<Result> SetTagsGroup(List<string> tags, string tagGroupId, int regId, int secId, int descId)
        {
            try
            {
                string message = string.Empty;
                var httpUser = httpContext.HttpContext.User;
                var user = await userManager.GetUserAsync(httpUser);

                List<TagMap> _tags = tags.Select(x => new TagMap
                {
                    Tag = x,
                    TagGroupKey = tagGroupId,
                    RegId = regId,
                    SecId = secId,
                    DescId = descId,
                    TagType = TagType.DetailTag
                }).ToList();
                this.dbContext.TagMaps.AddRange(_tags);
                int result = await this.dbContext.SaveChangesAsync();

                if (result == 0) throw new Exception("Something went wrong please try again");
                message = "Tag(s) linked.";

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

        public async Task<Result> GetRegulationIdByCustomURL(string customURL) 
        {
            try
            {
                var regulation = await this.dbContext.Regulations.Where(x => x.CustomURL.Equals(customURL)).FirstOrDefaultAsync();
                if (regulation != null)
                {
                    return new Result
                    {
                        Status = ResultStatus.Success,
                        Data = regulation.RegId
                    };
                }
                else
                {
                    return new Result
                    {
                        Status = ResultStatus.Error,
                        Message = "Invalid Customer URL"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result { 
                    Message = ex.Message,
                    Status = ResultStatus.Error
                };
            }
        }

        public async Task<Result> GetRelatedRegulation(int regId)
        {
            try
            {
                string message = string.Empty;

                var linkedRelatedRegulations = await dbContext.LinkedRelatedRegulations.Where(x => x.RegId.Equals(regId)).Select(x => x.RelatedRegulation).ToListAsync();

                return new Result
                {
                    Status = ResultStatus.Success,
                    Message = message,
                    Data = linkedRelatedRegulations
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

        public async Task<Result> SetLinkedRelatedRegulation(int regId, int relatedRegId)
        {
            try
            {
                string message = string.Empty;
                LinkedRelatedRegulation linkedRelatedRegulation = new LinkedRelatedRegulation { 
                    RegId = regId,
                    RelatedRegId = relatedRegId
                };
                this.dbContext.LinkedRelatedRegulations.Add(linkedRelatedRegulation);
                int result = await this.dbContext.SaveChangesAsync();

                var regulation = this.dbContext.Regulations.Where(x => x.RegId.Equals(relatedRegId)).Select(x => x.RegulationSource.ShortName + " | " + x.RegulationTitle + " | " + x.IssueDate.ToShortDateString()).FirstOrDefault();
                
                if (result == 0) throw new Exception("Something went wrong please try again");
                message = "Related Regulation linked.";

                return new Result
                {
                    Status = ResultStatus.Success,
                    Message = message,
                    Data = regulation
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

        public async Task<Result> GetAllTagFilters(int? sourceId, int? typeId, TagType tagType)
        {
            try
            {
                string message = string.Empty;
                List<TagMap> tags = new List<TagMap>();

                if (typeId == null && sourceId == null)
                {
                    tags = await (from tagM in dbContext.TagMaps
                                  join reg in dbContext.Regulations on tagM.RegId equals reg.RegId
                                  where tagM.TagType.Equals(tagType)
                                  select tagM).ToListAsync<TagMap>();
                }
                else if (typeId == null)
                {
                    tags = await (from tagM in dbContext.TagMaps
                                  join reg in dbContext.Regulations on tagM.RegId equals reg.RegId
                                  where reg.SourceID.Equals(sourceId) && tagM.TagType.Equals(tagType)
                                  select tagM).ToListAsync<TagMap>();
                }
                else
                {
                    tags = await (from tagM in dbContext.TagMaps
                                  join reg in dbContext.Regulations on tagM.RegId equals reg.RegId
                                  where reg.SourceID.Equals(sourceId) && reg.RegTypeID.Equals(typeId) && tagM.TagType.Equals(tagType)
                                  select tagM).ToListAsync<TagMap>();
                }

                return new Result
                {
                    Status = ResultStatus.Success,
                    Message = message,
                    Data = tags
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    Status = ResultStatus.Success,
                    Message = ex.Message
                };
            }
        }

        public async Task<Result> GetAllTagFiltersByRegId(int regId, TagType tagType)
        {
            try
            {
                string message = string.Empty;
                List<TagMap> tags = new List<TagMap>();

                tags = await (from tagM in dbContext.TagMaps
                              join reg in dbContext.Regulations on tagM.RegId equals reg.RegId
                              where reg.RegId.Equals(regId) && tagM.TagType.Equals(tagType)
                              select tagM).ToListAsync<TagMap>();

                return new Result
                {
                    Status = ResultStatus.Success,
                    Message = message,
                    Data = tags
                };
            }
            catch (Exception ex)
            {
                return new Result
                {
                    Status = ResultStatus.Success,
                    Message = ex.Message
                };
            }
        }

        public async Task<Result> GetAllRegulations(AjaxDropDown ajaxDropDown)
        {
            try
            {
                string message = string.Empty;

                int pageSize = 10;
                int PageNo = ajaxDropDown.page * pageSize;
                int pageLimit = pageSize * PageNo;

                var regulations = await dbContext.Regulations.Where(x => x.RegulationTitle.Contains(ajaxDropDown.q)).Select(x => new { id = x.RegId, text = x.RegulationTitle }).ToListAsync();

                return new Result
                {
                    Status = ResultStatus.Success,
                    Message = message,
                    Data = regulations
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

        private List<RegulationSection> SetSaveRegulationSectionsModel(List<SectionDetailViewModel> sectionDetailViewModels, int regId)
        {
            List<RegulationSection> sections = new List<RegulationSection>();

            #region Fresh Regulation
            foreach (var regulationSection in sectionDetailViewModels)
            {
                sections.Add(new RegulationSection
                {
                    SectionTitle = regulationSection.Title,
                    Sequence = regulationSection.Seq,
                    RegulationDetails = regulationSection.Descriptions.Select(x => new RegulationDetail { RegDescription = x.Description, RegulationId = regId }).ToList(),
                    RegulationId = regId,
                    Children = regulationSection.Children == null ? null : SetSaveRegulationSectionsModel(regulationSection.Children, regId)
                });
            }
            #endregion
            return sections;
        }


        public async Task<Result> UpdateMetaDetails(UpdateMetaDataRegulationViewModel viewModel)
        {
            try
            {
                string message = string.Empty;
                var httpUser = httpContext.HttpContext.User;
                var user = await userManager.GetUserAsync(httpUser);
                int result = 0;

                var regulation = this.dbContext.Regulations.Where(x => x.RegId.Equals(viewModel.RegId)).FirstOrDefault();
                if (regulation != null)
                {
                    regulation.CustomURL = viewModel.CustomURL;
                    regulation.MetaTag = viewModel.MetaTag;
                    regulation.MetaDescription = viewModel.MetaDescription;

                    this.dbContext.Entry<Models.Library.Regulation>(regulation).State = EntityState.Modified;
                    result = await this.dbContext.SaveChangesAsync();
                }


                if (result == 0) throw new Exception("Something went wrong please try again");
                message = "Update Regulation Meta Detail has been saved.";

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

        private void SetUpdateRegulationSectionsModel(List<SectionDetailViewModel> sectionDetailViewModels, int regId, ref List<RegulationSection> regulationSections)
        {
            #region Existing Regulation
            foreach (var regSection in sectionDetailViewModels)
            {
                string sectionTitle = regSection.Title;
                int sectionSeq = regSection.Seq;
                int? parentId = regSection.parentId;
                List<DescriptionViewModel> descriptions = regSection.Descriptions;
                List<SectionDetailViewModel> children = regSection.Children ?? new List<SectionDetailViewModel>();

                if (regSection.SecId == null)
                {
                    regulationSections.Add(new RegulationSection
                    {
                        SectionTitle = sectionTitle,
                        Sequence = sectionSeq,
                        ParentId = parentId,
                        RegulationId = regId,
                        RegulationDetails = descriptions.Select(x => new RegulationDetail { RegDescription = x.Description, Sequence = x.Seq, RegulationId = regId }).ToList(),
                        Children = SetSaveRegulationSectionsModel(children, regId)
                    });
                }
                else
                {
                    var existingSection = regulationSections.Where(x => x.SectionId.Equals(regSection.SecId)).FirstOrDefault();

                    existingSection.SectionTitle = sectionTitle;
                    existingSection.Sequence = sectionSeq;
                    existingSection.ParentId = parentId;
                    #region Existing Section Detail Alteration
                    foreach (var desc in descriptions)
                    {
                        if (desc.DescId == null)
                        {
                            existingSection.RegulationDetails.Add(new RegulationDetail
                            {
                                RegDescription = desc.Description,
                                Sequence = desc.Seq,
                                RegulationId = regId,
                                SectionId = existingSection.SectionId
                            });
                        }
                        else
                        {
                            var existingDesc = existingSection.RegulationDetails.Where(x => x.RegDetailId.Equals(desc.DescId)).FirstOrDefault();

                            existingDesc.RegDescription = desc.Description;
                            existingDesc.Sequence = desc.Seq;
                        }
                    }
                    #endregion
                    if (children.Count > 0)
                    {
                        List<RegulationSection> childrenSections = existingSection.Children;
                        SetUpdateRegulationSectionsModel(children, regId, ref childrenSections);
                        existingSection.Children = childrenSections;
                    }
                }

            }
            #endregion
        }
    }
}
