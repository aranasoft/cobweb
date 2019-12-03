using System;
using System.Linq;
using System.Reflection;
using Cobweb.Extentions;

namespace Cobweb.Data {
    /// <summary>
    ///     Static methods to gather types that implement Entity.
    /// </summary>
    public class EntityManager {
        private static EntityManager _current;

        public static EntityManager Current {
            get { return _current ?? (_current = new EntityManager()); }
        }

        public IQueryable<Type> GetEntityTypes(IQueryable<Assembly> assemblies) {
            return assemblies.SelectMany(assembly => assembly.GetTypes().Where(IsEntity));
        }

        public bool IsEntity(Type type) {
            return type.IsClass && !type.IsAbstract && type.IsAssignableToGeneric(typeof(IEntity<>));
        }
    }
}
