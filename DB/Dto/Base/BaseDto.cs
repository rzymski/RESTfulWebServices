using System.ComponentModel.DataAnnotations;

namespace DB.Dto.Message
{
    public class BaseDto
    {
        [Required]
        public int Id { get; set; }

        public BaseDto(){}
        public BaseDto(int id)
        {
            Id = id;
        }
    }
}
