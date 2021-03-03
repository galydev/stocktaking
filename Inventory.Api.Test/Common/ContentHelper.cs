using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Inventory.Api.Test.Common
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(object obj)
            => new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
    }
}