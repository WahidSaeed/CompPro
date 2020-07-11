using CRMData.Configurations.Constants.Enums;
using System.Collections.Generic;

namespace CRMData.Configurations.Generics
{
    public struct Result<T> where T : class
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
    public struct ResultSingleObject<T> where T : class
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }

    public struct Result
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
    }

}
