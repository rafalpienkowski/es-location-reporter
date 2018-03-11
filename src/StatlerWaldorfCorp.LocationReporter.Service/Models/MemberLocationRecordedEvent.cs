using System;
using Newtonsoft.Json;

namespace StatlerWaldorfCorp.LocationReporter.Service.Models
{
    public class MemberLocationRecordedEvent
    {
        public string Origin { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid MemberId { get; set; }
        public long RecordedTime { get; set; }
        public Guid ReportId { get; set; }
        public Guid TeamId { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}