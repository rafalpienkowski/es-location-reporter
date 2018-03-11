using System;
using System.Threading.Tasks;

namespace StatlerWaldorfCorp.LocationReporter.Service.Service
{
    public interface ITeamServiceClient
    {
        Task<Guid> GetTeamForMemberAsync(Guid memberId);
    }
}