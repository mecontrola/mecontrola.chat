using MeControla.Core.Data.Entities;
using System;

namespace MeControla.Chat.Core.Builders
{
    public class BaseBuilder<TBuild, TObject>
        where TBuild : class, new()
        where TObject : IEntity, new()
    {
        protected TObject obj;

        protected BaseBuilder()
        {
            obj = new TObject
            {
                Uuid = Guid.NewGuid()
            };
        }

        public static TBuild GetInstance()
            => new TBuild();

        public TObject ToBuild()
            => obj;
    }
}