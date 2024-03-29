<a name="1.5.0"></a>
# 1.5.0 (2022-12-01)


### Bug Fixes

* **typo:** correct misspelled extentions to extensions ([b4f19c2](https://github.com/aranasoft/cobweb/commit/b4f19c284186a16fdd85a09d0e18444778f74169))


### Features

* **core:** remove net40 support ([6e210e7](https://github.com/aranasoft/cobweb/commit/6e210e708620db9fc47d4c332fa340a31fac3fbc))
* **core:** remove netcoreapp2.1 support ([077023b](https://github.com/aranasoft/cobweb/commit/077023b9d316e4c1a4e2724107a670ba6d9c6e49))
* **extensions:** add GetGenericParentType method to Type extensions ([37991f4](https://github.com/aranasoft/cobweb/commit/37991f4050e80fccf22537c73afbef0c9d34a2cd))
* **x64:** add x64 compile ([7bc1eec](https://github.com/aranasoft/cobweb/commit/7bc1eecde3bc216208c540be3acca1ea9b4b32fe))



<a name="1.4.2"></a>
# 1.4.2 (2019-12-14)


### Features

* **net40:** restore NET40 support to Cobweb (core) for MVC4 / WebApi1 projects ([810a2ca](https://github.com/aranasoft/cobweb/commit/810a2ca471c9a11f200b57d48cbe5b56c02b6d99))



<a name="1.4.1"></a>
# 1.4.1 (2019-12-10)

### Code Refactoring

* **namespaces:** move Core src from Cobweb to Aranasoft.Cobweb ([9464ccf](https://github.com/aranasoft/cobweb/commit/9464ccf))

### BREAKING CHANGES

* **namespaces:** the root namespace has been changed from `Cobweb` to `Aranasoft.Cobweb`. For example, the `IDependency` interface is now located at `Aranasoft.Cobweb.DependencyInjection.IDependency`. ([9464ccf](https://github.com/aranasoft/cobweb/commit/9464ccf))


<a name="1.4.0"></a>
# 1.4.0 (2018-05-22)


### Features

* **guid:** add support for generating a sequential Guid ([66c3e13](https://github.com/aranasoft/cobweb-core/commit/66c3e13))
* **net:** support for netstandard2.0 ([3e52c62](https://github.com/aranasoft/cobweb-core/commit/3e52c62))


<a name="1.3.2"></a>
# 1.3.2 (2016-11-13)


### Features

* **transactions:** remove new() constraint from TransactionManager ([c60080a](https://github.com/aranasoft/cobweb/commit/c60080a))


<a name="1.3.1"></a>
# 1.3.1 (2016-11-12)


### Bug Fixes

* **expression:** resolve issue with certain expression values not getting resolved ([203ae0c](https://github.com/aranasoft/cobweb/commit/203ae0c))


<a name="1.3.0"></a>
# 1.3.0 (2016-06-08)


### Features

* **ientity:** entity manager should check for all IEntity types ([f9c0af0](https://github.com/aranasoft/cobweb/commit/f9c0af0))
* **id:** allow Entity to use any Identity type, including strings ([f9bda6e](https://github.com/aranasoft/cobweb/commit/f9bda6e))


<a name="1.2.3"></a>
# 1.2.3 (2015-04-22)


### Features

* **extensions:** add inheritance check and Reflection extensions ([bb188e5](https://github.com/aranasoft/cobweb/commit/bb188e5))


<a name="1.2.2"></a>
# 1.2.2 (2015-04-13)


### Features

* **extensions:** add `ForEach` and `IfExists` extention methods ([112c186](https://github.com/aranasoft/cobweb/commit/112c186))

