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
            var result = messageService.GetByIdDtoObject(id);
            if (result == null)
                return NotFound(new { message = "Nie znaleziono rekordu o takim id." });
            return Ok(result);
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
        public string? GetAbsolutePath()
        {
            var uri = httpContextAccessor.HttpContext?.Request.GetEncodedUrl();
            return uri;
        }

        [HttpGet]
        public string? GetUserAgent()
        {
            var userAgent = httpContextAccessor.HttpContext?.Request.Headers.UserAgent;
            return userAgent;
        }

        // nie działa wraz z HateoasMiddleware
        [HttpGet("{*path}")]
        public IActionResult GetMatrixParams(string path)
        {
            var matrixParams = ParseMatrixParams(path);
            return Ok(matrixParams);
        }

        private Dictionary<string, string> ParseMatrixParams(string path)
        {
            var matrixParams = new Dictionary<string, string>();
            // Sprawdź, czy ścieżka zawiera parametry macierzyste
            var parts = path.Split(';');
            foreach (var part in parts.Skip(1)) // Pomijamy pierwszy segment ścieżki, który jest samą nazwą kontrolera
            {
                var keyValue = part.Split('=');
                if (keyValue.Length == 2)
                {
                    var key = keyValue[0];
                    var value = keyValue[1];
                    matrixParams[key] = value;
                }
            }
            return matrixParams;
        }
    }
}
