using System;
using StatlerWaldorfCorp.LocationReporter.Service.Models;

namespace StatlerWaldorfCorp.LocationReporter.Service.Models
{
    public class CommandEventConverter : ICommandEventConverter
    {
        public MemberLocationRecordedEvent CommandToEvent(LocationReport locationReport)
        {
            MemberLocationRecordedEvent locationRecordedEvent = new MemberLocationRecordedEvent 
            {
                Latitude = locationReport.Latitude,
                Longitude = locationReport.Longitude,
                Origin = locationReport.Origin,
                MemberId = locationReport.MemberId,
                ReportId = locationReport.Id,
                RecordedTime = DateTime.Now.ToUniversalTime().Ticks
            };

            return locationRecordedEvent;
        }
    }
}