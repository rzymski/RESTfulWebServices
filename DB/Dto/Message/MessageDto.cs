using DB.Dto.Link;

namespace DB.Dto.Message
{
    public class MessageDto : BaseDto
    {
        public string? Author { get; set; }
        public string? Content { get; set; }
        public int Priority { get; set; }
        public DateTime Created { get; set; }

        public MessageDto() { }
        public MessageDto(int id, string? author, string? content, DateTime created, int priority) : base(id)
        {
            Author = author;
            Content = content;
            Created = created;
            Priority = priority;
        }

        public override string ToString()
        {
            return $"MessageDto {base.ToString()}";
        }
    }
}
