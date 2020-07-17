using CRMData.Data;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Util;
using Lucene.Net.Store;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Analysis.En;

namespace CompData.Configurations.Initializer
{
    public static class Initializer
    {
        public static void Lucene(IServiceProvider serviceProvider)
        {
            try
            {
                var directory = FSDirectory.Open("c:\\temp\\directory");
                using (Analyzer analyzer = new EnglishAnalyzer(LuceneVersion.LUCENE_48))
                {
                    using (var writer = new IndexWriter(directory, new IndexWriterConfig(LuceneVersion.LUCENE_48, analyzer)))
                    {
                        writer.DeleteAll();
                        using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>())
                        {
                            var regulationDetails = context.RegulationDetails.Select(x => new { x.Regulation.RegId, x.RegDetailId, x.Regulation.ReferenceNumber, x.Regulation.RegulationTitle, x.RegDescriptionClean }).Where(x => !string.IsNullOrEmpty(x.ReferenceNumber) && !string.IsNullOrEmpty(x.RegulationTitle) && !string.IsNullOrEmpty(x.RegDescriptionClean)).ToList();
                            foreach (var item in regulationDetails)
                            {
                                var doc = new Document();
                                doc.Add(new Field("regId", item.RegId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                                doc.Add(new Field("ref", item.ReferenceNumber, Field.Store.YES, Field.Index.NOT_ANALYZED));
                                doc.Add(new Field("title", item.ReferenceNumber + " " +item.RegulationTitle, Field.Store.YES, Field.Index.ANALYZED));
                                doc.Add(new Field("regDetailId", item.RegDetailId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                                doc.Add(new Field("desc", item.RegDescriptionClean.StripHTML(), Field.Store.YES, Field.Index.ANALYZED));

                                writer.AddDocument(doc);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
