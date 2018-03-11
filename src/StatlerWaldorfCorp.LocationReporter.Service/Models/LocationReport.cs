using System;

namespace StatlerWaldorfCorp.LocationReporter.Service.Models
{
    public class LocationReport
    {
        public Guid Id { get; set; }
        public string Origin { get; set; }  
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Guid MemberId { get; set; }
    }
}