using System;

namespace Cobweb.Data {
    public interface IEntity<TEntity>
        where TEntity : IEntity<TEntity>, IEquatable<TEntity> {}

    /// <summary>
    ///     Base class for objects to be persisted in a relational database. Equates on reference or non-default identifier
    ///     value
    /// </summary>
    /// <typeparam name="TEntity">Derived entity type</typeparam>
    /// <typeparam name="TIdentifier">Itentifier-property type, such as Int32 or Guid</typeparam>
    public abstract class Entity<TEntity, TIdentifier> : IEntity<TEntity>
        where TEntity : Entity<TEntity, TIdentifier>, IEquatable<TEntity>
        where TIdentifier : IComparable, IComparable<TIdentifier>, IEquatable<TIdentifier> {
        public virtual TIdentifier Id { get; set; }

        public override int GetHashCode() {
            unchecked {
                return GetType().GetHashCode() * 29 * Id.GetHashCode();
            }
        }

        public virtual bool Equals(TEntity other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }

            if (ReferenceEquals(this, other)) {
                return true;
            }

            if (Id == null || Id.Equals(default(TIdentifier))) {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj) {
            var other = obj as TEntity;
            return other != null && Equals(other);
        }

        public static bool operator ==(Entity<TEntity, TIdentifier> entity1, Entity<TEntity, TIdentifier> entity2) {
            return Equals(entity1, null) ? Equals(entity2, null) : entity1.Equals(entity2);
        }

        public static bool operator !=(Entity<TEntity, TIdentifier> entity1, Entity<TEntity, TIdentifier> entity2) {
            return !(entity1 == entity2);
        }
    }
}
