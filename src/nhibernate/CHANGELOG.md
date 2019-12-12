<a name="1.50.3"></a>
# 1.50.3 (2019-12-11)

### Code Refactoring

* **namespaces:** revise namespaces to Aranasoft.Cobweb ([e3f4775](https://github.com/aranasoft/cobweb/commit/e3f4775))

### BREAKING CHANGES

* **namespaces:** the root namespace has been changed from `Cobweb.Data.NHibernate` and `Cobweb.Testing.NHibernate` to `Aranasoft.Cobweb.NHibernate` and `Aranasoft.Cobweb.NHibernate.Validation`, respectively. For example, the `EntityTypeSource` class is now located within `Aranasoft.Cobweb.NHibernate`. ([e3f4775](https://github.com/aranasoft/cobweb/commit/e3f4775))



<a name="1.50.2"></a>
## 1.50.2 (2018-06-17)


### Features

* **query_opts:** add support for testability under WithOptions query options



<a name="1.50.1"></a>
## 1.50.1 (2018-06-15)


### Features

* **nhibernate:** add support for nhibernate v5



<a name="1.40.4"></a>
# 1.40.4 (2019-12-11)

### Code Refactoring

* **namespaces:** revise namespaces to Aranasoft.Cobweb ([e3f4775](https://github.com/aranasoft/cobweb/commit/e3f4775))

### BREAKING CHANGES

* **namespaces:** the root namespace has been changed from `Cobweb.Data.NHibernate` and `Cobweb.Testing.NHibernate` to `Aranasoft.Cobweb.NHibernate` and `Aranasoft.Cobweb.NHibernate.Validation`, respectively. For example, the `EntityTypeSource` class is now located within `Aranasoft.Cobweb.NHibernate`. ([e3f4775](https://github.com/aranasoft/cobweb/commit/e3f4775))



<a name="1.40.3"></a>
## 1.40.3 (2018-06-15)


### Bug Fixes

* **fetch:** resolve invalid cast exception on ThenMany within FakeFetchingProvider


### Features

* **config:** add EntityTypeSource for automapping



<a name="1.40.2"></a>
# 1.40.2 (2017-07-20)


### Bug Fixes

* **fetch:** support ThenFetch / ThenFetchMany on Eager Load ([23e60a8](https://github.com/aranasoft/cobweb/commit/23e60a8))


### Features

* **cache:** add NHibernate Caching Wrapper to support testability ([d2d571e](https://github.com/aranasoft/cobweb/commit/d2d571e))



<a name="1.40.1"></a>
# 1.40.1 (2015-01-17)


### Features

* **nhibernate:** add support for NHibernate v4



<a name="1.33.6"></a>
# 1.33.6 (2019-12-11)

### Code Refactoring

* **namespaces:** revise namespaces to Aranasoft.Cobweb ([e3f4775](https://github.com/aranasoft/cobweb/commit/e3f4775))

### BREAKING CHANGES

* **namespaces:** the root namespace has been changed from `Cobweb.Data.NHibernate` and `Cobweb.Testing.NHibernate` to `Aranasoft.Cobweb.NHibernate` and `Aranasoft.Cobweb.NHibernate.Validation`, respectively. For example, the `EntityTypeSource` class is now located within `Aranasoft.Cobweb.NHibernate`. ([e3f4775](https://github.com/aranasoft/cobweb/commit/e3f4775))



<a name="1.33.4"></a>
## 1.33.4 (2018-06-15)


### Bug Fixes

* **fetch:** support ThenFetch / ThenFetchMany on Eager Load in NHib v3


### Features

* **config:** add EntityTypeSource for automapping



<a name="1.40.2"></a>
<a name="1.0.3"></a>
# 1.0.3 (2014-01-22)


### Features

* **transactions:** add Transaction Manager to execute a unit of work

