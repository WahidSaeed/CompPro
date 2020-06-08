using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRMData.Generics
{
    public static class ExtensionMethods
    {
        public static string ToJson(this object value)
        {
            return JsonConvert.SerializeObject(value, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
        }
    }
}
