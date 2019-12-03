using System;
using System.Linq;

namespace Cobweb
{
    /// <summary>
    /// Methods to build a sequential <see cref="Guid"/> compatible with SQL Server sorting algorithms.
    /// </summary>
    public static class SequentialGuid {
        private const long BaselineTicks = 599266080000000000L; // new DateTime(1900, 1, 1).Ticks;

        /// <summary>
        /// Generate a new <see cref="Guid"/> that can be sorted against others generated through <see cref="SequentialGuid"/>.
        /// </summary>
        /// <remarks>
        /// <remarks>The seven most-significant bytes will be based on the current timestamp, leaving nine bytes for randomization.
        /// Using seven bytes allows for 227 years of timestamp-based sorting (1900-2127).</remarks>
        /// <remarks>This sequential guid is compatible with SQL Server GUID sorting algorithms.</remarks>
        /// </remarks>
        /// <returns>The sequential <see cref="Guid"/>.</returns>
        public static Guid NewGuid() {
            return Guid.NewGuid().ToSequentialGuid();
        }

        /// <summary>
        /// Convert an existing <see cref="Guid"/> into a <see cref="SequentialGuid"/>.
        /// </summary>
        /// <param name="guid">An existing <see cref="Guid"/> to be converted to a <see cref="SequentialGuid"/>.</param>
        /// <remarks>
        /// <remarks>The seven most-significant bytes will be based on the current timestamp, leaving nine bytes for randomization.
        /// Using seven bytes allows for 227 years of timestamp-based sorting (1900-2127).</remarks>
        /// <remarks>This sequential guid is compatible with SQL Server GUID sorting algorithms.</remarks>
        /// </remarks>
        /// <returns>The sequential <see cref="Guid"/>.</returns>
        public static Guid ToSequentialGuid(this Guid guid) {
            byte[] guidArray = guid.ToByteArray();

            var counter = DateTime.UtcNow.Ticks - BaselineTicks;
            byte[] counterArray = BitConverter.GetBytes(counter);

            // We only need seven bytes. Seven bytes of ticks is 227 years.
            // This leaves nine bytes of randomization.
            counterArray = counterArray.Take(7).ToArray();

            // Reverse the array to match SQL Server sort algorithm 
            Array.Reverse(counterArray);

            // Copy the bytes into the byte array.
            // Priority of position in SQL Sorting:
            // 10, 11, 12, 13, 14, 15, 8, 9.
            Array.Copy(counterArray, 0, guidArray, 10, 6);
            Array.Copy(counterArray, 6, guidArray, 8, 1);

            return new Guid(guidArray);
        }
    }
}
