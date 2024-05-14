using DB.Entities;
using DB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RESTfulWebServices.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<HelloController> logger;
        private readonly IMessageService messageRepository;

        public MessageController(ILogger<HelloController> logger, IMessageService messageRepository)
        {
            this.logger = logger;
            this.messageRepository = messageRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            return Ok(messageRepository.GetByIdDtoObject(id));
        }

        [HttpGet]
        public ActionResult<List<Message>> GetList()
        {
            return Ok(messageRepository.GetAllDtoList());
        }
    }
}
