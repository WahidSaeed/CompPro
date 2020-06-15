using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMData.ViewModels.BaseViewModel
{
    public class DataTable
    {
        public List<DataTableParameters> Parameters { get; set; }
    }

    public class DataTableParameters
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class KeyValuePair
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }

    public class jQueryDataTableInput
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class JQueryDtaTableOutput
    {
        public int draw { get; set; }
        public string error { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public string data { get; set; }
    }

    public class JQueryDtaTableOutput<T> where T : class
    {
        public int draw { get; set; }
        public string error { get; set; }
        public int recordsFiltered { get; set; }
        public int recordsTotal { get; set; }
        public IEnumerable<T> data { get; set; }
    }
}
