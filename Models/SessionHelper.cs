using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace dhamo.aleksander._5H.SecondaWeb.Models
{
    public static class SessionHelper
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}

// link come usare Newtonsoft.Json: https://stackoverflow.com/questions/40675162/install-a-nuget-package-in-visual-studio-code
// link come usare session: https://learningprogramming.net/net/asp-net-core-mvc-5/use-session-in-asp-net-core-mvc-5/