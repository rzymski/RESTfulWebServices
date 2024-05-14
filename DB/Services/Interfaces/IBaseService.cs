using DB.Dto.Message;
using DB.Entities;

namespace DB.Services.Interfaces
{
    public interface IBaseService<T, TDto> where T : BaseEntity where TDto : BaseDto
    {
        TDto GetByIdDtoObject(int id);
        List<TDto> GetAllDtoList();
        bool Delete(int id);
        int Add(TDto item);
        bool Update(int id, TDto item);
    }
}
