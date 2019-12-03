using System.Collections.Generic;
using System.Reflection;
using FluentNHibernate;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Cfg;
using Cobweb.Data.NHibernate.Tests.Entities;

namespace Cobweb.Data.NHibernate.Tests.Util {
    public class SqLiteNHibernateFixture {
        public Configuration SessionConfiguration { get; private set; }

        public SqLiteNHibernateFixture() {
            var connectionConfig = SQLiteConfiguration.Standard.InMemory().QuerySubstitutions("true=1;false=0");
            FluentNHibernate.Cfg.Fluently.Configure()
                            .Mappings(
                                m =>
                                    m.AutoMappings.Add(
                                        AutoMap.Source(GetEntityTypeSources())
                                               .Conventions.Setup(ConfigureConventions)
                                               .Override<PersonEntity>(map => {
                                                   map.References(entity => entity.Representative).Column("RepresentativeId");
                                                   map.References(entity => entity.Employer).Column("EmployerId");
                                                   map.HasMany(entity => entity.Cars).KeyColumn("OwnerId");
                                                   map.HasMany(entity => entity.Pets).KeyColumn("OwnerId");
                                               })
                                               .Override<EmployerEntity>(map => map.HasMany(entity => entity.Employees).KeyColumn("EmployerId"))
                                               .Override<RepresentativeEntity>(map => map.HasMany(entity => entity.Constituents).KeyColumn("RepresentativeId"))
                                               .Override<PetEntity>(map => map.References(entity => entity.Owner).Column("OwnerId"))
                                               .Override<CarEntity>(map => map.References(entity => entity.Owner).Column("OwnerId"))
                                               )
                                     )
                            .ExposeConfiguration(config => { SessionConfiguration = config; })
                            .Database(connectionConfig)
                            .BuildConfiguration();
        }

        private static void ConfigureConventions(IConventionFinder conventions) {
            conventions.Add(DefaultCascade.All());
            conventions.Add(DefaultLazy.Always());
            conventions.Add(ConventionBuilder.Id.Always(convention => convention.GeneratedBy.GuidComb()));
            conventions.Add(ConventionBuilder.HasMany.Always(convention => convention.Inverse()));
            conventions.Add(ConventionBuilder.HasMany.Always(convention => convention.Cascade.AllDeleteOrphan()));
        }

        private ITypeSource GetEntityTypeSources() {
            var assemblies = new List<Assembly> {GetType().Assembly};
            return new EntityTypeSource(assemblies);
        }
    }
}
