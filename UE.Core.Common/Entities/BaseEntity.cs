using System.ComponentModel.DataAnnotations;

namespace UE.Core.Common.Entities;

/// <summary>
/// Represents the base entity with common properties for PostgreSQL.
/// </summary>
public class BaseEntity
{
    /// <summary>
    /// Gets or sets the unique identifier for the entity.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the entity was created.
    /// </summary>
    [Required]
    public DateTime CreatedTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who created the entity.
    /// </summary>
    [Required]
    public int CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the entity was last modified.
    /// </summary>
    public DateTime? ModifiedTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who last modified the entity.
    /// </summary>
    public int? ModifiedBy { get; set; }

    /// <summary>
    /// Gets or sets the timestamp when the entity was deleted.
    /// </summary>
    public DateTime? DeletedTimestamp { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the user who deleted the entity.
    /// </summary>
    public int? DeletedBy { get; set; }

    /// <summary>
    /// Gets or sets the record is deleted when the entity was deleted.
    /// </summary>
    public bool IsDeleted { get; set; } = false;
}
