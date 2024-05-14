using Microsoft.AspNetCore.Mvc;
using DB.Entities;
using DB.Services;
using DB.Services.Interfaces;

namespace RESTfulWebServices.Controllers
{
    [ApiController]
    [Route("rest/api/[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly ILogger<HelloController> logger;
        private readonly IMessageService messageRepository;

        public HelloController(ILogger<HelloController> logger, IMessageService messageRepository)
        {
            this.logger = logger;
            this.messageRepository = messageRepository;

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

        [HttpGet("echo/{message}")]
        public string Echo(string message)
        {
            return $"Witaj echo: {message}";
        }

        [HttpGet("messages/json")]
        public ActionResult<List<Message>> Messages()
        {
            var messages = messageRepository.GetAllDtoList();
            return Ok(messages);
        }

        [HttpGet("messages/xml")]
        public ActionResult<List<Message>> MessagesAsXml()
        {
            var messages = messageRepository.GetAllDtoList();
            var result = new ObjectResult(messages);
            result.ContentTypes.Add("application/xml");
            return result;
        }
    }
}
