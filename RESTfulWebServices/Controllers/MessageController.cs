using DB.Entities;
using DB.Dto.Message;
using DB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB;
using Microsoft.AspNetCore.Http.Extensions;

namespace RESTfulWebServices.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> logger;
        private readonly IMessageService messageService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public MessageController(ILogger<MessageController> logger, IMessageService messageService, IHttpContextAccessor httpContextAccessor)
        {
            this.logger = logger;
            this.messageService = messageService;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("{id}")]
        public IActionResult GetOne([FromRoute] int id)
        {
            return Ok(messageService.GetByIdDtoObject(id));
        }

        [HttpGet]
        public ActionResult<List<MessageDto>> GetList()
        {
            return Ok(messageService.GetAllDtoList());
        }

        [HttpPost]
        public ActionResult Add([FromBody] MessageAddDto message)
        {
            var result = messageService.Add(message);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] MessageAddDto message)
        {
            var result = messageService.Update(id, message);
            if (result)
                return NoContent();
            return NotFound();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id) 
        {
            var result = messageService.Delete(id);
            if (result)
                return NoContent();
            return NotFound();
        }

        [HttpGet]
        public ActionResult<string> GetByValues([FromQuery] string? author, [FromQuery] string? content, [FromQuery] int? priority)
        {
            return Ok(messageService.GetByParameters(author, content, priority));
        }

        [HttpGet]
        public IActionResult GetHeaderValues([FromHeader(Name = "username")] string username, [FromHeader(Name = "password")] string password)
        {
            return Ok($"username Header Value: {username} i password Header Value: {password}");
        }

        [HttpGet]
        public string GetAbsolutePath()
        {
            var uri = httpContextAccessor.HttpContext.Request.GetEncodedUrl();
            return uri;
        }

        [HttpGet]
        public string GetUserAgent()
        {
            var userAgent = httpContextAccessor.HttpContext.Request.Headers.UserAgent;
            return userAgent;
        }

        //[HttpGet]
        //public ActionResult<string> GetInfo()
        //{
        //    // Przykładowe uzyskiwanie parametrów zapytania
        //    string queryParamValue = HttpContext.Request.Query["paramName"];

        //    // Przykładowe uzyskiwanie nagłówka
        //    string headerValue = HttpContext.Request.Headers["HeaderName"];

        //    // Przykładowe uzyskiwanie innych informacji kontekstowych
        //    string absolutePath = HttpContext.Request.Path;

        //    return Ok($"QueryParam: {queryParamValue}, HeaderValue: {headerValue}, AbsolutePath: {absolutePath}");
        //}
    }
}
