using StatlerWaldorfCorp.LocationReporter.Service.Models;
using Newtonsoft.Json;

namespace StatlerWaldorfCorp.LocationReporter.Service.Extenions
{   
    public static class ServiceExtensions
    {
        public static string ToJson(this MemberLocationRecordedEvent model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}