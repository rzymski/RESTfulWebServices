using DB.Dto.Message;
using DB.Entities;
using DB.Repositories.Interfaces;
using DB.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DB.Services
{
    public class MessageService : BaseService<Message, MessageDto, MessageAddDto>, IMessageService
    {
        public MessageService(IMessageRepository repository) : base(repository) {}

        protected override MessageDto MapToDto(Message entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            return new MessageDto
            {
                Id = entity.Id,
                Author = entity.Author,
                Content = entity.Content,
                Priority = entity.Priority,
                Created = entity.Created
            };
        }

        // Nadpisz metodę MapToEntity dla klasy MessageDto
        protected override Message MapAddEditDtoToEntity(MessageAddDto dto, Message entity)
        {
            if (entity == null)
                entity = new Message(); // Utwórz nową instancję klasy Message, jeśli nie została dostarczona

            entity.Author = dto.Author;
            entity.Content = dto.Content;
            entity.Priority = dto.Priority;
            entity.Created = dto.Created;
            return entity;
        }

        public List<MessageDto> GetByParameters(string? author, string? content, int? priority)
        {
            var results =  repository.GetAll()
                                     .Where(p => (string.IsNullOrEmpty(author) || p.Author == author) && 
                                                 (string.IsNullOrEmpty(content) || p.Content == content) && 
                                                 p.Priority == priority).Select(MapToDto).ToList();
            return results;
        }
    }
}
