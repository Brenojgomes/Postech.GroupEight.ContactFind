using Postech.GroupEight.ContactFind.Core.Interfaces.Entities;

namespace Postech.GroupEight.ContactFind.Core.Entities
{
    /// <summary>
    /// Represents the base entity class.
    /// </summary>
    public class EntityBase : IEntity
    {
        /// <summary>
        /// The unique identifier of the entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The creation date and time of the entity.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The last modification date and time of the entity.
        /// </summary>
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// A value indicating whether the entity is active.
        /// </summary>
        public bool Active { get; set; }
    }
}
