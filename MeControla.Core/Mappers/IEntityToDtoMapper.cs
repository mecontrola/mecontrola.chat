using MeControla.Core.Data.Dtos;
using MeControla.Core.Data.Entities;

namespace MeControla.Core.Mappers
{
    public interface IEntityToDtoMapper<TParam, TResult> : IMapper<TParam, TResult>
        where TParam : class, IEntity
        where TResult : class, IDto
    { }
}