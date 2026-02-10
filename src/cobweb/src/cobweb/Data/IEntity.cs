using System;

namespace Aranasoft.Cobweb.Data;
/// <summary>
///     Marker interface for domain entities that support identity-based equality.
/// </summary>
/// <typeparam name="TEntity">The derived entity type.</typeparam>
public interface IEntity<TEntity>
    where TEntity : IEntity<TEntity>, IEquatable<TEntity> {}
