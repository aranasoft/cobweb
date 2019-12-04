using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cobweb.Data;
using FluentNHibernate;
using FluentNHibernate.Diagnostics;

namespace Aranasoft.Cobweb.NHibernate {
    public class EntityTypeSource : ITypeSource {
        private readonly IEnumerable<Type> _sources;

        public EntityTypeSource(IEnumerable<Assembly> assemblies) {
            _sources = EntityManager.Current.GetEntityTypes(assemblies.AsQueryable());
        }

        public IEnumerable<Type> GetTypes() {
            return _sources.ToArray();
        }

        public void LogSource(IDiagnosticLogger logger) {
            logger.LoadedFluentMappingsFromSource(this);
        }

        public string GetIdentifier() {
            return "EntitySource";
        }
    }
}