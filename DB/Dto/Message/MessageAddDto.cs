using System.ComponentModel.DataAnnotations;

namespace DB.Dto.Message
{
    public class MessageAddDto
    {
        [Required]
        [StringLength(50)]
        public string? Author { get; set; }
        [Required]
        public string? Content { get; set; }
        public int Priority { get; set; }
        public DateTime Created { get; set; }

        public MessageAddDto() { }
        public MessageAddDto(string? author, string? content, DateTime created, int priority)
        {
            Author = author;
            Content = content;
            Created = created;
            Priority = priority;
        }

        public override string ToString()
        {
            return $"MessageAddDto {base.ToString()}";
        }
    }
}
