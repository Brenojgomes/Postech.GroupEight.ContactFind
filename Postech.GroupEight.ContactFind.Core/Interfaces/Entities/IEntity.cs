namespace Postech.GroupEight.ContactFind.Core.Interfaces.Entities
{
    /// <summary>
    /// Represents an entity.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Identifier of the entity.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Creation date and time of the entity.
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Last modification date and time of the entity.
        /// </summary>
        DateTime? ModifiedAt { get; }
    }
}
