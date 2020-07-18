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
using CompData.Services.Lucene;

namespace CompData.Configurations.Initializer
{
    public static class Initializer
    {
        public static void Lucene(IServiceProvider serviceProvider)
        {
            try
            {
                var lucene = serviceProvider.GetRequiredService<ILuceneService>();
                lucene.CreateLuceneIndex("c:\\temp\\directory");
            }
            catch (Exception ex)
            {

            }
        }
    }
}
