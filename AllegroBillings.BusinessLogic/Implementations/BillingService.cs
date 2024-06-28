using AllegroBillings.BusinessLogic.Dtos;
using AllegroBillings.Contracts.Configurations;
using AllegroBillings.Contracts.Interfaces.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace AllegroBillings.BusinessLogic.Services
{
    public class BillingService : IBillingService
    {
        private readonly HttpClient _httpClient;
        private readonly AllegroApiSettings _settings;

        public BillingService(HttpClient httpClient, IOptions<AllegroApiSettings> options)
        {
            _httpClient = httpClient;
            _settings = options.Value;
            _httpClient.BaseAddress = new Uri(_settings.BaseUrl);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settings.Token);
        }

        public async Task<IEnumerable<BillingDto>> FetchBillingEntriesAsync(Dictionary<string, string> queryParams)
        {
            var query = new FormUrlEncodedContent(queryParams);
            var response = await _httpClient.GetAsync($"{_settings.GetBillingsEndpoint}?{await query.ReadAsStringAsync()}");

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Dictionary<string, List<BillingDto>>>(content);

            return result["billingEntries"];
        }
    }
}
