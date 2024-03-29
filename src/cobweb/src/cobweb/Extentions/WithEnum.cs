﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

// ReSharper disable once IdentifierTypo
namespace Aranasoft.Cobweb.Extentions {
    [Obsolete("Use Aranasoft.Cobweb.Extensions.WithEnum")]
    public static class WithEnum {
        /// <summary>
        ///     Returns the value of an enum element's DescriptionAttribute or the name of the element.
        /// </summary>
        /// <example>
        ///     <code> System.Reflection.BindingFlags.Public.GetDescription(); </code>
        /// </example>
        /// <remarks>Uses the value within a System.ComponentModel.DescriptionAttribute, if available.</remarks>
        /// <param name="enumeration">The enum element.</param>
        /// <returns>The identified element description.</returns>
        [Obsolete("Use Aranasoft.Cobweb.Extensions.WithEnum")]
        public static string GetDescription(this Enum enumeration) {
            return Extensions.WithEnum.GetDescription(enumeration);
        }
    }
}
