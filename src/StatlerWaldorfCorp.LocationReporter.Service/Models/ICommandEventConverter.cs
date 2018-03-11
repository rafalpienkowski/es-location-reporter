using StatlerWaldorfCorp.LocationReporter.Service.Models;

namespace StatlerWaldorfCorp.LocationReporter.Service.Models
{
    public interface ICommandEventConverter
    {
        MemberLocationRecordedEvent CommandToEvent(LocationReport locationReport); 
    }
}