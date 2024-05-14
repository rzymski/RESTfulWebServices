using DB.Dto.Message;
using DB.Entities;

namespace DB.Services.Interfaces
{
    public interface IMessageService : IBaseService<Message, MessageDto>
    {
    }
}
