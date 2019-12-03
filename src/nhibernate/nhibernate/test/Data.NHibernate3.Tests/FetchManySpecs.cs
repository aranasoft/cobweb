using System.Linq;
using Cobweb.Data.NHibernate.Providers;
using Cobweb.Data.NHibernate.Tests.Entities;
using Cobweb.Data.NHibernate.Tests.Util;
using FluentAssertions;
using NHibernate;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests {
    [Collection("FetchingProvider")]
    public class FetchManySpecs : SqLiteNHibernateTest {
        public FetchManySpecs(SqLiteNHibernateFixture fixture) : base(fixture) {
            using (var tx = Session.BeginTransaction()) {
                EmployerEntity employer;
                PersonEntity person;
                RepresentativeEntity representative;
                CarEntity car;

                employer = new EmployerEntity();
                Session.Save(employer);

                representative = new RepresentativeEntity();
                Session.Save(representative);
                person = new PersonEntity {Employer = employer, Representative = representative};
                representative.Constituents.Add(person);
                employer.Employees.Add(person);

                car = new CarEntity {Owner = person};
                person.Cars.Add(car);
                car = new CarEntity {Owner = person};
                person.Cars.Add(car);

                Session.Save(person);


                representative = new RepresentativeEntity();
                Session.Save(representative);
                person = new PersonEntity {Employer = employer, Representative = representative};
                representative.Constituents.Add(person);
                employer.Employees.Add(person);

                car = new CarEntity {Owner = person};
                person.Cars.Add(car);
                car = new CarEntity {Owner = person};
                person.Cars.Add(car);

                Session.Save(person);

                tx.Commit();
            }

            Session.Clear();
            SessionFactory.Statistics.Clear();
        }

        [Fact]
        public void ItShouldHaveTwoPersonsFromSetup() {
            Session.Query<PersonEntity>().Count().Should().Be(2);
        }

        [Fact]
        public void ItShouldHaveTwoRepresentativesFromSetup() {
            Session.Query<RepresentativeEntity>().Count().Should().Be(2);
            Session.Query<RepresentativeEntity>().First().Constituents.Count().Should().Be(1);
        }

        [Fact]
        public void ItShouldHaveFourCarsFromSetup() {
            Session.Query<CarEntity>().Count().Should().Be(4);
        }

        [Fact]
        public void ItShouldHaveOneEmployerFromSetup() {
            Session.Query<EmployerEntity>().Count().Should().Be(1);
            Session.Query<EmployerEntity>().First().Employees.Count().Should().Be(2);
        }

        [Fact]
        public void ItShouldNotInitializeReferenceCollectionsWhenNotFetched() {
            var root = Session.Query<EmployerEntity>().FirstOrDefault();
            root.Should().NotBeNull();

            NHibernateUtil.IsInitialized(root.Employees).Should().BeFalse("Employees should not be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "nothing should have been lazy-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }

        [Fact]
        public void ItShouldLazyLoadReferenceCollectionsWhenNotFetched() {
            var root = Session.Query<EmployerEntity>().FirstOrDefault();
            root.Should().NotBeNull();

            var name = root.Employees.First().Name;

            NHibernateUtil.IsInitialized(root.Employees).Should().BeTrue("Employees should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should()
                          .Be(0, "the Employees should have been loaded through the collection");
            SessionFactory.Statistics.CollectionFetchCount.Should()
                          .Be(1, "the Employees collection should have been lazy-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(2, "there should have been two queries");
        }

        [Fact]
        public void ItShouldInitializeReferenceCollectionsWhenFetched() {
            var employerEntities = Session.Query<EmployerEntity>();
            var fetchRequest = EagerFetch.FetchMany(employerEntities, personEntity => personEntity.Employees);
            var root = fetchRequest.FirstOrDefault();
            root.Should().NotBeNull();

            NHibernateUtil.IsInitialized(root.Employees).Should().BeTrue("Employees should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "nothing should have been lazy-loaded");
            SessionFactory.Statistics.CollectionFetchCount.Should()
                          .Be(0, "the Employees collection should have been eager-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }

        [Fact]
        public void ItShouldEagerLoadReferenceCollectionsWhenFetched() {
            var root = EagerFetch.FetchMany(Session.Query<EmployerEntity>(), employerEntity => employerEntity.Employees).FirstOrDefault();
            root.Should().NotBeNull();

            var name = root.Employees.First().Name;

            NHibernateUtil.IsInitialized(root.Employees).Should().BeTrue("Employees should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "the Employees should have been eager-loaded");
            SessionFactory.Statistics.CollectionFetchCount.Should()
                          .Be(0, "the Employees collection should have been eager-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }
               
        [Fact]
        public void ItShouldInitializeReferenceGrandchildEntitiesWhenFetched() {
            var root = EagerFetch.ThenFetch(EagerFetch.FetchMany(Session.Query<EmployerEntity>(), employerEntity => employerEntity.Employees), personEntity => personEntity.Employer)
                              .FirstOrDefault();
            root.Should().NotBeNull();

            NHibernateUtil.IsInitialized(root.Employees).Should().BeTrue("Employees should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "nothing should have been lazy-loaded");
            SessionFactory.Statistics.CollectionFetchCount.Should()
                          .Be(0, "the child collections should have been eager-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }
        
        [Fact]
        public void ItShouldInitializeReferenceGrandchildCollectionsWhenFetched() {
            var root = EagerFetch.ThenFetchMany(EagerFetch.FetchMany(Session.Query<EmployerEntity>(), employerEntity => employerEntity.Employees), personEntity => personEntity.Cars)
                               .FirstOrDefault();
            root.Should().NotBeNull();

            NHibernateUtil.IsInitialized(root.Employees).Should().BeTrue("Employees should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "nothing should have been lazy-loaded");
            SessionFactory.Statistics.CollectionFetchCount.Should()
                          .Be(0, "the child collections should have been eager-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }
    }
}

