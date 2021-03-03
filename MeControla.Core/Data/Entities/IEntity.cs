using System;

namespace MeControla.Core.Data.Entities
{
    public interface IEntity
    {
        long Id { get; }
        Guid Uuid { get; set; }
    }
}