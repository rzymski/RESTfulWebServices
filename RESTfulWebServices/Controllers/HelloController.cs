using Microsoft.AspNetCore.Mvc;
using DB.Entities;
using DB.Repositories;
using DB.Repositories.Interfaces;

namespace RESTfulWebServices.Controllers
{
    [ApiController]
    [Route("rest/api/[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly ILogger<HelloController> logger;
        private readonly IMessageRepository messageRepository;

        public HelloController(ILogger<HelloController> logger, IMessageRepository messageRepository)
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
            var messages = messageRepository.GetAll();
            return Ok(messages);
        }

        [HttpGet("messages/xml")]
        public ActionResult<List<Message>> MessagesAsXml()
        {
            var messages = messageRepository.GetAll();
            var result = new ObjectResult(messages);
            result.ContentTypes.Add("application/xml");
            return result;
        }
    }
}
