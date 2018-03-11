using StatlerWaldorfCorp.LocationReporter.Service.Models;

namespace StatlerWaldorfCorp.LocationReporter.Service.Events
{
    public interface IEventEmitter
    {
        void EmitLocationRecordedEvent(MemberLocationRecordedEvent memberLocationRecordedEvent);
    }
}