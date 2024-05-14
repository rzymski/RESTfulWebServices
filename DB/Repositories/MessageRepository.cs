using DB.Entities;
using DB.Repositories.Interfaces;

namespace DB.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(MyDBContext dbContext) : base(dbContext)
        {
        }
    }
}
