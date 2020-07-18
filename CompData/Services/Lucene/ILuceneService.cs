using System;
using System.Collections.Generic;
using System.Text;

namespace CompData.Services.Lucene
{
    interface ILuceneService
    {
        public void CreateLuceneIndex(string indexPath);
        public List<int> GetIds(string searchTerm, string searchColumn, string returnIdColumn);
    }
}
