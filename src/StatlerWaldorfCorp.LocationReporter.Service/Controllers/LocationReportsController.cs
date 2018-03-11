using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StatlerWaldorfCorp.LocationReporter.Service.Events;
using StatlerWaldorfCorp.LocationReporter.Service.Models;
using StatlerWaldorfCorp.LocationReporter.Service.Service;

namespace StatlerWaldorfCorp.LocationReporter.Service.Controllers
{
    [Route("api/members/{memberId}/locationreports")]
    public class LocationReportsController : Controller
    {
        private readonly ICommandEventConverter _commandEventConverter;
        private readonly IEventEmitter _eventEmitter;
        private readonly ITeamServiceClient _teamServiceClient;

        public LocationReportsController(ICommandEventConverter commandEventConverter, IEventEmitter eventEmitter, ITeamServiceClient teamServiceClient)
        {
            _commandEventConverter = commandEventConverter;
            _eventEmitter = eventEmitter;
            _teamServiceClient = teamServiceClient;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(Guid memberId, [FromBody]LocationReport locationReport)
        {
            var locationRecordedEvent = _commandEventConverter.CommandToEvent(locationReport);
            locationRecordedEvent.TeamId = await _teamServiceClient.GetTeamForMemberAsync(locationReport.MemberId);
            _eventEmitter.EmitLocationRecordedEvent(locationRecordedEvent);

            return this.Created($"/api/members/{memberId}/locationreposts/{locationReport.Id}", locationReport);
        }

    }
}
