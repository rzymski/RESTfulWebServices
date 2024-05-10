using Microsoft.AspNetCore.Mvc;

namespace RESTfulWebServices.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly ILogger<HelloController> _logger;

        public HelloController(ILogger<HelloController> logger)
        {
            _logger = logger;
        }

        [HttpGet("")]
        public string Hello()
        {
            return "Witaj hello";
        }

        [HttpGet("echo")]
        public string Echo()
        {
            return "Witaj echo";
        }

        [HttpGet("echo2/{message}")]
        public string Echo2(string message)
        {
            return $"Witaj echo: {message}";
        }

        [HttpGet("messages")]
        public ActionResult<List<Message>> Messages()
        {
            var messages = Message.getSampleMessages();
            return Ok(messages);
        }

        [HttpGet("messages/xml")]
        public ActionResult<List<Message>> MessagesAsXml()
        {
            var messages = Message.getSampleMessages();
            var result = new ObjectResult(messages);
            result.ContentTypes.Add("application/xml");
            return result;
        }
    }
}
