using System;
using System.Linq;
using System.Reflection;
using Aranasoft.Cobweb.Extensions;

namespace Aranasoft.Cobweb.Data;
/// <summary>
///     Static methods to gather types that implement Entity.
/// </summary>
public class EntityManager {
    private static EntityManager _current;

    /// <summary>
    ///     Gets the singleton instance of <see cref="EntityManager" />.
    /// </summary>
    public static EntityManager Current {
        get { return _current ?? (_current = new EntityManager()); }
    }

    /// <summary>
    ///     Gets all concrete types implementing <see cref="IEntity{TEntity}" /> from the specified assemblies.
    /// </summary>
    /// <param name="assemblies">The assemblies to scan for entity types.</param>
    /// <returns>A queryable sequence of entity types.</returns>
    public IQueryable<Type> GetEntityTypes(IQueryable<Assembly> assemblies) {
        return assemblies.SelectMany(assembly => assembly.GetTypes().Where(IsEntity));
    }

    /// <summary>
    ///     Determines whether the specified type is a concrete entity implementing <see cref="IEntity{TEntity}" />.
    /// </summary>
    /// <param name="type">The type to evaluate.</param>
    /// <returns>true if the type is a non-abstract class implementing <see cref="IEntity{TEntity}" />; otherwise, false.</returns>
    public bool IsEntity(Type type) {
        return type.IsClass && !type.IsAbstract && type.IsAssignableToGeneric(typeof(IEntity<>));
    }
}
