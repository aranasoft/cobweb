using System;
using System.Linq;
using System.Reflection;
using Aranasoft.Cobweb.Extentions;

namespace Aranasoft.Cobweb.Data {
    /// <summary>
    /// Static methods to gather types that implement Entity.
    /// </summary>
    public static class EntityManager {
        public static IQueryable<Type> GetEntityTypes(IQueryable<Assembly> assemblies) {
            return assemblies.SelectMany(assembly => assembly.GetTypes().Where(IsEntity));
        }

        public static bool IsEntity(Type type) {
            return type.IsClass && !type.IsAbstract && type.IsAssignableToGeneric(typeof (Entity<,>));
        }
    }
}