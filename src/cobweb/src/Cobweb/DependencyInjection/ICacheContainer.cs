using System;

namespace Cobweb.DependencyInjection {
    /// <summary>
    ///     Wrapper interface for dependency-injected cache implementations
    /// </summary>
    public interface ICacheContainer : IDependency {
        object this[Object key] { get; set; }
        void Remove(Object key);
    }
}
