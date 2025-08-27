using System;
namespace EFakturCallback.Authorizations
{
	public interface IApiKeyValidation
	{
		bool IsValidApiKey(string userApiKey);
	}
}

