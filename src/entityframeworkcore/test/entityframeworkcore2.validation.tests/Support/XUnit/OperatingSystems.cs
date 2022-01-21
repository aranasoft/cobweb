using System;

namespace Aranasoft.Cobweb.EntityFrameworkCore.Validation.Tests.Support.XUnit {
    [Flags]
    public enum OperatingSystems {
        Windows = 1,
        // ReSharper disable once InconsistentNaming
        MacOS = 2,
        Linux = 4,
    }
}
