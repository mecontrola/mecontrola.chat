using System;

namespace MeControla.Core.Data.Entities
{
    public interface IModificationDateTimeEntity : IEntity
    {
        DateTime CreatedAt { get; }
        DateTime? UpdatedAt { get; }
    }
}