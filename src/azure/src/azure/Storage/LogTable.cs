namespace Cobweb.Azure.Storage {
    public abstract class LogTable : Table {
        protected abstract string Category { get; }
        protected abstract string SubCategory { get; }
    }
}
