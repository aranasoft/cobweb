using System;

namespace Aranasoft.Cobweb.DependencyInjection;
/// <summary>
///     Wrapper interface for dependency-injected cache implementations
/// </summary>
public interface ICacheContainer : IDependency {
    /// <summary>
    ///     Gets or sets the cached object associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the cached object.</param>
    /// <returns>The cached object, or null if the key is not present.</returns>
    object this[Object key] { get; set; }

    /// <summary>
    ///     Removes the cached object associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the cached object to remove.</param>
    void Remove(Object key);
}
