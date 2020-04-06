using Canary.Application;
using FluentValidation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Canary.UI.Client.Middlewares
{
   
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IWebHostEnvironment env;

        public ErrorHandlingMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            this.next = next;
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment environment)
        {
            var _code = HttpStatusCode.InternalServerError;
            var _exception = ex.InnermostException();

            Type _exceptionType = _exception.GetType();
            string _errorType = _exceptionType.Name;
            object _message = _exception.Message;

            if (_exception is InvalidReferenceException)
            {
                _code = HttpStatusCode.NotFound;
            }
            else if (_exception is SqlException && !environment.IsDevelopment())
            {
                _code = HttpStatusCode.BadRequest;
                _errorType = "Server Data Error";
                _message = "Please contact support!";
            }
            else if (_exception is InvalidOperationException)
            {
                _code = HttpStatusCode.BadRequest;
                _errorType = "Invalid Request";
            }
            else if (_exception is ValidationException)
            {
                var _valEx = _exception as ValidationException;

                _code = HttpStatusCode.BadRequest;
                _errorType = "Invalid Data";

                _message = _valEx.Errors
                    .Select(a => a.ErrorMessage)
                    .ToList();
            }

            var _result = JsonConvert.SerializeObject(new ErrorHandlingResult
            {
                type = _errorType,
                message = _message
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)_code;

            return context.Response.WriteAsync(_result);
        }
    }

    public class ErrorHandlingResult
    {
        public string type { get; set; }
        public object message { get; set; }
    }
}
