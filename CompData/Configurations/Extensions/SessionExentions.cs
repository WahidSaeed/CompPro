using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace CRMData
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            return value == null ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }

        public static T GetOrCreateSession<T>(this ISession session, string key, Func<T> func)
        {
            T value = Get<T>(session, key);
            if (value == null)
            {
                string serializeObject = JsonConvert.SerializeObject(func());
                value = JsonConvert.DeserializeObject<T>(serializeObject);
                session.Set<T>(key, value);
            }
            return value;
        }
    }
}
