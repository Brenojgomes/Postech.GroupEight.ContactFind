using Postech.GroupEight.ContactFind.Core.Interfaces.Entities;

namespace Postech.GroupEight.ContactFind.Core.Entities
{
    public class EntityBase : IEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedAt { get; set; }
        public bool Active { get; set; }
    }
}
