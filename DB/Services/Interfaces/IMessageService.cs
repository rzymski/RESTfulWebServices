using DB.Dto.Message;
using DB.Entities;

namespace DB.Services.Interfaces
{
    public interface IMessageService : IBaseService<Message, MessageDto, MessageAddDto>
    {
        List<MessageDto> GetByParameters(string? author, string? content, int? priority);
    }
}
