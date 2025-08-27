using System;
using EFakturCallback.Authorizations;
using Microsoft.AspNetCore.Mvc;
namespace EFakturCallback.Attributes
{
	public class ApiKeyAttribute : ServiceFilterAttribute
	{
		public ApiKeyAttribute() : base(typeof(ApiKeyAuthFilter))
		{
		}
	}
}

