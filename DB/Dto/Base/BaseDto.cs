using DB.Dto.Link;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace DB.Dto.Message
{
    public class BaseDto
    {
        [Required]
        public int Id { get; set; }
        public List<LinkDto> Links { get; set; } = new List<LinkDto>();

        public BaseDto(){}
        public BaseDto(int id)
        {
            Id = id;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Type type = GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (PropertyInfo property in properties)
            {
                sb.Append($"{property.Name}: {property.GetValue(this)}, ");
            }
            return sb.ToString().TrimEnd(',', ' ');
        }
    }
}
