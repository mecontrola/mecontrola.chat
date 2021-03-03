namespace MeControla.Core.Data.Entities
{
    public interface IManyEntity<TRoot, TTarget>
        where TRoot : IEntity
        where TTarget : IEntity
    {
        long RootId { get; set; }
        long TargetId { get; set; }
        TRoot Root { get; set; }
        TTarget Target { get; set; }
    }
}