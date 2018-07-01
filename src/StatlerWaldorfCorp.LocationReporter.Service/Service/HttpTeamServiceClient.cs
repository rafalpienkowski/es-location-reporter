using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using StatlerWaldorfCorp.LocationReporter.Service.Models;

namespace StatlerWaldorfCorp.LocationReporter.Service.Service
{
    public class HttpTeamServiceClient : ITeamServiceClient
    {
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;

        public HttpTeamServiceClient(IOptions<TeamServiceOptions> serviceOptions, ILogger<HttpTeamServiceClient> logger)
        {
            _logger = logger;
            
            var url = serviceOptions.Value.Url;
            logger.LogError($"Team Service HTTP client using URI {url}");

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(url);
        }

        public async Task<Guid> GetTeamForMemberAsync(Guid memberId)
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.GetAsync($"/api/members/{memberId}/team");
            if(!response.IsSuccessStatusCode)
            {
                return Guid.Empty;
            }

            var json = await response.Content.ReadAsStringAsync();
            var teamIdResponse = JsonConvert.DeserializeObject<TeamIdResponse>(json);
            return teamIdResponse.TeamId;
        }
    }

    public class TeamIdResponse
    {
        public Guid TeamId { get; set; }
    }
}