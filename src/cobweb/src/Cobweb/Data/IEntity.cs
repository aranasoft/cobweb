using System;

namespace Aranasoft.Cobweb.Data {
    public interface IEntity<TEntity>
        where TEntity : IEntity<TEntity>, IEquatable<TEntity> {}
}