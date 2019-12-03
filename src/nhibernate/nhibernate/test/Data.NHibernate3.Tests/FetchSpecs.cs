using System.Linq;
using Cobweb.Data.NHibernate.Providers;
using Cobweb.Data.NHibernate.Tests.Entities;
using Cobweb.Data.NHibernate.Tests.Util;
using FluentAssertions;
using NHibernate;
using Xunit;

namespace Cobweb.Data.NHibernate.Tests {
    [Collection("FetchingProvider")]
    public class FetchSpecs : SqLiteNHibernateTest {
        public FetchSpecs(SqLiteNHibernateFixture fixture) : base(fixture) {
            using (var tx = Session.BeginTransaction()) {
                var person = new PersonEntity();

                CarEntity car;
                car = new CarEntity{Owner = person};
                person.Cars.Add(car);
                car = new CarEntity{Owner = person};
                person.Cars.Add(car);

                PetEntity pet;
                pet = new PetEntity {Owner = person};
                person.Pets.Add(pet);
                pet = new PetEntity {Owner = person};
                person.Pets.Add(pet);

                var employer = new EmployerEntity();
                Session.Save(employer);

                employer.Employees.Add(person);
                person.Employer = employer;
             
                Session.Save(person);
                tx.Commit();
            }

            Session.Clear();
            SessionFactory.Statistics.Clear();
        }

        [Fact]
        public void ItShouldHaveOnePersonFromSetup() {
            Session.Query<PersonEntity>().Count().Should().Be(1);
        }

        [Fact]
        public void ItShouldHaveOneEmployerFromSetup() {
            Session.Query<EmployerEntity>().Count().Should().Be(1);
        }

        [Fact]
        public void ItShouldHaveTwoCarsFromSetup() {
            Session.Query<CarEntity>().Count().Should().Be(2);
        }

        [Fact]
        public void ItShouldHaveTwoPetsFromSetup() {
            Session.Query<PetEntity>().Count().Should().Be(2);
        }

        [Fact]
        public void ItShouldNotInitializeReferenceEntitiesWhenNotFetched() {
            var root = Session.Query<CarEntity>().FirstOrDefault();
            root.Should().NotBeNull();

            NHibernateUtil.IsInitialized(root.Owner).Should().BeFalse("Owner should not be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "nothing should have been lazy-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }

        [Fact]
        public void ItShouldLazyLoadReferenceEntitiesWhenNotFetched() {
            var root = Session.Query<CarEntity>().FirstOrDefault();
            root.Should().NotBeNull();

            var name = root.Owner.Name;

            NHibernateUtil.IsInitialized(root.Owner).Should().BeTrue("Owner should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(1, "the Owner should have been lazy-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(2, "there should have been two queries");
        }

        [Fact]
        public void ItShouldInitializeReferenceEntitiesWhenFetched() {
            var root = EagerFetch.Fetch(Session.Query<CarEntity>(), personEntity => personEntity.Owner).FirstOrDefault();
            root.Should().NotBeNull();

            NHibernateUtil.IsInitialized(root.Owner).Should().BeTrue("Owner should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "nothing should have been lazy-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }


        [Fact]
        public void ItShouldEagerLoadReferenceEntitiesWhenFetched() {
            var root = EagerFetch.Fetch(Session.Query<CarEntity>(), personEntity => personEntity.Owner).FirstOrDefault();
            root.Should().NotBeNull();

            var name = root.Owner.Name;

            NHibernateUtil.IsInitialized(root.Owner).Should().BeTrue("Owner should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "the Owner should have been eager-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }
                       
        [Fact]
        public void ItShouldInitializeReferenceGrandchildEntitiesWhenFetched() {
            var root = EagerFetch.ThenFetch(EagerFetch.Fetch(Session.Query<CarEntity>(), personEntity => personEntity.Owner), childEntity => childEntity.Employer)
                              .FirstOrDefault();
            root.Should().NotBeNull();

            NHibernateUtil.IsInitialized(root.Owner).Should().BeTrue("Owner should be initialized");
            NHibernateUtil.IsInitialized(root.Owner.Employer).Should().BeTrue("Owner.Employer should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "nothing should have been lazy-loaded");
            SessionFactory.Statistics.CollectionFetchCount.Should()
                          .Be(0, "the child collections should have been eager-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }
        
        [Fact]
        public void ItShouldInitializeReferenceGrandchildCollectionsWhenFetched() {
            var root = EagerFetch.ThenFetchMany(EagerFetch.Fetch(Session.Query<CarEntity>(), personEntity => personEntity.Owner), childEntity => childEntity.Pets)
                              .FirstOrDefault();
            root.Should().NotBeNull();

            NHibernateUtil.IsInitialized(root.Owner).Should().BeTrue("Owner should be initialized");
            NHibernateUtil.IsInitialized(root.Owner.Pets).Should().BeTrue("Owner.Pets should be initialized");
            SessionFactory.Statistics.EntityFetchCount.Should().Be(0, "northing should have been lazy-loaded");
            SessionFactory.Statistics.CollectionFetchCount.Should()
                          .Be(0, "the children collections should have been eager-loaded");
            SessionFactory.Statistics.PrepareStatementCount.Should().Be(1, "there should have been one query");
        }
    }
}
