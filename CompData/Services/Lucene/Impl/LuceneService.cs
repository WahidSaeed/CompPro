using CRMData.Data;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.En;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers.Classic;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompData.Services.Lucene.Impl
{
    public class LuceneService : ILuceneService
    {
        private readonly ApplicationDbContext _dbContext;
        public LuceneService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public void CreateLuceneIndex(string indexPath)
        {
            try
            {
                var directory = FSDirectory.Open(indexPath);
                using (Analyzer analyzer = new EnglishAnalyzer(LuceneVersion.LUCENE_48))
                {
                    using (var writer = new IndexWriter(directory, new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer)))
                    {
                        writer.DeleteAll();
                        var regulationDetails = this._dbContext.RegulationDetails.Select(x => new { x.Regulation.RegId, x.RegDetailId, x.Regulation.ReferenceNumber, x.Regulation.RegulationTitle, x.RegDescriptionClean }).Where(x => !string.IsNullOrEmpty(x.ReferenceNumber) && !string.IsNullOrEmpty(x.RegulationTitle) && !string.IsNullOrEmpty(x.RegDescriptionClean)).ToList();
                        foreach (var item in regulationDetails)
                        {
                            var doc = new Document();
                            doc.Add(new Field("regId", item.RegId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                            doc.Add(new Field("ref", item.ReferenceNumber, Field.Store.YES, Field.Index.NOT_ANALYZED));
                            doc.Add(new Field("title", item.ReferenceNumber + " " + item.RegulationTitle, Field.Store.YES, Field.Index.ANALYZED));
                            doc.Add(new Field("regDetailId", item.RegDetailId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                            doc.Add(new Field("desc", item.RegDescriptionClean.StripHTML(), Field.Store.YES, Field.Index.ANALYZED));

                            writer.AddDocument(doc);
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<int> GetIds(string searchTerm, string searchColumn, string returnIdColumn)
        {
            List<int> luceneIds = new List<int>();
            try
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var directory = FSDirectory.Open("c:\\temp\\directory");
                    using (Analyzer analyzer = new EnglishAnalyzer(LuceneVersion.LUCENE_48))
                    {
                        using (var reader = DirectoryReader.Open(directory))
                        {
                            var searcher = new IndexSearcher(reader);
                            var queryParser = new QueryParser(LuceneVersion.LUCENE_48, searchColumn, analyzer)
                            {
                                AllowLeadingWildcard = true,
                                AutoGeneratePhraseQueries = true,
                                FuzzyMinSim = 3f
                            };
                            var query = queryParser.Parse(searchTerm);
                            var collector = TopScoreDocCollector.Create(10000, true);
                            searcher.Search(query, collector);

                            var matches = collector.GetTopDocs().ScoreDocs;
                            if (matches.Count() > 0)
                            {
                                foreach (var item in matches)
                                {
                                    var docId = item.Doc;
                                    var doc = searcher.Doc(docId);

                                    string Id = doc.GetField(returnIdColumn).GetStringValue();
                                    luceneIds.Add(int.Parse(Id));
                                } 
                            }
                            else
                            {
                                luceneIds.Add(-1);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return luceneIds;
        }
    }
}
