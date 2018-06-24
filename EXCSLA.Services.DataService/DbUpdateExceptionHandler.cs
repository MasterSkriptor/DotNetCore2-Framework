using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace EXCSLA.Services.DataServices
{
    public class DbUpdateExceptionHandler
    {
        private readonly RequestDelegate _next;

        public DbUpdateExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DbUpdateException ex)
            {                
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, DbUpdateException ex)
        {
            object result;
            var code = 400;

            if (ex.InnerException.Message.ToLower().Contains("duplicate"))
            {
                code = 409;

                result = new 
                { 
                    Error = "Error updating database. Duplicate value."
                };
            }
            else
            {
                result = new 
                {
                    Error = "Error updating database"
                };
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}