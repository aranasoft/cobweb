using System;

namespace Cobweb.Data {
    public interface IDataTransaction : IDisposable {
        bool IsActive { get; }
        void Commit();
        void Rollback();
    }
}
