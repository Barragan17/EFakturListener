using System;
using EFakturCallback.Configuration;
namespace EFakturCallback.Authorizations
{
    public class ApiKeyValidation : IApiKeyValidation
    {
        readonly GeneralConfiguration _configuration;
        public ApiKeyValidation(GeneralConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool IsValidApiKey(string userApiKey)
        {
            if (string.IsNullOrWhiteSpace(userApiKey))
                return false;
            string? apiKey = _configuration.ApiKey;
            if (apiKey == null || apiKey != userApiKey)
                return false;
            return true;
        }
    }
}

