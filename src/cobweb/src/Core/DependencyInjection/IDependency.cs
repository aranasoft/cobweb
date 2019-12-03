namespace Cobweb.DependencyInjection {
    /// <summary>
    ///     Dependency to be handled by dependency-injection framework. Traditionally per-request.
    /// </summary>
    public interface IDependency {}

    /// <summary>
    ///     Singleton lifecycle dependency to be handled by dependency-injection framework
    /// </summary>
    public interface ISingletonDependency : IDependency {}

    /// <summary>
    ///     Transient lifecycle dependency to be handled by dependency-injection framework
    /// </summary>
    public interface ITransientDependency : IDependency {}
}
