using DB.Dto.Link;
using DB.Dto.Message;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using RESTfulWebServices.Controllers;
using System.Text;
using System.Text.Json;

namespace RESTfulWebServices.Middleware
{
    public class HateoasMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LinkGenerator _linkGenerator;

        public HateoasMiddleware(RequestDelegate next, LinkGenerator linkGenerator)
        {
            _next = next;
            _linkGenerator = linkGenerator;
        }

        //public async Task InvokeAsync(HttpContext context, IActionContextAccessor actionContextAccessor)
        //{
        //    var originalResponseBodyStream = context.Response.Body;
        //    using var responseBodyStream = new MemoryStream();
        //    context.Response.Body = responseBodyStream;

        //    await _next(context);

        //    context.Response.Body.Seek(0, SeekOrigin.Begin);
        //    var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
        //    context.Response.Body.Seek(0, SeekOrigin.Begin);

        //    if (context.Response.ContentType != null && context.Response.ContentType.Contains("application/json"))
        //    {
        //        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        //        var result = JsonSerializer.Deserialize<JsonElement>(responseBody, options);

        //        if (result.ValueKind == JsonValueKind.Object)
        //        {
        //            var messageDto = JsonSerializer.Deserialize<MessageDto>(result.GetRawText(), options);
        //            if (messageDto != null)
        //            {
        //                AddHateoasLinks(messageDto, actionContextAccessor);
        //                responseBody = JsonSerializer.Serialize(messageDto, options);
        //            }
        //        }
        //        else if (result.ValueKind == JsonValueKind.Array)
        //        {
        //            var messageList = JsonSerializer.Deserialize<List<MessageDto>>(result.GetRawText(), options);
        //            if (messageList != null)
        //            {
        //                foreach (var messageDto in messageList)
        //                {
        //                    AddHateoasLinks(messageDto, actionContextAccessor);
        //                }
        //                responseBody = JsonSerializer.Serialize(messageList, options);
        //            }
        //        }

        //        var responseBytes = Encoding.UTF8.GetBytes(responseBody);
        //        context.Response.Body = originalResponseBodyStream;
        //        await context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
        //    }
        //    else
        //    {
        //        context.Response.Body.Seek(0, SeekOrigin.Begin);
        //        await responseBodyStream.CopyToAsync(originalResponseBodyStream);
        //    }
        //}

        public async Task InvokeAsync(HttpContext context, IActionContextAccessor actionContextAccessor)
        {
            var originalResponseBodyStream = context.Response.Body;
            using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            // Check if the response is JSON
            if (context.Response.ContentType != null && context.Response.ContentType.Contains("application/json"))
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<JsonElement>(responseBody, options);
                // Only success response
                if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
                {
                    if (result.ValueKind == JsonValueKind.Object)
                    {
                        var messageDto = JsonSerializer.Deserialize<MessageDto>(result.GetRawText(), options);
                        if (messageDto != null)
                        {
                            AddHateoasLinks(messageDto, actionContextAccessor);
                            responseBody = JsonSerializer.Serialize(messageDto, options);
                        }
                    }
                    else if (result.ValueKind == JsonValueKind.Array)
                    {
                        var messageList = JsonSerializer.Deserialize<List<MessageDto>>(result.GetRawText(), options);
                        if (messageList != null)
                        {
                            foreach (var messageDto in messageList)
                                AddHateoasLinks(messageDto, actionContextAccessor);
                            responseBody = JsonSerializer.Serialize(messageList, options);
                        }
                    }
                }
                var responseBytes = Encoding.UTF8.GetBytes(responseBody);
                context.Response.Body = originalResponseBodyStream;
                await context.Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
            else
            {
                // Non-JSON response
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);
            }
        }


        private void AddHateoasLinks(MessageDto messageDto, IActionContextAccessor actionContextAccessor)
        {
            var actionContext = actionContextAccessor.ActionContext;

            if (actionContext != null)
            {
                messageDto.Links.Add(new LinkDto(
                    _linkGenerator.GetPathByAction(actionContext.HttpContext, nameof(MessageController.GetOne), "Message", new { id = messageDto.Id }),
                    "self",
                    "GET"));
                messageDto.Links.Add(new LinkDto(
                    _linkGenerator.GetPathByAction(actionContext.HttpContext, nameof(MessageController.Update), "Message", new { id = messageDto.Id }),
                    "updateMessage",
                    "PUT"));
                messageDto.Links.Add(new LinkDto(
                    _linkGenerator.GetPathByAction(actionContext.HttpContext, nameof(MessageController.Delete), "Message", new { id = messageDto.Id }),
                    "deleteMessage",
                    "DELETE"));
            }
        }
    }
}
