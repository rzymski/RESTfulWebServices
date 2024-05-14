using DB.Dto.Message;
using DB.Entities;
using DB.Repositories.Interfaces;
using DB.Services.Interfaces;

namespace DB.Services
{
    public class MessageService : BaseService<Message, MessageDto>, IMessageService
    {

        public MessageService(IMessageRepository repository) : base(repository) {}

        protected override MessageDto MapToDto(Message entity)
        {
            return new MessageDto
            {
                Id = entity.Id,
                Author = entity.Author,
                Content = entity.Content,
                Created = entity.Created
            };
        }

        // Nadpisz metodę MapToEntity dla klasy MessageDto
        protected override Message MapToEntity(MessageDto dto, Message entity)
        {
            if (entity == null)
                entity = new Message(); // Utwórz nową instancję klasy Message, jeśli nie została dostarczona

            entity.Id = dto.Id;
            entity.Author = dto.Author;
            entity.Content = dto.Content;
            entity.Created = dto.Created;

            return entity;
        }
    }
}
