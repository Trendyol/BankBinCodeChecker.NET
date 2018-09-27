using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BankBinCodeChecker.Helpers;
using BankBinCodeChecker.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BankBinCodeChecker
{
    public class BankBinCodeChecker : IBankBinCodeChecker
    {
        private const string BaseEndpoint = "https://lookup.binlist.net/";
        private readonly HttpClient _client;
        private readonly ILogger _logger;

        public BankBinCodeChecker(ILogger<BankBinCodeChecker> logger = null,
            IHttpClientFactory httpClientFactory = null)
        {
            _logger = logger;
            _client = httpClientFactory != null
                ? httpClientFactory.CreateClient()
                : new HttpClient();

            _client.BaseAddress = new Uri(BaseEndpoint);
        }

        public async Task<BankBinCodeResult> CheckBankBinCodeAsync(int bankBınCode)
        {
            var request = await _client.GetAsync($"/{bankBınCode.ToString()}");
            if (request.IsSuccessStatusCode)
            {
                var json = await request.Content?.ReadAsStringAsync();
                return !string.IsNullOrEmpty(json)
                    ? JsonConvert.DeserializeObject<BankBinCodeResult>(json)
                    : null;
            }

            string message = "";
            if (request.StatusCode == HttpStatusCode.NotFound)
            {
                message = "Bank bin code is not found";
            }
            else if (request.StatusCode == HttpStatusCode.BadRequest)
            {
                message = "Bank bin code is checking invalid";
            }
            else
            {
                message = "Something went wrong when bank bin code is checking";
            }

            _logger?.LogWarning(message);

            return null;
        }

        public BankBinCodeResult CheckBankBinCode(int bankBınCode)
        {
            var request = AsyncHelpers.RunSync(() => _client.GetAsync($"/{bankBınCode.ToString()}"));
            if (request.IsSuccessStatusCode)
            {
                var json = AsyncHelpers.RunSync(() => request.Content?.ReadAsStringAsync());
                return !string.IsNullOrEmpty(json)
                    ? JsonConvert.DeserializeObject<BankBinCodeResult>(json)
                    : null;
            }

            string message = "";
            if (request.StatusCode == HttpStatusCode.NotFound)
            {
                message = Constants.NotFoundMessage;
            }
            else if (request.StatusCode == HttpStatusCode.BadRequest)
            {
                message = Constants.InvalidCodeMessage;
            }
            else
            {
                message = Constants.UnknownErrorMessage;
            }

            _logger?.LogWarning(message);

            return null;
        }
    }
}