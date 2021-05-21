using HotChocolateAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotChocolateAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch(EmptyListException emptyList)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(emptyList.Message);
            }
            catch(ProductAlreadyExistException prod)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(prod.Message);
            }
            catch (NoAccess prod)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(prod.Message);
            }
            catch(PictureDoesntExistException pic)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(pic.Message);
            }
            catch(AlreadyExists pic)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(pic.Message);
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
