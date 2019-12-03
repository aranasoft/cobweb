using System;
using System.Collections.Generic;

namespace Cobweb.Data.NHibernate.Tests.Entities {
    public class CarEntity : Entity<CarEntity, Guid>, IEquatable<CarEntity> {
        public virtual string Name { get; set; }
        public virtual PersonEntity Owner { get; set; }
    }
    public class PetEntity : Entity<PetEntity, Guid>, IEquatable<PetEntity> {
        public virtual string Name { get; set; }
        public virtual PersonEntity Owner { get; set; }
    }

    public class PersonEntity : Entity<PersonEntity, Guid>, IEquatable<PersonEntity> {
        private ISet<PetEntity> _pets;
        private ISet<CarEntity> _cars;
        public virtual string Name { get; set; }
        public virtual EmployerEntity Employer { get; set; }
        public virtual RepresentativeEntity Representative { get; set; }

        public virtual ISet<CarEntity> Cars {
            get { return _cars ?? (_cars = new HashSet<CarEntity>()); }
            set { _cars = value; }
        }

        public virtual ISet<PetEntity> Pets {
            get { return _pets ?? (_pets = new HashSet<PetEntity>()); }
            set { _pets = value; }
        }
    }

    public class EmployerEntity : Entity<EmployerEntity, Guid>, IEquatable<EmployerEntity> {
        private ISet<PersonEntity> _employees;
        public virtual string Name { get; set; }

        public virtual ISet<PersonEntity> Employees {
            get { return _employees ?? (_employees = new HashSet<PersonEntity>()); }
            set { _employees = value; }
        }
    }
    
    public class RepresentativeEntity : Entity<RepresentativeEntity, Guid>, IEquatable<RepresentativeEntity> {
        private ISet<PersonEntity> _constituents;
        public virtual string Name { get; set; }

        public virtual ISet<PersonEntity> Constituents {
            get { return _constituents ?? (_constituents = new HashSet<PersonEntity>()); }
            set { _constituents = value; }
        }
    }
}