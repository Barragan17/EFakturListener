using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Serilog;
using System.ComponentModel.DataAnnotations;
using EFakturCallback.Attributes;

namespace EFakturCallback
{
    [ETagFilter(200)]
    [ApiKey]
    public class SecureController : ControllerBase
    {
        private readonly ICollection<object> _errors = new List<object>();
        protected SecureController()
        {
        }
        protected ActionResult Response()
        {
            if (IsRequestValid())
            {
                return Ok("Success");
            }

            return BadRequest(_errors.ToArray()[0]);
        }

        protected bool IsRequestValid()
        {
            return !_errors.Any();
        }
        protected void AddError(string error, string errorCode, string logError)
        {
            Log.Error(logError);
            _errors.Add(new
            {
                message = error,
                status = errorCode
            });
        }
        protected void ClearErrors()
        {
            _errors.Clear();
        }
    }
}